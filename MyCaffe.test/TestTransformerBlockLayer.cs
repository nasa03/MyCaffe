﻿using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyCaffe.param;
using MyCaffe.basecode;
using MyCaffe.common;
using MyCaffe.fillers;
using MyCaffe.layers;
using System.Diagnostics;
using System.IO;
using MyCaffe.param.gpt;

///
/// WORK IN PROGRESS
///
namespace MyCaffe.test
{
    [TestClass]
    public class TestTransformerBlockLayer
    {
        [TestMethod]
        public void TestForwardPico()
        {
            TransformerBlockLayerTest test = new TransformerBlockLayerTest(EngineParameter.Engine.CAFFE);

            try
            {
                foreach (ITransformerBlockLayerTest t in test.Tests)
                {
                    t.TestForwardPico(false, 1);
                }
            }
            finally
            {
                test.Dispose();
            }
        }

        [TestMethod]
        public void TestBackwardPico()
        {
            TransformerBlockLayerTest test = new TransformerBlockLayerTest(EngineParameter.Engine.CAFFE);

            try
            {
                foreach (ITransformerBlockLayerTest t in test.Tests)
                {
                    t.TestBackwardPico(false, 1);
                }
            }
            finally
            {
                test.Dispose();
            }
        }

        [TestMethod]
        public void TestGradientPico()
        {
            TransformerBlockLayerTest test = new TransformerBlockLayerTest(EngineParameter.Engine.CAFFE);

            try
            {
                foreach (ITransformerBlockLayerTest t in test.Tests)
                {
                    t.TestGradientPico(false, 1);
                }
            }
            finally
            {
                test.Dispose();
            }
        }

        [TestMethod]
        public void TestForwardPico3()
        {
            TransformerBlockLayerTest test = new TransformerBlockLayerTest(EngineParameter.Engine.CAFFE);

            try
            {
                foreach (ITransformerBlockLayerTest t in test.Tests)
                {
                    t.TestForwardPico(false, 3);
                }
            }
            finally
            {
                test.Dispose();
            }
        }

        [TestMethod]
        public void TestBackwardPico3()
        {
            TransformerBlockLayerTest test = new TransformerBlockLayerTest(EngineParameter.Engine.CAFFE);

            try
            {
                foreach (ITransformerBlockLayerTest t in test.Tests)
                {
                    t.TestBackwardPico(false, 3);
                }
            }
            finally
            {
                test.Dispose();
            }
        }

        [TestMethod]
        public void TestGradientPico3()
        {
            TransformerBlockLayerTest test = new TransformerBlockLayerTest(EngineParameter.Engine.CAFFE);

            try
            {
                foreach (ITransformerBlockLayerTest t in test.Tests)
                {
                    t.TestGradientPico(false, 3);
                }
            }
            finally
            {
                test.Dispose();
            }
        }

        [TestMethod]
        public void TestForwardPico3Batch()
        {
            TransformerBlockLayerTest test = new TransformerBlockLayerTest(EngineParameter.Engine.CAFFE);

            try
            {
                foreach (ITransformerBlockLayerTest t in test.Tests)
                {
                    t.TestForwardPico(true, 3);
                }
            }
            finally
            {
                test.Dispose();
            }
        }

        [TestMethod]
        public void TestBackwardPico3Batch()
        {
            TransformerBlockLayerTest test = new TransformerBlockLayerTest(EngineParameter.Engine.CAFFE);

            try
            {
                foreach (ITransformerBlockLayerTest t in test.Tests)
                {
                    t.TestBackwardPico(true, 3);
                }
            }
            finally
            {
                test.Dispose();
            }
        }

        [TestMethod]
        public void TestGradientPico3Batch()
        {
            TransformerBlockLayerTest test = new TransformerBlockLayerTest(EngineParameter.Engine.CAFFE);

            try
            {
                foreach (ITransformerBlockLayerTest t in test.Tests)
                {
                    t.TestGradientPico(true, 3);
                }
            }
            finally
            {
                test.Dispose();
            }
        }

