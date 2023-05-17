﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Policy;
using System.Text;
using System.Threading;
using System.Xml.Linq;
using MyCaffe.basecode;
using MyCaffe.common;
using MyCaffe.param;

namespace MyCaffe.layers.tft
{
    /// <summary>
    /// The DataTemporalLayer implements the data layer used to load the temporal data into the model.
    /// </summary>
    /// <remarks>
    /// @see [Temporal Fusion Transformers for Interpretable Multi-horizon Time Series Forecasting](https://arxiv.org/abs/1912.09363) by Bryan Lim, Sercan O. Arik, Nicolas Loeff, and Tomas Pfister, 2019, arXiv 1912.09363
    /// @see [Github - PlaytikaOSS/tft-torch](https://github.com/PlaytikaOSS/tft-torch) by Playtika Research, 2021.
    /// @see [Github - PlaytikaOSS/tft-torch tft.py](https://github.com/PlaytikaOSS/tft-torch/blob/main/tft_torch/tft.py#L405) by Playtika Research, 2021.
    /// </remarks>
    /// <typeparam name="T">Specifies the base type <i>float</i> or <i>double</i>.  Using <i>float</i> is recommended to conserve GPU memory.</typeparam>
    public class DataTemporalLayer<T> : Layer<T>
    {
        List<int> m_rgShape = new List<int>(4);
        uint m_nBatchSize;
        uint m_nNumHistoricalSteps;
        uint m_nNumFutureSteps;
        uint m_nNumStaticFeatsNumeric;
        uint m_nNumStaticFeatsCategorical;
        uint m_nNumHistoricalNumeric;
        uint m_nNumHistoricalCategorical;
        uint m_nNumFutureNumeric;
        uint m_nNumFutureCategorical;
        RawFileData<T> m_data = null;
        CancelEvent m_evtCancel;

        /// <summary>
        /// The constructor.
        /// </summary>
        /// <param name="cuda">Specifies the CudaDnn connection to Cuda.</param>
        /// <param name="log">Specifies the Log for output.</param>
        /// <param name="p">Specifies the LayerParameter of type DATA_TEMPORAL with parameter data_temporal_param</param>
        /// <param name="evtCancel">Specifies the cancel event used to cancel background data loading.</param>
        public DataTemporalLayer(CudaDnn<T> cuda, Log log, LayerParameter p, CancelEvent evtCancel)
            : base(cuda, log, p)
        {
            m_evtCancel = evtCancel;
            m_type = LayerParameter.LayerType.DATA_TEMPORAL;
        }

        /** @copydoc Layer::dispose */
        protected override void dispose()
        {
        }

        /** @copydoc Layer::setup_internal_blobs */
        protected override void setup_internal_blobs(BlobCollection<T> col)
        {
            if (col.Count > 0)
                return;
        }

        /// <summary>
        /// The data layer has no bottom blobs.
        /// </summary>
        public override int ExactNumBottomBlobs
        {
            get { return 0; }
        }

        /// <summary>
        /// Returns the exact number of required top (output) Blobs: static_numeric, static_categorical, hist_numeric, hist_categorical, future_numeric, future_categorical
        /// </summary>
        public override int MinTopBlobs
        {
            get { return 6; }
        }

        /// <summary>
        /// Returns the exact number of required top (output) Blobs: static_numeric, static_categorical, hist_numeric, hist_categorical, future_numeric, future_categorical, target
        /// </summary>
        public override int MaxTopBlobs
        {
            get { return 7; }
        }

        /// <summary>
        /// Setup the layer.
        /// </summary>
        /// <param name="colBottom">Specifies the collection of bottom (input) Blobs, where the numeric blobs are ordered first, then the categorical blbos.</param>
        /// <param name="colTop">Specifies the collection of top (output) Blobs.</param>
        public override void LayerSetUp(BlobCollection<T> colBottom, BlobCollection<T> colTop)
        {
            m_nBatchSize = m_param.data_temporal_param.batch_size;
            m_nNumHistoricalSteps = m_param.data_temporal_param.num_historical_steps;
            m_nNumFutureSteps = m_param.data_temporal_param.num_future_steps;

            if (m_data == null)
                m_data = new RawFileData<T>(m_param.data_temporal_param.seed);

            Phase phase = m_phase;
            if (m_param.data_temporal_param.forced_phase.HasValue)
            {
                m_log.WriteLine("INFO: Using forced phase = " + m_param.data_temporal_param.forced_phase.Value.ToString() + ".");
                phase = m_param.data_temporal_param.forced_phase.Value;
            }

            if (!m_data.LoadData(phase, m_param.data_temporal_param.source, m_param.data_temporal_param.shuffle_data, (int)m_param.data_temporal_param.batch_size, (int)m_nNumHistoricalSteps, (int)m_nNumFutureSteps, m_param.data_temporal_param.max_load_count, m_param.data_temporal_param.drip_refresh_rate_in_sec, m_param.data_temporal_param.chunk_count, m_log, m_evtCancel))
                throw new Exception("DataTemporalLayer data loading aborted!");

            int nTotalSize = m_data.GetTotalSize();
            m_log.CHECK_GE(nTotalSize, m_nBatchSize, "There must be enough items for at least one batch - items found = " + nTotalSize.ToString() + ", batch size = " + m_nBatchSize.ToString());

            m_nNumStaticFeatsNumeric = (uint)m_data.GetCount(Data<T>.DATA_TYPE.STATIC_FEAT_NUMERIC, nTotalSize);
            m_nNumStaticFeatsCategorical = (uint)m_data.GetCount(Data<T>.DATA_TYPE.STATIC_FEAT_CATEGORICAL, nTotalSize);

            m_nNumHistoricalNumeric = (uint)m_data.GetCount(Data<T>.DATA_TYPE.HISTORICAL_NUMERIC, nTotalSize);
            m_nNumHistoricalCategorical = (uint)m_data.GetCount(Data<T>.DATA_TYPE.HISTORICAL_CATEGORICAL, nTotalSize);

            m_nNumFutureNumeric = (uint)m_data.GetCount(Data<T>.DATA_TYPE.FUTURE_NUMERIC, nTotalSize);
            m_nNumFutureCategorical = (uint)m_data.GetCount(Data<T>.DATA_TYPE.FUTURE_CATEGORICAL, nTotalSize);
        }

