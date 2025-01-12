﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MyCaffe.basecode;
using MyCaffe.common;
using MyCaffe.param;

namespace MyCaffe.layers
{
    /// <summary>
    /// The AccuracyLayer computes the classification accuracy for a one-of-many
    /// classification task.
    /// This layer is initialized with the MyCaffe.param.AccuracyParameter.
    /// </summary>
    /// <remarks>
    /// @see [Convolutional Architecture Exploration for Action Recognition and Image Classification](https://arxiv.org/abs/1512.07502v1) by J. T. Turner, David Aha, Leslie Smith, and Kalyan Moy Gupta, 2015.
    /// </remarks>
    /// <typeparam name="T">Specifies the base type <i>float</i> or <i>double</i>.  Using <i>float</i> is recommended to conserve GPU memory.</typeparam>
    public class AccuracyLayer<T> : Layer<T>
    {
        int m_nLabelAxis;
        int m_nOuterNum;
        int m_nInnerNum;
        int m_nTopK;
        int? m_nIgnoreLabel = null;
        Blob<T> m_blobNumsBuffer;
        Blob<T> m_blobAccData;
        bool m_bDirectLabels = false;
        bool m_bEnableSimpleAccuracy = false;
        bool m_bEnableLastElementOnly = false;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="cuda">Cuda engine.</param>
        /// <param name="log">General log.</param>
        /// <param name="p">provides AccuracyParameter accuracy_param,
        /// with AccuracyLayer options:
        ///  - top_k (optional, default 1)
        ///          Sets the maximumrank k at which prediction is considered
        ///          correct, For example, if k = 5, a prediction is counted
        ///          correct if the correct label is among the top 5 predicted labels.</param>
        public AccuracyLayer(CudaDnn<T> cuda, Log log, LayerParameter p)
            : base(cuda, log, p)
        {
            m_type = LayerParameter.LayerType.ACCURACY;
            m_blobNumsBuffer = new Blob<T>(cuda, log, false);
            m_blobAccData = new Blob<T>(cuda, log);
        }

        /** @copydoc Layer::dispose */
        protected override void dispose()
        {
            m_blobNumsBuffer.Dispose();
            base.dispose();
        }

        /// <summary>
        /// Returns the number of bottom blobs used: predicted, label
        /// </summary>
        public override int ExactNumBottomBlobs
        {
            get { return 2; }
        }

        /// <summary>
        /// Returns the minimum number of top blobs: accuracy
        /// </summary>
        public override int MinTopBlobs
        {
            get { return 1; }
        }

        /// <summary>
        /// Returns the maximum number of top blobs: accuracy, labels
        /// </summary>
        public override int MaxTopBlobs
        {
            get { return 2; }
        }

        /// <summary>
        /// Setup the layer.
        /// </summary>
        /// <param name="colBottom">Specifies the collection of bottom (input) Blobs.</param>
        /// <param name="colTop">Specifies the collection of top (output) Blobs.</param>
        public override void LayerSetUp(BlobCollection<T> colBottom, BlobCollection<T> colTop)
        {
            m_bEnableSimpleAccuracy = m_param.accuracy_param.enable_simple_accuracy;
            m_bEnableLastElementOnly = m_param.accuracy_param.enable_last_element_only;

            if (!m_bEnableSimpleAccuracy && m_bEnableLastElementOnly)
                m_log.WriteLine("WARNING: The accuracy layer currently only supports last element accuracy when using the simple accuracy.");

            m_nTopK = (int)m_param.accuracy_param.top_k;
            m_nIgnoreLabel = null;
            if (m_param.accuracy_param.ignore_labels.Count > 0)
            {
                if (m_bEnableSimpleAccuracy && m_param.accuracy_param.ignore_labels.Count > 1)
                    m_log.WriteLine("WARNING: The accuracy layer currently only supports a single ignore label.");
                m_nIgnoreLabel = m_param.accuracy_param.ignore_labels[0];
            }

            if (m_bEnableSimpleAccuracy && m_nTopK > 1)
            {
                m_log.WriteLine("WARNING: The accuracy layer currently only supports top_k = 1 for simple accuracy.");
                m_nTopK = 1;
            }

            m_bDirectLabels = false;
        }

