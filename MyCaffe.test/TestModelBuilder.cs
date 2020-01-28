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
using MyCaffe.model;

/// <summary>
/// Testing the Model Builders.
/// </summary> 
namespace MyCaffe.test
{
    [TestClass]
    public class TestModelBuilder
    {
        [TestMethod]
        public void TestSSDPascal_CreateSolver()
        {
            SSDPascalModelBuilderTest test = new SSDPascalModelBuilderTest();

            try
            {
                foreach (IModelBuilderTest t in test.Tests)
                {
                    t.TestCreateSolver();
                }
            }
            finally
            {
                test.Dispose();
            }
        }

        [TestMethod]
        public void TestSSDPascal_CreateDeployModel()
        {
            SSDPascalModelBuilderTest test = new SSDPascalModelBuilderTest();

            try
            {
                foreach (IModelBuilderTest t in test.Tests)
                {
                    t.TestCreateDeployModel();
                }
            }
            finally
            {
                test.Dispose();
            }
        }

        [TestMethod]
        public void TestSSDPascal_CreateTrainingModel()
        {
            SSDPascalModelBuilderTest test = new SSDPascalModelBuilderTest();

            try
            {
                foreach (IModelBuilderTest t in test.Tests)
                {
                    t.TestCreateTrainingModel();
                }
            }
            finally
            {
                test.Dispose();
            }
        }

        [TestMethod]
        public void TestResNet101_CreateSolver()
        {
            ResNetModelBuilderTest test = new ResNetModelBuilderTest(ResNetModelBuilder.MODEL.RESNET101);

            try
            {
                foreach (IModelBuilderTest t in test.Tests)
                {
                    t.TestCreateSolver();
                }
            }
            finally
            {
                test.Dispose();
            }
        }

        [TestMethod]
        public void TestResNet101_CreateDeployModel()
        {
            ResNetModelBuilderTest test = new ResNetModelBuilderTest(ResNetModelBuilder.MODEL.RESNET101);

            try
            {
                foreach (IModelBuilderTest t in test.Tests)
                {
                    t.TestCreateDeployModel();
                }
            }
            finally
            {
                test.Dispose();
            }
        }

        [TestMethod]
        public void TestResNet101_CreateTrainingModel()
        {
            ResNetModelBuilderTest test = new ResNetModelBuilderTest(ResNetModelBuilder.MODEL.RESNET101);

            try
            {
                foreach (IModelBuilderTest t in test.Tests)
                {
                    t.TestCreateTrainingModel();
                }
            }
            finally
            {
                test.Dispose();
            }
        }

        [TestMethod]
        public void TestResNet152_CreateSolver()
        {
            ResNetModelBuilderTest test = new ResNetModelBuilderTest(ResNetModelBuilder.MODEL.RESNET152);

            try
            {
                foreach (IModelBuilderTest t in test.Tests)
                {
                    t.TestCreateSolver();
                }
            }
            finally
            {
                test.Dispose();
            }
        }

        [TestMethod]
        public void TestResNet152_CreateDeployModel()
        {
            ResNetModelBuilderTest test = new ResNetModelBuilderTest(ResNetModelBuilder.MODEL.RESNET152);

            try
            {
                foreach (IModelBuilderTest t in test.Tests)
                {
                    t.TestCreateDeployModel();
                }
            }
            finally
            {
                test.Dispose();
            }
        }

        [TestMethod]
        public void TestResNet152_CreateTrainingModel()
        {
            ResNetModelBuilderTest test = new ResNetModelBuilderTest(ResNetModelBuilder.MODEL.RESNET152);

            try
            {
                foreach (IModelBuilderTest t in test.Tests)
                {
                    t.TestCreateTrainingModel();
                }
            }
            finally
            {
                test.Dispose();
            }
        }


        [TestMethod]
        public void TestResNet101_Siamese_CreateSolver()
        {
            ResNetModelBuilderTest test = new ResNetModelBuilderTest(ResNetModelBuilder.MODEL.RESNET101, true);

            try
            {
                foreach (IModelBuilderTest t in test.Tests)
                {
                    t.TestCreateSolver();
                }
            }
            finally
            {
                test.Dispose();
            }
        }

        [TestMethod]
        public void TestResNet101_Siamese_CreateDeployModel()
        {
            ResNetModelBuilderTest test = new ResNetModelBuilderTest(ResNetModelBuilder.MODEL.RESNET101, true);

            try
            {
                foreach (IModelBuilderTest t in test.Tests)
                {
                    t.TestCreateDeployModel();
                }
            }
            finally
            {
                test.Dispose();
            }
        }

