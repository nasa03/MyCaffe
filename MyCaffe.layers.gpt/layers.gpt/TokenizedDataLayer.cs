﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MyCaffe.basecode;
using MyCaffe.common;
using MyCaffe.param;
using MyCaffe.fillers;
using System.IO;
using MyCaffe.db.image;
using MyCaffe.param.gpt;

namespace MyCaffe.layers.gpt
{
    /// <summary>
    /// The TokenizedDataLayer loads and tokenizes data for a transformer model where data is loaded in the form: data, pos, target(optional)
    /// </summary>
    /// <typeparam name="T">Specifies the base type <i>float</i> or <i>double</i>.  Using <i>float</i> is recommended to conserve GPU memory.</typeparam>
    public class TokenizedDataLayer<T> : Layer<T>
    {
        CancelEvent m_evtCancel;
        InputData m_data;

        /// <summary>
        /// The TokenizedDataLayer constructor.
        /// </summary>
        /// <param name="cuda">Specifies the CudaDnn connection to Cuda.</param>
        /// <param name="log">Specifies the Log for output.</param>
        /// <param name="p">
        /// Provides TokenizedDataParameter model_data_param with options:
        ///  - source.  The data source(s) where the source is the data input table who's RawImageResults table contains the data for training.
        ///  
        ///  - batch_size.  The batch size (currently only 1 supported).
        ///  
        ///  - time_steps.  The maximum number of time steps.
        ///  
        ///  - input_dim.  The input dimension of the encoder input.
        ///  
        ///  - sample_size.  The number of samples to load for training.
        ///  
        ///  - shuffle.  Whether or not to shuffle the data.
        /// </param>
        /// <param name="db">Specifies the external database to use.</param>
        /// <param name="evtCancel">Specifies the CancelEvent used to cancel any pre-fetching operations.</param>
        public TokenizedDataLayer(CudaDnn<T> cuda, Log log, LayerParameter p, IXImageDatabaseBase db, CancelEvent evtCancel)
            : base(cuda, log, p)
        {
            m_evtCancel = evtCancel;
            m_type = LayerParameter.LayerType.TOKENIZED_DATA;
        }

        /// <summary>
        /// Release all internal blobs.
        /// </summary>
        protected override void dispose()
        {
            base.dispose();
        }

        /// <summary>
        /// No bottom blobs are used by this layer when training.
        /// </summary>
        public override int MinBottomBlobs
        {
            get { return 0; }
        }

        /// <summary>
        /// The data input is placed in the bottom blob when running.
        /// </summary>
        public override int MaxBottomBlobs
        {
            get { return 1; }
        }

        /// <summary>
        /// Returns the minimum number of required top (output) Blobs: data, pos, target (only valid on TRAIN or TEST)
        /// </summary>
        public override int MinTopBlobs
        {
            get { return (m_phase == Phase.RUN) ? 2 : 3; }
        }

        /// <summary>
        /// Returns the maximum number of required top (output) Blobs: data, pos, target (only valid on TRAIN or TEST)
        /// </summary>
        public override int MaxTopBlobs
        {
            get { return (m_phase == Phase.RUN) ? 2 : 3; }
        }

        /// <summary>
        /// Setup the layer.
        /// </summary>
        /// <param name="colBottom">Not used.</param>
        /// <param name="colTop">Specifies the collection of top (output) Blobs.</param>
        public override void LayerSetUp(BlobCollection<T> colBottom, BlobCollection<T> colTop)
        {
            switch (m_param.tokenized_data_param.input_type)
            {
                case TokenizedDataParameter.INPUT_TYPE.TEXT_FILE:
                    m_data = new TextInputData(m_param.tokenized_data_param.source, m_param.tokenized_data_param.seed);
                    break;

                default:
                    throw new Exception("Unknown input type '" + m_param.tokenized_data_param.input_type.ToString() + "'");
            }
        }