        [TestMethod]
        public void TestForwardMini()
        {
            TransformerBlockLayerTest test = new TransformerBlockLayerTest(EngineParameter.Engine.CAFFE);

            try
            {
                foreach (ITransformerBlockLayerTest t in test.Tests)
                {
                    t.TestForwardMini();
                }
            }
            finally
            {
                test.Dispose();
            }
        }
    }

    interface ITransformerBlockLayerTest : ITest
    {
        void TestForwardPico(bool bBatch, int nHeads);
        void TestBackwardPico(bool bBatch, int nHeads);
        void TestGradientPico(bool bBatch, int nHeads);
        void TestForwardMini();
    }

    class TransformerBlockLayerTest : TestBase
    {
        public TransformerBlockLayerTest(EngineParameter.Engine engine = EngineParameter.Engine.DEFAULT)
            : base("Transformer Block Layer Test", TestBase.DEFAULT_DEVICE_ID, engine)
        {
        }

        protected override ITest create(common.DataType dt, string strName, int nDeviceID, EngineParameter.Engine engine)
        {
            if (dt == common.DataType.DOUBLE)
                return new TransformerBlockLayerTest2<double>(strName, nDeviceID, engine);
            else
                return new TransformerBlockLayerTest2<float>(strName, nDeviceID, engine);
        }
    }

    class TransformerBlockLayerTest2<T> : TestEx<T>, ITransformerBlockLayerTest
    {
        Blob<T> m_blobY;

        public TransformerBlockLayerTest2(string strName, int nDeviceID, EngineParameter.Engine engine)
            : base(strName, new List<int>() { 3, 2, 4, 1 }, nDeviceID)
        {
            m_engine = engine;
            m_blobY = new Blob<T>(m_cuda, m_log);
        }

        protected override FillerParameter getFillerParam()
        {
            return base.getFillerParam();
        }

        private void dispose1(ref Blob<T> b)
        {
            if (b != null)
            {
                b.Dispose();
                b = null;
            }
        }

        protected override void dispose()
        {
            dispose(ref m_blobY);
            base.dispose();
        }

        public Tuple<List<int>, float[]> Fill(string strGpt, string strName, Log log, TransformerBlockParameter p)
        {
            string strFile = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) + "\\MyCaffe\\test_data\\data\\text\\" + strGpt + "\\" + strName + ".txt";
            string[] rgstrLines = File.ReadAllLines(strFile);
            string strSize = rgstrLines[0].Trim('#', ' ', '(', ')', ',');
            string[] rgstrSize = strSize.Split(',');
            List<int> rgnShape = rgstrSize.Select(p1 => int.Parse(p1)).ToList();
            List<float> rgfVal = new List<float>();
            
            while (rgnShape.Count < 4)
            {
                rgnShape.Add(1);
            }

            int nCount = 1;
            foreach (int nDim in rgnShape)
            {
                nCount *= nDim;
            }

            for (int i = 1; i < rgstrLines.Length; i++)
            {
                string[] rgstrVals = rgstrLines[i].Split(' ');

                for (int j = 0; j < rgstrVals.Length; j++)
                {
                    string strVal = rgstrVals[j].Trim();

                    if (!string.IsNullOrEmpty(strVal))
                    {
                        float fVal = float.Parse(strVal);
                        rgfVal.Add(fVal);
                    }
                }
            }

            log.CHECK_EQ(rgfVal.Count, nCount, "The bottom count does not match the number of values read in!");

            float[] rgf = rgfVal.ToArray();

            return new Tuple<List<int>, float[]>(rgnShape, rgf);
        }