        /// <summary>
        /// Reshape the top (output) blobs.
        /// </summary>
        /// <param name="colBottom">Specifies the collection of bottom (input) Blobs.</param>
        /// <param name="colTop">Specifies the collection of top (output) Blobs.</param>
        public override void Reshape(BlobCollection<T> colBottom, BlobCollection<T> colTop)
        {
            m_rgShape.Clear();
            m_rgShape.Add((int)m_nBatchSize);
            m_rgShape.Add(0);

            if (m_nNumStaticFeatsNumeric > 0)
            {
                m_rgShape[1] = (int)m_nNumStaticFeatsNumeric;
                colTop[0].Reshape(m_rgShape);
            }

            if (m_nNumStaticFeatsCategorical > 0)
            {
                m_rgShape[1] = (int)m_nNumStaticFeatsCategorical;
                colTop[1].Reshape(m_rgShape);
            }

            m_rgShape[1] = (int)m_nNumHistoricalSteps;
            m_rgShape.Add(0);

            m_rgShape[2] = (int)m_nNumHistoricalNumeric;
            colTop[2].Reshape(m_rgShape);

            m_rgShape[2] = (int)m_nNumHistoricalCategorical;
            colTop[3].Reshape(m_rgShape);

            m_rgShape[1] = (int)m_nNumFutureSteps;

            m_rgShape[2] = (int)m_nNumFutureNumeric;
            colTop[4].Reshape(m_rgShape);

            m_rgShape[2] = (int)m_nNumFutureCategorical;
            colTop[5].Reshape(m_rgShape);

            if (colTop.Count > 6)
            {
                m_rgShape.Clear();
                m_rgShape.Add((int)m_nBatchSize);
                m_rgShape.Add((int)m_nNumFutureSteps);
                colTop[6].Reshape(m_rgShape);
            }
        }

        /// <summary>
        /// Forward computation
        /// </summary>
        /// <param name="colBottom">inpub Blob vector (length 1)
        ///  -# @f$ (N \times C \times H \times W) @f$ 
        ///     the inputs @f$ x @f$
        ///  </param>
        /// <param name="colTop">top output Blob vector)
        ///  -# @f$ (N \times C \times H \times W size) @f$
        ///     the computed outputs @f$ y @f$
        /// </param>
        protected override void forward(BlobCollection<T> colBottom, BlobCollection<T> colTop)
        {
            m_data.LoadBatch((int)m_nBatchSize, colTop);
        }

        /// @brief Not implemented - data Layers do not perform backward.
        protected override void backward(BlobCollection<T> colTop, List<bool> rgbPropagateDown, BlobCollection<T> colBottom)
        {
        }
    }

    /// <summary>
    /// The RawFileData object is used to load raw NPY file data.
    /// </summary>
    /// <typeparam name="T">Specifies the base data type of 'float' or 'double'.</typeparam>
    class RawFileData<T>
    {
        Data<T> m_data;
        Random m_random;
        int m_nBatchSize;


        /// <summary>
        /// The constructor.
        /// </summary>
        public RawFileData(uint? nSeed) 
        {
            if (nSeed.HasValue)
                m_random = new Random((int)nSeed.Value);
            else
                m_random = new Random();
        }

        /// <summary>
        /// Verify that the data files exist.
        /// </summary>
        /// <param name="phase">Specifies the phase.</param>
        /// <param name="strPath">Specifies the file path.</param>
        /// <exception cref="Exception">An exception is thrown if the file is missing.</exception>
        public void VerifyFiles(Phase phase, string strPath)
        {
            string strFile;
            string strType = "train";
            strPath = strPath.TrimEnd('\\', '/');
            strPath += "\\";

            if (phase == Phase.TEST)
                strType = "test";
            else if (phase == Phase.RUN)
                strType = "validation";

            strFile = strPath + strType + "_time_index.npy";
            if (!File.Exists(strFile))
                throw new Exception("Could not find the data file '" + strFile + "'.  You may need to run the data generation scripts.");

            Data<T>.VerifyFiles(strPath, strType);

            return;
        }