        /// <summary>
        /// Reshape the bottom (input) and top (output) blobs.
        /// </summary>
        /// <param name="colBottom">Specifies the collection of bottom (input) Blobs.</param>
        /// <param name="colTop">Specifies the collection of top (output) Blobs.</param>
        public override void Reshape(BlobCollection<T> colBottom, BlobCollection<T> colTop)
        {
            m_log.CHECK_LE(m_nTopK, colBottom[0].count() / colBottom[1].count(), "top_k must be less than or equal to the number of classes.");

            m_nLabelAxis = colBottom[0].CanonicalAxisIndex(m_param.accuracy_param.axis);
            m_nOuterNum = colBottom[0].count(0, m_nLabelAxis);
            m_nInnerNum = colBottom[0].count(m_nLabelAxis + 1);
            int nLabelDim = m_nOuterNum * m_nInnerNum;

            if (m_param.accuracy_param.axis == 0 && nLabelDim == 1)
            {
                if (!m_bDirectLabels)
                    m_log.WriteLine("WARNING: Using direct label comparisons where a label is expected in each item (e.g. no Softmax used).");
                m_bDirectLabels = true;
            }
            else
            {
                m_log.CHECK_EQ(m_nOuterNum * m_nInnerNum, colBottom[1].count(), "Number of labels must match number of predictions; e.g., if label axis = 1 and prediction shape is (N, C, H, W), label count (number of labels) must be N*H*W, with integer values in {0, 1, ..., C=1}.");
            }

            List<int> rgTopShape = new List<int>(); // Accuracy is a scalar; 0 axes.
            colTop[0].Reshape(rgTopShape);
            colTop[0].type = BLOB_TYPE.ACCURACY;

            if (colTop.Count > 1)
            {
                // Per-class accuracy is a vector; 1 axes.
                List<int> rgTopShapePerClass = new List<int>() { colBottom[0].shape(m_nLabelAxis) };
                colTop[1].Reshape(rgTopShapePerClass);
                m_blobNumsBuffer.Reshape(rgTopShapePerClass);
            }

            if (m_bEnableSimpleAccuracy)
                m_blobAccData.Reshape(m_nOuterNum, 1, 1, 1);
        }

        /// <summary>
        /// Forward compuation.
        /// </summary>
        /// <param name="colBottom">bottom input blob (length 2)
        ///  -# @f$ (N \times C \times H \times W) @f$
        ///     the predictions @f$ x @f$, a blob with values in
        ///     @f$ [-\infty, +\infty] @f$ indicating the predicted score of each of
        ///     the @f$ K = CHW @f$ classes.  Each @f$ x_n @f$ is mapped to a predicted 
        ///     label @f$ \hat{l}_n @f$ given by its maximal index:
        ///     @f$ \hat{l}_n = \arg\max\limits_k x_{nk} @f$
        ///  -# @f$ (N \times 1 \times 1 \times 1) @f$
        ///     the labels l, an integer-valued blob with values
        ///     @f$ l_n \in [0, 1, 2, ..., K-1] @f$
        ///     indicating the correct class label among the @f$ K @f$ classes.</param>
        /// <param name="colTop">top output blob vector (length 1)
        ///  -# @f$ (1 \times 1 \times 1 \times 1) @f$
        ///     the computed accuracy: @f$
        ///       \frac{1}{N} \sum\limits_{n=1}^N \delta\{ \hat{l}_n = l_n \}
        ///     @f$
        ///     where @f$ 
        ///       \delta\{\mathrm{condition}\} = \left\{
        ///         \begin{array}{lr}
        ///           1 \: \mbox{if condition} \\
        ///           0 \: \mbox{otherwise}
        ///         \end{array} \right.
        ///     @f$
        /// </param>
        protected override void forward(BlobCollection<T> colBottom, BlobCollection<T> colTop)
        {
            // Currently using cpu version for gpu version fails in the auto tests.
            if (m_bEnableSimpleAccuracy)
                forward_gpu(colBottom, colTop);
            else if (m_bDirectLabels)
                forward_cpu_direct(colBottom, colTop);
            else
                forward_cpu(colBottom, colTop);
        }

