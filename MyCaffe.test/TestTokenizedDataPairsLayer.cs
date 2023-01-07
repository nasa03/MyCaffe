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
using MyCaffe.db.image;
using MyCaffe.basecode.descriptors;
using MyCaffe.data;
using MyCaffe.layers.beta;
using MyCaffe.layers.gpt;
using MyCaffe.param.gpt;
using System.IO.Compression;
using System.IO;

/// <summary>
/// Testing the tokenized data layer.
/// 
/// TokenizedDataPairsLayer - layer converts data into tokens for transformer type models.
/// </remarks> 
namespace MyCaffe.test
{
    [TestClass]
    public class TestTokenizedDataPairsLayer
    {
        [TestMethod]
        public void TestForwardEnFrCharacter()
        {            
            TokenizedDataPairsLayerTest test = new TokenizedDataPairsLayerTest();
            
            try
            {
                foreach (ITokenizedDataPairsLayerTest t in test.Tests)
                {
                    t.TestForward(10, 128, TokenizedDataParameter.VOCABULARY_TYPE.CHARACTER);
                }
            }
            finally
            {
                test.Dispose();
            }
        }

        [TestMethod]
        public void TestForwardEnFrWord()
        {
            TokenizedDataPairsLayerTest test = new TokenizedDataPairsLayerTest();

            try
            {
                foreach (ITokenizedDataPairsLayerTest t in test.Tests)
                {
                    t.TestForward(10, 128, TokenizedDataParameter.VOCABULARY_TYPE.WORD);
                }
            }
            finally
            {
                test.Dispose();
            }
        }

        [TestMethod]
        public void TestForwardEnFrSentencePieceSmall()
        {
            TokenizedDataPairsLayerTest test = new TokenizedDataPairsLayerTest();

            try
            {
                foreach (ITokenizedDataPairsLayerTest t in test.Tests)
                {
                    t.TestForward(1, 10, TokenizedDataParameter.VOCABULARY_TYPE.SENTENCEPIECE);
                }
            }
            finally
            {
                test.Dispose();
            }
        }

        [TestMethod]
        public void TestForwardEnFrSentencePieceSmall2()
        {
            TokenizedDataPairsLayerTest test = new TokenizedDataPairsLayerTest();

            try
            {
                foreach (ITokenizedDataPairsLayerTest t in test.Tests)
                {
                    t.TestForward(2, 10, TokenizedDataParameter.VOCABULARY_TYPE.SENTENCEPIECE);
                }
            }
            finally
            {
                test.Dispose();
            }
        }

        [TestMethod]
        public void TestForwardEnFrSentencePiece()
        {
            TokenizedDataPairsLayerTest test = new TokenizedDataPairsLayerTest();

            try
            {
                foreach (ITokenizedDataPairsLayerTest t in test.Tests)
                {
                    t.TestForward(10, 128, TokenizedDataParameter.VOCABULARY_TYPE.SENTENCEPIECE);
                }
            }
            finally
            {
                test.Dispose();
            }
        }
    }

    interface ITokenizedDataPairsLayerTest : ITest
    {
        void TestForward(int nBatchSize, int nBlockSize, TokenizedDataPairsParameter.VOCABULARY_TYPE vocabType);
    }

    class TokenizedDataPairsLayerTest : TestBase
    {
        public TokenizedDataPairsLayerTest(EngineParameter.Engine engine = EngineParameter.Engine.DEFAULT)
            : base("TokenizedDataPairs Layer Test", TestBase.DEFAULT_DEVICE_ID, engine)
        {
        }

        protected override ITest create(common.DataType dt, string strName, int nDeviceID, EngineParameter.Engine engine)
        {
            if (dt == common.DataType.DOUBLE)
                return new TokenizedDataPairsLayerTest<double>(strName, nDeviceID, engine);
            else
                return new TokenizedDataPairsLayerTest<float>(strName, nDeviceID, engine);
        }
    }

    class TokenizedDataPairsLayerTest<T> : TestEx<T>, ITokenizedDataPairsLayerTest
    {
        Blob<T> m_blobEncInput;
        Blob<T> m_blobDecInput;
        Blob<T> m_blobDecOutput;
        Blob<T> m_blobEncMask;
        Blob<T> m_blobDecMask;