        /// <summary>
        /// Loads all data values for the phase specified.
        /// </summary>
        /// <param name="phase">Specifies the phase to load.</param>
        /// <param name="strPath">Specifies the base path for all data.</param>
        /// <param name="bShuffleData">Specifies to randomly select from the data.</param>
        /// <param name="nBatchSize">Specifies the batch size.</param>
        /// <param name="nHistoricalSteps">Specifies the number of historical steps.</param>
        /// <param name="nFutureSteps">Specifies the number of future steps.</param>
        /// <param name="nMaxLoadCount">Specifies the max items to load in background (default = 10000).</param>
        /// <param name="nDripRefreshRateInSec">Specifies the rate in seconds to refresh the data.</param>
        /// <param name="nChunkCount">Specifies the number of items to load on each cycle.</param>
        /// <param name="log">Specifies the output log.</param>
        /// <param name="evtCancel">Specifies the cancel event.</param>
        public bool LoadData(Phase phase, string strPath, bool bShuffleData, int nBatchSize, int nHistoricalSteps, int nFutureSteps, int nMaxLoadCount, int nDripRefreshRateInSec, uint nChunkCount, Log log, CancelEvent evtCancel)
        {
            m_nBatchSize = nBatchSize;
            m_data = new Data<T>(log, nHistoricalSteps, nFutureSteps, bShuffleData);

            VerifyFiles(phase, strPath);

            ManualResetEvent evtReady = new ManualResetEvent(false);
            ManualResetEvent evtDone = new ManualResetEvent(false);
            Thread threadLoad = new Thread(new ParameterizedThreadStart(loadDataFunction));
            threadLoad.Start(new DataLoadParameters(phase, strPath, nHistoricalSteps, nFutureSteps, nMaxLoadCount, nDripRefreshRateInSec, nChunkCount, bShuffleData, log, evtCancel, evtReady, evtDone));

            while (!evtReady.WaitOne(1000))
            {
                if (evtCancel.WaitOne(0))
                    return false;

                Thread.Sleep(50);
            }

            return true;
        }

        private void loadDataFunction(object obj)
        {
            DataLoadParameters arg = obj as DataLoadParameters;
            string strPath = arg.Path;
            Phase phase = arg.Phase;
            Log log = arg.Log;
            int nNumHistSteps = arg.HistoricalSteps;
            int nNumFutureSteps = arg.FutureSteps;
            int nMaxLoadCount = arg.MaxLoadCount;
            int nDripRefreshRateInSec = arg.DripRefreshRateInSec;
            uint nChunkCount = arg.ChunkCount;
            bool bShuffleData = arg.ShuffleData;
            CancelEvent evtCancel = arg.CancelEvent;
            ManualResetEvent evtReady = arg.ReadyEvent;
            ManualResetEvent evtDone = arg.DoneEvent;
            int nMaxWaitCountInSeconds = nDripRefreshRateInSec;

            try
            {
                string strFile;
                string strType = "train";
                strPath = strPath.TrimEnd('\\', '/');
                strPath += "\\";

                if (phase == Phase.TEST)
                    strType = "test";
                else if (phase == Phase.RUN)
                    strType = "validation";

                strFile = strPath + strType + "_time_index.npy";
                Tuple<List<float[]>, int[], List<string>> rgTimeRaw = Blob<float>.LoadFromNumpyEx(strFile, log);
                strFile = strPath + strType + "_combination_id.npy";
                Tuple<List<float[]>, int[], List<string>> rgCombinationIdRaw = Blob<float>.LoadFromNumpyEx(strFile, log);

                int nTotalCount = rgTimeRaw.Item1.Count;
                int nStartIdx = 0;
                int nCount = (int)nChunkCount;
                int nWaitCount = 0;
                Data<T> dataChunk = new Data<T>(m_data.Log, nNumHistSteps, nNumFutureSteps, bShuffleData);
                Stopwatch sw = new Stopwatch();

                sw.Start();

                if (nMaxLoadCount > rgTimeRaw.Item1.Count)
                    nMaxLoadCount = rgTimeRaw.Item1.Count;

                while (!evtCancel.WaitOne(0))
                {
                    while (dataChunk.Load(strPath, strType, nStartIdx, nCount, rgTimeRaw, rgCombinationIdRaw))
                    {
                        bool bRefreshed = m_data.Add(dataChunk, nMaxLoadCount);
                        dataChunk.Clear();

                        if (evtCancel.WaitOne(0))
                        {
                            log.WriteLine("Background data loading for '" + strType + "' aborted.");
                            break;
                        }

                        evtReady.Set();

                        nStartIdx += nCount;

                        if (nStartIdx + nCount > nTotalCount)
                            nCount = nTotalCount - nStartIdx;

                        if (nCount <= 0)
                            break;

                        if (sw.Elapsed.TotalMilliseconds > 1000)
                        {
                            double dfPct = (double)nStartIdx / (double)nTotalCount;
                            if (nMaxLoadCount > 0)
                            {
                                if (nStartIdx > nMaxLoadCount)
                                    dfPct = 1;
                                else
                                    dfPct = (double)nStartIdx / (double)nMaxLoadCount;
                            }

                            log.WriteLine("Background data loading '" + strType + "' data at " + dfPct.ToString("P") + "...");
                            sw.Restart();
                        }

                        if (bRefreshed)
                        {
                            log.WriteLine("Background data loading '" + strType + "' refreshed...");

                            // Wait roughly 5 minutes before refreshing the data;
                            nWaitCount = 0;
                            while (!evtCancel.WaitOne(1000))
                            {
                                Thread.Sleep(50);
                                nWaitCount++;

                                if (nWaitCount > nDripRefreshRateInSec)
                                    break;
                            }

                            if (nDripRefreshRateInSec == 0)
                                break;
                        }
                    }

                    if (nDripRefreshRateInSec <= 0)
                        break;

                    // Wait roughly 5 minutes before refreshing the data;
                    nWaitCount = 0;
                    while (!evtCancel.WaitOne(1000))
                    {
                        Thread.Sleep(50);
                        nWaitCount++;

                        if (nWaitCount > nDripRefreshRateInSec)
                            break;
                    }

                    nStartIdx = 0;
                }
            }
            finally
            {
                evtDone.Set();
            }
        }