        public void TestForwardPico(bool bBatch, int nHeads)
        {
            LayerParameter p = new LayerParameter(LayerParameter.LayerType.TRANSFORMER_BLOCK);
            p.transformer_block_param.heads = nHeads;
            p.transformer_block_param.embed = 3;
            p.transformer_block_param.block_size = 4;
            p.transformer_block_param.attn_dropout = 0.0;
            p.transformer_block_param.resid_dropout = 0.0;
            Layer<T> layer = Layer<T>.Create(m_cuda, m_log, p, new CancelEvent());

            try
            {
                string strModel = "gpt-pico-blk";
                if (nHeads > 1)
                    strModel += nHeads.ToString();
                if (bBatch)
                    strModel += "B";

                m_log.CHECK(layer.type == LayerParameter.LayerType.TRANSFORMER_BLOCK, "The layer type is incorrect!");

                Tuple<List<int>, float[]> x = Fill(strModel, "1_x", m_log, p.transformer_block_param);
                m_blob_bottom.Reshape(x.Item1);
                m_blob_bottom.mutable_cpu_data = convert(x.Item2);
                
                Tuple<List<int>, float[]> y = Fill(strModel, "10_y", m_log, p.transformer_block_param);
                m_blobY.Reshape(y.Item1);
                m_blobY.mutable_cpu_data = convert(y.Item2);
                
                Tuple<List<int>, float[]> attnBias = Fill(strModel, "attn_bias", m_log, p.transformer_block_param);
                Tuple<List<int>, float[]> attnWt = Fill(strModel, "attn_weight", m_log, p.transformer_block_param);
                Tuple<List<int>, float[]> attnProjBias = Fill(strModel, "attn_proj_bias", m_log, p.transformer_block_param);
                Tuple<List<int>, float[]> attnProjWt = Fill(strModel, "attn_proj_weight", m_log, p.transformer_block_param);

                Tuple<List<int>, float[]> fcBias = Fill(strModel, "fc_bias", m_log, p.transformer_block_param);
                Tuple<List<int>, float[]> fcWt = Fill(strModel, "fc_weight", m_log, p.transformer_block_param);
                Tuple<List<int>, float[]> projBias = Fill(strModel, "proj_bias", m_log, p.transformer_block_param);
                Tuple<List<int>, float[]> projWt = Fill(strModel, "proj_weight", m_log, p.transformer_block_param);

                layer.Setup(BottomVec, TopVec);

                layer.blobs[0].mutable_cpu_data = convert(attnWt.Item2);
                layer.blobs[1].mutable_cpu_data = convert(attnBias.Item2);
                layer.blobs[2].mutable_cpu_data = convert(attnProjWt.Item2);
                layer.blobs[3].mutable_cpu_data = convert(attnProjBias.Item2);
                
                layer.blobs[4].mutable_cpu_data = convert(fcWt.Item2);
                layer.blobs[5].mutable_cpu_data = convert(fcBias.Item2);
                layer.blobs[6].mutable_cpu_data = convert(projWt.Item2);
                layer.blobs[7].mutable_cpu_data = convert(projBias.Item2);

                layer.Forward(BottomVec, TopVec);

                // Now, check values
                float[] rgExpected = convertF(m_blobY.mutable_cpu_data);
                float[] rgActual = convertF(m_blob_top.mutable_cpu_data);

                for (int i = 0; i < rgExpected.Length; i++)
                {
                    float fExpected = rgExpected[i];
                    float fActual = rgActual[i];
                    float fErr = 0.00000001f;

                    m_log.EXPECT_NEAR_FLOAT(fExpected, fActual, fErr, "The values are not as expected!");
                }
            }
            finally
            {
                layer.Dispose();
            }
        }