        public TokenizedDataPairsLayerTest(string strName, int nDeviceID, EngineParameter.Engine engine)
            : base(strName, null, nDeviceID)
        {
            m_engine = engine;

            m_blobEncInput = new Blob<T>(m_cuda, m_log);
            m_blobDecInput = new Blob<T>(m_cuda, m_log);
            m_blobDecOutput = new Blob<T>(m_cuda, m_log);
            m_blobEncMask = new Blob<T>(m_cuda, m_log);
            m_blobDecMask = new Blob<T>(m_cuda, m_log);
        }

        protected override void dispose()
        {
            dispose(ref m_blobEncInput);
            dispose(ref m_blobDecInput);
            dispose(ref m_blobDecOutput);
            dispose(ref m_blobEncMask);
            dispose(ref m_blobDecMask);

            base.dispose();
        }

        protected override FillerParameter getFillerParam()
        {
            return new FillerParameter("gaussian");
        }

        private Tuple<string, string, string, string> loadDataFiles1()
        {
            string strPath = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) + "\\MyCaffe\\test_data\\data\\text\\encdec";
            string strFileName = "en_fr.zip";
            
            string strTestData = downloadTestData(strPath, strFileName);
            string strTestDataPath = Path.GetDirectoryName(strTestData);

            if (!File.Exists(strTestDataPath + "\\en_fr\\src\\train.txt"))
                ZipFile.ExtractToDirectory(strTestData, strPath);
            
            string strSrcText = strPath + "\\en_fr\\src\\train.txt";
            string strTrgText = strPath + "\\en_fr\\trg\\train.txt";
            string strSrcVocabFile = strPath + "\\en_fr\\sp\\src_sp.vocab";
            string strTrgVocabFile = strPath + "\\en_fr\\sp\\trg_sp.vocab";

            return new Tuple<string, string, string, string>(strSrcText, strTrgText, strSrcVocabFile, strTrgVocabFile);
        }