        /// <summary>
        /// Loads a batch of data items into the BlobCollection.
        /// </summary>
        /// <param name="nBatchSize">Specifies the batch size.</param>
        /// <param name="col">Specifies the blob collection to load the batch into.</param>
        public void LoadBatch(int nBatchSize, BlobCollection<T> col)
        {
            m_data.LoadBatch(nBatchSize, col);
        }

        public int GetTotalSize()
        {
            return m_data.GetTotalSize();
        }

        public int GetCount(Data<T>.DATA_TYPE dtype, int nTotalSize)
        {
            return m_data.GetCount(dtype, nTotalSize);
        }
    }

#pragma warning disable 1591

    class DataLoadParameters /** @private */
    {
        Phase m_phase;
        string m_strPath;
        int m_nNumHistSteps;
        int m_nNumFutureSteps;
        int m_nMaxLoadCount;
        int m_nDripRrefreshRateInSec;
        uint m_nChunkCount;
        bool m_bShuffleData;
        Log m_log;
        CancelEvent m_evtCancel;
        ManualResetEvent m_evtReady;
        ManualResetEvent m_evtDone;

        public DataLoadParameters(Phase phase, string strPath, int nNumHistSteps, int nNumFutureSteps, int nMaxLoadCount, int nDripRefreshRateInSec, uint nChunkCount, bool bShuffleData, Log log, CancelEvent evtCancel, ManualResetEvent evtReady, ManualResetEvent evtDone)
        {
            m_phase = phase;
            m_strPath = strPath;
            m_nNumHistSteps = nNumHistSteps;
            m_nNumFutureSteps = nNumFutureSteps;
            m_nMaxLoadCount = nMaxLoadCount;
            m_nDripRrefreshRateInSec = nDripRefreshRateInSec;
            m_nChunkCount = nChunkCount;
            m_bShuffleData = bShuffleData;
            m_log = log;
            m_evtCancel = evtCancel;
            m_evtReady = evtReady;
            m_evtDone = evtDone;
        }

        public Phase Phase { get { return m_phase; } }
        public string Path { get { return m_strPath; } }
        public int HistoricalSteps {  get { return m_nNumHistSteps; } }
        public int FutureSteps { get { return m_nNumFutureSteps; } }
        public int MaxLoadCount { get { return m_nMaxLoadCount; } }
        public int DripRefreshRateInSec { get { return m_nDripRrefreshRateInSec; } }
        public uint ChunkCount { get { return m_nChunkCount; } }
        public bool ShuffleData { get { return m_bShuffleData; } }
        public Log Log { get { return m_log; } }
        public CancelEvent CancelEvent { get { return m_evtCancel; } }
        public ManualResetEvent ReadyEvent { get { return m_evtReady; } }
        public ManualResetEvent DoneEvent { get { return m_evtDone; } } 
    }

    class Data<T> /** @private */
    {
        int m_nIdx = 0;
        Random m_random = new Random();
        int m_nHistoricalSteps = 1;
        int m_nFutureSteps = 1;
        int m_nTotalCount = 0;
        Dictionary<DATA_TYPE, Tuple<List<float[]>, int[], List<string>>> m_rgData = new Dictionary<DATA_TYPE, Tuple<List<float[]>, int[], List<string>>>();
        int m_nStaticNumericCount = 0;
        float[] m_rgTimeIndexBatch = null;
        int m_nTimeIndexCount = 0;
        int m_nCombinationIdCount = 0;
        float[] m_rgStaticNumericBatch = null;
        int m_nStaticCategoricalCount = 0;
        float[] m_rgStaticCategoricalBatch = null;
        int m_nHistoricalNumericCount = 0;
        float[] m_rgHistoricalNumericBatch = null;
        int m_nHistoricalCategoricalCount = 0;
        float[] m_rgHistoricalCategoricalBatch = null;
        int m_nFutureNumericCount = 0;
        float[] m_rgFutureNumericBatch = null;
        int m_nFutureCategoricalCount = 0;
        float[] m_rgFutureCategoricalBatch = null;
        int m_nTargetCount = 0;
        float[] m_rgTargetBatch = null;
        Log m_log;
        object m_syncObj = new object();
        bool m_bShuffleData = true;

        /// <summary>
        /// Defines the data type.
        /// </summary>
        public enum DATA_TYPE
        {
            /// <summary>
            /// Specifies the ID of the combination.
            /// </summary>
            COMBINATION_ID,
            /// <summary>
            /// Specifies the timestamp in UnixTime.
            /// </summary>
            TIME_INDEX,
            /// <summary>
            /// Specifies the static feature numerical values.
            /// </summary>
            STATIC_FEAT_NUMERIC,
            /// <summary>
            /// Specifies the static features categorical values.
            /// </summary>
            STATIC_FEAT_CATEGORICAL,
            /// <summary>
            /// Specifies the historical numerical values.
            /// </summary>
            HISTORICAL_NUMERIC,
            /// <summary>
            /// Specifies the historical categorical values.
            /// </summary>
            HISTORICAL_CATEGORICAL,
            /// <summary>
            /// Specifies the future numerical values.
            /// </summary>
            FUTURE_NUMERIC,
            /// <summary>
            /// Specifies the future categorical values.
            /// </summary>
            FUTURE_CATEGORICAL,
            /// <summary>
            /// Specifies the target values.
            /// </summary>
            TARGET
        }