        /// <summary>
        /// The simple accuracy calculates the total accuracy across all predictions using an argmax comparison
        /// with the target label.
        /// </summary>
        /// <param name="colBottom">Specifies the bottom input data (length 2).
        ///  -# @f$ (N \times C \times H \times W) @f$
        ///     the predictions @f$ x @f$, a blob with values in
        ///     @f$ [-\infty, +\infty] @f$ indicating the predicted score of each of
        ///     the @f$ K = CHW @f$ classes.  Each @f$ x_n @f$ is mapped to a predicted 
        ///     label @f$ \hat{l}_n @f$ given by its maximal index:
        ///     @f$ \hat{l}_n = \arg\max\limits_k x_{nk} @f$
        ///  -# @f$ (N \times 1 \times 1 \times 1) @f$
        ///     the labels l, an integer-valued blob with values
        ///     @f$ l_n \in [0, 1, 2, ..., K-1] @f$
        ///     indicating the correct class label among the @f$ K @f$ classes.
        /// </param>
        /// <param name="colTop">Specifies the top output data where the accuracy is placed (length 1).
        ///  -# @f$ (1 \times 1 \times 1 \times 1) @f$
        ///     the computed accuracy: @f$
        ///       \frac{1}{N} \sum\limits_{n=1}^N \delta\{ \hat{l}_n = l_n \}
        ///     @f$
        ///     where @f$ 
        ///       \delta\{\mathrm{condition}\} = \left\{
        ///         \begin{array}{lr}
        ///           1 \: \mbox{if condition} \\
        ///           0 \: \mbox{otherwise}
        ///         \end{array} \right.
        ///     @f$
        /// </param>
        protected void forward_gpu(BlobCollection<T> colBottom, BlobCollection<T> colTop)
        {
            int nDim = colBottom[0].count() / m_nOuterNum;
            int? nIgnoreLabel = null;
            if (m_param.accuracy_param.ignore_labels != null)
            {
                if (m_param.accuracy_param.ignore_labels.Count > 0)
                    nIgnoreLabel = m_param.accuracy_param.ignore_labels[0];
                if (m_param.accuracy_param.ignore_labels.Count > 1)
                    m_log.WriteLine("WARNING: Only the first ignore label recognized when using the simple accuracy layer.");
            }
            
            m_cuda.accuracy_fwd(colBottom[0].count(), m_nOuterNum, nDim, colBottom[0].gpu_data, colBottom[1].gpu_data, m_blobAccData.mutable_gpu_data, m_blobAccData.mutable_gpu_diff, nIgnoreLabel, m_bEnableLastElementOnly, colBottom[0].num);

            float fAccCount = m_cuda.asum_float(m_blobAccData.count(), m_blobAccData.gpu_data);
            float fTotalCount = m_cuda.asum_float(m_blobAccData.count(), m_blobAccData.gpu_diff);
            float fAccuracy = (fTotalCount == 0) ? 0 : fAccCount / fTotalCount;

            colTop[0].SetData(fAccuracy, 0);
        }

        /// <summary>
        /// Forward compuation.
        /// </summary>
        /// <param name="colBottom">bottom input blob (length 2)
        ///  -# @f$ (N \times C \times H \times W) @f$
        ///     the predictions @f$ x @f$, a blob with values in
        ///     @f$ [-\infty, +\infty] @f$ indicating the predicted score of each of
        ///     the @f$ K = CHW @f$ classes.  Each @f$ x_n @f$ is mapped to a predicted 
        ///     label @f$ \hat{l}_n @f$ given by its maximal index:
        ///     @f$ \hat{l}_n = \arg\max\limits_k x_{nk} @f$
        ///  -# @f$ (N \times 1 \times 1 \times 1) @f$
        ///     the labels l, an integer-valued blob with values
        ///     @f$ l_n \in [0, 1, 2, ..., K-1] @f$
        ///     indicating the correct class label among the @f$ K @f$ classes.</param>
        /// <param name="colTop">top output blob vector (length 1)
        ///  -# @f$ (1 \times 1 \times 1 \times 1) @f$
        ///     the computed accuracy: @f$
        ///       \frac{1}{N} \sum\limits_{n=1}^N \delta\{ \hat{l}_n = l_n \}
        ///     @f$
        ///     where @f$ 
        ///       \delta\{\mathrm{condition}\} = \left\{
        ///         \begin{array}{lr}
        ///           1 \: \mbox{if condition} \\
        ///           0 \: \mbox{otherwise}
        ///         \end{array} \right.
        ///     @f$
        /// </param>
        protected void forward_cpu(BlobCollection<T> colBottom, BlobCollection<T> colTop)
        {
            if (typeof(T) == typeof(double))
                forward_cpuD(colBottom, colTop);
            else
                forward_cpuF(colBottom, colTop);
        }