        [TestMethod]
        public void TestResNet101_Siamease_CreateTrainingModel()
        {
            ResNetModelBuilderTest test = new ResNetModelBuilderTest(ResNetModelBuilder.MODEL.RESNET101, true);

            try
            {
                foreach (IModelBuilderTest t in test.Tests)
                {
                    t.TestCreateTrainingModel();
                }
            }
            finally
            {
                test.Dispose();
            }
        }

        [TestMethod]
        public void TestResNet152_Siamese_CreateSolver()
        {
            ResNetModelBuilderTest test = new ResNetModelBuilderTest(ResNetModelBuilder.MODEL.RESNET152, true);

            try
            {
                foreach (IModelBuilderTest t in test.Tests)
                {
                    t.TestCreateSolver();
                }
            }
            finally
            {
                test.Dispose();
            }
        }

        [TestMethod]
        public void TestResNet152_Siamease_CreateDeployModel()
        {
            ResNetModelBuilderTest test = new ResNetModelBuilderTest(ResNetModelBuilder.MODEL.RESNET152, true);

            try
            {
                foreach (IModelBuilderTest t in test.Tests)
                {
                    t.TestCreateDeployModel();
                }
            }
            finally
            {
                test.Dispose();
            }
        }

        [TestMethod]
        public void TestResNet152_Siamese_CreateTrainingModel()
        {
            ResNetModelBuilderTest test = new ResNetModelBuilderTest(ResNetModelBuilder.MODEL.RESNET152, true);

            try
            {
                foreach (IModelBuilderTest t in test.Tests)
                {
                    t.TestCreateTrainingModel();
                }
            }
            finally
            {
                test.Dispose();
            }
        }
    }

    interface IModelBuilderTest : ITest
    {
        void TestCreateSolver();
        void TestCreateTrainingModel();
        void TestCreateDeployModel();
    }

    class ResNetModelBuilderTest : TestBase
    {
        public ResNetModelBuilderTest(ResNetModelBuilder.MODEL model, bool bSiamese = false, EngineParameter.Engine engine = EngineParameter.Engine.DEFAULT)
            : base("ResNet Model Builder Test" + ((bSiamese) ? " SIAMESE" : "") + " " + model.ToString(), TestBase.DEFAULT_DEVICE_ID, engine)
        {
        }

        protected override ITest create(common.DataType dt, string strName, int nDeviceID, EngineParameter.Engine engine)
        {
            if (strName.Contains("SIAMESE"))
            {
                if (dt == common.DataType.DOUBLE)
                    return new ResNetSiameseModelBuilderTest<double>(strName, nDeviceID, engine);
                else
                    return new ResNetSiameseModelBuilderTest<float>(strName, nDeviceID, engine);
            }
            else
            {
                if (dt == common.DataType.DOUBLE)
                    return new ResNetModelBuilderTest<double>(strName, nDeviceID, engine);
                else
                    return new ResNetModelBuilderTest<float>(strName, nDeviceID, engine);
            }
        }
    }

    class ResNetSiameseModelBuilderTest<T> : ModelBuilderTest<T>
    {
        ResNetModelBuilder.MODEL m_model;

        public ResNetSiameseModelBuilderTest(string strName, int nDeviceID, EngineParameter.Engine engine)
            : base(strName, nDeviceID, engine)
        {
            if (strName.Contains(ResNetModelBuilder.MODEL.RESNET101.ToString()))
                m_model = ResNetModelBuilder.MODEL.RESNET101;
            else
                m_model = ResNetModelBuilder.MODEL.RESNET152;
        }

        protected override ModelBuilder create()
        {
            List<Tuple<int, bool>> rgIP = new List<Tuple<int, bool>>();
            rgIP.Add(new Tuple<int, bool>(1024, false));
            rgIP.Add(new Tuple<int, bool>(512, true));
            rgIP.Add(new Tuple<int, bool>(10, false));
            int nBatch = (m_model == ResNetModelBuilder.MODEL.RESNET101) ? 16 : 12;
            return new ResNetModelBuilder(m_strBaseDir, "CIFAR-10", 3, true, rgIP, true, false, m_model, nBatch, nBatch);
        }
    }

    class ResNetModelBuilderTest<T> : ModelBuilderTest<T>
    {
        ResNetModelBuilder.MODEL m_model;