        /// <summary>
        /// Data layers have no bottoms, so reshaping is trivial.
        /// </summary>
        /// <param name="colBottom">Not used.</param>
        /// <param name="colTop">Specifies the collection of top (output) Blobs.</param>
        public override void Reshape(BlobCollection<T> colBottom, BlobCollection<T> colTop)
        {
            int nBatchSize = (int)m_param.tokenized_data_param.batch_size;
            int nBlockSize = (int)m_param.tokenized_data_param.block_size;
            int nTokenSize = (int)m_data.TokenSize;

            Blob<T> blobData = colTop[0];
            Blob<T> blobPos = colTop[1];
            Blob<T> blobTarget = null;
            
            if (colTop.Count > 2)   
                blobTarget = colTop[2];

            if (colBottom.Count == 0)
            {
                blobData.SetParameter("vocab_size", m_data.VocabularySize);
                // reshape for single characters (each character is an index into the vocab vector)
                blobData.Reshape(nBatchSize, nBlockSize, nTokenSize, 1);
                blobPos.Reshape(1, nBlockSize, 1, 1);

                if (blobTarget != null)
                    blobTarget.Reshape(nBatchSize, nBlockSize, nTokenSize, 1);
            }
            else
            {
                nBlockSize = colBottom[0].channels;

                double? dfVocabSize = colBottom[0].GetParameter("vocab_size");
                if (!dfVocabSize.HasValue)
                    throw new Exception("The bottom blob[0] must have its 'vocab_size' parameter set!");

                m_log.CHECK_EQ(colBottom[0].height, nTokenSize, "The colBottom[0].height should equal the token size of " + nTokenSize.ToString() + "!");

                blobData.SetParameter("vocab_size", dfVocabSize.Value);
                blobData.ReshapeLike(colBottom[0]);
                blobPos.Reshape(1, colBottom[0].channels, 1, 1);
            }

            // Set the position data = 0, 1, 2, 3, ... block_size-1
            float[] rgPos = new float[blobPos.channels];
            for (int i = 0; i < nBlockSize; i++)
            {
                rgPos[i] = i;
            }

            blobPos.mutable_cpu_data = convert(rgPos);
        }

        /// <summary>
        /// Run the Forward computation, which fills the data into the top (output) Blobs.
        /// </summary>
        /// <param name="colBottom">Not used.</param>
        /// <param name="colTop">top output blob vector (length 2-3)
        ///  -# @f$ (N \times C \times H \times W) @f$
        ///     the data outputs.  
        ///  -# @f$ (N \times C \times 1 \times 1) @f$
        ///     the position outputs.
        ///  -# @f$ (N \times C \times H \times W) @f$ (only on training and testing)
        ///     the target outputs
        /// </param>
        protected override void forward(BlobCollection<T> colBottom, BlobCollection<T> colTop)
        {
            if (colBottom.Count == 0)
            {
                Tuple<float[], float[]> data = m_data.GetData((int)m_param.tokenized_data_param.batch_size, (int)m_param.tokenized_data_param.block_size);

                colTop[0].mutable_cpu_data = convert(data.Item1);
                if (colTop.Count > 2)
                    colTop[2].mutable_cpu_data = convert(data.Item2);
            }
            else
            {
                float[] rgData = m_data.Tokenize(convertF(colBottom[0].mutable_cpu_data));
                colTop[0].mutable_cpu_data = convert(rgData);
            }
        }

        /// @brief Not implemented - data Layers do not perform backward..
        protected override void backward(BlobCollection<T> colTop, List<bool> rgbPropagateDown, BlobCollection<T> colBottom)
        {
        }

        /// <summary>
        /// Detokenize the source data by converting it to its native form.
        /// </summary>
        /// <param name="blobSrc">Specifies the tokenized source data.</param>
        /// <param name="blobDst">Specifies the detokenized destination data.</param>
        public void Detokenize(Blob<T> blobSrc, Blob<T> blobDst)
        {
            float[] rgSrc = convertF(blobSrc.mutable_cpu_data);
            blobDst.mutable_cpu_data = convert(m_data.Detokenize(rgSrc));
        }
    }

    /// <summary>
    /// The InputData is an abstract class used to get training data and tokenize input data.
    /// </summary>
    internal abstract class InputData
    {
        protected Random m_random;

        /// <summary>
        /// The constructor.
        /// </summary>
        /// <param name="nRandomSeed">Optionally, specifies the seed to use for testing.</param>
        public InputData(int? nRandomSeed = null)
        {
            if (nRandomSeed.HasValue)
                m_random = new Random(nRandomSeed.Value);
            else
                m_random = new Random();
        }

        /// <summary>
        /// Returns the size of a single token (e.g. 1 for character data)
        /// </summary>
        public abstract uint TokenSize { get; }
        /// <summary>
        /// Returns the size of the vocabulary.
        /// </summary>
        public abstract uint VocabularySize { get; }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="nBatchSize">Specifies the number of blocks in the batch.</param>
        /// <param name="nBlockSize">Specifies the size of each block.</param>
        /// <returns>A tuple containing the data and target is returned.</returns>
        public abstract Tuple<float[], float[]> GetData(int nBatchSize, int nBlockSize);
        /// <summary>
        /// Tokenize the input data.
        /// </summary>
        /// <param name="rgInput">Specifies the untokenized input data.</param>
        /// <returns>The tokenized data is returned.</returns>
        public abstract float[] Tokenize(float[] rgInput);
        /// <summary>
        /// Detokenize the input data, but converting tokenized data to its native form.
        /// </summary>
        /// <param name="rgInput">Specifies the tokenized data.</param>
        /// <returns>The de-tokenized data is returned.</returns>
        public abstract float[] Detokenize(float[] rgInput);
    }