        private void forward_cpuD(BlobCollection<T> colBottom, BlobCollection<T> colTop)
        {
            double dfAccuracy = 0;
            double[] rgBottomData = convertD(colBottom[0].update_cpu_data());
            double[] rgBottomLabel = convertD(colBottom[1].update_cpu_data());
            int nDim = colBottom[0].count() / m_nOuterNum;
            int nNumLabels = colBottom[0].shape(m_nLabelAxis);
            double[] rgNumsBuffer = null;
            double[] rgTopLabel = null;

            if (colTop.Count > 1)
            {
                m_blobNumsBuffer.SetData(0.0);
                colTop[1].SetData(0.0);
                rgTopLabel = convertD(colTop[1].mutable_cpu_data);
                rgNumsBuffer = convertD(m_blobNumsBuffer.update_cpu_data());
            }

            int nCount = 0;
            bool bNanDetected = false;

            for (int i = 0; i < m_nOuterNum; i++)
            {
                for (int j = 0; j < m_nInnerNum; j++)
                {
                    int nLabelValue = (int)rgBottomLabel[i * m_nInnerNum + j];

                    if (m_nIgnoreLabel.HasValue && m_nIgnoreLabel.Value == nLabelValue)
                        continue;

                    m_log.CHECK_GE(nLabelValue, 0, "The lable value must be >= 0.");
                    m_log.CHECK_LT(nLabelValue, nNumLabels, "The label value must be < " + nNumLabels.ToString() + ".  Make sure that the prototxt 'num_outputs' setting is > the highest label number.");

                    if (colTop.Count > 1)
                        rgNumsBuffer[nLabelValue]++;

                    double prob_of_true_class = rgBottomData[i * nDim
                                                             + nLabelValue * m_nInnerNum
                                                             + j];
                    int num_better_predictions = -1; // true_class also counts as 'better'
                    // Top-k accuracy
                    for (int k = 0; k < nNumLabels && num_better_predictions < m_nTopK; k++)
                    {
                        double dfVal = rgBottomData[i * nDim + k * m_nInnerNum + j];

                        if (double.IsNaN(dfVal) || double.IsInfinity(dfVal))
                            bNanDetected = true;
                        else if (dfVal >= prob_of_true_class)
                            num_better_predictions += 1;
                    }

                    // Check if true label is in top_k predictions
                    if (num_better_predictions != -1 && num_better_predictions < m_nTopK)
                    {
                        dfAccuracy += 1.0;

                        if (colTop.Count > 1)
                            rgTopLabel[nLabelValue] += 1.0;
                    }

                    nCount++;
                }
            }

            if (bNanDetected)
                m_log.WriteLine("WARNING: NAN/INF detected in output!");

            // m_log.WriteLine("Accuracy: " + dfAccuracy.ToString());
            dfAccuracy = (nCount == 0) ? 0 : (dfAccuracy / nCount);
            colTop[0].SetData(dfAccuracy, 0);
            colTop[0].Tag = m_param.accuracy_param.top_k;

            if (colTop.Count > 1)
            {
                for (int i = 0; i < colTop[1].count(); i++)
                {
                    double dfVal = 0.0;

                    if (rgNumsBuffer[i] != 0)
                        dfVal = rgTopLabel[i] / rgNumsBuffer[i];

                    rgTopLabel[i] = dfVal;
                }

                colTop[1].mutable_cpu_data = convert(rgTopLabel);
            }

            // Accuracy layer should not be used as a loss function.
        }