        public ResNetModelBuilderTest(string strName, int nDeviceID, EngineParameter.Engine engine)
            : base(strName, nDeviceID, engine)
        {
            if (strName.Contains(ResNetModelBuilder.MODEL.RESNET101.ToString()))
                m_model = ResNetModelBuilder.MODEL.RESNET101;
            else
                m_model = ResNetModelBuilder.MODEL.RESNET152;
        }

        protected override ModelBuilder create()
        {
            List<Tuple<int, bool>> rgIP = new List<Tuple<int, bool>>();
            rgIP.Add(new Tuple<int, bool>(10, false));
            return new ResNetModelBuilder(m_strBaseDir, "CIFAR-10", 3, false, rgIP, true, false, m_model);
        }
    }

    class SSDPascalModelBuilderTest : TestBase
    {
        public SSDPascalModelBuilderTest(EngineParameter.Engine engine = EngineParameter.Engine.DEFAULT)
            : base("SSD Pascal Model Builder Test", TestBase.DEFAULT_DEVICE_ID, engine)
        {
        }

        protected override ITest create(common.DataType dt, string strName, int nDeviceID, EngineParameter.Engine engine)
        {
            if (dt == common.DataType.DOUBLE)
                return new SSDModelBuilderTest<double>(strName, nDeviceID, engine);
            else
                return new SSDModelBuilderTest<float>(strName, nDeviceID, engine);
        }
    }

    class SSDModelBuilderTest<T> : ModelBuilderTest<T>
    {
        public SSDModelBuilderTest(string strName, int nDeviceID, EngineParameter.Engine engine)
            : base(strName, nDeviceID, engine)
        {
            m_engine = engine;
            m_strBaseDir = TestBase.GetTestPath("\\MyCaffe\\test_data", true, true);
        }

        protected override ModelBuilder create()
        {
            return new SsdPascalModelBuilder(m_strBaseDir);
        }
    }


    abstract class ModelBuilderTest<T> : TestEx<T>, IModelBuilderTest
    {
        protected string m_strBaseDir;

        public ModelBuilderTest(string strName, int nDeviceID, EngineParameter.Engine engine)
            : base(strName, null, nDeviceID)
        {
            m_engine = engine;
            m_strBaseDir = TestBase.GetTestPath("\\MyCaffe\\test_data", true, true);
        }

        protected override void dispose()
        {
            base.dispose();
        }

        protected abstract ModelBuilder create();

        public void TestCreateSolver()
        {
            ModelBuilder builder = create();

            SolverParameter solverParam = builder.CreateSolver();
            RawProto proto = solverParam.ToProto("root");
            string strSolver = proto.ToString();

            RawProto proto2 = RawProto.Parse(strSolver);
            SolverParameter solverParam2 = SolverParameter.FromProto(proto2);

            m_log.CHECK(solverParam2.Compare(solverParam), "The two solver parameters should be the same!");
        }

        public void TestCreateTrainingModel()
        {
            ModelBuilder builder = create();

            NetParameter net_param = builder.CreateModel();
            RawProto proto = net_param.ToProto("root");
            string strNet = proto.ToString();

            RawProto proto2 = RawProto.Parse(strNet);
            NetParameter net_param2 = NetParameter.FromProto(proto2);

            m_log.CHECK(net_param2.Compare(net_param), "The two net parameters should be the same!");

            // verify creating the model.
            SolverParameter solver = builder.CreateSolver();
            RawProto protoSolver = solver.ToProto("root");
            string strSolver = protoSolver.ToString();

            SettingsCaffe settings = new SettingsCaffe();
            CancelEvent evtCancel = new CancelEvent();
            MyCaffeControl<T> mycaffe = new MyCaffeControl<T>(settings, m_log, evtCancel);

            //            mycaffe.LoadLite(Phase.TRAIN, strSolver, strNet, null);
            mycaffe.Dispose();
        }

        public void TestCreateDeployModel()
        {
            ModelBuilder builder = create();

            NetParameter net_param = builder.CreateDeployModel();
            RawProto proto = net_param.ToProto("root");
            string strNet = proto.ToString();

            RawProto proto2 = RawProto.Parse(strNet);
            NetParameter net_param2 = NetParameter.FromProto(proto2);

            m_log.CHECK(net_param2.Compare(net_param), "The two net parameters should be the same!");

            // verify creating the model.
            SettingsCaffe settings = new SettingsCaffe();
            CancelEvent evtCancel = new CancelEvent();
            MyCaffeControl<T> mycaffe = new MyCaffeControl<T>(settings, m_log, evtCancel);

            //            mycaffe.LoadToRun(strNet, null, new BlobShape(1, 3, 300, 300));
            mycaffe.Dispose();
        }
    }
}