        public void TestBackwardPico(bool bBatch, int nHeads)
        {
            LayerParameter p = new LayerParameter(LayerParameter.LayerType.TRANSFORMER_BLOCK);
            p.transformer_block_param.heads = nHeads;
            p.transformer_block_param.embed = 3;
            p.transformer_block_param.block_size = 4;
            p.transformer_block_param.attn_dropout = 0.0;
            p.transformer_block_param.resid_dropout = 0.0;
            Layer<T> layer = Layer<T>.Create(m_cuda, m_log, p, new CancelEvent());

            try
            {
                string strModel = "gpt-pico-blk";
                if (nHeads > 1)
                    strModel += nHeads.ToString();
                if (bBatch)
                    strModel += "B";

                m_log.CHECK(layer.type == LayerParameter.LayerType.TRANSFORMER_BLOCK, "The layer type is incorrect!");

                Tuple<List<int>, float[]> x = Fill(strModel, "1_x", m_log, p.transformer_block_param);
                m_blob_bottom.Reshape(x.Item1);
                m_blob_bottom.mutable_cpu_data = convert(x.Item2);

                Tuple<List<int>, float[]> y_grad = Fill(strModel, "grad_1_y", m_log, p.transformer_block_param);                
                Tuple<List<int>, float[]> x_grad = Fill(strModel, "grad_10_x", m_log, p.transformer_block_param);

                Tuple<List<int>, float[]> attnBias = Fill(strModel, "attn_bias", m_log, p.transformer_block_param);
                Tuple<List<int>, float[]> attnWt = Fill(strModel, "attn_weight", m_log, p.transformer_block_param);
                Tuple<List<int>, float[]> attnProjBias = Fill(strModel, "attn_proj_bias", m_log, p.transformer_block_param);
                Tuple<List<int>, float[]> attnProjWt = Fill(strModel, "attn_proj_weight", m_log, p.transformer_block_param);

                Tuple<List<int>, float[]> fcBias = Fill(strModel, "fc_bias", m_log, p.transformer_block_param);
                Tuple<List<int>, float[]> fcWt = Fill(strModel, "fc_weight", m_log, p.transformer_block_param);
                Tuple<List<int>, float[]> projBias = Fill(strModel, "proj_bias", m_log, p.transformer_block_param);
                Tuple<List<int>, float[]> projWt = Fill(strModel, "proj_weight", m_log, p.transformer_block_param);

                layer.Setup(BottomVec, TopVec);

                layer.blobs[0].mutable_cpu_data = convert(attnWt.Item2);
                layer.blobs[1].mutable_cpu_data = convert(attnBias.Item2);
                layer.blobs[2].mutable_cpu_data = convert(attnProjWt.Item2);
                layer.blobs[3].mutable_cpu_data = convert(attnProjBias.Item2);

                layer.blobs[4].mutable_cpu_data = convert(fcWt.Item2);
                layer.blobs[5].mutable_cpu_data = convert(fcBias.Item2);
                layer.blobs[6].mutable_cpu_data = convert(projWt.Item2);
                layer.blobs[7].mutable_cpu_data = convert(projBias.Item2);

                layer.Forward(BottomVec, TopVec);

                m_blob_top.mutable_cpu_diff = convert(y_grad.Item2);

                layer.Backward(TopVec, new List<bool>() { true }, BottomVec);
                
                // Now, check values
                float[] rgExpected = x_grad.Item2;
                float[] rgActual = convertF(m_blob_bottom.mutable_cpu_diff);

                for (int i = 0; i < rgExpected.Length; i++)
                {
                    float fExpected = rgExpected[i];
                    float fActual = rgActual[i];
                    float fErr = 1e-3f;

                    m_log.EXPECT_NEAR_FLOAT(fExpected, fActual, fErr, "The values are not as expected!");
                }
            }
            finally
            {
                layer.Dispose();
            }
        }

        public void TestGradientPico(bool bBatch, int nHeads)
        {
            LayerParameter p = new LayerParameter(LayerParameter.LayerType.TRANSFORMER_BLOCK);
            p.transformer_block_param.heads = nHeads;
            p.transformer_block_param.embed = 3;
            p.transformer_block_param.block_size = 4;
            p.transformer_block_param.attn_dropout = 0.0;
            p.transformer_block_param.resid_dropout = 0.0;
            Layer<T> layer = Layer<T>.Create(m_cuda, m_log, p, new CancelEvent());

            try
            {
                string strModel = "gpt-pico-blk";
                if (nHeads > 1)
                    strModel += nHeads.ToString();
                if (bBatch)
                    strModel += "B";

                m_log.CHECK(layer.type == LayerParameter.LayerType.TRANSFORMER_BLOCK, "The layer type is incorrect!");

                Tuple<List<int>, float[]> data = Fill(strModel, "1_x", m_log, p.transformer_block_param);
                m_blob_bottom.Reshape(data.Item1);
                m_blob_bottom.mutable_cpu_data = convert(data.Item2);

                GradientChecker<T> checker = new GradientChecker<T>(m_cuda, m_log, 0.01, 0.01);
                checker.CheckGradient(layer, BottomVec, TopVec);
            }
            finally
            {
                layer.Dispose();
            }
        }