        private void forward_cpuF(BlobCollection<T> colBottom, BlobCollection<T> colTop)
        {
            float dfAccuracy = 0;
            float[] rgBottomData = convertF(colBottom[0].update_cpu_data());
            float[] rgBottomLabel = convertF(colBottom[1].update_cpu_data());
            int nDim = colBottom[0].count() / m_nOuterNum;
            int nNumLabels = colBottom[0].shape(m_nLabelAxis);
            float[] rgNumsBuffer = null;
            float[] rgTopLabel = null;

            if (colTop.Count > 1)
            {
                m_blobNumsBuffer.SetData(0.0);
                colTop[1].SetData(0.0);
                rgTopLabel = convertF(colTop[1].mutable_cpu_data);
                rgNumsBuffer = convertF(m_blobNumsBuffer.update_cpu_data());
            }

            int nCount = 0;
            bool bNanDetected = false;

            for (int i = 0; i < m_nOuterNum; i++)
            {
                for (int j = 0; j < m_nInnerNum; j++)
                {
                    int nLabelValue = (int)rgBottomLabel[i * m_nInnerNum + j];

                    if (m_nIgnoreLabel.HasValue && m_nIgnoreLabel.Value == nLabelValue)
                        continue;

                    m_log.CHECK_GE(nLabelValue, 0, "The lable value must be >= 0.");
                    m_log.CHECK_LT(nLabelValue, nNumLabels, "The label value must be < " + nNumLabels.ToString() + ".  Make sure that the prototxt 'num_outputs' setting is > the highest label number.");

                    if (colTop.Count > 1)
                        rgNumsBuffer[nLabelValue]++;

                    double prob_of_true_class = rgBottomData[i * nDim
                                                             + nLabelValue * m_nInnerNum
                                                             + j];
                    int num_better_predictions = -1; // true_class also counts as 'better'
                    // Top-k accuracy
                    for (int k = 0; k < nNumLabels && num_better_predictions < m_nTopK; k++)
                    {
                        double dfVal = rgBottomData[i * nDim + k * m_nInnerNum + j];

                        if (double.IsNaN(dfVal) || double.IsInfinity(dfVal))
                            bNanDetected = true;
                        else if (dfVal >= prob_of_true_class)
                            num_better_predictions += 1;
                    }

                    // Check if true label is in top_k predictions
                    if (num_better_predictions != -1 && num_better_predictions < m_nTopK)
                    {
                        dfAccuracy += 1.0f;

                        if (colTop.Count > 1)
                            rgTopLabel[nLabelValue] += 1.0f;
                    }

                    nCount++;
                }
            }

            if (bNanDetected)
                m_log.WriteLine("WARNING: NAN/INF detected in output!");

            // m_log.WriteLine("Accuracy: " + dfAccuracy.ToString());
            dfAccuracy = (nCount == 0) ? 0 : (dfAccuracy / nCount);
            colTop[0].SetData(dfAccuracy, 0);
            colTop[0].Tag = m_param.accuracy_param.top_k;

            if (colTop.Count > 1)
            {
                for (int i = 0; i < colTop[1].count(); i++)
                {
                    float dfVal = 0.0f;

                    if (rgNumsBuffer[i] != 0)
                        dfVal = rgTopLabel[i] / rgNumsBuffer[i];

                    rgTopLabel[i] = dfVal;
                }

                colTop[1].mutable_cpu_data = convert(rgTopLabel);
            }

            // Accuracy layer should not be used as a loss function.
        }

        /// <summary>
        /// Forward compuation.
        /// </summary>
        /// <param name="colBottom">bottom input blob (length 2)
        ///  -# @f$ (N \times 1 \times 1 \times 1) @f$
        ///     the predictions @f$ x @f$, a blob with values in
        ///     @f$ [0, max_label] @f$ indicating the predicted label value.
        ///  -# @f$ (N \times 1 \times 1 \times 1) @f$
        ///     the labels l, an integer-valued blob with values
        ///     @f$ l_n \in [0, 1, 2, ..., K-1] @f$
        ///     indicating the correct class label among the @f$ K @f$ classes.</param>
        /// <param name="colTop">top output blob vector (length 1)
        ///  -# @f$ (1 \times 1 \times 1 \times 1) @f$
        ///     the computed accuracy: @f$
        ///       \frac{1}{N} \sum\limits_{n=1}^N \delta\{ \hat{l}_n = l_n \}
        ///     @f$
        ///     where @f$ 
        ///       \delta\{\mathrm{condition}\} = \left\{
        ///         \begin{array}{lr}
        ///           1 \: \mbox{if condition} \\
        ///           0 \: \mbox{otherwise}
        ///         \end{array} \right.
        ///     @f$
        /// </param>
        protected void forward_cpu_direct(BlobCollection<T> colBottom, BlobCollection<T> colTop)
        {
            double dfAccuracy = 0;
            double[] rgBottomData = convertD(colBottom[0].update_cpu_data());
            double[] rgBottomLabel = convertD(colBottom[1].update_cpu_data());
            int nNumLabels = colBottom[0].num;
            int nNumMatches = 0;
            bool bNanDetected = false;

            for (int i = 0; i < nNumLabels; i++)
            {
                double dfDiff = Math.Abs(rgBottomData[i] - rgBottomLabel[i]);
                if (dfDiff < 0.00001)
                    nNumMatches++;
            }

            if (bNanDetected)
                m_log.WriteLine("WARNING: NAN/INF detected in output!");

            dfAccuracy = (double)nNumMatches / (double)nNumLabels;
            colTop[0].SetData(dfAccuracy, 0);
            colTop[0].Tag = m_param.accuracy_param.top_k;

            if (colTop.Count > 1)
                colTop[1].SetData(0);

            // Accuracy layer should not be used as a loss function.
        }

        /// @brief Not implemented -- AccuracyLayer cannot be used as a loss.
        protected override void backward(BlobCollection<T> colTop, List<bool> rgbPropagateDown, BlobCollection<T> colBottom)
        {
            if (rgbPropagateDown[0])
                throw new NotImplementedException();
        }
    }
}