        public void TestForward(int nBatchSize, int nBlockSize, TokenizedDataPairsParameter.VOCABULARY_TYPE vocabType)
        {
            Tuple<string, string, string, string> dataFiles = loadDataFiles1();
            string strSrcFile = dataFiles.Item1;
            string strTrgFile = dataFiles.Item2;
            string strSrcVocabFile = dataFiles.Item3;
            string strTrgVocabFile = dataFiles.Item4;
            
            LayerParameter p = new LayerParameter(LayerParameter.LayerType.TOKENIZED_DATA_PAIRS);
            p.tokenized_data_pairs_param.batch_size = (uint)nBatchSize;
            p.tokenized_data_pairs_param.block_size = (uint)nBlockSize;
            p.tokenized_data_pairs_param.input_type = TokenizedDataParameter.INPUT_TYPE.TEXT_FILE;
            p.tokenized_data_pairs_param.vocabulary_type = vocabType;
            p.tokenized_data_pairs_param.source_vocab_file = strSrcVocabFile;
            p.tokenized_data_pairs_param.source = strSrcFile;
            p.tokenized_data_pairs_param.target_vocab_file = strTrgVocabFile;
            p.tokenized_data_pairs_param.target = strTrgFile;
            p.tokenized_data_pairs_param.seed = 1701;
            Layer<T> layer = Layer<T>.Create(m_cuda, m_log, p, null);

            try
            {
                BottomVec.Clear();
                TopVec.Clear();

                TopVec.Add(m_blobEncInput);
                TopVec.Add(m_blobDecInput);
                TopVec.Add(m_blobDecOutput);
                TopVec.Add(m_blobEncMask);
                TopVec.Add(m_blobDecMask);

                layer.Setup(BottomVec, TopVec);

                m_log.CHECK_EQ(TopVec.Count, 5, "The top vector should contain 5 blobs.");
                
                // EncInput
                m_log.CHECK_EQ(TopVec[0].num, nBatchSize, "The top[0].num should equal the batch size of " + nBatchSize.ToString());
                m_log.CHECK_EQ(TopVec[0].channels, nBlockSize, "The top[0].channels should equal the block size of " + nBlockSize.ToString());
//                m_log.CHECK_EQ(TopVec[0].height, 1, "The top[0].height should equal 1.");

                // DecInput
                m_log.CHECK_EQ(TopVec[1].num, nBatchSize, "The top[1].num should equal the batch size of " + nBatchSize.ToString());
                m_log.CHECK_EQ(TopVec[1].channels, nBlockSize, "The top[1].channels should equal the block size of " + nBlockSize.ToString());
//                m_log.CHECK_EQ(TopVec[1].height, 1, "The top[1].height should equal 1.");

                // DecTarget
                m_log.CHECK_EQ(TopVec[2].num, nBatchSize, "The top[2].num should equal the batch size of " + nBatchSize.ToString());
                m_log.CHECK_EQ(TopVec[2].channels, nBlockSize, "The top[2].channels should equal the block size of " + nBlockSize.ToString());
//                m_log.CHECK_EQ(TopVec[2].height, 1, "The top[2].height should equal 1.");

                // EncMask
                m_log.CHECK_EQ(TopVec[3].num, nBatchSize, "The top[2].num should equal the batch size of " + nBatchSize.ToString());
                m_log.CHECK_EQ(TopVec[3].channels, nBlockSize, "The top[2].channels should equal the block size of " + nBlockSize.ToString());
                m_log.CHECK_EQ(TopVec[3].height, 1, "The top[2].channels should equal to 1.");

                // DecMask
                m_log.CHECK_EQ(TopVec[4].num, nBatchSize, "The top[4].num should equal the batch size of " + nBatchSize.ToString());
                m_log.CHECK_EQ(TopVec[4].channels, nBlockSize, "The top[4].channels should equal the block size of " + nBlockSize.ToString());
                m_log.CHECK_EQ(TopVec[4].height, nBlockSize, "The top[4].channels should equal the block size of " + nBlockSize.ToString());

                layer.Forward(BottomVec, TopVec);
 
                float[] rgDecInput = convertF(TopVec[1].mutable_cpu_data);
                float[] rgDecOutput = convertF(TopVec[2].mutable_cpu_data);

                for (int i = 0; i < nBatchSize; i++)
                {
                    for (int j = 0; j < nBlockSize - 1; j++)
                    {
                        float fExpected = rgDecInput[i * nBlockSize + j + 1];
                        float fActual = rgDecOutput[i * nBlockSize + j];

                        if (fExpected != 0 && fActual != 2)
                            m_log.CHECK_EQ(fActual, fExpected, "The token in batch " + i.ToString() + " at block " + j.ToString() + " is not correct.");
                    }
                }
                
                float[] rgEncInput = convertF(TopVec[0].mutable_cpu_data);
                rgDecInput = convertF(TopVec[1].mutable_cpu_data);
                rgDecOutput = convertF(TopVec[2].mutable_cpu_data);
                List<Tuple<string, string, string>> rgDataOut = new List<Tuple<string, string, string>>();

                for (int i = 0; i < nBatchSize; i++)
                {
                    string strEncInput = ((TokenizedDataPairsLayer<T>)layer).Detokenize(rgEncInput, i * nBlockSize, nBlockSize, TokenizedDataPairsLayer<T>.VOCABULARY.ENCODER);
                    string strDecInput = ((TokenizedDataPairsLayer<T>)layer).Detokenize(rgDecInput, i * nBlockSize, nBlockSize, TokenizedDataPairsLayer<T>.VOCABULARY.DECODER);
                    string strDecOutput = ((TokenizedDataPairsLayer<T>)layer).Detokenize(rgDecOutput, i * nBlockSize, nBlockSize, TokenizedDataPairsLayer<T>.VOCABULARY.DECODER);

                    rgDataOut.Add(new Tuple<string, string, string>(strEncInput, strDecInput, strDecOutput));
                }

                m_log.EnableTrace = true;
                for (int i = 0; i < rgDataOut.Count; i++)
                {
                    m_log.WriteLine("-------------");
                    m_log.WriteLine("EncInput: " + rgDataOut[i].Item1);
                    m_log.WriteLine("DecInput: " + rgDataOut[i].Item2);
                    m_log.WriteLine("DecOutput: " + rgDataOut[i].Item3);
                }

                m_log.WriteLine("Source Vocabulary Size = " + ((TokenizedDataPairsLayer<T>)layer).GetVocabuarySize(TokenizedDataPairsLayer<T>.VOCABULARY.ENCODER).ToString());
                m_log.WriteLine("Target Vocabulary Size = " + ((TokenizedDataPairsLayer<T>)layer).GetVocabuarySize(TokenizedDataPairsLayer<T>.VOCABULARY.DECODER).ToString());
            }
            finally
            {
                layer.Dispose();
            }
        }

        private string getChar(float f)
        {
            char ch = (char)f;

            if (ch == 0)
                return "";

            if (ch == 1)
                return "<BOS>";
            
            if (ch == 2)
                return "<EOS>";

            string str = "";
            str += ch;

            return str;
        }
    }
}
