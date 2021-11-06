﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HDF5DotNet;
using MyCaffe.basecode;
using MyCaffe.common;

namespace MyCaffe.layers.hdf5
{
    /// <summary>
    /// The HDF5 object provides HDF5 dataset support to the HDF5DataLayer.
    /// </summary>
    /// <typeparam name="T">Specifies the base type.</typeparam>
    public class HDF5<T> : IDisposable
    {
        Log m_log;
        CudaDnn<T> m_cuda;
        H5FileId m_file;
        string m_strFile;

        /// <summary>
        /// The constructor.
        /// </summary>
        /// <param name="cuda">Specifies the CudaDnn connection to Cuda.</param>
        /// <param name="log">Specifies the Log for output.</param>
        /// <param name="strFile">Specifies the HDF5 file to load.</param>
        public HDF5(CudaDnn<T> cuda, Log log, string strFile)
        {
            m_strFile = strFile;
            m_cuda = cuda;
            m_log = log;

            m_file = H5F.open(strFile, H5F.OpenMode.ACC_RDONLY);
            if (m_file == null)
                m_log.FAIL("Failed opening HDF5 file: '" + strFile + "'!");
        }

        private H5DataSetId load_nd_datasetEx(Blob<T> blob, string strDatasetName, bool bReshape, int nMinDim = 1, int nMaxDim = int.MaxValue)
        {
            H5DataSetId ds = null;

            try
            {
                ds = H5D.open(m_file, strDatasetName);
                if (ds == null)
                    m_log.FAIL("Failed to find the dataset '" + strDatasetName + "'!");

                // Verify that the number of dimensions are in the accepted range.
                H5DataSpaceId dsSpace = H5D.getSpace(ds);
                if (dsSpace == null)
                    m_log.FAIL("Failed to get the dataset space!");

                int nDims = H5S.getSimpleExtentNDims(dsSpace);
                m_log.CHECK_GE(nDims, nMinDim, "The dataset dim is out of range!");
                m_log.CHECK_LE(nDims, nMaxDim, "The dataset dim is out of range!");

                long[] rgDims = H5S.getSimpleExtentDims(dsSpace);

                // Verify that the data format is what we expect: float or double
                H5DataTypeId dsType = H5D.getType(ds);
                if (dsType == null)
                    m_log.FAIL("Failed to get the dataset type!");

                H5T.H5TClass dataClass = H5T.getClass(dsType);
                switch (dataClass)
                {
                    case H5T.H5TClass.FLOAT:
                        m_log.WriteLine("Datatype class: H5T_FLOAT");
                        break;

                    case H5T.H5TClass.INTEGER:
                        m_log.WriteLine("Datatype class: H5T_INTEGER");
                        break;

                    default:
                        m_log.FAIL("Unsupported datatype class: " + dataClass.ToString());
                        break;
                }

                List<int> rgBlobDims = new List<int>();
                for (int i = 0; i < nDims; i++)
                {
                    rgBlobDims.Add((int)rgDims[i]);
                }

                if (bReshape)
                {
                    blob.Reshape(rgBlobDims);
                }
                else
                {
                    if (!Utility.Compare<int>(rgBlobDims, blob.shape()))
                    {
                        string strSrcShape = Utility.ToString<int>(rgBlobDims);
                        m_log.FAIL("Cannot load blob from  hdf5; shape mismatch.  Source shape = " + strSrcShape + ", target shape = " + blob.shape_string);
                    }
                }
            }
            catch (Exception excpt)
            {
                if (ds != null)
                {
                    H5D.close(ds);
                    ds = null;
                }

                throw excpt;
            }

            return ds;
        }

        /// <summary>
        /// Creates a new dataset from an HDF5 data file.
        /// </summary>
        /// <param name="blob">The input blob is reshaped to the dataset item shape.</param>
        /// <param name="strDatasetName">Specifies the new dataset name.</param>
        /// <param name="bReshape">Specifies whether to reshape the 'blob' parameter.</param>
        /// <param name="nMinDim">Specifies the minimum dimension.</param>
        /// <param name="nMaxDim">Specifies the maximum dimension.</param>
        public void load_nd_dataset(Blob<T> blob, string strDatasetName, bool bReshape = false, int nMinDim = 1, int nMaxDim = int.MaxValue)
        {
            H5DataSetId ds = null;

            try
            {
                ds = load_nd_datasetEx(blob, strDatasetName, bReshape, nMinDim, nMaxDim);

                H5DataTypeId dsType = H5D.getType(ds);
                int nSize = H5T.getSize(dsType);

                if (nSize == sizeof(double))
                {
                    double[] rgBuffer = new double[blob.count()];
                    H5Array<double> rgData = new H5Array<double>(rgBuffer);

                    H5D.read<double>(ds, dsType, rgData);
                    blob.mutable_cpu_data = Utility.ConvertVec<T>(rgBuffer);
                }
                else if (nSize == sizeof(float))
                {
                    float[] rgBuffer = new float[blob.count()];
                    H5Array<float> rgData = new H5Array<float>(rgBuffer);

                    H5D.read<float>(ds, dsType, rgData);
                    blob.mutable_cpu_data = Utility.ConvertVec<T>(rgBuffer);
                }
                else if (nSize == sizeof(byte))
                {
                    byte[] rgBuffer = new byte[blob.count()];
                    H5Array<byte> rgData = new H5Array<byte>(rgBuffer);

                    H5D.read<byte>(ds, dsType, rgData);

                    float[] rgf = rgBuffer.Select(p1 => (float)p1).ToArray();
                    blob.mutable_cpu_data = Utility.ConvertVec<T>(rgf);
                }
                else
                    m_log.FAIL("The dataset size of '" + nSize.ToString() + "' is not supported!");
            }
            catch (Exception excpt)
            {
                m_log.FAIL(excpt.Message);
            }
            finally
            {
                if (ds != null)
                    H5D.close(ds);
            }
        }
      
        /// <summary>
        /// Release all resources uses.
        /// </summary>
        public void Dispose()
        {
            if (m_file != null)
            {
                H5F.close(m_file);
                m_file = null;
            }
        }
    }
}