    /// <summary>
    /// The TextInputData manages character data read in from a text file.  Data is tokenized into indexes that reference each character
    /// within the vocabulary.
    /// </summary>
    /// <remarks>
    /// For example if the data source contains the text "a red fox ran.",
    /// the vocabulary would be:
    /// 
    /// Vocabulary: ' ', '.', 'a', 'd', 'e', 'f', 'o', 'n', 'r'
    /// Index Vals:  0,   1,   2,   3,   4,   5,   6,   7,   8
    /// 
    /// Tokenizing is the process of converting each input character to its respective 'token' or in this case, index value.
    /// So, for example, 'a' is tokenized as index 2; 'd' is tokenized as index 3, etc.
    /// </remarks>
    internal class TextInputData : InputData
    {
        string m_strData;
        Dictionary<char, int> m_rgVocabKeyToIdx = new Dictionary<char, int>();
        Dictionary<int, char> m_rgVocabIdxToKey = new Dictionary<int, char>();

        /// <summary>
        /// The constructor.
        /// </summary>
        /// <param name="strSrc">Specifies the data source as the filename of the text data file.</param>
        /// <param name="nRandomSeed">Optionally, specifies a random seed for testing.</param>
        public TextInputData(string strSrc, int? nRandomSeed = null) : base(nRandomSeed)
        {
            m_strData = File.ReadAllText(strSrc);

            m_rgVocabKeyToIdx.Clear();
            foreach (char ch in m_strData)
            {
                if (!m_rgVocabKeyToIdx.ContainsKey(ch))
                    m_rgVocabKeyToIdx.Add(ch, 1);
            }

            List<char> rgKeys = m_rgVocabKeyToIdx.Keys.ToList();
            rgKeys.Sort();

            m_rgVocabKeyToIdx.Clear();

            for (int i = 0; i < rgKeys.Count; i++)
            {
                m_rgVocabKeyToIdx.Add(rgKeys[i], i);
                m_rgVocabIdxToKey.Add(i, rgKeys[i]);
            }
        }

        /// <summary>
        /// The text data token size is a single character.
        /// </summary>
        public override uint TokenSize
        {
            get { return 1; }
        }

        /// <summary>
        /// Returns the number of unique characters in the data.
        /// </summary>
        public override uint VocabularySize
        {
            get { return (uint)m_rgVocabKeyToIdx.Count; }
        }

        /// <summary>
        /// Retrieve random blocks from the source data where the data and target are the same
        /// but offset by one element where the target is offset +1 from the data.
        /// </summary>
        /// <param name="nBatchSize">Specifies the batch size.</param>
        /// <param name="nBlockSize">Specifies teh block size.</param>
        /// <returns>A tuple containing the data and target is returned.</returns>
        public override Tuple<float[], float[]> GetData(int nBatchSize, int nBlockSize)
        {
            float[] rgData = new float[nBlockSize * nBatchSize];
            float[] rgTgt = new float[nBlockSize * nBatchSize];

            for (int i = 0; i < nBatchSize; i++)
            {
                int nDataIdx = m_random.Next(m_strData.Count() - (nBlockSize + 1));
                int nDstIdx = i * nBlockSize;

                for (int j = 0; j < nBlockSize; j++)
                {                    
                    char ch = m_strData[nDataIdx + j];
                    int nCharIdx = m_rgVocabKeyToIdx[ch];                                  
                    rgData[nDstIdx + j] = nCharIdx;

                    ch = m_strData[nDataIdx + j + 1];
                    nCharIdx = m_rgVocabKeyToIdx[ch];
                    rgTgt[nDstIdx + j] = nCharIdx;
                }
            }

            return new Tuple<float[], float[]>(rgData, rgTgt);
        }

        /// <summary>
        /// Convert text input (input as a set of ASCII character values) into their respective
        /// char indexes in the vocabulary.
        /// </summary>
        /// <param name="rgInput">Specifies input data where each element is an ASCII character numeric value.</param>
        /// <returns>The tokenized input is returned.</returns>
        public override float[] Tokenize(float[] rgInput)
        {
            for (int i = 0; i < rgInput.Count(); i++)
            {
                char ch = (char)rgInput[i];
                int nCharIdx = m_rgVocabKeyToIdx[ch];
                rgInput[i] = nCharIdx;
            }

            return rgInput;
        }
        
        /// <summary>
        /// Convert tokenized data back to its native character form.
        /// </summary>
        /// <param name="rgInput">Specifies the tokenized data.</param>
        /// <returns>The characters in numeric form are returned.</returns>
        public override float[] Detokenize(float[] rgInput)
        {
            for (int i = 0; i < rgInput.Count(); i++)
            {
                int nCharIdx = (int)rgInput[i];
                char ch = m_rgVocabIdxToKey[nCharIdx];
                rgInput[i] = ch;
            }

            return rgInput;
        }
    }
}