        public void TestForwardMini()
        {
            LayerParameter p = new LayerParameter(LayerParameter.LayerType.TRANSFORMER_BLOCK);
            p.transformer_block_param.heads = 6;
            p.transformer_block_param.embed = 192;
            p.transformer_block_param.block_size = 128;
            p.transformer_block_param.attn_dropout = 0.0;
            p.transformer_block_param.resid_dropout = 0.0;
            Layer<T> layer = Layer<T>.Create(m_cuda, m_log, p, new CancelEvent());

            try
            {
                string strModel = "gpt-mini-blk";

                m_log.CHECK(layer.type == LayerParameter.LayerType.TRANSFORMER_BLOCK, "The layer type is incorrect!");

                Tuple<List<int>, float[]> x = Fill(strModel, "1_x", m_log, p.transformer_block_param);
                m_blob_bottom.Reshape(x.Item1);
                m_blob_bottom.mutable_cpu_data = convert(x.Item2);

                Tuple<List<int>, float[]> y = Fill(strModel, "10_y", m_log, p.transformer_block_param);
                m_blobY.Reshape(y.Item1);
                m_blobY.mutable_cpu_data = convert(y.Item2);

                Tuple<List<int>, float[]> attnBias = Fill(strModel, "attn_bias", m_log, p.transformer_block_param);
                Tuple<List<int>, float[]> attnWt = Fill(strModel, "attn_weight", m_log, p.transformer_block_param);
                Tuple<List<int>, float[]> attnProjBias = Fill(strModel, "attn_proj_bias", m_log, p.transformer_block_param);
                Tuple<List<int>, float[]> attnProjWt = Fill(strModel, "attn_proj_weight", m_log, p.transformer_block_param);

                Tuple<List<int>, float[]> fcBias = Fill(strModel, "fc_bias", m_log, p.transformer_block_param);
                Tuple<List<int>, float[]> fcWt = Fill(strModel, "fc_weight", m_log, p.transformer_block_param);
                Tuple<List<int>, float[]> projBias = Fill(strModel, "proj_bias", m_log, p.transformer_block_param);
                Tuple<List<int>, float[]> projWt = Fill(strModel, "proj_weight", m_log, p.transformer_block_param);

                layer.Setup(BottomVec, TopVec);

                layer.blobs[0].mutable_cpu_data = convert(attnWt.Item2);
                layer.blobs[1].mutable_cpu_data = convert(attnBias.Item2);
                layer.blobs[2].mutable_cpu_data = convert(attnProjWt.Item2);
                layer.blobs[3].mutable_cpu_data = convert(attnProjBias.Item2);

                layer.blobs[4].mutable_cpu_data = convert(fcWt.Item2);
                layer.blobs[5].mutable_cpu_data = convert(fcBias.Item2);
                layer.blobs[6].mutable_cpu_data = convert(projWt.Item2);
                layer.blobs[7].mutable_cpu_data = convert(projBias.Item2);

                layer.Forward(BottomVec, TopVec);

                // Now, check values
                float[] rgExpected = convertF(m_blobY.mutable_cpu_data);
                float[] rgActual = convertF(m_blob_top.mutable_cpu_data);

                for (int i = 0; i < rgExpected.Length; i++)
                {
                    float fExpected = rgExpected[i];
                    float fActual = rgActual[i];
                    float fErr = 1e-5f;

                    m_log.EXPECT_NEAR_FLOAT(fExpected, fActual, fErr, "The values are not as expected!");
                }
            }
            finally
            {
                layer.Dispose();
            }
        }
    }
}