        /// <summary>
        /// The constructor.
        /// </summary>
        /// <param name="log">Specifies the output log.</param>
        /// <param name="nHistoricalSteps">Specifies the number of historical steps.</param>
        /// <param name="nFutureSteps">Specifies the number of future steps.</param>
        /// <param name="bShuffleData">Specifies to randomly select from the data.</param>
        public Data(Log log, int nHistoricalSteps, int nFutureSteps, bool bShuffleData)
        {
            m_log = log;
            m_nHistoricalSteps = nHistoricalSteps;
            m_nFutureSteps = nFutureSteps;
            m_bShuffleData = bShuffleData;
        }

        public int GetTotalSize()
        {
            lock (m_syncObj)
            {
                return m_rgData[DATA_TYPE.STATIC_FEAT_NUMERIC].Item2[0];
            }
        }

        public int GetCount(DATA_TYPE dtype, int nTotalSize)
        {
            lock (m_syncObj)
            {
                switch (dtype)
                {
                    case DATA_TYPE.TIME_INDEX:
                        m_log.CHECK_EQ(m_rgData[DATA_TYPE.TIME_INDEX].Item2[0], nTotalSize, "The batch sizes do not match!");
                        return m_rgData[DATA_TYPE.TIME_INDEX].Item2.Last();

                    case DATA_TYPE.COMBINATION_ID:
                        m_log.CHECK_EQ(m_rgData[DATA_TYPE.COMBINATION_ID].Item2[0], nTotalSize, "The batch sizes do not match!");
                        return m_rgData[DATA_TYPE.COMBINATION_ID].Item2.Last();

                    case DATA_TYPE.STATIC_FEAT_NUMERIC:
                        m_log.CHECK_EQ(m_rgData[DATA_TYPE.STATIC_FEAT_NUMERIC].Item2[0], nTotalSize, "The batch sizes do not match!");
                        return m_rgData[DATA_TYPE.STATIC_FEAT_NUMERIC].Item2.Last();

                    case DATA_TYPE.STATIC_FEAT_CATEGORICAL:
                        m_log.CHECK_EQ(m_rgData[DATA_TYPE.STATIC_FEAT_CATEGORICAL].Item2[0], nTotalSize, "The batch sizes do not match!");
                        return m_rgData[DATA_TYPE.STATIC_FEAT_CATEGORICAL].Item2.Last();

                    case DATA_TYPE.HISTORICAL_NUMERIC:
                        m_log.CHECK_EQ(m_rgData[DATA_TYPE.HISTORICAL_NUMERIC].Item2[0], nTotalSize, "The batch sizes do not match!");
                        return m_rgData[DATA_TYPE.HISTORICAL_NUMERIC].Item2.Last();

                    case DATA_TYPE.HISTORICAL_CATEGORICAL:
                        m_log.CHECK_EQ(m_rgData[DATA_TYPE.HISTORICAL_CATEGORICAL].Item2[0], nTotalSize, "The batch sizes do not match!");
                        return m_rgData[DATA_TYPE.HISTORICAL_CATEGORICAL].Item2.Last();

                    case DATA_TYPE.FUTURE_NUMERIC:
                        m_log.CHECK_EQ(m_rgData[DATA_TYPE.FUTURE_NUMERIC].Item2[0], nTotalSize, "The batch sizes do not match!");
                        return m_rgData[DATA_TYPE.FUTURE_NUMERIC].Item2.Last();

                    case DATA_TYPE.FUTURE_CATEGORICAL:
                        m_log.CHECK_EQ(m_rgData[DATA_TYPE.FUTURE_CATEGORICAL].Item2[0], nTotalSize, "The batch sizes do not match!");
                        return m_rgData[DATA_TYPE.FUTURE_CATEGORICAL].Item2.Last();

                    case DATA_TYPE.TARGET:
                        m_log.CHECK_EQ(m_rgData[DATA_TYPE.TARGET].Item2[0], nTotalSize, "The batch sizes do not match!");
                        return m_rgData[DATA_TYPE.TARGET].Item2.Last();

                    default:
                        throw new Exception("Unsupported count '" + dtype.ToString() + "'!");
                }
            }
        }

        /// <summary>
        /// Return the log object.
        /// </summary>
        public Log Log
        {
            get { return m_log; }
        }

        /// <summary>
        /// Converts the unix time values to DateTime values.
        /// </summary>
        /// <param name="unixTimeStamp">Specififies the Unix Time value to convert.</param>
        /// <returns>The DateTime value associated with the unix time value is returned.</returns>
        public static DateTime UnixTimeStampToDateTime(double unixTimeStamp)
        {
            // Unix timestamp is seconds past epoch
            DateTime dateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            dateTime = dateTime.AddSeconds(unixTimeStamp).ToLocalTime();
            return dateTime;
        }

        private int calculateCount(DATA_TYPE dtype)
        {
            return m_rgData[dtype].Item2.Last();
        }

        private float[] createBatchBuffer(float[] rg, int nBatchSize, int nSteps, int nCount)
        {
            if (nCount == 0)
                return rg;

            int nDim = nBatchSize * nSteps * nCount;
            if (rg == null || rg.Length != nDim)
                return new float[nDim];

            return rg;
        }

        private bool add(DATA_TYPE type, Data<T> data, int nMax, out int nCount)
        {
            bool bRefreshed = false;

            if (!m_rgData.ContainsKey(type))
            {
                m_rgData.Add(type, data.m_rgData[type]);
            }
            else
            {
                m_rgData[type].Item1.AddRange(data.m_rgData[type].Item1);
                m_rgData[type].Item2[0] += data.m_rgData[type].Item2[0];
                m_rgData[type].Item3.AddRange(data.m_rgData[type].Item3);

                // Clip to max if needed.
                if (nMax > 0)
                {
                    if (m_rgData[type].Item1.Count == nMax)
                        bRefreshed = true;

                    while (m_rgData[type].Item1.Count > nMax)
                    {
                        if (m_rgData[type].Item1.Count > 0)
                            m_rgData[type].Item1.RemoveAt(0);
                        m_rgData[type].Item2[0] -= 1;
                        if (m_rgData[type].Item3.Count > 0)
                            m_rgData[type].Item3.RemoveAt(0);
                        bRefreshed = true;
                    }
                }
            }

            nCount = 1;
            if (m_rgData[type].Item2.Length > 1)
                nCount = m_rgData[type].Item2.Last();

            return bRefreshed;
        }

        public bool Add(Data<T> data, int nMax)
        {
            lock (m_syncObj)
            {
                add(DATA_TYPE.TIME_INDEX, data, nMax, out m_nTimeIndexCount);
                add(DATA_TYPE.COMBINATION_ID, data, nMax, out m_nCombinationIdCount);
                add(DATA_TYPE.STATIC_FEAT_NUMERIC, data, nMax, out m_nStaticNumericCount);
                add(DATA_TYPE.STATIC_FEAT_CATEGORICAL, data, nMax, out m_nStaticCategoricalCount);
                add(DATA_TYPE.HISTORICAL_NUMERIC, data, nMax, out m_nHistoricalNumericCount);
                add(DATA_TYPE.HISTORICAL_CATEGORICAL, data, nMax, out m_nHistoricalCategoricalCount);
                add(DATA_TYPE.FUTURE_NUMERIC, data, nMax, out m_nFutureNumericCount);
                add(DATA_TYPE.FUTURE_CATEGORICAL, data, nMax, out m_nFutureCategoricalCount);
                bool bRefreshed = add(DATA_TYPE.TARGET, data, nMax, out m_nTargetCount);

                if (!bRefreshed)
                    m_nTotalCount += data.m_nTotalCount;
                else
                    m_nTotalCount = nMax;

                return bRefreshed;
            }
        }

        public void Clear()
        {
            m_rgData.Clear();
            m_nTimeIndexCount = 0;
            m_nStaticCategoricalCount = 0;
            m_nStaticNumericCount = 0;
            m_nHistoricalCategoricalCount = 0;
            m_nHistoricalNumericCount = 0;
            m_nFutureCategoricalCount = 0;
            m_nFutureNumericCount = 0;
            m_nTargetCount = 0;
            m_nTotalCount = 0;
        }

        public static bool VerifyFiles(string strPath, string strType)
        {
            string strFile;

            strFile = strPath + strType + "_static_feats_numeric.npy";
            if (!File.Exists(strFile))
                throw new Exception("Missing data file '" + strFile + "'!");

            strFile = strPath + strType + "_static_feats_categorical.npy";
            if (!File.Exists(strFile))
                throw new Exception("Missing data file '" + strFile + "'!");

            strFile = strPath + strType + "_historical_ts_numeric.npy";
            if (!File.Exists(strFile))
                throw new Exception("Missing data file '" + strFile + "'!");

            strFile = strPath + strType + "_historical_ts_categorical.npy";
            if (!File.Exists(strFile))
                throw new Exception("Missing data file '" + strFile + "'!");

            strFile = strPath + strType + "_future_ts_numeric.npy";
            if (!File.Exists(strFile))
                throw new Exception("Missing data file '" + strFile + "'!");

            strFile = strPath + strType + "_future_ts_categorical.npy";
            if (!File.Exists(strFile))
                throw new Exception("Missing data file '" + strFile + "'!");

            strFile = strPath + strType + "_target.npy";
            if (!File.Exists(strFile))
                throw new Exception("Missing data file '" + strFile + "'!");

            return true;
        }

        private Tuple<List<float[]>, int[], List<string>> clone(Tuple<List<float[]>, int[], List<string>> rg, int nStartIdx, int nCount)
        {
            List<float[]> rgData = new List<float[]>();
            List<string> rgstr = new List<string>();

            for (int i = 0; i < nCount; i++)
            {
                int nIdx = nStartIdx + i;

                if (nIdx < rg.Item1.Count)
                    rgData.Add(rg.Item1[nIdx]);
                if (nIdx < rg.Item3.Count)
                    rgstr.Add(rg.Item3[nIdx]);
            }

            nCount = Math.Max(rgData.Count(), rgstr.Count());

            return new Tuple<List<float[]>, int[], List<string>>(rgData, new int[] { nCount }, rgstr);
        }

        public bool Load(string strPath, string strType, int nStartIdx, int nCount, Tuple<List<float[]>, int[], List<string>> rgTimeIdx, Tuple<List<float[]>, int[], List<string>> rgComboId)
        {
            Log log = m_log;
            string strFile;

            try
            {
                if (!m_rgData.ContainsKey(DATA_TYPE.TIME_INDEX))
                {
                    m_rgData.Add(DATA_TYPE.TIME_INDEX, clone(rgTimeIdx, nStartIdx, nCount));
                }

                if (!m_rgData.ContainsKey(DATA_TYPE.COMBINATION_ID))
                {
                    m_rgData.Add(DATA_TYPE.COMBINATION_ID, clone(rgComboId, nStartIdx, nCount));
                }

                if (!m_rgData.ContainsKey(DATA_TYPE.STATIC_FEAT_NUMERIC))
                {
                    strFile = strPath + strType + "_static_feats_numeric.npy";
                    m_rgData.Add(DATA_TYPE.STATIC_FEAT_NUMERIC, Blob<float>.LoadFromNumpyEx(strFile, log, int.MaxValue, nStartIdx, nCount));
                    m_nStaticNumericCount = calculateCount(DATA_TYPE.STATIC_FEAT_NUMERIC);
                }

                if (!m_rgData.ContainsKey(DATA_TYPE.STATIC_FEAT_CATEGORICAL))
                {
                    strFile = strPath + strType + "_static_feats_categorical.npy";
                    m_rgData.Add(DATA_TYPE.STATIC_FEAT_CATEGORICAL, Blob<float>.LoadFromNumpyEx(strFile, log, int.MaxValue, nStartIdx, nCount));
                    m_nStaticCategoricalCount = calculateCount(DATA_TYPE.STATIC_FEAT_CATEGORICAL);
                }

                if (!m_rgData.ContainsKey(DATA_TYPE.HISTORICAL_NUMERIC))
                {
                    strFile = strPath + strType + "_historical_ts_numeric.npy";
                    m_rgData.Add(DATA_TYPE.HISTORICAL_NUMERIC, Blob<float>.LoadFromNumpyEx(strFile, log, int.MaxValue, nStartIdx, nCount));
                    m_nHistoricalNumericCount = calculateCount(DATA_TYPE.HISTORICAL_NUMERIC);
                }

                if (!m_rgData.ContainsKey(DATA_TYPE.HISTORICAL_CATEGORICAL))
                {
                    strFile = strPath + strType + "_historical_ts_categorical.npy";
                    m_rgData.Add(DATA_TYPE.HISTORICAL_CATEGORICAL, Blob<float>.LoadFromNumpyEx(strFile, log, int.MaxValue, nStartIdx, nCount));
                    m_nHistoricalCategoricalCount = calculateCount(DATA_TYPE.HISTORICAL_CATEGORICAL);
                }

                if (!m_rgData.ContainsKey(DATA_TYPE.FUTURE_NUMERIC))
                {
                    strFile = strPath + strType + "_future_ts_numeric.npy";
                    m_rgData.Add(DATA_TYPE.FUTURE_NUMERIC, Blob<float>.LoadFromNumpyEx(strFile, log, int.MaxValue, nStartIdx, nCount));
                    m_nFutureNumericCount = calculateCount(DATA_TYPE.FUTURE_NUMERIC);
                }

                if (!m_rgData.ContainsKey(DATA_TYPE.FUTURE_CATEGORICAL))
                {
                    strFile = strPath + strType + "_future_ts_categorical.npy";
                    m_rgData.Add(DATA_TYPE.FUTURE_CATEGORICAL, Blob<float>.LoadFromNumpyEx(strFile, log, int.MaxValue, nStartIdx, nCount));
                    m_nFutureCategoricalCount = calculateCount(DATA_TYPE.FUTURE_CATEGORICAL);
                }

                if (!m_rgData.ContainsKey(DATA_TYPE.TARGET))
                {
                    strFile = strPath + strType + "_target.npy";
                    m_rgData.Add(DATA_TYPE.TARGET, Blob<float>.LoadFromNumpyEx(strFile, log, int.MaxValue, nStartIdx, nCount));
                    m_nTargetCount = calculateCount(DATA_TYPE.TARGET);
                }

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        /// <summary>
        /// Loads a batch of data items into the BlobCollection.
        /// </summary>
        /// <param name="nBatchSize">Specifies the batch size.</param>
        /// <param name="col">Specifies the blob collection to load the batch into.</param>
        public void LoadBatch(int nBatchSize, BlobCollection<T> col)
        {
            List<float[]> rgTimeIndex = m_rgData[DATA_TYPE.TIME_INDEX].Item1;
            List<float[]> rgStaticNumeric = m_rgData[DATA_TYPE.STATIC_FEAT_NUMERIC].Item1;
            List<float[]> rgStaticCategorical = m_rgData[DATA_TYPE.STATIC_FEAT_CATEGORICAL].Item1;
            List<float[]> rgHistoricalNumeric = m_rgData[DATA_TYPE.HISTORICAL_NUMERIC].Item1;
            List<float[]> rgHistoricalCategorical = m_rgData[DATA_TYPE.HISTORICAL_CATEGORICAL].Item1;
            List<float[]> rgFutureNumeric = m_rgData[DATA_TYPE.FUTURE_NUMERIC].Item1;
            List<float[]> rgFutureCategorical = m_rgData[DATA_TYPE.FUTURE_CATEGORICAL].Item1;
            List<float[]> rgTarget = m_rgData[DATA_TYPE.TARGET].Item1;

            m_rgTimeIndexBatch = createBatchBuffer(m_rgTimeIndexBatch, nBatchSize, 1, m_nTimeIndexCount);
            m_rgStaticNumericBatch = createBatchBuffer(m_rgStaticNumericBatch, nBatchSize, 1, m_nStaticNumericCount);
            m_rgStaticCategoricalBatch = createBatchBuffer(m_rgStaticCategoricalBatch, nBatchSize, 1, m_nStaticCategoricalCount);
            m_rgHistoricalNumericBatch = createBatchBuffer(m_rgHistoricalNumericBatch, nBatchSize, m_nHistoricalSteps, m_nHistoricalNumericCount);
            m_rgHistoricalCategoricalBatch = createBatchBuffer(m_rgHistoricalCategoricalBatch, nBatchSize, m_nHistoricalSteps, m_nHistoricalCategoricalCount);
            m_rgFutureNumericBatch = createBatchBuffer(m_rgFutureNumericBatch, nBatchSize, m_nFutureSteps, m_nFutureNumericCount);
            m_rgFutureCategoricalBatch = createBatchBuffer(m_rgFutureCategoricalBatch, nBatchSize, m_nFutureSteps, m_nFutureCategoricalCount);
            m_rgTargetBatch = createBatchBuffer(m_rgTargetBatch, nBatchSize, 1,m_nTargetCount);

            int nTimeIndexSize = m_nTimeIndexCount;
            int nStaticNumericSize = m_nStaticNumericCount;
            int nStaticCategoricalSize = m_nStaticCategoricalCount;
            int nHistoricalNumericSize = m_nHistoricalNumericCount * m_nHistoricalSteps;
            int nHistoricalCategoricalSize = m_nHistoricalCategoricalCount * m_nHistoricalSteps;
            int nFutureNumericSize = m_nFutureNumericCount * m_nFutureSteps;
            int nFutureCategoricalSize = m_nFutureCategoricalCount * m_nFutureSteps;
            int nTargetSize = m_nTargetCount;

            lock (m_syncObj)
            {
                for (int i = 0; i < nBatchSize; i++)
                {
                    int nIdx = (m_bShuffleData) ? m_random.Next(m_nTotalCount) : m_nIdx;
                    m_nIdx++;

                    if (m_nIdx == m_nTotalCount)
                        m_nIdx = 0;

                    if (m_rgTimeIndexBatch != null)
                    {
                        float[] rgTimeIndex1 = rgTimeIndex[nIdx];
                        Array.Copy(rgTimeIndex1, 0, m_rgTimeIndexBatch, i * nTimeIndexSize, nTimeIndexSize);
                    }

                    if (m_rgStaticNumericBatch != null)
                    {
                        float[] rgStaticNumeric1 = rgStaticNumeric[nIdx];
                        Array.Copy(rgStaticNumeric1, 0, m_rgStaticNumericBatch, i * nStaticNumericSize, nStaticNumericSize);
                    }

                    if (m_rgStaticCategoricalBatch != null)
                    {
                        float[] rgStaticCategorical1 = rgStaticCategorical[nIdx];
                        Array.Copy(rgStaticCategorical1, 0, m_rgStaticCategoricalBatch, i * nStaticCategoricalSize, nStaticCategoricalSize);
                    }

                    if (m_rgHistoricalNumericBatch != null)
                    {
                        float[] rgHistoricalNumeric1 = rgHistoricalNumeric[nIdx];
                        Array.Copy(rgHistoricalNumeric1, 0, m_rgHistoricalNumericBatch, i * nHistoricalNumericSize, nHistoricalNumericSize);
                    }

                    if (m_rgHistoricalCategoricalBatch != null)
                    {
                        float[] rgHistoricalCategorical1 = rgHistoricalCategorical[nIdx];
                        Array.Copy(rgHistoricalCategorical1, 0, m_rgHistoricalCategoricalBatch, i * nHistoricalCategoricalSize, nHistoricalCategoricalSize);
                    }

                    if (m_rgFutureNumericBatch != null)
                    {
                        float[] rgFutureNumeric1 = rgFutureNumeric[nIdx];
                        Array.Copy(rgFutureNumeric1, 0, m_rgFutureNumericBatch, i * nFutureNumericSize, nFutureNumericSize);
                    }

                    if (m_rgFutureCategoricalBatch != null)
                    {
                        float[] rgFutureCategorical1 = rgFutureCategorical[nIdx];
                        Array.Copy(rgFutureCategorical1, 0, m_rgFutureCategoricalBatch, i * nFutureCategoricalSize, nFutureCategoricalSize);
                    }

                    if (m_rgTargetBatch != null && col.Count > 6)
                    {
                        float[] rgTarget1 = rgTarget[nIdx];
                        Array.Copy(rgTarget1, 0, m_rgTargetBatch, i * nTargetSize, nTargetSize);
                    }
                }
            }

            if (m_rgStaticNumericBatch != null)
                col[0].mutable_cpu_data = Utility.ConvertVec<T>(m_rgStaticNumericBatch);

            if (m_rgStaticCategoricalBatch != null)
                col[1].mutable_cpu_data = Utility.ConvertVec<T>(m_rgStaticCategoricalBatch);

            col[2].mutable_cpu_data = Utility.ConvertVec<T>(m_rgHistoricalNumericBatch);
            col[3].mutable_cpu_data = Utility.ConvertVec<T>(m_rgHistoricalCategoricalBatch);
            col[4].mutable_cpu_data = Utility.ConvertVec<T>(m_rgFutureNumericBatch);
            col[5].mutable_cpu_data = Utility.ConvertVec<T>(m_rgFutureCategoricalBatch);

            if (col.Count > 6)
                col[6].mutable_cpu_data = Utility.ConvertVec<T>(m_rgTargetBatch);
        }
    }

#pragma warning restore 1591
}
