﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Diagnostics;
using System.Threading;
using System.IO;
using MyCaffe.basecode;
using System.ComponentModel;

namespace MyCaffe.common
{
    /// <summary>
    /// Defines the mathematical function to run.
    /// </summary>
    public enum MATH_FUNCTION
    {
        /// <summary>
        /// Specifies to run a no operation.
        /// </summary>
        NOP = 0,

        /// <summary>
        /// Specifies to run the acos function.
        /// </summary>
        ACOS = 1,
        /// <summary>
        /// Specifies to run the acosh function.
        /// </summary>
        ACOSH = 2,
        /// <summary>
        /// Specifies to run the cos function.
        /// </summary>
        COS = 3,
        /// <summary>
        /// Specifies to run the cosh function.
        /// </summary>
        COSH = 4,

        /// <summary>
        /// Specifies to run the asin function.
        /// </summary>
        ASIN = 10,
        /// <summary>
        /// Specifies to run the asinh function.
        /// </summary>
        ASINH = 11,
        /// <summary>
        /// Specifies to run the sin function.
        /// </summary>
        SIN = 12,
        /// <summary>
        /// Specifies to run the sinh function.
        /// </summary>
        SINH = 13,

        /// <summary>
        /// Specifies to run the atan function.
        /// </summary>
        ATAN = 20,
        /// <summary>
        /// Specifies to run the atanh function.
        /// </summary>
        ATANH = 21,
        /// <summary>
        /// Specifies to run the tan function.
        /// </summary>
        TAN = 22,
        /// <summary>
        /// Specifies to run the tanh function.
        /// </summary>
        TANH = 23,

        /// <summary>
        /// Specifies to run the ceil function.
        /// </summary>
        CEIL = 30,
        /// <summary>
        /// Specifies to run the floor function.
        /// </summary>
        FLOOR = 31,
        /// <summary>
        /// Specifies to flip the sign of the inputs.
        /// </summary>
        NEG = 32,
        /// <summary>
        /// Specifies to run the sign function.
        /// </summary>
        SIGN = 33,
        /// <summary>
        /// Specifies to run the sqrt function.
        /// </summary>
        SQRT = 34
    }

    /// <summary>
    /// Specifies the distance method used when calculating batch distances.
    /// </summary>
    public enum DistanceMethod
    {
        /// <summary>
        /// Specifies to calculate the hamming distance.
        /// </summary>
        HAMMING = 0,
        /// <summary>
        /// Specifies to calculate the euclidean distance.
        /// </summary>
        EUCLIDEAN = 1
    }

    /// <summary>
    /// Specifies the pooling method used by the cuDnn function SetPoolingDesc.
    /// </summary>
    /// <remarks>
    /// @see CudaDnn::SetPoolingDesc
    /// </remarks>
    public enum PoolingMethod
    {
        /// <summary>
        /// Specifies to use <code>CUDNN_POOLING_MAX</code> in CUDA C++ code.
        /// </summary>
        MAX = 0,        
        /// <summary>
        /// Specifies to use <code>CUDNN_POOLING_AVERAGE_COUNT_INCLUDE_PADDING</code> in CUDA C++ code.
        /// </summary>
        AVE = 1         
    }

    /// <summary>
    /// Specifies the base datatype corresponding the the template type 'T'.  Currently, only <code>double</code> and <code>float</code> types are supported. 
    /// </summary>
    public enum DataType
    {
        /// <summary>
        /// Specifies the double type.
        /// </summary>
        DOUBLE,
        /// <summary>
        /// Specifies the single type.
        /// </summary>
        FLOAT
    }

    /// <summary>
    /// Specifies the initialization flags used when initializing CUDA.
    /// </summary>
    public enum DEVINIT
    {
        /// <summary>
        /// No flag specified.
        /// </summary>
        NONE = 0x0000,

        /// <summary>
        /// Initialize cuBlas.  This should be initialized for cuBlas is used for many of the math operations.
        /// </summary>
        CUBLAS = 0x0001,

        /// <summary>
        /// Initialize cuRand.  This should be initialized for cuRand is used for most of the random operations.
        /// </summary>
        CURAND = 0x0002,

        /// <summary>
        /// Set the cuRand random number generator seed - typically only used when testing to ensure that 
        /// random numbers are generated in a predictable ordering.
        /// </summary>
        SETSEED = 0x0004
    }

    /// <summary>
    /// Specifies the cuDnn batch norm mode to use.
    /// </summary>
    /// <remarks>
    /// @see [NVIDIA cuDnn](https://developer.nvidia.com/cudnn) documenation for more details.
    /// </remarks>
    public enum BATCHNORM_MODE
    {
        /// <summary>
        /// Specifies to use the per-activation batch normalization mode.
        /// </summary>
        PER_ACTIVATION = 0,
        /// <summary>
        /// Specifies to use the spatial batch normalization mode.
        /// </summary>
        SPATIAL = 1,
        /// <summary>
        /// Specifies to use the spatial persistent batch normalization mode.
        /// </summary>
        SPATIAL_PERSISTENT = 2
    }

    /// <summary>
    /// Specifies the cuDnn convolution forward algorithm to use.
    /// </summary>
    /// <remarks>
    /// @see [NVIDIA cuDnn](https://developer.nvidia.com/cudnn) documenation for more details.
    /// </remarks>
    public enum CONV_FWD_ALGO
    {
        /// <summary>
        /// Specifies to not use a forward algorithm.
        /// </summary>
        NONE = -1,
        /// <summary>
        /// Specifies to use the implicit gemm algorithm.
        /// </summary>
        IMPLICIT_GEMM = 0,
        /// <summary>
        /// Specifies to use the implicit pre-computation gemm algorithm.
        /// </summary>
        IMPLICIT_PRECOMP_GEMM = 1,
        /// <summary>
        /// Specifies to use the gemm algorithm.
        /// </summary>
        ALGO_GEMM = 2,
        /// <summary>
        /// Specifies to use the direct algorithm.
        /// </summary>
        ALGO_DIRECT = 3,
        /// <summary>
        /// Specifies to use the fft algorithm.
        /// </summary>
        ALGO_FFT = 4,
        /// <summary>
        /// Specifies to use the fft tiling algorithm.
        /// </summary>
        ALGO_FFT_TILING = 5,
        /// <summary>
        /// Specifies to use the winograd algorithm.
        /// </summary>
        ALGO_WINOGRAD = 6,
        /// <summary>
        /// Specifies to use the non-fused winograd algorithm.
        /// </summary>
        ALGO_WINOGRAD_NONFUSED = 7
    }

    /// <summary>
    /// Specifies the cuDnn convolution backward filter algorithm to use.
    /// </summary>
    /// <remarks>
    /// @see [NVIDIA cuDnn](https://developer.nvidia.com/cudnn) documenation for more details.
    /// </remarks>
    public enum CONV_BWD_FILTER_ALGO
    {
        /// <summary>
        /// Specifies to use algorithm 0 - which is non-deterministic.
        /// </summary>
        ALGO_0 = 0,     
        /// <summary>
        /// Specifies to use algorithm 1.
        /// </summary>
        ALGO_1 = 1,
        /// <summary>
        /// Specifies to use the fft algorithm.
        /// </summary>
        ALGO_FFT = 2,
        /// <summary>
        /// Specifies to use algorithm 0 with a workspace - which is non-deterministic.
        /// </summary>
        ALGO_3 = 3      
    }

    /// <summary>
    /// Specifies the cuDnn convolution backward data algorithm to use.
    /// </summary>
    /// <remarks>
    /// @see [NVIDIA cuDnn](https://developer.nvidia.com/cudnn) documenation for more details.
    /// </remarks>
    public enum CONV_BWD_DATA_ALGO
    {
        /// <summary>
        /// Specifies to use algorithm 0 - which is non-deterministic.
        /// </summary>
        ALGO_0 = 0,     
        /// <summary>
        /// Specifies to use algorithm 1.
        /// </summary>
        ALGO_1 = 1,
        /// <summary>
        /// Specifies to use the fft algorithm.
        /// </summary>
        ALGO_FFT = 2
    }

    /// <summary>
    /// Specifies the pooling method to use when using the Caffe pooling (instead of the pooling from NVIDIA's cuDnn).
    /// </summary>
    /// <remarks>
    /// @see CudaDnn::pooling_fwd
    /// </remarks>
    public enum POOLING_METHOD
    {
        /// <summary>
        /// Select the maximum value from the kernel.
        /// </summary>
        MAX = 0,
        /// <summary>
        /// Select the average of the values in the kernel.
        /// </summary>
        AVE = 1,
        /// <summary>
        /// Select the stochastic value in the kernel - used during a training pass.
        /// </summary>
        STO_TRAIN = 2,
        /// <summary>
        /// Select the stochastic value in the kernel - used during a testing pass.
        /// </summary>
        STO_TEST = 3
    }

    /// <summary>
    /// Specifies the RNN mode to use with the Recurrent Layer when using the cuDNN engine.
    /// </summary>
    public enum RNN_MODE
    {
        /// <summary>
        /// Specifies to use a single RelU gate Recurrent Learning unit.
        /// </summary>
        RNN_RELU = 0,
        /// <summary>
        /// Specifies to use a single TanH gate Recurrent Learning unit.
        /// </summary>
        RNN_TANH = 1,
        /// <summary>
        /// Specifies to use a 4 gate LSTM Recurrent Learning unit.
        /// </summary>
        LSTM = 2,
        /// <summary>
        /// Specifies to use the GRU RNN where
        /// @f$ h' = tanh(r * Uh(t-1) + Wx) @f$ and 
        /// @f$ h = (1 - z) * h' + z * h(t-1) @f$
        /// </summary>
        GRU = 3
    }

    /// <summary>
    /// Specifies the RNN data layout of the data input.
    /// </summary>
    public enum RNN_DATALAYOUT
    {
        /// <summary>
        /// Specifies ordering with sequence major ordering.
        /// </summary>
        RNN_SEQ_MAJOR = 0,
        /// <summary>
        /// Specifies ordering with batch major ordering.
        /// </summary>
        RNN_BATCH_MAJOR = 2
    }

    /// <summary>
    /// Specifies the RNN directional used.
    /// </summary>
    public enum RNN_DIRECTION
    {
        /// <summary>
        /// Specifies a single direction RNN (default)
        /// </summary>
        RNN_UNIDIRECTIONAL,
        /// <summary>
        /// Specifies a bi-direction RNN where the output is concatinated at each layer.
        /// </summary>
        RNN_BIDIRECTIONAL
    }

    /// <summary>
    /// Specifies certain device properties to query from Cuda.
    /// </summary>
    public enum DEVPROP
    {
        /// <summary>
        /// Query the number of devices (gpu's) installed.
        /// </summary>
        DEVICECOUNT = 1,
        /// <summary>
        /// Query the name of a given GPU.
        /// </summary>
        NAME = 2,
        /// <summary>
        /// Query a GPU board group ID.
        /// </summary>
        MULTIGPUBOARDGROUPID = 3,
    }

    /// <summary>
    /// Specifies the memory test to perform.
    /// </summary>
    /// <remarks>
    /// @see CudaDnn::RunMemoryTest
    /// </remarks>
    public enum MEMTEST_TYPE
    {
        /// <summary>
        /// Specifies the mov-inv-8 test.
        /// </summary>
        MOV_INV_8 = 1
    }

    /// <summary>
    /// Specifies the reduction operation to use with 'Nickel' NCCL.
    /// </summary>
    /// <remarks>
    /// @see CudaDnn::NcclAllReduce
    /// </remarks>
    public enum NCCL_REDUCTION_OP
    {
        /// <summary>
        /// Sum the values.
        /// </summary>
        SUM = 0,
        /// <summary>
        /// Multiply the values.
        /// </summary>
        PROD = 1,
        /// <summary>
        /// Return the maximum value.
        /// </summary>
        MAX = 2,
        /// <summary>
        /// Return the minimum value.
        /// </summary>
        MIN = 3
    }

    /// <summary>
    /// Defines the mining type used during SSD cuda training.
    /// </summary>
    /// <remarks>
    /// This enum matches the values of the MultiBoxLossParameter.MiningType with the values supported
    /// in the low level CudaDnnDll.
    /// </remarks>
    public enum SSD_MINING_TYPE
    {
        /// <summary>
        /// Use all negatives.
        /// </summary>
        NONE = 0,
        /// <summary>
        /// Select negatives based on the score.
        /// </summary>
        MAX_NEGATIVE = 1,
        /// <summary>
        /// Select hard examples based on Shrivastava et. al. method.
        /// </summary>
        /// <remarks>
        /// @see [Training Region-based Object Detectors with Online Hard Example Mining](https://arxiv.org/abs/1604.03540) by Abhinav Shrivastava, Abhinav Gupta, Ross Girshick, 2016.
        /// </remarks>
        HARD_EXAMPLE = 2
    }

    /// <summary>
    /// Defines the matching method used during SSD cuda training.
    /// </summary>
    /// <remarks>
    /// This enum matches the values of the MultiBoxLossParameter.MatchType with the values supported
    /// in the low level CudaDnnDll.
    /// </remarks>
    public enum SSD_MATCH_TYPE
    {
        /// <summary>
        /// Specifies to use Bi-Partite.
        /// </summary>
        BIPARTITE,
        /// <summary>
        /// Specifies to use per-prediction matching.
        /// </summary>
        PER_PREDICTION
    }

    /// <summary>
    /// Defines the encode/decode type used during SSD cuda training.
    /// </summary>
    /// <remarks>
    /// This enum matches the values of the PriorBoxParameter.CodeType with the values supported
    /// in the low level CudaDnnDll.
    /// </remarks>
    public enum SSD_CODE_TYPE
    {
        /// <summary>
        /// Encode the corner.
        /// </summary>
        CORNER = 1,
        /// <summary>
        /// Encode the center size.
        /// </summary>
        CENTER_SIZE = 2,
        /// <summary>
        /// Encode the corner size.
        /// </summary>
        CORNER_SIZE = 3
    }

    /// <summary>
    /// Defines the confidence loss types used during SSD cuda training.
    /// </summary>
    /// <remarks>
    /// This enum matches the values of the MultiboxLossParameter.ConfLossType with the values supported
    /// in the low level CudaDnnDll.
    /// </remarks>
    public enum SSD_CONF_LOSS_TYPE
    {
        /// <summary>
        /// Specifies to use softmax.
        /// </summary>
        SOFTMAX,
        /// <summary>
        /// Specifies to use logistic.
        /// </summary>
        LOGISTIC
    }

    /// <summary>
    /// Defines the location loss types used during SSD cuda training.
    /// </summary>
    /// <remarks>
    /// This enum matches the values of the MultiboxLossParameter.LocLossType with the values supported
    /// in the low level CudaDnnDll.
    /// </remarks>
    public enum SSD_LOC_LOSS_TYPE
    {
        /// <summary>
        /// Specifies to use L2 loss.
        /// </summary>
        L2,
        /// <summary>
        /// Specifies to use smooth L1 loss.
        /// </summary>
        SMOOTH_L1
    }

    /// <summary>
    /// Specifies the orientation of a matrix.
    /// </summary>
    /// <remarks>
    /// @see CudaDnn::matrix_add_vector
    /// </remarks>
    public enum ORIENTATION 
    {
        /// <summary>
        /// Specifies to add the vector to each column.
        /// </summary>
        COL = 0,
        /// <summary>
        /// Specifies to add the vector to each row.
        /// </summary>
        ROW = 1
    }

    /// <summary>
    /// Specifies the type of operation to perform along with a matrix transposition.
    /// </summary>
    /// <remarks>
    /// @see CudaDnn::matrix_transpose_operation
    /// </remarks>
    public enum TRANSPOSE_OPERATION 
    {
        /// <summary>
        /// Add the matrix values after transposing.
        /// </summary>
        ADD = 0,
        /// <summary>
        /// Multiply the matrix values after transposing.
        /// </summary>
        MUL = 1,
        /// <summary>
        /// Divide the matrix values after transposing.
        /// </summary>
        DIV = 2
    }

    /// <summary>
    /// Specifies different aggregation operations.
    /// </summary>
    public enum AGGREGATIONS 
    {
        /// <summary>
        /// Sum the values.
        /// </summary>
        SUM = 0,
        /// <summary>
        /// Return the maximum value.
        /// </summary>
        MAX = 1,
        /// <summary>
        /// Return the minimum value.
        /// </summary>
        MIN = 2
    }

#pragma warning disable 1591

    /// <summary>
    /// Specifies the general cuda device interface.
    /// </summary>
    /// <remarks>
    /// This interface is primarily used for testing.
    /// </remarks>
    public interface ICudaDevice /** @private */
    {
        void SetDeviceID(int nDeviceID, DEVINIT flags = DEVINIT.NONE, long? lSeed = null);
        void SetRandomSeed(long lSeed);
        int GetDeviceCount();
        int GetDeviceID();
        void ResetDevice();
        void SynchronizeDevice();
        string GetDeviceName(int nDeviceID);
        string GetDeviceP2PInfo(int nDeviceID);
        string GetRequiredCompute(out int nMinMajor, out int nMinMinor);

    }

    /// <summary>
    /// Specifies the cuda memory operations interface.
    /// </summary>
    /// <remarks>
    /// This interface is primarily used for testing.
    /// </remarks>
    public interface ICudaMemory /** @private */
    {
        long AllocMemory(long lCount, bool bHalf = false);
        long AllocMemory(List<double> rg);
        long AllocMemory(List<float> rg);
        long AllocMemory(double[] rgSrc, long hStream = 0);
        long AllocMemory(float[] rgSrc, long hStream = 0);
        void FreeMemory(long hMem);
        double[] GetMemoryDouble(long hMem, long lCount = -1);
        float[] GetMemoryFloat(long hMem, long lCount = -1);
        void SetMemory(long hMem, List<double> rg);
        void SetMemory(long hMem, List<float> rg);
        void SetMemory(long hMem, double[] rgSrc, long hStream = 0);
        void SetMemory(long hMem, float[] rgSrc, long hStream = 0);
        void SetMemoryAt(long hMem, double[] rgSrc, int nOffset);
        void SetMemoryAt(long hMem, float[] rgSrc, int nOffset);
        long AllocHostBuffer(long lCount);
        void FreeHostBuffer(long hMem);
        double[] GetHostMemoryDouble(long hMem);
        float[] GetHostMemoryFloat(long hMem);
        long CreateMemoryPointer(long hData, long lOffset, long lCount);
        void FreeMemoryPointer(long hMem);
    }

    /// <summary>
    /// Specifies the interface to common cuDnn functionality.
    /// </summary>
    /// <remarks>
    /// This interface is primarily used for testing.
    /// </remarks>
    public interface ICudaCuDnn /** @private */
    {
        long CreateStream(bool bNonBlocking = false, int nIndex = -1);
        void FreeStream(long h); 
        void SynchronizeStream(long h = 0);
        void SynchronizeThread();

        long CreateCuDNN(long hStream = 0);
        void FreeCuDNN(long h);

        long CreateTensorDesc();
        void FreeTensorDesc(long h);
        void SetTensorNdDesc(long hHandle, int[] rgDim, int[] rgStride, bool bHalf = false);
        void SetTensorDesc(long hHandle, int n, int c, int h, int w, bool bHalf = false);
        void SetTensorDesc(long hHandle, int n, int c, int h, int w, int nStride, int cStride, int hStride, int wStride, bool bHalf = false);
        void AddTensor(long hHandle, long hSrcDesc, long hSrc, int nSrcOffset, long hDstDesc, long hDst, int nDstOffset);

        void DeriveBatchNormDesc(long hFwdScaleBiasMeanVarDesc, long hFwdBottomDesc, long hBwdScaleBiasMeanVarDesc, long hBwdBottomDesc, BATCHNORM_MODE mode);

        long CreateFilterDesc();
        void FreeFilterDesc(long h);
        void SetFilterNdDesc(long hHandle, int[] rgDim, bool bHalf = false);
        void SetFilterDesc(long hHandle, int n, int c, int h, int w, bool bHalf = false);

        long CreateConvolutionDesc();
        void FreeConvolutionDesc(long h);
        void SetConvolutionDesc(long hHandle, int hPad, int wPad, int hStride, int wStride, int hDilation, int wDilation, bool bUseTensorCores, bool bHalf = false);

        long CreatePoolingDesc();
        void FreePoolingDesc(long h);
        void SetPoolingDesc(long hHandle, PoolingMethod method, int h, int w, int hPad, int wPad, int hStride, int wStride);
        
        long CreateLRNDesc();
        void FreeLRNDesc(long h);
        void SetLRNDesc(long hHandle, uint nSize, double fAlpha, double fBeta, double fK);

        long CreateRnnDataDesc();
        void FreeRnnDataDesc(long h);
        void SetRnnDataDesc(long hRnnDataDesc, RNN_DATALAYOUT layout, int nMaxSeqLen, int nBatchSize, int nVectorSize, bool bBidirectional = false, int[] rgSeqLen = null);

        long CreateRnnDesc();
        void FreeRnnDesc(long h);
        void SetRnnDesc(long hHandle, long hRnnDesc, int nHiddenSize, int nNumLayers, long hDropoutDesc, RNN_MODE mode, bool bUseTensorCores, RNN_DIRECTION direction = RNN_DIRECTION.RNN_UNIDIRECTIONAL);
        int GetRnnParamCount(long hHandle, long hRnnDesc, long hXDesc);
        ulong GetRnnWorkspaceCount(long hHandle, long hRnnDesc, long hXDesc, out ulong nReservedCount);
        void GetRnnLinLayerParams(long hHandle, long hRnnDesc, int nLayer, long hXDesc, long hWtDesc, long hWtData, int nLinLayer, out int nWtCount, out long hWt, out int nBiasCount, out long hBias);
        void RnnForward(long hHandle, long hRnnDesc, long hXDesc, long hXData, long hHxDesc, long hHxData, long hCxDesc, long hCxData, long hWtDesc, long hWtData, long hYDesc, long hYData, long hHyDesc, long hHyData, long hCyDesc, long hCyData, long hWorkspace, ulong nWsCount, long hReserved, ulong hResCount, bool bTraining);
        void RnnBackwardData(long hHandle, long hRnnDesc, long hYDesc, long hYData, long hYDiff, long hHyDesc, long hHyDiff, long hCyDesc, long hCyDiff, long hWtDesc, long hWtData, long hHxDesc, long hHxData, long hCxDesc, long hCxData, long hXDesc, long hXDiff, long hdHxDesc, long hHxDiff, long hdCxDesc, long hCxDiff, long hWorkspace, ulong nWsCount, long hReserved, ulong nResCount);
        void RnnBackwardWeights(long hHandle, long hRnnDesc, long hXDesc, long hXData, long hHxDesc, long hHxData, long hYDesc, long hYData, long hWorkspace, ulong nWsCount, long hWtDesc, long hWtDiff, long hReserved, ulong nResCount);
    }

    /// <summary>
    /// Specifies the interface to common math functions.
    /// </summary>
    /// <remarks>
    /// This interface is primarily used for testing.
    /// </remarks>
    public interface ICudaMath /** @private */
    {
        void set(int nCount, long hHandle, double fVal, int nIdx = -1);
        void set(int nCount, long hHandle, float fVal, int nIdx = -1);
        double[] get_double(int nCount, long hHandle, int nIdx = -1);
        float[] get_float(int nCount, long hHandle, int nIdx = -1);
        void copy(int nCount, long hSrc, long hDst, int nSrcOffset = 0, int nDstOffset = 0, long hAsyncStream = -1, bool? bSrcHalfOverride = null, bool? bDstHalfOverride = null);
        void copy(int nCount, int nNum, int nDim, long hSrc1, long hSrc2, long hDst, long hSimilar, bool bInvert = false);
        void copy_expand(int n, int nNum, int nDim, long hSrc, long hDs);
        void fill(int n, int nDim, long hSrc, int nSrcOff, int nCount, long hDst);
        void sort(int nCount, long hY);

        void channel_compare(int nCount, int nOuterNum, int nChannels, int nInnerNum, long hX, long hY);
        void channel_fill(int nCount, int nOuterNum, int nChannels, int nInnerNum, long hX, int nLabelDim, long hLabels, long hY);
        void channel_scale(int nCount, int nOuterNum, int nChannels, int nInnerNum, long hX, long hA, long hY);
        void channel_mulv(int nCount, int nOuterNum, int nChannels, int nInnerNum, long hA, long hX, long hC);
        void channel_sum(int nCount, int nOuterNum, int nChannels, int nInnerNum, long hX, long hY);

        void gemm(bool bTransA, bool bTransB, int m, int n, int k, double fAlpha, long hA, long hB, double fBeta, long hC);
        void gemm(bool bTransA, bool bTransB, int m, int n, int k, float fAlpha, long hA, long hB, float fBeta, long hC);
        void gemv(bool bTransA, int m, int n, double fAlpha, long hA, long hX, double fBeta, long hY);
        void gemv(bool bTransA, int m, int n, float fAlpha, long hA, long hX, float fBeta, long hY);
        void geam(bool bTransA, bool bTransB, int m, int n, double fAlpha, long hA, long hB, double fBeta, long hC);
        void geam(bool bTransA, bool bTransB, int m, int n, float fAlpha, long hA, long hB, float fBeta, long hC);

        void ger(int m, int n, double fAlpha, long hX, long hY, long hA);
        void ger(int m, int n, float fAlpha, long hX, long hY, long hA);
        void axpy(int n, double fAlpha, long hX, long hY);
        void axpy(int n, float fAlpha, long hX, long hY);
        void axpby(int n, double fAlpha, long hX, double fBeta, long hY);
        void axpby(int n, float fAlpha, long hX, float fBeta, long hY);
        void scal(int n, double fAlpha, long hX, int nXOff = 0);
        void scal(int n, float fAlpha, long hX, int nXOff = 0);
        double dot_double(int n, long hX, long hY);
        float dot_float(int n, long hX, long hY);
        double asum_double(int n, long hX, int nXOff = 0);
        float asum_float(int n, long hX, int nXOff = 0);
        void scale(int n, double fAlpha, long hX, long hY);
        void scale(int n, float fAlpha, long hX, long hY);
        void add_scalar(int n, double fAlpha, long hY);
        void add_scalar(int n, float fAlpha, long hY);
        void add(int n, long hA, long hB, long hY);
        void add(int n, long hA, long hB, long hY, double dfAlpha);
        void add(int n, long hA, long hB, long hY, float fAlpha);
        void sub(int n, long hA, long hB, long hY, int nAOff = 0, int nBOff = 0, int nYOff = 0, int nB = 0);
        void mul(int n, long hA, long hB, long hY, int nAOff = 0, int nBOff = 0, int nYOff = 0);
        void mul_scalar(int n, double fAlpha, long hY);
        void mul_scalar(int n, float fAlpha, long hY);
        void div(int n, long hA, long hB, long hY);
        void abs(int n, long hA, long hY);
        void exp(int n, long hA, long hY);
        void log(int n, long hA, long hY);
        void powx(int n, long hA, double fAlpha, long hY, int nAOff = 0, int nYOff = 0);
        void powx(int n, long hA, float fAlpha, long hY, int nAOff = 0, int nYOff = 0);
        void sign(int n, long hX, long hY, int nXOff = 0, int nYOff = 0);
        double min(int n, long hA, out long lPos, int nAOff = 0);
        double max(int n, long hA, out long lPos, int nAOff = 0);
        double sumsq(int n, long hW, long hA, int nAOff = 0);
        double sumsqdiff(int n, long hW, long hA, long hB, int nAOff = 0, int nBOff = 0);
        void sqrt(int n, long hA, long hY);
        void sqrt_scale(int n, long hA, long hY);

        void im2col(long hDataIm, int nDataImOffset, int nChannels, int nHeight, int nWidth, int nKernelH, int nKernelW, int nPadH, int nPadW, int nStrideH, int nStrideW, int nDilationH, int nDilationW, long hDataCol, int nDataColOffset);
        void im2col_nd(long hDataIm, int nDataImOffset, int nNumSpatialAxes, int nColCount, int nChannelAxis, long hImShape, long hColShape, long hKernelShape, long hPad, long hStride, long hDilation, long hDataCol, int nDataColOffset);
        void col2im(long hDataCol, int nDataColOffset, int nChannels, int nHeight, int nWidth, int nKernelH, int nKernelW, int nPadH, int nPadW, int nStrideH, int nStrideW, int nDilationH, int nDilationW, long hDataIm, int nDataImOffset);
        void col2im_nd(long hDataCol, int nDataColOffset, int nNumSpatialAxes, int nColCount, int nChannelAxis, long hImShape, long hColShape, long hKernelShape, long hPad, long hStride, long hDilation, long hDataIm, int nDataImOffset);
    }

    /// <summary>
    /// Specifies the interface to common random number generation functions.
    /// </summary>
    /// <remarks>
    /// This interface is primarily used for testing.
    /// </remarks>
    public interface ICudaRandom /** @private */
    {
        void rng_setseed(long lSeed);
        void rng_uniform(int n, double fMin, double fMax, long hY);
        void rng_uniform(int n, float fMin, float fMax, long hY);
        void rng_gaussian(int n, double fMu, double fSigma, long hY);
        void rng_gaussian(int n, float fMu, float fSigma, long hY);
        void rng_bernoulli(int n, double fNonZeroProb, long hY);
        void rng_bernoulli(int n, float fNonZeroProb, long hY);
    }

    /// <summary>
    /// Specifies the combination interface that encompasses all other interfaces.
    /// </summary>
    /// <remarks>
    /// This interface is primarily used for testing.
    /// </remarks>
    public interface ICudaDnn : ICudaDevice, ICudaMemory, ICudaCuDnn, ICudaMath, ICudaRandom /** @private */
    {
    }

#pragma warning restore 1591 


    /// <summary>
    /// The CudaDnn object is the main interface to the Low-Level Cuda C++ DLL.
    /// </summary>
    /// <remarks>
    /// This is the transition location where C# meets C++.
    /// </remarks>
    /// <typeparam name="T">Specifies the base type <i>float</i> or <i>double</i>.  Using <i>float</i> is recommended to conserve GPU memory.</typeparam>
    public class CudaDnn<T> : ICudaDnn, IDisposable
    {
        Params m_param = new Params();
        CudaDnnMemoryTracker<T> m_memTracker;
        int m_nDeviceId;
        string m_strPath = "";
        static int s_nIdxSeed = 0;
        static string s_strCudaPath = "";
        CudaControlLib.ICudaKernel m_cuda;
        long m_hKernel = 0;
        DataType m_dt;
        CryptoRandom m_random = new CryptoRandom();
        T m_tOne;
        T m_tZero;
        int m_nIdx;
        long m_nGhostMemoryIndex = 1000;
        Dictionary<long, T[]> m_rgGhostMemory = null;
        bool m_bGhostMemoryEnabled = false;
        bool m_bOwner = true;
        object m_memSync = new object();
        bool m_bEnableRnnExtendedVersion = false;
        static object m_createSync = new object();

        /// <summary>
        /// Specifies the type of string information to quer from the Cuda C++ layer.
        /// </summary>
        public enum CUDAQRY
        {
            /// <summary>
            /// Query the device (GPU) name.
            /// </summary>
            DEVICE_NAME = 1000,
            /// <summary>
            /// Query the device (GPU) Peer-to-Peer information.  Note, P2P mode is only available when
            /// running a device in TCC mode.  For more information see the [NVIDIA SMI Documentation](http://developer.download.nvidia.com/compute/DCGM/docs/nvidia-smi-367.38.pdf)
            /// </summary>
            DEVICE_P2P_INFO = 1001,
            /// <summary>
            /// Query the device (GPU) general information such as memory and processor usage.
            /// </summary>
            DEVICE_INFO = 1002
        }

#pragma warning disable 1591

        /// <summary>
        /// Specifies the function indexes supported by the Low-Level Cuda Dnn DLL.
        /// </summary>
        /// <remarks><b>IMPORTANT:</b> These index values must match the index values 
        /// specified within the Low-Level Cuda Dnn DLL.</remarks>
        public enum CUDAFN /** @private */
        {
            INITIALIZE = -2,
            CLEANUP = -3,
            KERNEL_MEMCOPY = -4,
            KERNEL_ADD = -5,
            KERNEL_COPY_NCCL = -10,

            SETDEVICE = 1,
            SETRANDOMSEED = 2,
            GETDEVICE = 3,
            RESETDEVICE = 4,
            SYNCHRONIZEDEVICE = 5,
            GETDEVICEPROP = 6,
            CHECKMEMORYATTRIB = 7,
            GETDEVICEMEMORY = 8,
            GETREQUIREDCOMPUTE = 9,

            DEVICE_CANACCESSPEER = 10,
            DEVICE_ENABLEPEERACCESS = 11,
            DEVICE_DISABLEPEERACCESS = 12,

            COPY_DEVICE_TO_HOST = 14,
            COPY_HOST_TO_DEVICE = 15,

            CREATE_MEMORYPOINTER = 16,
            FREE_MEMORYPOINTER = 17,

            ALLOCMEM_HALF = 19,
            ALLOCMEM = 20,
            FREEMEM = 21,
            GETMEM = 22,
            SETMEM = 23,
            SETMEMAT = 24,

            ALLOCHOSTBUFFER = 25,
            FREEHOSTBUFFER = 26,
            GETHOSTMEM = 27,
            SETHOSTMEM = 28,
            GETHOSTBUFFERCAPACITY = 29,

            CREATE_STREAM = 30,
            FREE_STREAM = 31,
            SYNCRHONIZE_STREAM = 32,
            SYNCHRONIZE_THREAD = 33,

            CREATE_MEMTEST = 34,
            FREE_MEMTEST = 35,
            RUN_MEMTEST = 36,

            CREATE_IMAGEOP = 37,
            FREE_IMAGEOP = 38,
            DISTORTIMAGE_IMAGEOP = 39,

            CREATE_NCCL = 40,
            FREE_NCCL = 41,
            NCCL_INIT_SINGLEPROCESS = 42,
            NCCL_INIT_MULTIPROCESS = 43,
            NCCL_BROADCAST = 44,
            NCCL_ALLREDUCE = 45,

            SETPIXEL = 46,

            CREATE_CUDNN = 47,
            FREE_CUDNN = 48,

            CREATE_TENSORDESC = 50,
            FREE_TENSORDESC = 51,
            SET_TENSORDESC = 52,
            ADD_TENSOR = 53,
            SET_TENSORNDDESC = 54,

            CREATE_FILTERDESC = 60,
            FREE_FILTERDESC = 61,
            SET_FILTERDESC = 62,
            SET_FILTERNDDESC = 63,

            CREATE_EXTENSION = 67,
            FREE_EXTENSION = 68,
            EXTENSION_RUN = 69,

            CREATE_CONVDESC = 70,
            FREE_CONVDESC = 71,
            SET_CONVDESC = 72,
            GET_CONVINFO = 73,
            FWD_CONV = 74,
            BWD_CONV_BIAS = 75,
            BWD_CONV_FILTER = 76,
            BWD_CONV_DATA = 77,

            CREATE_POOLDESC = 80,
            FREE_POOLDESC = 81,
            SET_POOLDESC = 82,
            FWD_POOL = 83,
            BWD_POOL = 84,

            DERIVE_BNDESC = 86,
            FWD_BN = 87,
            BWD_BN = 88,

            CREATE_LRNDESC = 90,
            FREE_LRNDESC = 91,
            SET_LRNDESC = 92,

            GET_DROPOUT_INFO = 94,
            CREATE_DROPOUTDESC = 95,
            FREE_DROPOUTDESC = 96,
            SET_DROPOUTDESC = 97,
            FWD_DROPOUT = 98,
            BWD_DROPOUT = 99,

            TANH_FWD = 100,
            TANH_BWD = 101,

            ELU_FWD = 102,
            ELU_BWD = 103,

            SIGMOID_FWD = 104,
            SIGMOID_BWD = 105,

            RELU_FWD = 108,
            RELU_BWD = 109,

            SOFTMAX_FWD = 111,
            SOFTMAX_BWD = 112,

            LRN_CC_FWD = 120,
            LRN_CC_BWD = 121,
            LCN_CC_FWD = 122,
            LCN_CC_BWD = 123,

            CREATE_RNN_DATA_DESC = 130,
            FREE_RNN_DATA_DESC = 131,
            SET_RNN_DATA_DESC = 132,

            CREATE_RNN_DATA_DESCEX = 135,
            FREE_RNN_DATA_DESCEX = 136,
            SET_RNN_DATA_DESCEX = 137,

            CREATE_RNN_DESC = 140,
            FREE_RNN_DESC = 141,
            SET_RNN_DESC = 142,
            GET_RNN_PARAMCOUNT = 143,
            GET_RNN_WORKSPACECOUNT = 144,
            GET_RNN_LINLAYERPARAMS = 145,
            FWD_RNN = 146,
            BWD_RNN_DATA = 147,
            BWD_RNN_WTS = 148,

            CUDA_SET = 200,
            CUDA_GET = 201,
            CUDA_COPY = 202,
            CUDA_COPY_SIM = 203,
            CUDA_COPY_FILL = 204,
            CUDA_SORT = 205,
            CUDA_COPY_BATCH = 206,
            CUDA_COPY_SEQUENCE = 207,
            CUDA_COPY_EXPAND = 208,
            CUDA_COPY_SEQUENCE2 = 209,

            CUDA_GEAM = 218,
            CUDA_GEMM2 = 219,
            CUDA_GEMM = 220,
            CUDA_GEMV = 221,
            CUDA_AXPY = 222,
            CUDA_AXPBY = 223,
            CUDA_SCAL = 224,
            CUDA_DOT = 225,
            CUDA_ASUM = 226,
            CUDA_SCALE = 227,
            CUDA_ADD_SCALAR = 228,
            CUDA_ADD = 229,
            CUDA_SUB = 230,
            CUDA_MUL = 231,
            CUDA_MUL_SCALAR = 232,
            CUDA_DIV = 233,
            CUDA_ABS = 234,
            CUDA_EXP = 235,
            CUDA_LOG = 236,
            CUDA_POWX = 237,
            CUDA_SIGN = 238,
            CUDA_SQRT = 239,
            CUDA_RECIPROCOL = 240,
            CUDA_STUDENT = 241,
            CUDA_LOGISTIC1 = 242,
            CUDA_LOGISTIC2 = 243,
            CUDA_ADD2 = 244,
            CUDA_COMPARE_SIGNS = 245,
            CUDA_MAXVAL = 246,
            CUDA_MINVAL = 247,
            CUDA_SUMSQ = 248,
            CUDA_SUMSQDIFF = 249,
            CUDA_WIDTH = 250,
            CUDA_CONTAINS_POINT = 251,
            CUDA_DENAN = 252,
            CUDA_SUB_AND_DOT = 253,
            CUDA_MINMAXVAL = 254,
            CUDA_SUM = 255,
            CUDA_SQRT_SCALE = 256,
            CUDA_GER = 257,
            CUDA_SET_BOUNDS = 259,
            CUDA_MINMAXVEC = 260,
            CUDA_TRANSPOSE = 261,
            CUDA_SCALE_TO_RANGE = 262,

            CUDA_MULBSX = 270,
            CUDA_DIVBSX = 271,

            CUDA_IM2COL = 280,
            CUDA_IM2COL_ND = 281,
            CUDA_COL2IM = 282,
            CUDA_COL2IM_ND = 283,

            CUDA_ACCURACY_FWD = 286,

            CUDA_CHANNEL_MIN = 289,
            CUDA_CHANNEL_MAX = 290,
            CUDA_CHANNEL_SUB = 291,
            CUDA_CHANNEL_SUM = 292,
            CUDA_CHANNEL_DIV = 293,
            CUDA_CHANNEL_DOT = 294,
            CUDA_CHANNEL_MUL = 295,
            CUDA_CHANNEL_COMPARE = 296,
            CUDA_CHANNEL_FILL = 297,
            CUDA_CHANNEL_SCALE = 298,
            CUDA_CHANNEL_MULV = 299,

            CUDA_RNG_SETSEED = 349,
            CUDA_RNG_UNIFORM = 350,
            CUDA_RNG_GAUSSIAN = 351,
            // CUDA_RNG_BERNOULLI = 352,   // Not implemented yet.

            CUDA_BATCHREIDX_FWD = 386,
            CUDA_BATCHREIDX_BWD = 387,

            CUDA_EMBED_FWD = 390,
            CUDA_EMBED_BWD = 391,

            CUDA_CLIP_FWD = 394,
            CUDA_CLIP_BWD = 395,

            CUDA_POOL_FWD = 400,
            CUDA_POOL_BWD = 401,

            CUDA_UNPOOL_FWD = 410,
            CUDA_UNPOOL_BWD = 411,

            CUDA_TANH_FWD = 420,
            CUDA_TANH_BWD = 421,

            CUDA_MISH_FWD = 422,
            CUDA_MISH_BWD = 423,

            CUDA_SIGMOID_FWD = 424,
            CUDA_SIGMOID_BWD = 425,

            CUDA_SWISH_BWD = 427,

            CUDA_RELU_FWD = 428,
            CUDA_RELU_BWD = 429,

            CUDA_ELU_FWD = 430,
            CUDA_ELU_BWD = 431,

            CUDA_DROPOUT_FWD = 432,
            CUDA_DROPOUT_BWD = 433,

            CUDA_BNLL_FWD = 435,
            CUDA_BNLL_BWD = 436,

            CUDA_PRELU_FWD = 438,
            CUDA_PRELU_BWD = 439,
            CUDA_PRELU_BWD_PARAM = 440,

            CUDA_SOFTMAXLOSS_FWD = 444,
            CUDA_SOFTMAXLOSS_BWD = 445,

            CUDA_MIN_FWD = 446,
            CUDA_MIN_BWD = 447,

            CUDA_MAX_FWD = 448,
            CUDA_MAX_BWD = 449,

            CUDA_CROP_FWD = 450,
            CUDA_CROP_BWD = 451,

            CUDA_CONCAT_FWD = 452,
            CUDA_CONCAT_BWD = 453,

            CUDA_SLICE_FWD = 455,
            CUDA_SLICE_BWD = 456,

            CUDA_TILE_FWD = 457,
            CUDA_TILE_BWD = 458,

            CUDA_BIAS_FWD = 460,

            CUDA_SCALE_FWD = 461,

            CUDA_THRESHOLD_FWD = 462,

            CUDA_CLL_BWD = 463,

            CUDA_LRN_FILLSCALE = 465,
            CUDA_LRN_COMPUTEOUTPUT = 466,
            CUDA_LRN_COMPUTEDIFF = 467,

            CUDA_SMOOTHL1_FWD = 470,
            CUDA_SMOOTHL1_BWD = 471,

            CUDA_PERMUTE = 474,

            CUDA_GATHER_FWD = 476,
            CUDA_GATHER_BWD = 477,

            CUDA_LSTM_FWD = 480,
            CUDA_LSTM_BWD = 481,

            CUDA_LSTM_UNIT_FWD = 482,
            CUDA_LSTM_UNIT_BWD = 483,

            CUDA_MATH_FWD = 487,
            CUDA_MATH_BWD = 488,

            CUDA_COEFF_SUM_FWD = 490,
            CUDA_COEFF_SUM_BWD = 491,

            CUDA_COEFF_SUB_FWD = 492,
            CUDA_COEFF_SUB_BWD = 493,

            CUDA_MAE_LOSS_BWD = 495,

            CUDA_CROSS_ENTROPY_FWD = 496,
            CUDA_CROSS_ENTROPY_IGNORE = 497,

            CUDA_SGD_UPDATE = 500,
            CUDA_NESTEROV_UPDATE = 501,
            CUDA_ADAGRAD_UPDATE = 502,
            CUDA_ADADELTA_UPDATE = 503,
            CUDA_ADAM_UPDATE = 504,
            CUDA_RMSPROP_UPDATE = 505,

            CUDA_COMBINE_DATA = 550,

            CUDA_MTX_SET_DIAGONAL = 700,
            CUDA_MTX_SET_DIAGONAL2 = 701,
            CUDA_MTX_ADD_VECTOR = 702,
            CUDA_MTX_TRANSPOSE_OPERATION = 703,
            CUDA_MTX_AGGREGATE_COLS = 704,
            CUDA_MTX_AGGREGATE_ROWS = 705,
            CUDA_MTX_TRANSPOSE = 706,
            CUDA_MTX_MEANCENTER_BY_COL = 707,
            CUDA_MTX_MEANCENTER_BY_ROW = 708,
            CUDA_MTX_EUCLIDEAN_DIST = 709,
            CUDA_MTX_DOT = 710,
            CUDA_MTX_MEAN = 711,
            CUDA_MTX_STDEV = 712,
            CUDA_MTX_CORRELATIONS = 714,

            CUDA_CREATE_PCA = 800,
            CUDA_RUN_PCA = 801,
            CUDA_FREE_PCA = 802,

            CUDA_TSNE_UPDATE = 850,
            CUDA_TSNE_UPDATE_GRAD = 851,
            CUDA_TSNE_COMPUTE_EXACT_ERROR = 852,
            CUDA_TSNE_COMPUTE_SQUARED_EUCLIDEAN_DISTANCE = 854,
            CUDA_TSNE_COMPUTE_Q_MATRIX = 855,
            CUDA_TSNE_COMPUTE_EXACT_GRADIENT = 856,
            CUDA_TSNE_SYMMETRIZE_MATRIX = 858,
            CUDA_TSNE_COMPUTE_KNN_BOUNDS = 859,

            CUDA_TSNE_CREATE_GAUSSIAN_PERPLEXITY = 870,
            CUDA_TSNE_FREE_GAUSSIAN_PERPLEXITY = 871,
            CUDA_TSNE_FIND_GAUSSIAN_PERPLEXITY = 872,

            CUDA_TSNE_CREATE = 875,
            CUDA_TSNE_FREE = 876,
            CUDA_TSNE_COMPUTE_GRADIENT1 = 877,
            CUDA_TSNE_COMPUTE_ERROR1 = 878,

            CUDA_GUASSIAN_BLUR = 900,
            CUDA_HAMMING_DIFF = 901,
            CUDA_CALC_BATCH_DIST = 902,

            CUDA_CREATE_SSD = 950,
            CUDA_FREE_SSD = 951,
            CUDA_SETUP_SSD = 952,
            CUDA_SSD_FWD_MULTIBOXLOSS = 955,
            CUDA_SSD_ENCODE_LOCPRED = 958,
            CUDA_SSD_ENCODE_CONFPRED = 959,

            CUDA_DEBUG = 1000
        }

#pragma warning restore 1591


        /// <summary>
        /// The CudaDnn constructor.
        /// </summary>
        /// <param name="nDeviceID">Specifies the zero-based device (GPU) id.  Note, if there are 5 GPU's in the system, the device ID's will be numbered 0, 1, 2, 3, 4.</param>
        /// <param name="flags">Specifies the flags under which to initialize the Low-Level Cuda system.</param>
        /// <param name="lSeed">Optionally specifies the random number generator seed.  Typically this is only used during testing.</param>
        /// <param name="strPath">Specifies the file path of the Low-Level Cuda DNN Dll file. When NULL or empty, the Low-Level <code>CudaDNNDll.dll</code> file in the directory of 
        /// the currently executing process (that is using the CudaDnn object) is used.</param>
        /// <param name="bResetFirst">Specifies to reset the device before initialzing.  <b>IMPORTANT:</b> It is only recommended to set this to <code>true</code> when testing.</param>
        /// <param name="bEnableMemoryTrace">Optionally, specifies to enable the memory tracing (only supported in debug mode and dramatically slows down processing).</param>
        public CudaDnn(int nDeviceID, DEVINIT flags = (DEVINIT.CUBLAS | DEVINIT.CURAND), long? lSeed = null, string strPath = "", bool bResetFirst = false, bool bEnableMemoryTrace = false)
        {
            m_memTracker = new CudaDnnMemoryTracker<T>(bEnableMemoryTrace);
            m_nDeviceId = nDeviceID;
            m_nIdx = get_index();

            if (strPath == null || strPath.Length == 0)
                strPath = s_strCudaPath;

            m_strPath = strPath;
            m_dt = (typeof(T) == typeof(double)) ? DataType.DOUBLE : DataType.FLOAT;

            try
            {
                m_cuda = new CudaControlLib.CudaKernel();
            }
            catch (Exception excpt)
            {
                throw new Exception("The CudaControl is not registered! Make sure that you are using the 'x64' build and if so, run 'regsvr32 CudaControl.dll' from a CMD window with Administrative privileges to register.", excpt);
            }

            try
            {
                if (string.IsNullOrEmpty(strPath))
                    strPath = GetCudaDnnDllPath();

                m_strPath = strPath;

                string strDir = System.IO.Path.GetDirectoryName(strPath);
                string strCurDir = Directory.GetCurrentDirectory();
                Directory.SetCurrentDirectory(strDir);

                m_cuda.Load(strPath);

                Directory.SetCurrentDirectory(strCurDir);
            }
            catch (Exception excpt)
            {
                if (excpt.Message != null && excpt.Message.Length > 0)
                    throw excpt;

                throw new Exception("The CudaDnnDll.x.dll at '" + strPath + "' failed to load.  The error code = 0x" + excpt.HResult.ToString("X"));
            }

            try
            {
                lock (m_createSync)
                {
                    if (m_dt == DataType.DOUBLE)
                    {
                        double[] rg = m_cuda.RunDouble(0, (int)CUDAFN.INITIALIZE, m_param.AsDouble(nDeviceID, (int)flags));
                        m_hKernel = (long)rg[0];
                    }
                    else
                    {
                        float[] rg = m_cuda.RunFloat(0, (int)CUDAFN.INITIALIZE, m_param.AsFloat(nDeviceID, (int)flags));
                        m_hKernel = (long)rg[0];
                    }
                }
            }
            catch (Exception excpt)
            {
                if (excpt.Message != null && excpt.Message.Length > 0)
                    throw excpt;

                throw new Exception("CudaDnn failed to initialize.  You may need to reboot or reset the Cuda GPU #" + nDeviceID.ToString() + ".  The error code = 0x" + excpt.HResult.ToString("X"));
            }

            if (bResetFirst)
            {
                ResetDevice();

                lock (m_createSync)
                {
                    if (m_dt == DataType.DOUBLE)
                    {
                        double[] rg = m_cuda.RunDouble(0, (int)CUDAFN.INITIALIZE, m_param.AsDouble(nDeviceID, (int)flags));
                        m_hKernel = (long)rg[0];
                    }
                    else
                    {
                        float[] rg = m_cuda.RunFloat(0, (int)CUDAFN.INITIALIZE, m_param.AsFloat(nDeviceID, (int)flags));
                        m_hKernel = (long)rg[0];
                    }
                }
            }

            if (lSeed.HasValue)
                SetRandomSeed(lSeed.Value);

            m_tOne = (T)Convert.ChangeType(1.0, typeof(T));
            m_tZero = (T)Convert.ChangeType(0.0, typeof(T));
        }

        /// <summary>
        /// Alternate CudaDnn constructor.
        /// </summary>
        /// <param name="cuda">Specifies an already created CudaDn instance.  The internal Cuda Control of this instance is used by the new instance.</param>
        /// <param name="bEnableGhostMemory">Specifies to enable the ghost memory used to estimate GPU memory usage without allocating any GPU memory.</param>
        public CudaDnn(CudaDnn<T> cuda, bool bEnableGhostMemory)
        {
            m_nDeviceId = cuda.m_nDeviceId;
            m_nIdx = get_index();

            m_strPath = cuda.m_strPath;
            m_dt = cuda.m_dt;
            m_cuda = cuda.m_cuda;
            m_hKernel = cuda.m_hKernel;
            m_tOne = cuda.m_tOne;
            m_tZero = cuda.m_tZero;

            if (bEnableGhostMemory)
            {
                m_rgGhostMemory = new Dictionary<long, T[]>();
                m_bGhostMemoryEnabled = true;
            }

            m_bOwner = false;
        }

        /// <summary>
        /// Disposes this instance freeing up all of its host and GPU memory.
        /// </summary>
        /// <param name="bDisposing">When true, specifies that the call is from a Dispose call.</param>
        protected virtual void Dispose(bool bDisposing)
        {
            if (m_bOwner && m_hKernel != 0)
            {
                if (m_dt == DataType.DOUBLE)
                    m_cuda.RunDouble((int)m_hKernel, (int)CUDAFN.CLEANUP, null);
                else
                    m_cuda.RunFloat((int)m_hKernel, (int)CUDAFN.CLEANUP, null);

                m_hKernel = 0;
                m_cuda = null;
            }
        }

        /// <summary>
        /// Disposes this instance freeing up all of its host and GPU memory.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
        }

        /// <summary>
        /// Returns the path to the CudaDnnDll module to use for low level CUDA processing.
        /// </summary>
        /// <returns>The CudaDnnDll path is returned.</returns>
        public static string GetCudaDnnDllPath()
        {
            FileInfo fi = new FileInfo(Process.GetCurrentProcess().MainModule.FileName);

            string strPath = fi.DirectoryName + "\\cuda_11.3\\CudaDnnDll.11.3.dll";
            if (!File.Exists(strPath))
            {
                strPath = fi.DirectoryName + "\\CudaDnnDll.11.3.dll";
                if (!File.Exists(strPath))
                {
                    strPath = fi.DirectoryName + "\\cuda_11.2\\CudaDnnDll.11.2.dll";
                    if (!File.Exists(strPath))
                    {
                        strPath = fi.DirectoryName + "\\CudaDnnDll.11.2.dll";
                        if (!File.Exists(strPath))
                        {
                            strPath = fi.DirectoryName + "\\cuda_11.1\\CudaDnnDll.11.1.dll";
                            if (!File.Exists(strPath))
                            {
                                strPath = fi.DirectoryName + "\\CudaDnnDll.11.1.dll";
                                if (!File.Exists(strPath))
                                {
                                    strPath = fi.DirectoryName + "\\cuda_11.0\\CudaDnnDll.11.0.dll";
                                    if (!File.Exists(strPath))
                                    {
                                        strPath = fi.DirectoryName + "\\CudaDnnDll.11.0.dll";
                                        if (!File.Exists(strPath))
                                        {
                                            strPath = fi.DirectoryName + "\\cuda_10.2\\CudaDnnDll.10.2.dll";
                                            if (!File.Exists(strPath))
                                            {
                                                strPath = fi.DirectoryName + "\\CudaDnnDll.10.2.dll";
                                                if (!File.Exists(strPath))
                                                {
                                                    strPath = fi.DirectoryName + "\\cuda_10.2.3_5\\CudaDnnDll.10.2.dll";
                                                    if (!File.Exists(strPath))
                                                    {
                                                        strPath = fi.DirectoryName + "\\CudaDnnDll.10.2.3_5.dll";
                                                        if (!File.Exists(strPath))
                                                        {
                                                            strPath = fi.DirectoryName + "\\CudaDnnDll.10.1.dll";
                                                            if (!File.Exists(strPath))
                                                            {
                                                                strPath = fi.DirectoryName + "\\CudaDnnDll.10.0.dll";
                                                                if (!File.Exists(strPath))
                                                                {
                                                                    strPath = fi.DirectoryName + "\\CudaDnnDll.9.2.dll";
                                                                    if (!File.Exists(strPath))
                                                                    {
                                                                        strPath = fi.DirectoryName + "\\CudaDnnDll.9.1.dll";
                                                                        if (!File.Exists(strPath))
                                                                        {
                                                                            if (!File.Exists(strPath))
                                                                                strPath = fi.DirectoryName + "\\CudaDnnDll.8.dll";
                                                                        }
                                                                    }
                                                                }
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }

            return strPath;
        }

        /// <summary>
        /// Disables the ghost memory, if enabled.
        /// </summary>
        public void DisableGhostMemory()
        {
            m_bGhostMemoryEnabled = false;
        }

        /// <summary>
        /// Resets the ghost memory by enabling it if this instance was configured to use ghost memory.
        /// </summary>
        public void ResetGhostMemory()
        {
            if (m_rgGhostMemory != null)
                m_bGhostMemoryEnabled = true;
            else
                m_bGhostMemoryEnabled = false;
        }

        /// <summary>
        /// Returns the total amount of GPU memory used by this instance.
        /// </summary>
        public ulong TotalMemoryUsed
        {
            get { return m_memTracker.TotalMemoryUsed; }
        }

        /// <summary>
        /// Returns the total amount of memory used.
        /// </summary>
        public string TotalMemoryUsedAsText
        {
            get { return m_memTracker.TotalMemoryUsedText; }
        }

        /// <summary>
        /// Returns the Low-Level kernel handle used for this instance.  Each Low-Level kernel maintains its own
        /// set of look-up tables for memory, streams, cuDnn constructs, etc.
        /// </summary>
        public long KernelHandle
        {
            get { return m_hKernel; }
        }

        /// <summary>
        /// Copy memory from the look-up tables in one kernel to another.
        /// </summary>
        /// <param name="nCount">Specifies the number of items to copy.</param>
        /// <param name="hSrc">Specifies the handle to the source memory.</param>
        /// <param name="nSrcOffset">Specifies the offset (in items, not bytes) from which to start the copy in the source memory.</param>
        /// <param name="hDstKernel">Specifies the destination kernel holding the look-up table and memory where the data is to be copied.</param>
        /// <param name="hDst">Specifies the handle to the destination memory where the data is to be copied.</param>
        /// <param name="nDstOffset">Specifies the offset (in items, not bytes) where the copy to to be placed within the destination data.</param>
        /// <param name="hHostBuffer">Specifies the handle to the host buffer to be used when transfering the data from one kernel to another.</param>
        /// <param name="hHostKernel">Optionally, specifies the handle to the kernel holding the look-up table for the host buffer.</param>
        /// <param name="hStream">Optionally, specifies the handle to the CUDA stream to use for the transfer.</param>
        /// <param name="hSrcKernel">Optionally, specifies the handle to the source kernel.</param>
        public void KernelCopy(int nCount, long hSrc, int nSrcOffset, long hDstKernel, long hDst, int nDstOffset, long hHostBuffer, long hHostKernel = -1, long hStream = -1, long hSrcKernel = -1)
        {
            if (hSrcKernel == -1)
                hSrcKernel = m_hKernel;

            if (m_dt == DataType.DOUBLE)
                m_cuda.RunDouble((int)hSrcKernel, (int)CUDAFN.KERNEL_MEMCOPY, m_param.AsDouble(nCount, hSrc, nSrcOffset, hDstKernel, hDst, nDstOffset, hHostBuffer, hHostKernel, hStream));
            else
                m_cuda.RunFloat((int)hSrcKernel, (int)CUDAFN.KERNEL_MEMCOPY, m_param.AsFloat(nCount, hSrc, nSrcOffset, hDstKernel, hDst, nDstOffset, hHostBuffer, hHostKernel, hStream));
        }

        /// <summary>
        /// Add memory from one kernel to memory residing on another kernel.
        /// </summary>
        /// <param name="nCount">Specifies the number of items within both A and B.</param>
        /// <param name="hA">Specifies the handle to the memory A.</param>
        /// <param name="hDstKernel">Specifies the kernel where the memory B and the desitnation memory C reside.</param>
        /// <param name="hB">Specifies the handle to the memory B (for which A will be added).</param>
        /// <param name="hC">Specifies the destination data where A+B will be placed.</param>
        public void KernelAdd(int nCount, long hA, long hDstKernel, long hB, long hC)
        {
            if (m_dt == DataType.DOUBLE)
                m_cuda.RunDouble((int)m_hKernel, (int)CUDAFN.KERNEL_ADD, m_param.AsDouble(nCount, hA, hDstKernel, hB, hC));
            else
                m_cuda.RunFloat((int)m_hKernel, (int)CUDAFN.KERNEL_ADD, m_param.AsFloat(nCount, hA, hDstKernel, hB, hC));
        }

        /// <summary>
        /// Copies an Nccl handle from one kernel to the current kernel of the current CudaDnn instance.
        /// </summary>
        /// <remarks>
        /// Nccl handles are created on the main Kernel, but when used must transferred to the destination kernel (running on
        /// a different thread) where the secondary Nccl handle is used.
        /// </remarks>
        /// <param name="hSrcKernel">Specifies the source kernel (typically where the Nccl handle was created).</param>
        /// <param name="hSrcNccl">Specifies the source Nccl handle to be copied.</param>
        /// <returns></returns>
        public long KernelCopyNccl(long hSrcKernel, long hSrcNccl)
        {
            if (m_dt == DataType.DOUBLE)
            {
                double[] rg = m_cuda.RunDouble((int)m_hKernel, (int)CUDAFN.KERNEL_COPY_NCCL, m_param.AsDouble( hSrcKernel, hSrcNccl));
                return (long)rg[0];
            }
            else
            {
                float[] rg = m_cuda.RunFloat((int)m_hKernel, (int)CUDAFN.KERNEL_COPY_NCCL, m_param.AsFloat( hSrcKernel, hSrcNccl));
                return (long)rg[0];
            }
        }

        private static int get_index()
        {
            s_nIdxSeed++;
            return s_nIdxSeed;
        }

        /// <summary>
        /// Used to <i>optionally</i> set the default path to the Low-Level Cuda Dnn DLL file.
        /// </summary>
        /// <param name="strPath">Specifies the file path to the Low-Level Cuda Dnn DLL file to use.</param>
        public static void SetDefaultCudaPath(string strPath)
        {
            s_strCudaPath = strPath;
        }

        /// <summary>
        /// Returns the base type size in bytes.
        /// </summary>
        /// <param name="bUseHalfSize">Specifies whether or not to use half size or the base size.</param>
        public static ulong basetype_size(bool bUseHalfSize)
        {
            if (bUseHalfSize)
                return 2;

            if (typeof(T) == typeof(float))
                return 4;
            else
                return 8;
        }

        private double convertD(T fVal)
        {
            return (double)Convert.ChangeType(fVal, typeof(double));
        }

        private float convertF(T fVal)
        {
            return (float)Convert.ChangeType(fVal, typeof(float));
        }

        /// <summary>
        /// Specifies the file path used to load the Low-Level Cuda DNN Dll file.
        /// </summary>
        public string Path
        {
            get { return m_strPath; }
        }

        /// <summary>
        /// Specifies the default path used t load the Low-Level Cuda DNN Dll file.
        /// </summary>
        public static string DefaultPath
        {
            get { return s_strCudaPath; }
        }

#pragma warning disable 1591

        public void CombineData(int nCount, long hOriginal, long hUpdated, double dfUpdatedPct, long hServer, double dfServerPct, long hNewData) /** @private */
        {
            if (m_dt == DataType.DOUBLE)
                m_cuda.RunDouble((int)m_hKernel, (int)CUDAFN.CUDA_COMBINE_DATA, m_param.AsDouble( nCount, hOriginal, hUpdated, dfUpdatedPct, hServer, dfServerPct, hNewData));
            else
                m_cuda.RunFloat((int)m_hKernel, (int)CUDAFN.CUDA_COMBINE_DATA, m_param.AsFloat( nCount, hOriginal, hUpdated, (float)dfUpdatedPct, hServer, (float)dfServerPct, hNewData));
        }

#pragma warning restore 1591


        //---------------------------------------------------------------------
        //  ICudaDevice Methods
        //---------------------------------------------------------------------
        #region ICudaDevice Methods

        /// <summary>
        /// Set the device ID used by the current instance of CudaDnn.
        /// </summary>
        /// <param name="nDeviceID">Specifies the zero-based device (GPU) id.  When -1, the device ID is set to the device ID used to create the instance of CudaDnn.</param>
        /// <param name="flags">Optionally, specifies the initialization flags.</param>
        /// <param name="lSeed">Optionally, specifies the random number generator seed.</param>
        public void SetDeviceID(int nDeviceID = -1, DEVINIT flags = DEVINIT.NONE, long? lSeed = null)
        {
            if (nDeviceID == -1)
                nDeviceID = m_nDeviceId;
            else
                m_nDeviceId = nDeviceID;

            if (m_dt == DataType.DOUBLE)
            {
                if (lSeed.HasValue)
                    m_cuda.RunDouble((int)m_hKernel, (int)CUDAFN.SETDEVICE, m_param.AsDouble(nDeviceID, (int)flags, lSeed.Value));
                else
                    m_cuda.RunDouble((int)m_hKernel, (int)CUDAFN.SETDEVICE, m_param.AsDouble(nDeviceID, (int)flags));
            }
            else
            {
                if (lSeed.HasValue)
                    m_cuda.RunFloat((int)m_hKernel, (int)CUDAFN.SETDEVICE, m_param.AsFloat(nDeviceID, (int)flags, lSeed.Value));
                else
                    m_cuda.RunFloat((int)m_hKernel, (int)CUDAFN.SETDEVICE, m_param.AsFloat(nDeviceID, (int)flags));
            }
        }

        /// <summary>
        /// Set the random number generator seed.
        /// </summary>
        /// <param name="lSeed">Specifies the seed to set.</param>
        public void SetRandomSeed(long lSeed)
        {
            if (m_dt == DataType.DOUBLE)
                m_cuda.RunDouble((int)m_hKernel, (int)CUDAFN.SETRANDOMSEED, m_param.AsDouble(lSeed));
            else
                m_cuda.RunFloat((int)m_hKernel, (int)CUDAFN.SETRANDOMSEED, m_param.AsFloat(lSeed));
        }

        /// <summary>
        /// Returns the original device ID used to create the instance of CudaDnn.
        /// </summary>
        public int OriginalDeviceID
        {
            get { return m_nDeviceId; }
        }

        /// <summary>
        /// Returns the current device id set within Cuda.
        /// </summary>
        /// <returns>The device id.</returns>
        public int GetDeviceID()
        {
            if (m_dt == DataType.DOUBLE)
            {
                double[] rg = m_cuda.RunDouble((int)m_hKernel, (int)CUDAFN.GETDEVICE, null);
                return (int)rg[0];
            }
            else
            {
                float[] rg = m_cuda.RunFloat((int)m_hKernel, (int)CUDAFN.GETDEVICE, null);
                return (int)rg[0];
            }
        }

        /// <summary>
        /// Query the name of a device.
        /// </summary>
        /// <param name="nDeviceID">Specifies the device id.</param>
        /// <returns>The name of the GPU at the device id is returned.</returns>
        public string GetDeviceName(int nDeviceID)
        {
            string[] rgstr = m_cuda.QueryString((int)m_hKernel, (int)CUDAQRY.DEVICE_NAME, new int[] { nDeviceID });
            return rgstr[0];
        }

        /// <summary>
        /// Query the peer-to-peer information of a device.
        /// </summary>
        /// <param name="nDeviceID">Specifies the device id.</param>
        /// <returns>The peer-to-per information of the GPU at the device id is returned.</returns>
        public string GetDeviceP2PInfo(int nDeviceID)
        {
            string[] rgstr = m_cuda.QueryString((int)m_hKernel, (int)CUDAQRY.DEVICE_P2P_INFO, new int[] { nDeviceID });
            return rgstr[0];
        }

        /// <summary>
        /// Query the device information of a device.
        /// </summary>
        /// <param name="nDeviceID">Specifies the device id.</param>
        /// <param name="bVerbose">When true, more detailed information is returned.</param>
        /// <returns></returns>
        public string GetDeviceInfo(int nDeviceID, bool bVerbose = false)
        {
            string[] rgstr = m_cuda.QueryString((int)m_hKernel, (int)CUDAQRY.DEVICE_INFO, new int[] { nDeviceID, (bVerbose) ? 1 : 0 });
            return rgstr[0];
        }

        /// <summary>
        /// Reset the current device.
        /// </summary>
        /// <remarks><b>IMPORTANT:</b> This function will delete all memory and state information on the current device, which may
        /// cause other CudaDnn instances using the same device, to fail.  For that reason, it is recommended to only call
        /// this function when testing.</remarks>
        public void ResetDevice()
        {
            if (m_dt == DataType.DOUBLE)
                m_cuda.RunDouble((int)m_hKernel, (int)CUDAFN.RESETDEVICE, null);
            else
                m_cuda.RunFloat((int)m_hKernel, (int)CUDAFN.RESETDEVICE, null);
        }

        /// <summary>
        /// Synchronize the operations on the current device.
        /// </summary>
        public void SynchronizeDevice()
        {
            if (m_dt == DataType.DOUBLE)
                m_cuda.RunDouble((int)m_hKernel, (int)CUDAFN.SYNCHRONIZEDEVICE, null);
            else
                m_cuda.RunFloat((int)m_hKernel, (int)CUDAFN.SYNCHRONIZEDEVICE, null);
        }

        /// <summary>
        /// Query the mutli-gpu board group id for a device.
        /// </summary>
        /// <param name="nDeviceID">Specifies the device id.</param>
        /// <returns>The mutli-gpu board group id is returned.</returns>
        public int GetMultiGpuBoardGroupID(int nDeviceID)
        {
            if (m_dt == DataType.DOUBLE)
            {
                double[] rg = m_cuda.RunDouble((int)m_hKernel, (int)CUDAFN.GETDEVICEPROP, m_param.AsDouble(nDeviceID, (int)DEVPROP.MULTIGPUBOARDGROUPID));
                return (int)rg[0];
            }
            else
            {
                float[] rg = m_cuda.RunFloat((int)m_hKernel, (int)CUDAFN.GETDEVICEPROP, m_param.AsFloat(nDeviceID, (int)DEVPROP.MULTIGPUBOARDGROUPID));
                return (int)rg[0];
            }
        }

        /// <summary>
        /// Query the number of devices (gpu's) installed.
        /// </summary>
        /// <returns>The number of GPU's is returned.</returns>
        public int GetDeviceCount()
        {
            try
            {
                if (m_dt == DataType.DOUBLE)
                {
                    double[] rg = m_cuda.RunDouble((int)m_hKernel, (int)CUDAFN.GETDEVICEPROP, m_param.AsDouble(0, (int)DEVPROP.DEVICECOUNT));
                    return (int)rg[0];
                }
                else
                {
                    float[] rg = m_cuda.RunFloat((int)m_hKernel, (int)CUDAFN.GETDEVICEPROP, m_param.AsFloat(0, (int)DEVPROP.DEVICECOUNT));
                    return (int)rg[0];
                }
            }
            catch (Exception)
            {
                return 0;
            }
        }

        /// <summary>
        /// Check the memory attributes of two memory blocks on different devices to see if they are compatible
        /// for peer-to-peer memory transfers.
        /// </summary>
        /// <param name="hSrc">Specifies the handle to the source memory.</param>
        /// <param name="nSrcDeviceID">Specifies the device id where the source memory resides.</param>
        /// <param name="hDst">Specifies the handle to the destination memory.</param>
        /// <param name="nDstDeviceID">Specifies the device id where the destination memory resides.</param>
        /// <returns>This function returns <code>true</code> when both devices support peer-to-peer communcation, <code>false</code> otherwise.</returns>
        public bool CheckMemoryAttributes(long hSrc, int nSrcDeviceID, long hDst, int nDstDeviceID)
        {
            if (m_dt == DataType.DOUBLE)
            {
                double[] rg = m_cuda.RunDouble((int)m_hKernel, (int)CUDAFN.CHECKMEMORYATTRIB, m_param.AsDouble(hSrc, nSrcDeviceID, hDst, nDstDeviceID));
                return (rg[0] == 0) ? false : true;
            }
            else
            {
                float[] rg = m_cuda.RunFloat((int)m_hKernel, (int)CUDAFN.CHECKMEMORYATTRIB, m_param.AsFloat(hSrc, nSrcDeviceID, hDst, nDstDeviceID));
                return (rg[0] == 0) ? false : true;
            }
        }

        /// <summary>
        /// Queries the amount of total, free and used memory on a given GPU.
        /// </summary>
        /// <param name="dfFree">Specifies the amount of free memory in GB.</param>
        /// <param name="dfUsed">Specifies the amount of used memory in GB.</param>
        /// <param name="bCudaCallUsed">Specifies whether or not the used memory is an estimate calculated using the Low-Level Cuda DNN Dll handle table.</param>
        /// <param name="nDeviceID">Specifies the specific device id to query, or if -1, uses calculates an estimate of the memory used using the current low-level Cuda DNN Dll handle table.</param>
        /// <returns>The device's total amount of memory in GB is returned.</returns>
        public double GetDeviceMemory(out double dfFree, out double dfUsed, out bool bCudaCallUsed, int nDeviceID = -1)
        {
            if (nDeviceID == -1)
                nDeviceID = m_nDeviceId;

            if (m_dt == DataType.DOUBLE)
            {
                double[] rg = m_cuda.RunDouble((int)m_hKernel, (int)CUDAFN.GETDEVICEMEMORY, m_param.AsDouble(nDeviceID));
                dfFree = rg[1];
                dfUsed = rg[2];
                bCudaCallUsed = (rg[3] == 0) ? false : true;
                return rg[0];
            }
            else
            {
                float[] rg = m_cuda.RunFloat((int)m_hKernel, (int)CUDAFN.GETDEVICEMEMORY, m_param.AsFloat(nDeviceID));
                dfFree = (double)rg[1];
                dfUsed = (double)rg[2];
                bCudaCallUsed = (rg[3] == 0) ? false : true;
                return (double)rg[0];
            }
        }

        /// <summary>
        /// The GetRequiredCompute function returns the Major and Minor compute values required by the current CudaDNN DLL used.
        /// </summary>
        /// <param name="nMinMajor">Specifies the minimum required major compute value.</param>
        /// <param name="nMinMinor">Specifies the minimum required minor compute value.</param>
        /// <remarks>
        /// Together the Major.Minor compute values define the minimum required compute for the CudaDNN DLL used.
        /// </remarks>
        /// <returns>
        /// The path to the CudaDNN dll in use is returned.
        /// </returns>
        public string GetRequiredCompute(out int nMinMajor, out int nMinMinor)
        {
            if (m_dt == DataType.DOUBLE)
            {
                double[] rg = m_cuda.RunDouble((int)m_hKernel, (int)CUDAFN.GETREQUIREDCOMPUTE, null);
                nMinMajor = (int)rg[0];
                nMinMinor = (int)rg[1];
            }
            else
            {
                float[] rg = m_cuda.RunFloat((int)m_hKernel, (int)CUDAFN.GETREQUIREDCOMPUTE, null);
                nMinMajor = (int)rg[0];
                nMinMinor = (int)rg[1];
            }

            return m_strPath;
        }

        /// <summary>
        /// Query whether or not two devices can access each other via peer-to-peer memory copies.
        /// </summary>
        /// <param name="nSrcDeviceID">Specifies the device id of the source.</param>
        /// <param name="nPeerDeviceID">Specifies the device id of the peer to the source device.</param>
        /// <returns><code>true</code> is returned if the source device can access the peer device via peer-to-peer communcation, <code>false</code> otherwise.</returns>
        public bool DeviceCanAccessPeer(int nSrcDeviceID, int nPeerDeviceID)
        {
            if (m_dt == DataType.DOUBLE)
            {
                double[] rg = m_cuda.RunDouble((int)m_hKernel, (int)CUDAFN.DEVICE_CANACCESSPEER, m_param.AsDouble(nSrcDeviceID, nPeerDeviceID));
                return (rg[0] == 0) ? false : true;
            }
            else
            {
                float[] rg = m_cuda.RunFloat((int)m_hKernel, (int)CUDAFN.DEVICE_CANACCESSPEER, m_param.AsFloat(nSrcDeviceID, nPeerDeviceID));
                return (rg[0] == 0) ? false : true;
            }
        }

        /// <summary>
        /// Enables peer-to-peer access between the current device used by the CudaDnn instance and a peer device.
        /// </summary>
        /// <param name="nPeerDeviceID">Specifies the device id of the peer device.</param>
        public void DeviceEnablePeerAccess(int nPeerDeviceID)
        {
            if (m_dt == DataType.DOUBLE)
                m_cuda.RunDouble((int)m_hKernel, (int)CUDAFN.DEVICE_ENABLEPEERACCESS, m_param.AsDouble(nPeerDeviceID));
            else
                m_cuda.RunFloat((int)m_hKernel, (int)CUDAFN.DEVICE_ENABLEPEERACCESS, m_param.AsFloat(nPeerDeviceID));
        }

        /// <summary>
        /// Disables peer-to-peer access between the current device used by the CudaDnn instance and a peer device.
        /// </summary>
        /// <param name="nPeerDeviceID">Specifies the device id of the peer device.</param>
        public void DeviceDisablePeerAccess(int nPeerDeviceID)
        {
            if (m_dt == DataType.DOUBLE)
                m_cuda.RunDouble((int)m_hKernel, (int)CUDAFN.DEVICE_DISABLEPEERACCESS, m_param.AsDouble(nPeerDeviceID));
            else
                m_cuda.RunFloat((int)m_hKernel, (int)CUDAFN.DEVICE_DISABLEPEERACCESS, m_param.AsFloat(nPeerDeviceID));
        }

        #endregion

        //---------------------------------------------------------------------
        //  ICudaMemory Methods
        //---------------------------------------------------------------------
        #region ICudaMemory Methods

        /// <summary>
        /// Allocate a block of GPU memory and copy a list of doubles to it.
        /// </summary>
        /// <remarks>This function converts the input array into the base type 'T' for which the instance of CudaDnn was defined.</remarks>
        /// <param name="rg">Specifies a list of doubles to copy to the GPU.</param>
        /// <returns>The handle to the GPU memory is returned.</returns>
        public long AllocMemory(List<double> rg)
        {
            return AllocMemory(rg.ToArray());
        }

        /// <summary>
        /// Allocate a block of GPU memory and copy a list of floats to it.
        /// </summary>
        /// <remarks>This function converts the input array into the base type 'T' for which the instance of CudaDnn was defined.</remarks>
        /// <param name="rg">Specifies a list of floats to copy to the GPU.</param>
        /// <returns>The handle to the GPU memory is returned.</returns>
        public long AllocMemory(List<float> rg)
        {
            return AllocMemory(rg.ToArray());
        }

        /// <summary>
        /// Allocate a block of GPU memory and copy an array of doubles to it, optionally using a stream for the copy.
        /// </summary>
        /// <remarks>This function converts the input array into the base type 'T' for which the instance of CudaDnn was defined.</remarks>
        /// <param name="rgSrc">Specifies an array of doubles to copy to the GPU.</param>
        /// <param name="hStream">Optionally specifies a stream to use for the copy.</param>
        /// <returns>The handle to the GPU memory is returned.</returns>
        public long AllocMemory(double[] rgSrc, long hStream = 0)
        {
            return AllocMemory(convert(rgSrc), hStream);
        }

        /// <summary>
        /// Allocate a block of GPU memory and copy an array of float to it, optionally using a stream for the copy.
        /// </summary>
        /// <remarks>This function converts the input array into the base type 'T' for which the instance of CudaDnn was defined.</remarks>
        /// <param name="rgSrc">Specifies an array of float to copy to the GPU.</param>
        /// <param name="hStream">Optionally specifies a stream to use for the copy.</param>
        /// <returns>The handle to the GPU memory is returned.</returns>
        public long AllocMemory(float[] rgSrc, long hStream = 0)
        {
            return AllocMemory(convert(rgSrc), hStream);
        }

        /// <summary>
        /// Allocate a block of GPU memory and copy an array of type 'T' to it, optionally using a stream for the copy.
        /// </summary>
        /// <param name="rgSrc">Specifies an array of 'T' to copy to the GPU.</param>
        /// <param name="hStream">Optionally, specifies a stream to use for the copy.</param>
        /// <param name="bHalfSize">Optionally, specifies to use half size float memory - only available with the 'float' base type.</param>
        /// <returns>The handle to the GPU memory is returned.</returns>
        public long AllocMemory(T[] rgSrc, long hStream = 0, bool bHalfSize = false)
        {
            if (rgSrc == null)
                throw new ArgumentNullException();

            if (rgSrc.Length == 0)
                throw new ArgumentOutOfRangeException();

            try
            {
                if (m_dt == DataType.DOUBLE)
                {
                    if (bHalfSize)
                        throw new Exception("Half sizes are only supported with the 'float' base type.");

                    List<double> rgInput = new List<double>() { rgSrc.Length };

                    if (hStream > 0)
                        rgInput.Add(hStream);

                    rgInput.AddRange(convertD(rgSrc));

                    double[] rg;

                    lock (m_memSync)
                    {
                        if (m_rgGhostMemory == null || !m_bGhostMemoryEnabled)
                        {
                            rg = m_cuda.RunDouble((int)m_hKernel, (int)CUDAFN.ALLOCMEM, rgInput.ToArray());
                        }
                        else
                        {
                            m_nGhostMemoryIndex++;
                            m_rgGhostMemory.Add(m_nGhostMemoryIndex, convert(Utility.Clone<double>(rgInput).ToArray()));
                            rg = new double[] { m_nGhostMemoryIndex };
                        }

                        return m_memTracker.AllocMemory(m_hKernel, m_nDeviceId, (long)rg[0], (ulong)rgInput.Count, bHalfSize);
                    }
                }
                else
                {
                    List<float> rgInput = new List<float>() { rgSrc.Length };

                    if (hStream > 0)
                        rgInput.Add(hStream);

                    rgInput.AddRange(convertF(rgSrc));

                    float[] rg;

                    lock (m_memSync)
                    {
                        if (m_rgGhostMemory == null || !m_bGhostMemoryEnabled)
                        {
                            if (bHalfSize)
                                rg = m_cuda.RunFloat((int)m_hKernel, (int)CUDAFN.ALLOCMEM_HALF, rgInput.ToArray());
                            else
                                rg = m_cuda.RunFloat((int)m_hKernel, (int)CUDAFN.ALLOCMEM, rgInput.ToArray());
                        }
                        else
                        {
                            m_nGhostMemoryIndex++;
                            m_rgGhostMemory.Add(m_nGhostMemoryIndex, convert(Utility.Clone<float>(rgInput).ToArray()));
                            rg = new float[] { m_nGhostMemoryIndex };
                        }

                        return m_memTracker.AllocMemory(m_hKernel, m_nDeviceId, (long)rg[0], (ulong)rgInput.Count, bHalfSize);
                    }
                }
            }
            catch (Exception excpt)
            {
                string strMemory = m_memTracker.TotalMemoryUsedText;
                string strDevice = GetDeviceName(m_nDeviceId);
                throw new Exception("Out of memory!  You are currently using " + strMemory + " of memory on " + strDevice + ".  You may need to use a different GPU that has more memory.", excpt);
            }
        }

        /// <summary>
        /// Allocate a block of GPU memory with a specified capacity.
        /// </summary>
        /// <param name="lCapacity">Specifies the capacity to allocate (in items, not bytes).</param>
        /// <param name="bHalfSize">Optionally, specifies to use half size float memory - only available with the 'float' base type.</param>
        /// <returns>The handle to the GPU memory is returned.</returns>
        public long AllocMemory(long lCapacity, bool bHalfSize = false)
        {
            if (lCapacity <= 0)
                throw new ArgumentOutOfRangeException();

            try
            {
                if (m_dt == DataType.DOUBLE)
                {
                    if (bHalfSize)
                        throw new Exception("Half sizes are only supported with the 'float' base type.");

                    double[] rg = new double[] { lCapacity };

                    lock (m_memSync)
                    {
                        if (m_rgGhostMemory == null || !m_bGhostMemoryEnabled)
                        {
                            rg = m_cuda.RunDouble((int)m_hKernel, (int)CUDAFN.ALLOCMEM, rg);
                        }
                        else
                        {
                            m_nGhostMemoryIndex++;
                            m_rgGhostMemory.Add(m_nGhostMemoryIndex, convert(Utility.Create<double>((int)lCapacity, 0).ToArray()));
                            rg = new double[] { m_nGhostMemoryIndex };
                        }

                        return m_memTracker.AllocMemory(m_hKernel, m_nDeviceId, (long)rg[0], (ulong)lCapacity, bHalfSize);
                    }
                }
                else
                {
                    float[] rg = new float[] { lCapacity };

                    lock (m_memSync)
                    {
                        if (m_rgGhostMemory == null || !m_bGhostMemoryEnabled)
                        {
                            if (bHalfSize)
                                rg = m_cuda.RunFloat((int)m_hKernel, (int)CUDAFN.ALLOCMEM_HALF, rg);
                            else
                                rg = m_cuda.RunFloat((int)m_hKernel, (int)CUDAFN.ALLOCMEM, rg);
                        }
                        else
                        {
                            m_nGhostMemoryIndex++;
                            m_rgGhostMemory.Add(m_nGhostMemoryIndex, convert(Utility.Create<float>((int)lCapacity, 0).ToArray()));
                            rg = new float[] { m_nGhostMemoryIndex };
                        }

                        return m_memTracker.AllocMemory(m_hKernel, m_nDeviceId, (long)rg[0], (ulong)lCapacity, bHalfSize);
                    }
                }
            }
            catch (Exception excpt)
            {
                string strMemory = m_memTracker.TotalMemoryUsedText;
                string strDevice = GetDeviceName(m_nDeviceId);
                long lMb = (lCapacity * (int)basetype_size(false)) / 1000000;

                throw new Exception("Out of memory!  There is not enough memory to allocate the requested " + lMb.ToString("N0") + " MB of memory.  You are currently using " + strMemory + " of memory on " + strDevice + ".  You may need to use a different GPU that has more memory.", excpt);
            }
        }

        /// <summary>
        /// Free previously allocated GPU memory.
        /// </summary>
        /// <param name="hMem">Specifies the handle to the GPU memory.</param>
        public void FreeMemory(long hMem)
        {
            lock (m_memSync)
            {
                if (m_dt == DataType.DOUBLE)
                {
                    m_memTracker.FreeMemory(m_hKernel, m_nDeviceId, hMem);

                    if (m_rgGhostMemory == null || !m_bGhostMemoryEnabled)
                        m_cuda.RunDouble((int)m_hKernel, (int)CUDAFN.FREEMEM, m_param.AsDouble(hMem));
                    else
                        m_rgGhostMemory.Remove(hMem);
                }
                else
                {
                    m_memTracker.FreeMemory(m_hKernel, m_nDeviceId, hMem);

                    if (m_rgGhostMemory == null || !m_bGhostMemoryEnabled)
                        m_cuda.RunFloat((int)m_hKernel, (int)CUDAFN.FREEMEM, m_param.AsFloat(hMem));
                    else
                        m_rgGhostMemory.Remove(hMem);
                }
            }
        }

        /// <summary>
        /// Copy from GPU memory to Host memory.
        /// </summary>
        /// <param name="lCount">Specifies the number of items (of base type each) to copy.</param>
        /// <param name="hGpuSrc">Specifies the GPU memory containing the source data.</param>
        /// <param name="hHostDst">Specifies the Host memory containing the host destination.</param>
        public void CopyDeviceToHost(long lCount, long hGpuSrc, long hHostDst)
        {
            if (m_dt == DataType.DOUBLE)
                m_cuda.RunDouble((int)m_hKernel, (int)CUDAFN.COPY_DEVICE_TO_HOST, m_param.AsDouble(lCount, hGpuSrc, hHostDst));
            else
                m_cuda.RunFloat((int)m_hKernel, (int)CUDAFN.COPY_DEVICE_TO_HOST, m_param.AsFloat(lCount, hGpuSrc, hHostDst));
        }

        /// <summary>
        /// Copy from Host memory to GPU memory.
        /// </summary>
        /// <param name="lCount">Specifies the number of items (of base type each) to copy.</param>
        /// <param name="hHostSrc">Specifies the Host memory containing the host source data.</param>
        /// <param name="hGpuDst">Specifies the GPU memory containing the destination.</param>
        public void CopyHostToDevice(long lCount, long hHostSrc, long hGpuDst)
        {
            if (m_dt == DataType.DOUBLE)
                m_cuda.RunDouble((int)m_hKernel, (int)CUDAFN.COPY_HOST_TO_DEVICE, m_param.AsDouble(lCount, hHostSrc, hGpuDst));
            else
                m_cuda.RunFloat((int)m_hKernel, (int)CUDAFN.COPY_HOST_TO_DEVICE, m_param.AsFloat(lCount, hHostSrc, hGpuDst));
        }

        /// <summary>
        /// Allocate a block of host memory with a specified capacity.
        /// </summary>
        /// <param name="lCapacity">Specifies the capacity to allocate (in items, not bytes).</param>
        /// <returns>The handle to the host memory is returned.</returns>
        public long AllocHostBuffer(long lCapacity)
        {
            if (lCapacity == 0)
                throw new ArgumentOutOfRangeException();

            if (m_dt == DataType.DOUBLE)
            {
                double[] rg = m_cuda.RunDouble((int)m_hKernel, (int)CUDAFN.ALLOCHOSTBUFFER, m_param.AsDouble(lCapacity));
                return (long)rg[0];
            }
            else
            {
                float[] rg = m_cuda.RunFloat((int)m_hKernel, (int)CUDAFN.ALLOCHOSTBUFFER, m_param.AsFloat(lCapacity));
                return (long)rg[0];
            }
        }

        /// <summary>
        /// Free previously allocated host memory.
        /// </summary>
        /// <param name="hMem">Specifies the handle to the host memory.</param>
        public void FreeHostBuffer(long hMem)
        {
            if (m_dt == DataType.DOUBLE)
                m_cuda.RunDouble((int)m_hKernel, (int)CUDAFN.FREEHOSTBUFFER, m_param.AsDouble(hMem));
            else
                m_cuda.RunFloat((int)m_hKernel, (int)CUDAFN.FREEHOSTBUFFER, m_param.AsFloat(hMem));
        }

        /// <summary>
        /// Returns the host memory capacity.
        /// </summary>
        /// <param name="hMem">Specfies the host memory.</param>
        /// <returns>The current host memory capacity is returned.</returns>
        public long GetHostBufferCapacity(long hMem)
        {
            if (m_dt == DataType.DOUBLE)
            {
                double[] rg = m_cuda.RunDouble((int)m_hKernel, (int)CUDAFN.GETHOSTBUFFERCAPACITY, m_param.AsDouble(hMem));
                return (long)rg[0];
            }
            else
            {
                float[] rg = m_cuda.RunFloat((int)m_hKernel, (int)CUDAFN.GETHOSTBUFFERCAPACITY, m_param.AsFloat(hMem));
                return (long)rg[0];
            }
        }

        /// <summary>
        /// Retrieves the host memory as an array of doubles.
        /// </summary>
        /// <remarks>This function converts the output array into the base type 'T' for which the instance of CudaDnn was defined.</remarks>
        /// <param name="hMem">Specifies the handle to the host memory.</param>
        /// <returns>An array of doubles is returned.</returns>
        public double[] GetHostMemoryDouble(long hMem)
        {
            return convertD(GetHostMemory(hMem));
        }

        /// <summary>
        /// Retrieves the host memory as an array of floats.
        /// </summary>
        /// <remarks>This function converts the output array into the base type 'T' for which the instance of CudaDnn was defined.</remarks>
        /// <param name="hMem">Specifies the handle to the host memory.</param>
        /// <returns>An array of floats is returned.</returns>
        public float[] GetHostMemoryFloat(long hMem)
        {
            return convertF(GetHostMemory(hMem));
        }

        /// <summary>
        /// Retrieves the host memory as an array of type 'T'
        /// </summary>
        /// <param name="hMem">Specifies the handle to the host memory.</param>
        /// <returns>An array of type 'T' is returned.</returns>
        public T[] GetHostMemory(long hMem)
        {
            if (m_dt == DataType.DOUBLE)
                return convert(m_cuda.RunDouble((int)m_hKernel, (int)CUDAFN.GETHOSTMEM, m_param.AsDouble(hMem)));
            else
                return convert(m_cuda.RunFloat((int)m_hKernel, (int)CUDAFN.GETHOSTMEM, m_param.AsFloat(hMem)));
        }

        /// <summary>
        /// Retrieves the GPU memory as an array of doubles.
        /// </summary>
        /// <remarks>This function converts the output array into the base type 'T' for which the instance of CudaDnn was defined.</remarks>
        /// <param name="hMem">Specifies the handle to the GPU memory.</param>
        /// <param name="lCount">Optionally, specifies a count of items to retrieve.</param>
        /// <returns>An array of double is returned.</returns>
        public double[] GetMemoryDouble(long hMem, long lCount = -1)
        {
            return convertD(GetMemory(hMem, lCount));
        }

        /// <summary>
        /// Retrieves the GPU memory as an array of float.
        /// </summary>
        /// <remarks>This function converts the output array into the base type 'T' for which the instance of CudaDnn was defined.</remarks>
        /// <param name="hMem">Specifies the handle to the GPU memory.</param>
        /// <param name="lCount">Optionally, specifies a count of items to retrieve.</param>
        /// <returns>An array of float is returned.</returns>
        public float[] GetMemoryFloat(long hMem, long lCount = -1)
        {
            return convertF(GetMemory(hMem, lCount));
        }

        /// <summary>
        /// Retrieves the GPU memory as an array of type 'T'
        /// </summary>
        /// <param name="hMem">Specifies the handle to the GPU memory.</param>
        /// <param name="lCount">Optionally, specifies a count of items to retrieve.</param>
        /// <returns>An array of type 'T' is returned.</returns>
        public T[] GetMemory(long hMem, long lCount = -1)
        {
            if (m_dt == DataType.DOUBLE)
            {
                if (m_rgGhostMemory == null)
                {
                    double[] rgr = m_cuda.RunDouble((int)m_hKernel, (int)CUDAFN.GETMEM, m_param.AsDouble(hMem, lCount));
                    return convert(rgr);
                }
                else
                {
                    return m_rgGhostMemory[hMem];
                }
            }
            else
            {
                if (m_rgGhostMemory == null)
                {
                    float[] rgr = m_cuda.RunFloat((int)m_hKernel, (int)CUDAFN.GETMEM, m_param.AsFloat(hMem, lCount));
                    return convert(rgr);
                }
                else
                {
                    return m_rgGhostMemory[hMem];
                }
            }
        }

        /// <summary>
        /// Copies a list of doubles into a block of already allocated GPU memory.
        /// </summary>
        /// <remarks>This function converts the input array into the base type 'T' for which the instance of CudaDnn was defined.</remarks>
        /// <param name="hMem">Specifies the handle to the GPU memory.</param>
        /// <param name="rg">Specifies the list of doubles to copy.</param>
        public void SetMemory(long hMem, List<double> rg)
        {
            SetMemory(hMem, rg.ToArray());
        }

        /// <summary>
        /// Copies a list of float into a block of already allocated GPU memory.
        /// </summary>
        /// <remarks>This function converts the input array into the base type 'T' for which the instance of CudaDnn was defined.</remarks>
        /// <param name="hMem">Specifies the handle to the GPU memory.</param>
        /// <param name="rg">Specifies the list of float to copy.</param>
        public void SetMemory(long hMem, List<float> rg)
        {
            SetMemory(hMem, rg.ToArray());
        }

        /// <summary>
        /// Copies an array of double into a block of already allocated GPU memory.
        /// </summary>
        /// <remarks>This function converts the input array into the base type 'T' for which the instance of CudaDnn was defined.</remarks>
        /// <param name="hMem">Specifies the handle to the GPU memory.</param>
        /// <param name="rgSrc">Specifies the array of double to copy.</param>
        /// <param name="hStream">Optionally specifies the stream to use for the copy operation.</param>
        public void SetMemory(long hMem, double[] rgSrc, long hStream = 0)
        {
            SetMemory(hMem, convert(rgSrc), hStream);
        }

        /// <summary>
        /// Copies an array of float into a block of already allocated GPU memory.
        /// </summary>
        /// <remarks>This function converts the input array into the base type 'T' for which the instance of CudaDnn was defined.</remarks>
        /// <param name="hMem">Specifies the handle to the GPU memory.</param>
        /// <param name="rgSrc">Specifies the array of float to copy.</param>
        /// <param name="hStream">Optionally specifies the stream to use for the copy operation.</param>
        public void SetMemory(long hMem, float[] rgSrc, long hStream = 0)
        {
            SetMemory(hMem, convert(rgSrc), hStream);
        }

        /// <summary>
        /// Copies an array of type 'T' into a block of already allocated GPU memory.
        /// </summary>
        /// <param name="hMem">Specifies the handle to the GPU memory.</param>
        /// <param name="rgSrc">Specifies the array of type 'T' to copy.</param>
        /// <param name="hStream">Optionally specifies the stream to use for the copy operation.</param>
        /// <param name="nCount">Optionally, specifies a count of items to retrieve.</param>
        public void SetMemory(long hMem, T[] rgSrc, long hStream = 0, int nCount = -1)
        {
            if (nCount == -1)
                nCount = rgSrc.Length;

            if (rgSrc == null || nCount == 0)
                throw new ArgumentOutOfRangeException("There are no data items to set!");

            if (m_hKernel > 0)
            {
                if (m_rgGhostMemory != null)
                {
                    m_rgGhostMemory[hMem] = Utility.Clone<T>(rgSrc);
                }
                else
                {
                    if (m_dt == DataType.DOUBLE)
                    {
                        int nDataCount = 2;

                        if (hStream > 0)
                            nDataCount++;

                        nDataCount += nCount;

                        double[] rg = new double[nDataCount];

                        rg[0] = hMem;
                        rg[1] = nCount;
                        int nIdx = 2;

                        if (hStream > 0)
                        {
                            rg[nIdx] = hStream;
                            nIdx++;
                        }

                        convertD(rgSrc, rg, nIdx, nCount);
                        m_cuda.RunDouble((int)m_hKernel, (int)CUDAFN.SETMEM, rg);
                    }
                    else
                    {
                        int nDataCount = 2;

                        if (hStream > 0)
                            nDataCount++;

                        nDataCount += nCount;

                        float[] rg = new float[nDataCount];

                        rg[0] = hMem;
                        rg[1] = nCount;
                        int nIdx = 2;

                        if (hStream > 0)
                        {
                            rg[nIdx] = hStream;
                            nIdx++;
                        }

                        convertF(rgSrc, rg, nIdx, nCount);
                        m_cuda.RunFloat((int)m_hKernel, (int)CUDAFN.SETMEM, rg);
                    }
                }
            }
        }

        /// <summary>
        /// Copies an array of double into a block of already allocated GPU memory starting at a specific offset.
        /// </summary>
        /// <remarks>This function converts the input array into the base type 'T' for which the instance of CudaDnn was defined.</remarks>
        /// <param name="hMem">Specifies the handle to the GPU memory.</param>
        /// <param name="rgSrc">Specifies the array of double to copy.</param>
        /// <param name="nOffset">Specifies offset within the GPU memory from where the copy is to start.</param>
        public void SetMemoryAt(long hMem, double[] rgSrc, int nOffset)
        {
            SetMemoryAt(hMem, convert(rgSrc), nOffset);
        }

        /// <summary>
        /// Copies an array of float into a block of already allocated GPU memory starting at a specific offset.
        /// </summary>
        /// <remarks>This function converts the input array into the base type 'T' for which the instance of CudaDnn was defined.</remarks>
        /// <param name="hMem">Specifies the handle to the GPU memory.</param>
        /// <param name="rgSrc">Specifies the array of float to copy.</param>
        /// <param name="nOffset">Specifies offset within the GPU memory from where the copy is to start.</param>
        public void SetMemoryAt(long hMem, float[] rgSrc, int nOffset)
        {
            SetMemoryAt(hMem, convert(rgSrc), nOffset);
        }

        /// <summary>
        /// Copies an array of type 'T' into a block of already allocated GPU memory starting at a specific offset.
        /// </summary>
        /// <param name="hMem">Specifies the handle to the GPU memory.</param>
        /// <param name="rgSrc">Specifies the array of type 'T' to copy.</param>
        /// <param name="nOffset">Specifies offset within the GPU memory from where the copy is to start.</param>
        public void SetMemoryAt(long hMem, T[] rgSrc, int nOffset)
        {
            if (rgSrc == null || rgSrc.Length == 0)
                throw new ArgumentOutOfRangeException("There are no data items to set!");

            if (m_hKernel > 0)
            {
                if (m_rgGhostMemory != null)
                    throw new Exception("Ghost memory does not support SetMemoryAt.");

                if (m_dt == DataType.DOUBLE)
                {
                    int nDataCount = 3 + rgSrc.Length;
                    double[] rg = new double[nDataCount];

                    rg[0] = hMem;
                    rg[1] = rgSrc.Length;
                    rg[2] = nOffset;

                    convertD(rgSrc, rg, 3);
                    m_cuda.RunDouble((int)m_hKernel, (int)CUDAFN.SETMEMAT, rg);
                }
                else
                {
                    int nDataCount = 3 + rgSrc.Length;
                    float[] rg = new float[nDataCount];

                    rg[0] = hMem;
                    rg[1] = rgSrc.Length;
                    rg[2] = nOffset;

                    convertF(rgSrc, rg, 3);
                    m_cuda.RunFloat((int)m_hKernel, (int)CUDAFN.SETMEMAT, rg);
                }
            }
        }

        /// <summary>
        /// Set a pixel value where each pixel is defined a set index, value tuple.
        /// </summary>
        /// <param name="hMem">Specifies the memory where the values are set.</param>
        /// <param name="nCount">Specifies the number of allocated items in the memory.</param>
        /// <param name="bReturnOriginal">Specifies whether or not to return the original values (before setting).</param>
        /// <param name="nOffset">Specifies the offset of where the first pixel data starts.</param>
        /// <param name="rgPixel">Specifies the pixel values.</param>
        /// <returns>When 'bReturnOriginal' is True, the original values (before setting) are returned.</returns>
        public T[] SetPixel(long hMem, int nCount, bool bReturnOriginal, int nOffset, params Tuple<int, T>[] rgPixel)
        {
            if (rgPixel.Length == 0)
                throw new Exception("You must specify at least one pixel!");

            if (m_dt == DataType.DOUBLE)
            {
                double[] rg = new double[5 + rgPixel.Length * 2];

                rg[0] = hMem;
                rg[1] = nCount;
                rg[2] = (bReturnOriginal) ? 1 : 0;
                rg[3] = nOffset;
                rg[4] = rgPixel.Length;
                int nIdx = 5;

                for (int i = 0; i < rgPixel.Length; i++)
                {
                    rg[nIdx] = rgPixel[i].Item1;
                    nIdx++;
                    rg[nIdx] = convertD1(rgPixel[i].Item2);
                    nIdx++;
                }

                rg = m_cuda.RunDouble((int)m_hKernel, (int)CUDAFN.SETPIXEL, rg);
                if (rg == null)
                    return null;

                return convert(rg);
            }
            else
            {
                float[] rg = new float[5 + rgPixel.Length * 2];

                rg[0] = hMem;
                rg[1] = nCount;
                rg[2] = (bReturnOriginal) ? 1 : 0;
                rg[3] = nOffset;
                rg[4] = rgPixel.Length;
                int nIdx = 5;

                for (int i = 0; i < rgPixel.Length; i++)
                {
                    rg[nIdx] = rgPixel[i].Item1;
                    nIdx++;
                    rg[nIdx] = convertF1(rgPixel[i].Item2);
                    nIdx++;
                }

                rg = m_cuda.RunFloat((int)m_hKernel, (int)CUDAFN.SETPIXEL, rg);
                if (rg == null)
                    return null;

                return convert(rg);
            }
        }

        /// <summary>
        /// Copies an array of type 'T' into a block of already allocated host memory.
        /// </summary>
        /// <param name="hMem">Specifies the handle to the host memory.</param>
        /// <param name="rgSrc">Specifies the array of type 'T' to copy.</param>
        public void SetHostMemory(long hMem, T[] rgSrc)
        {
            if (m_dt == DataType.DOUBLE)
            {
                int nDataCount = 2 + rgSrc.Length;
                double[] rg = new double[nDataCount];

                rg[0] = hMem;
                rg[1] = rgSrc.Length;

                convertD(rgSrc, rg, 2);
                m_cuda.RunDouble((int)m_hKernel, (int)CUDAFN.SETHOSTMEM, rg);
            }
            else
            {
                int nDataCount = 2 + rgSrc.Length;
                float[] rg = new float[nDataCount];

                rg[0] = hMem;
                rg[1] = rgSrc.Length;

                convertF(rgSrc, rg, 2);
                m_cuda.RunFloat((int)m_hKernel, (int)CUDAFN.SETHOSTMEM, rg);
            }
        }

        /// <summary>
        /// Creates a memory pointer into an already existing block of GPU memory.
        /// </summary>
        /// <param name="hData">Specifies a handle to the GPU memory.</param>
        /// <param name="lOffset">Specifies the offset into the GPU memory (in items, not bytes), where the pointer is to start.</param>
        /// <param name="lCount">Specifies the number of items (not bytes) in the 'virtual' memory block pointed to by the memory pointer.</param>
        /// <returns>A handle to the memory pointer is returned.  Handles to memory poitners can be used like any other handle to GPU memory.</returns>
        public long CreateMemoryPointer(long hData, long lOffset, long lCount)
        {
            if (m_dt == DataType.DOUBLE)
            {
                double[] rg = m_cuda.RunDouble((int)m_hKernel, (int)CUDAFN.CREATE_MEMORYPOINTER, m_param.AsDouble(hData, lOffset, lCount));
                return (long)rg[0];
            }
            else
            {
                float[] rg = m_cuda.RunFloat((int)m_hKernel, (int)CUDAFN.CREATE_MEMORYPOINTER, m_param.AsFloat(hData, lOffset, lCount));
                return (long)rg[0];
            }
        }

        /// <summary>
        /// Frees a memory pointer.
        /// </summary>
        /// <param name="hData">Specifies the handle to the memory pointer.</param>
        public void FreeMemoryPointer(long hData)
        {
            if (m_dt == DataType.DOUBLE)
                m_cuda.RunDouble((int)m_hKernel, (int)CUDAFN.FREE_MEMORYPOINTER, m_param.AsDouble(hData));
            else
                m_cuda.RunFloat((int)m_hKernel, (int)CUDAFN.FREE_MEMORYPOINTER, m_param.AsFloat(hData));
        }

        /// <summary>
        /// Creates a new memory test on the current GPU.
        /// </summary>
        /// <param name="ulTotalNumBlocks">Returns the total number of blocks available to test.</param>
        /// <param name="dfMemAllocatedInGB">Returns the total amount of allocated memory, specified in GB.</param>
        /// <param name="ulMemStartAddr">Returns the start address of the memory test.</param>
        /// <param name="ulBlockSize">Returns the block size of the memory to be tested.</param>
        /// <param name="dfPctToAllocate">Specifies the percentage of avaiable memory to test, where 1.0 = 100%.</param>
        /// <returns>A handle to the memory test is returned.</returns>
        public long CreateMemoryTest(out ulong ulTotalNumBlocks, out double dfMemAllocatedInGB, out ulong ulMemStartAddr, out ulong ulBlockSize, double dfPctToAllocate = 1.0)
        {
            if (m_dt == DataType.DOUBLE)
            {
                double[] rg = m_cuda.RunDouble((int)m_hKernel, (int)CUDAFN.CREATE_MEMTEST, m_param.AsDouble(dfPctToAllocate));
                ulTotalNumBlocks = (ulong)rg[1];
                dfMemAllocatedInGB = (double)rg[2];
                ulMemStartAddr = (ulong)rg[3];
                ulBlockSize = (ulong)rg[4];
                return (long)rg[0];
            }
            else
            {
                float[] rg = m_cuda.RunFloat((int)m_hKernel, (int)CUDAFN.CREATE_MEMTEST, m_param.AsFloat((float)dfPctToAllocate));
                ulTotalNumBlocks = (ulong)rg[1];
                dfMemAllocatedInGB = (double)rg[2];
                ulMemStartAddr = (ulong)rg[3];
                ulBlockSize = (ulong)rg[4];
                return (long)rg[0];
            }
        }

        /// <summary>
        /// Free a memory test, freeing up all GPU memory used.
        /// </summary>
        /// <param name="h">Specifies the handle to the memory test.</param>
        public void FreeMemoryTest(long h)
        {
            if (m_dt == DataType.DOUBLE)
                m_cuda.RunDouble((int)m_hKernel, (int)CUDAFN.FREE_MEMTEST, m_param.AsDouble(h));
            else
                m_cuda.RunFloat((int)m_hKernel, (int)CUDAFN.FREE_MEMTEST, m_param.AsFloat(h));
        }

        /// <summary>
        /// The RunMemoryTest method runs the memory test from the block start offset through the block count on the
        /// memory previously allocated using CreateMemoryTest.
        /// </summary>
        /// <param name="h">Specifies the handle to the memory test data.</param>
        /// <param name="type">Specifies the type of memory test to run.</param>
        /// <param name="ulBlockStartOffset">Specifies the block start offset (offset into the total blocks returned
        /// by CreateMemoryTest).</param>
        /// <param name="ulBlockCount">Specifies the number of blocks to test.</param>
        /// <param name="bVerbose">When disabled, the memory test is just run once and the number of errors is returned.
        /// When eanbled, the memory test is run twice and the erroring adresses are returned along with the error count.</param>
        /// <returns>The format of the array returned is as follows:
        /// rg[0] - specifies the starting memory address used for this memory test run.
        /// rg[1] - specifies the number of addresses over which the test was run (specified in 1 byte increments).
        /// rg[2] - specifies the number of errors found.
        /// rg[3, ...] - specifies the erroring addresses (specified in 1-bit increments)
        /// <param name="bWrite">Specifies to perform a write test.</param>
        /// <param name="bReadWrite">Specifies to perform a read/write test.</param>
        /// <param name="bRead">Specifies to peroform a read test.</param>
        /// </returns>
        public T[] RunMemoryTest(long h, MEMTEST_TYPE type, ulong ulBlockStartOffset, ulong ulBlockCount, bool bVerbose, bool bWrite, bool bReadWrite, bool bRead)
        {
            List<ulong> rgErrorAddresses = new List<ulong>();

            if (m_dt == DataType.DOUBLE)
            {
                double[] rg = m_cuda.RunDouble((int)m_hKernel, (int)CUDAFN.RUN_MEMTEST, m_param.AsDouble(h, (double)type, ulBlockStartOffset, ulBlockCount, (bVerbose) ? 1 : 0, (bWrite) ? 1 : 0, (bReadWrite) ? 1 : 0, (bRead) ? 1 : 0));
                return (T[])Convert.ChangeType(rg, typeof(T[]));
            }
            else
            {
                float[] rg = m_cuda.RunFloat((int)m_hKernel, (int)CUDAFN.RUN_MEMTEST, m_param.AsFloat(h, (float)type, ulBlockStartOffset, ulBlockCount, (bVerbose) ? 1 : 0, (bWrite) ? 1 : 0, (bReadWrite) ? 1 : 0, (bRead) ? 1 : 0));
                return (T[])Convert.ChangeType(rg, typeof(T[]));
            }
        }

        /// <summary>
        /// Create a new ImageOp used to perform image operations on the GPU.
        /// </summary>
        /// <param name="nNum">Specifies the number of items (usually the blob.num).</param>
        /// <param name="dfBrightnessProb">Specifies the brightness probability [0,1].</param>
        /// <param name="dfBrightnessDelta">Specifies the brightness delta.</param>
        /// <param name="dfContrastProb">Specifies the contrast probability [0,1]</param>
        /// <param name="dfContrastLower">Specifies the contrast lower bound value.</param>
        /// <param name="dfContrastUpper">Specifies the contrast upper bound value.</param>
        /// <param name="dfSaturationProb">Specifies the saturation probability [0,1]</param>
        /// <param name="dfSaturationLower">Specifies the saturation lower bound value.</param>
        /// <param name="dfSaturationUpper">Specifies the saturation upper bound value.</param>
        /// <param name="lRandomSeed">Optionally, specifies the random seed or 0 to ignore (default = 0).</param>
        /// <returns>A handle to the ImageOp is returned.</returns>
        public long CreateImageOp(int nNum, double dfBrightnessProb, double dfBrightnessDelta, double dfContrastProb, double dfContrastLower, double dfContrastUpper, double dfSaturationProb, double dfSaturationLower, double dfSaturationUpper, long lRandomSeed = 0)
        {
            if (m_dt == DataType.DOUBLE)
            {
                double[] rg = m_cuda.RunDouble((int)m_hKernel, (int)CUDAFN.CREATE_IMAGEOP, m_param.AsDouble(nNum, dfBrightnessProb, dfBrightnessDelta, dfContrastProb, dfContrastLower, dfContrastUpper, dfSaturationProb, dfSaturationLower, dfSaturationUpper, lRandomSeed));
                return (long)rg[0];
            }
            else
            {
                float[] rg = m_cuda.RunFloat((int)m_hKernel, (int)CUDAFN.CREATE_IMAGEOP, m_param.AsFloat(nNum, (float)dfBrightnessProb, (float)dfBrightnessDelta, (float)dfContrastProb, (float)dfContrastLower, (float)dfContrastUpper, (float)dfSaturationProb, (float)dfSaturationLower, (float)dfSaturationUpper, lRandomSeed));
                return (long)rg[0];
            }
        }

        /// <summary>
        /// Free an image op, freeing up all GPU memory used.
        /// </summary>
        /// <param name="h">Specifies the handle to the image op.</param>
        public void FreeImageOp(long h)
        {
            if (m_dt == DataType.DOUBLE)
                m_cuda.RunDouble((int)m_hKernel, (int)CUDAFN.FREE_IMAGEOP, m_param.AsDouble(h));
            else
                m_cuda.RunFloat((int)m_hKernel, (int)CUDAFN.FREE_IMAGEOP, m_param.AsFloat(h));
        }

        /// <summary>
        /// Distort an image using the ImageOp handle provided.
        /// </summary>
        /// <param name="h">Specifies a handle to the ImageOp that defines how the image will be distorted.</param>
        /// <param name="nCount">Specifies the total number of data elements within 'hX' and 'hY'.</param>
        /// <param name="nNum">Specifies the number of items to be distorted (typically blob.num) in 'hX' and 'hY'.</param>
        /// <param name="nDim">Specifies the dimension of each item.</param>
        /// <param name="hX">Specifies a handle to the GPU memory containing the source data to be distorted.</param>
        /// <param name="hY">Specifies a handle to the GPU memory containing the destination of the distortion.</param>
        public void DistortImage(long h, int nCount, int nNum, int nDim, long hX, long hY)
        {
            if (m_dt == DataType.DOUBLE)
                m_cuda.RunDouble((int)m_hKernel, (int)CUDAFN.DISTORTIMAGE_IMAGEOP, m_param.AsDouble(h, nCount, nNum, nDim, hX, hY));
            else
                m_cuda.RunFloat((int)m_hKernel, (int)CUDAFN.DISTORTIMAGE_IMAGEOP, m_param.AsFloat(h, nCount, nNum, nDim, hX, hY));
        }

        #endregion

        //---------------------------------------------------------------------
        //  ICudaDnn Methods
        //---------------------------------------------------------------------
        #region ICudaDnn Methods

        /// <summary>
        /// Create a new stream on the current GPU.
        /// </summary>
        /// <param name="bNonBlocking">When <code>false</code> (the default) the created stream is a 'blocking' stream, otherwise it is an asynchronous, non-blocking stream.</param>
        /// <param name="nIndex">Specifies an index for the stream where indexed streams are shared when the index = 0 or greater.</param>
        /// <returns>The handle to the stream is returned.</returns>
        public long CreateStream(bool bNonBlocking = false, int nIndex = -1)
        {
            if (m_dt == DataType.DOUBLE)
            {
                double[] rg = m_cuda.RunDouble((int)m_hKernel, (int)CUDAFN.CREATE_STREAM, m_param.AsDouble((bNonBlocking) ? 1.0 : 0.0, nIndex));
                return (long)rg[0];
            }
            else
            {
                float[] rg = m_cuda.RunFloat((int)m_hKernel, (int)CUDAFN.CREATE_STREAM, m_param.AsFloat((bNonBlocking) ? 1.0f : 0.0f, nIndex));
                return (long)rg[0];
            }
        }

        /// <summary>
        /// Free a stream.
        /// </summary>
        /// <param name="h">Specifies the handle to the stream.</param>
        public void FreeStream(long h)
        {
            if (m_dt == DataType.DOUBLE)
                m_cuda.RunDouble((int)m_hKernel, (int)CUDAFN.FREE_STREAM, m_param.AsDouble(h));
            else
                m_cuda.RunFloat((int)m_hKernel, (int)CUDAFN.FREE_STREAM, m_param.AsFloat(h));
        }

        /// <summary>
        /// Synchronize a stream on the current GPU, waiting for its operations to complete.
        /// </summary>
        /// <param name="h">Specifies the handle to the stream.</param>
        public void SynchronizeStream(long h = 0)
        {
            if (m_dt == DataType.DOUBLE)
                m_cuda.RunDouble((int)m_hKernel, (int)CUDAFN.SYNCRHONIZE_STREAM, m_param.AsDouble(h));
            else
                m_cuda.RunFloat((int)m_hKernel, (int)CUDAFN.SYNCRHONIZE_STREAM, m_param.AsFloat(h));
        }

        /// <summary>
        /// Synchronize all kernel threads on the current GPU.
        /// </summary>
        public void SynchronizeThread()
        {
            if (m_dt == DataType.DOUBLE)
                m_cuda.RunDouble((int)m_hKernel, (int)CUDAFN.SYNCHRONIZE_THREAD, null);
            else
                m_cuda.RunFloat((int)m_hKernel, (int)CUDAFN.SYNCHRONIZE_THREAD, null);
        }

        /// <summary>
        /// Create a new instance of [NVIDIA's cuDnn](https://developer.nvidia.com/cudnn).
        /// </summary>
        /// <param name="hStream">Specifies a stream used by cuDnn.</param>
        /// <returns>The handle to cuDnn is returned.</returns>
        public long CreateCuDNN(long hStream = 0)
        {
            if (m_dt == DataType.DOUBLE)
            {
                double[] rg = m_cuda.RunDouble((int)m_hKernel, (int)CUDAFN.CREATE_CUDNN, m_param.AsDouble(hStream));
                return (long)rg[0];
            }
            else
            {
                float[] rg = m_cuda.RunFloat((int)m_hKernel, (int)CUDAFN.CREATE_CUDNN, m_param.AsFloat(hStream));
                return (long)rg[0];
            }
        }

        /// <summary>
        /// Free an instance of cuDnn.
        /// </summary>
        /// <param name="h">Specifies the handle to cuDnn.</param>
        public void FreeCuDNN(long h)
        {
            if (m_dt == DataType.DOUBLE)
                m_cuda.RunDouble((int)m_hKernel, (int)CUDAFN.FREE_CUDNN, m_param.AsDouble(h));
            else
                m_cuda.RunFloat((int)m_hKernel, (int)CUDAFN.FREE_CUDNN, m_param.AsFloat(h));
        }

        /// <summary>
        /// Create an instance of [NVIDIA's NCCL 'Nickel'](https://devblogs.nvidia.com/parallelforall/fast-multi-gpu-collectives-nccl/)
        /// </summary>
        /// <param name="nDeviceId">Specifies the device where this instance of NCCL is going to run.</param>
        /// <param name="nCount">Specifies the total number of NCCL instances used.</param>
        /// <param name="nRank">Specifies the zero-based rank of this instance of NCCL.</param>
        /// <param name="guid">Specifies the unique Guid for this isntance of NCCL.</param>
        /// <returns>The handle to a new instance of NCCL is returned.</returns>
        public long CreateNCCL(int nDeviceId, int nCount, int nRank, Guid guid)
        {
            if (m_dt == DataType.DOUBLE)
            {
                List<double> rgParam = new List<double>() { nDeviceId, nCount, nRank };
                List<double> rgGuid = guidToArrayDouble(guid);

                rgParam.Add(rgGuid.Count);
                rgParam.AddRange(rgGuid);

                double[] rg = m_cuda.RunDouble((int)m_hKernel, (int)CUDAFN.CREATE_NCCL, rgParam.ToArray());
                return (long)rg[0];
            }
            else
            {
                List<float> rgParam = new List<float>() { nDeviceId, nCount, nRank };
                List<float> rgGuid = guidToArrayFloat(guid);

                rgParam.Add(rgGuid.Count);
                rgParam.AddRange(rgGuid);

                float[] rg = m_cuda.RunFloat((int)m_hKernel, (int)CUDAFN.CREATE_NCCL, rgParam.ToArray());
                return (long)rg[0];
            }
        }

        private List<double> guidToArrayDouble(Guid guid)
        {
            List<double> rgdf = new List<double>();
            string str = guid.ToString();
            string[] rgstr = str.Split('-');

            foreach (string str1 in rgstr)
            {
                long val = Convert.ToInt64(str1, 16);
                rgdf.Add(val);
            }

            return rgdf;
        }

        private List<float> guidToArrayFloat(Guid guid)
        {
            List<double> rgDf = guidToArrayDouble(guid);
            List<float> rg = new List<float>();

            foreach (double df in rgDf)
            {
                rg.Add((float)df);
            }

            return rg;
        }

        /// <summary>
        /// Free an instance of NCCL.
        /// </summary>
        /// <param name="hNccl">Specifies the handle to NCCL.</param>
        public void FreeNCCL(long hNccl)
        {
            if (m_dt == DataType.DOUBLE)
                m_cuda.RunDouble((int)m_hKernel, (int)CUDAFN.FREE_NCCL, m_param.AsDouble(hNccl));
            else
                m_cuda.RunFloat((int)m_hKernel, (int)CUDAFN.FREE_NCCL, m_param.AsFloat(hNccl));
        }

        /// <summary>
        /// Initializes a set of NCCL instances for use in a single process.
        /// </summary>
        /// <remarks>
        /// See [Fast Multi-GPU collectives with NCCL](https://devblogs.nvidia.com/parallelforall/fast-multi-gpu-collectives-nccl/).
        /// </remarks>
        /// <param name="rghNccl">Specifies the array of NCCL handles that will be working together.</param>
        public void NcclInitializeSingleProcess(params long[] rghNccl)
        {
            if (m_dt == DataType.DOUBLE)
            {
                List<double> rg = new List<double>() { 0, rghNccl.Length };

                for (int i = 0; i < rghNccl.Length; i++)
                {
                    rg.Add(rghNccl[i]);
                }

                m_cuda.RunDouble((int)m_hKernel, (int)CUDAFN.NCCL_INIT_SINGLEPROCESS, rg.ToArray());
            }
            else
            {
                List<float> rg = new List<float>() { 0, rghNccl.Length };

                for (int i = 0; i < rghNccl.Length; i++)
                {
                    rg.Add(rghNccl[i]);
                }

                m_cuda.RunFloat((int)m_hKernel, (int)CUDAFN.NCCL_INIT_SINGLEPROCESS, rg.ToArray());
            }
        }

        /// <summary>
        /// Initializes a set of NCCL instances for use in different processes.
        /// </summary>
        /// <remarks>
        /// See [Fast Multi-GPU collectives with NCCL](https://devblogs.nvidia.com/parallelforall/fast-multi-gpu-collectives-nccl/).
        /// </remarks>
        /// <param name="hNccl">Specifies the handle of NCCL to initialize.</param>
        public void NcclInitializeMultiProcess(long hNccl)
        {
            if (m_dt == DataType.DOUBLE)
                m_cuda.RunDouble((int)m_hKernel, (int)CUDAFN.NCCL_INIT_MULTIPROCESS, m_param.AsDouble(hNccl));
            else
                m_cuda.RunFloat((int)m_hKernel, (int)CUDAFN.NCCL_INIT_MULTIPROCESS, m_param.AsFloat(hNccl));
        }

        /// <summary>
        /// Broadcasts a block of GPU data to all NCCL instances.
        /// </summary>
        /// <remarks>
        /// See [Fast Multi-GPU collectives with NCCL](https://devblogs.nvidia.com/parallelforall/fast-multi-gpu-collectives-nccl/).
        /// </remarks>
        /// <param name="hNccl">Specifies a handle to an NCCL instance.</param>
        /// <param name="hStream">Specifies a handle to the stream to use for synchronization.</param>
        /// <param name="hX">Specifies a handle to the GPU data to be broadcasted (or recieved).</param>
        /// <param name="nCount">Specifies the number of items (not bytes) in the data.</param>
        public void NcclBroadcast(long hNccl, long hStream, long hX, int nCount)
        {
            Trace.WriteLine("Broadcasting from device ID " + GetDeviceID().ToString());
            if (m_dt == DataType.DOUBLE)
                m_cuda.RunDouble((int)m_hKernel, (int)CUDAFN.NCCL_BROADCAST, m_param.AsDouble(hNccl, hStream, hX, nCount));
            else
                m_cuda.RunFloat((int)m_hKernel, (int)CUDAFN.NCCL_BROADCAST, m_param.AsFloat(hNccl, hStream, hX, nCount));
        }

        /// <summary>
        /// Performs a reduction on all NCCL instances as specified by the reduction operation.
        /// </summary>
        /// <remarks>
        /// See [Fast Multi-GPU collectives with NCCL](https://devblogs.nvidia.com/parallelforall/fast-multi-gpu-collectives-nccl/).
        /// </remarks>
        /// <param name="hNccl">Specifies a handle to an NCCL instance.</param>
        /// <param name="hStream">Specifies a handle to the stream to use for synchronization.</param>
        /// <param name="hX">Specifies a handle to the GPU data to reduce with the other instances of NCCL.</param>
        /// <param name="nCount">Specifies the number of items (not bytes) in the data.</param>
        /// <param name="op">Specifies the reduction operation to perform.</param>
        /// <param name="dfScale">Optionally, specifies a scaling to be applied to the final reduction.</param>
        public void NcclAllReduce(long hNccl, long hStream, long hX, int nCount, NCCL_REDUCTION_OP op, double dfScale = 1.0)
        {
            if (m_dt == DataType.DOUBLE)
                m_cuda.RunDouble((int)m_hKernel, (int)CUDAFN.NCCL_ALLREDUCE, m_param.AsDouble(hNccl, hStream, hX, nCount, (int)op, dfScale));
            else
                m_cuda.RunFloat((int)m_hKernel, (int)CUDAFN.NCCL_ALLREDUCE, m_param.AsFloat(hNccl, hStream, hX, nCount, (int)op, (float)dfScale));
        }


        /// <summary>
        /// Create an instance of an Extension DLL.
        /// </summary>
        /// <param name="strExtensionDllPath">Specifies the file path to the extension DLL.</param>
        /// <returns>The handle to a new instance of Extension is returned.</returns>
        public long CreateExtension(string strExtensionDllPath)
        {
            if (m_dt == DataType.DOUBLE)
            {
                double[] rg = m_cuda.RunDoubleEx((int)m_hKernel, (int)CUDAFN.CREATE_EXTENSION, null, strExtensionDllPath);
                return (long)rg[0];
            }
            else
            {
                float[] rg = m_cuda.RunFloatEx((int)m_hKernel, (int)CUDAFN.CREATE_EXTENSION, null, strExtensionDllPath);
                return (long)rg[0];
            }
        }

        /// <summary>
        /// Free an instance of an Extension.
        /// </summary>
        /// <param name="hExtension">Specifies the handle to the Extension.</param>
        public void FreeExtension(long hExtension)
        {
            if (m_dt == DataType.DOUBLE)
                m_cuda.RunDouble((int)m_hKernel, (int)CUDAFN.FREE_EXTENSION, m_param.AsDouble(hExtension));
            else
                m_cuda.RunFloat((int)m_hKernel, (int)CUDAFN.FREE_EXTENSION, m_param.AsFloat(hExtension));
        }

        /// <summary>
        /// Run a function on the extension specified.
        /// </summary>
        /// <param name="hExtension">Specifies the handle to the extension created with CreateExtension.</param>
        /// <param name="lfnIdx">Specifies the extension function to run.</param>
        /// <param name="rgParam">Specifies the parameters to pass to the extension.</param>
        /// <returns>The values returned by the extension are returned.</returns>
        public T[] RunExtension(long hExtension, long lfnIdx, T[] rgParam)
        {
            if (m_dt == DataType.DOUBLE)
            {
                List<double> rgdf = new List<double>() { hExtension, lfnIdx };

                if (rgParam != null)
                    rgdf.AddRange(Utility.ConvertVec<T>(rgParam));

                double[] rg = m_cuda.RunDouble((int)m_hKernel, (int)CUDAFN.EXTENSION_RUN, rgdf.ToArray());
                return Utility.ConvertVec<T>(rg);
            }
            else
            {
                List<float> rgf = new List<float>() { hExtension, lfnIdx };

                if (rgParam != null)
                    rgf.AddRange(Utility.ConvertVecF<T>(rgParam));

                float[] rg = m_cuda.RunFloat((int)m_hKernel, (int)CUDAFN.EXTENSION_RUN, rgf.ToArray());
                return Utility.ConvertVec<T>(rg);
            }
        }


        /// <summary>
        /// Create a new instance of a tensor descriptor for use with [NVIDIA's cuDnn](https://developer.nvidia.com/cudnn).
        /// </summary>
        /// <returns>The tensor descriptor handle is returned.</returns>
        public long CreateTensorDesc()
        {
            if (m_dt == DataType.DOUBLE)
            {
                double[] rg = m_cuda.RunDouble((int)m_hKernel, (int)CUDAFN.CREATE_TENSORDESC, null);
                return (long)rg[0];
            }
            else
            {
                float[] rg = m_cuda.RunFloat((int)m_hKernel, (int)CUDAFN.CREATE_TENSORDESC, null);
                return (long)rg[0];
            }
        }

        /// <summary>
        /// Free a tensor descriptor instance.
        /// </summary>
        /// <param name="h">Specifies the handle to the tensor descriptor instance.</param>
        public void FreeTensorDesc(long h)
        {
            if (m_dt == DataType.DOUBLE)
                m_cuda.RunDouble((int)m_hKernel, (int)CUDAFN.FREE_TENSORDESC, m_param.AsDouble(h));
            else
                m_cuda.RunFloat((int)m_hKernel, (int)CUDAFN.FREE_TENSORDESC, m_param.AsFloat(h));
        }

        /// <summary>
        /// Sets the values of a tensor descriptor.
        /// </summary>
        /// <param name="hHandle">Specifies the handle to the tensor descriptor.</param>
        /// <param name="rgDim">Specifies the dimensions of the data.</param>
        /// <param name="rgStride">Specifies the stride of the data.</param>
        /// <param name="bHalf">Optionally, specifies whether or not to use the FP16 half data type.</param>
        public void SetTensorNdDesc(long hHandle, int[] rgDim, int[] rgStride, bool bHalf = false)
        {
            if (rgDim.Length != rgStride.Length)
                throw new Exception("The stride and dim arrays must have the same length.");

            if (m_dt == DataType.DOUBLE)
            {
                List<double> rgArg = new List<double>() { hHandle, (bHalf) ? 1 : 0, rgDim.Length };

                for (int i = 0; i < rgDim.Length; i++)
                {
                    rgArg.Add(rgDim[i]);
                }

                for (int i = 0; i < rgStride.Length; i++)
                {
                    rgArg.Add(rgStride[i]);
                }

                m_cuda.RunDouble((int)m_hKernel, (int)CUDAFN.SET_TENSORNDDESC, rgArg.ToArray());
            }
            else
            {
                List<float> rgArg = new List<float>() { hHandle, (bHalf) ? 1 : 0, rgDim.Length };

                for (int i = 0; i < rgDim.Length; i++)
                {
                    rgArg.Add(rgDim[i]);
                }

                for (int i = 0; i < rgStride.Length; i++)
                {
                    rgArg.Add(rgStride[i]);
                }

                m_cuda.RunFloat((int)m_hKernel, (int)CUDAFN.SET_TENSORNDDESC, rgArg.ToArray());
            }
        }

        /// <summary>
        /// Sets the values of a tensor descriptor.
        /// </summary>
        /// <param name="hHandle">Specifies the handle to the tensor descriptor.</param>
        /// <param name="n">Specifies the number of items.</param>
        /// <param name="c">Specifies the number of channels in each item.</param>
        /// <param name="h">Specifies the height of each item.</param>
        /// <param name="w">Specifies the width of each item.</param>
        /// <param name="bHalf">Optionally, specifies whether or not to use the FP16 half data type.</param>
        public void SetTensorDesc(long hHandle, int n, int c, int h, int w, bool bHalf = false)
        {
            if (m_dt == DataType.DOUBLE)
                m_cuda.RunDouble((int)m_hKernel, (int)CUDAFN.SET_TENSORDESC, m_param.AsDouble(hHandle, (bHalf) ? 1 : 0, n, c, h, w));
            else
                m_cuda.RunFloat((int)m_hKernel, (int)CUDAFN.SET_TENSORDESC, m_param.AsFloat(hHandle, (bHalf) ? 1 : 0, n, c, h, w));
        }

        /// <summary>
        /// Sets the values of a tensor descriptor.
        /// </summary>
        /// <param name="hHandle">Specifies the handle to the tensor descriptor.</param>
        /// <param name="n">Specifies the number of items.</param>
        /// <param name="c">Specifies the number of channels in each item.</param>
        /// <param name="h">Specifies the height of each item.</param>
        /// <param name="w">Specifies the width of each item.</param>
        /// <param name="nStride">Specifies the stride between two images.</param>
        /// <param name="cStride">Specifies the stride between two channels.</param>
        /// <param name="hStride">Specifies the stride between two rows.</param>
        /// <param name="wStride">Specifies the stride between two columns.</param>
        /// <param name="bHalf">Optionally, specifies whether or not to use the FP16 half data type.</param>
        public void SetTensorDesc(long hHandle, int n, int c, int h, int w, int nStride, int cStride, int hStride, int wStride, bool bHalf = false)
        {
            if (m_dt == DataType.DOUBLE)
                m_cuda.RunDouble((int)m_hKernel, (int)CUDAFN.SET_TENSORDESC, m_param.AsDouble(hHandle, (bHalf) ? 1 : 0, n, c, h, w, nStride, cStride, hStride, wStride));
            else
                m_cuda.RunFloat((int)m_hKernel, (int)CUDAFN.SET_TENSORDESC, m_param.AsFloat(hHandle, (bHalf) ? 1 : 0, n, c, h, w, nStride, cStride, hStride, wStride));
        }

        /// <summary>
        /// Add two tensors together.
        /// </summary>
        /// <param name="hCuDnn">Specifies a handle to the cuDnn instance.</param>
        /// <param name="hSrcDesc">Specifies a handle to the source tensor descriptor.</param>
        /// <param name="hSrc">Specifies a handle to the source GPU memory.</param>
        /// <param name="nSrcOffset">Specifies an offset within the GPU memory.</param>
        /// <param name="hDstDesc">Specifies a handle to the destination tensor descriptor.</param>
        /// <param name="hDst">Specifies a handle to the desination GPU memory.</param>
        /// <param name="nDstOffset">Specifies an offset within the GPU memory.</param>
        public void AddTensor(long hCuDnn, long hSrcDesc, long hSrc, int nSrcOffset, long hDstDesc, long hDst, int nDstOffset)
        {
            AddTensor(hCuDnn, m_tOne, hSrcDesc, hSrc, nSrcOffset, m_tOne, hDstDesc, hDst, nDstOffset);
        }

        /// <summary>
        /// Add two tensors together.
        /// </summary>
        /// <param name="hCuDnn">Specifies a handle to the cuDnn instance.</param>
        /// <param name="fAlpha">Specifies a scaling factor applied to the source GPU memory before the add.</param>
        /// <param name="hSrcDesc">Specifies a handle to the source tensor descriptor.</param>
        /// <param name="hSrc">Specifies a handle to the source GPU memory.</param>
        /// <param name="nSrcOffset">Specifies an offset within the GPU memory.</param>
        /// <param name="fBeta">Specifies a scaling factor applied to the destination GPU memory before the add.</param>
        /// <param name="hDstDesc">Specifies a handle to the destination tensor descriptor.</param>
        /// <param name="hDst">Specifies a handle to the desination GPU memory.</param>
        /// <param name="nDstOffset">Specifies an offset within the GPU memory.</param>
        public void AddTensor(long hCuDnn, T fAlpha, long hSrcDesc, long hSrc, int nSrcOffset, T fBeta, long hDstDesc, long hDst, int nDstOffset)
        {
            if (m_dt == DataType.DOUBLE)
                m_cuda.RunDouble((int)m_hKernel, (int)CUDAFN.ADD_TENSOR, m_param.AsDouble(hCuDnn, convertD(fAlpha), hSrcDesc, hSrc, nSrcOffset, convertD(fBeta), hDstDesc, hDst, nDstOffset));
            else
                m_cuda.RunFloat((int)m_hKernel, (int)CUDAFN.ADD_TENSOR, m_param.AsFloat(hCuDnn, convertF(fAlpha), hSrcDesc, hSrc, nSrcOffset, convertF(fBeta), hDstDesc, hDst, nDstOffset));
        }


        /// <summary>
        /// Create a new instance of a filter descriptor for use with [NVIDIA's cuDnn](https://developer.nvidia.com/cudnn).
        /// </summary>
        /// <returns>The filter descriptor handle is returned.</returns>
        public long CreateFilterDesc()
        {
            if (m_dt == DataType.DOUBLE)
            {
                double[] rg = m_cuda.RunDouble((int)m_hKernel, (int)CUDAFN.CREATE_FILTERDESC, null);
                return (long)rg[0];
            }
            else
            {
                float[] rg = m_cuda.RunFloat((int)m_hKernel, (int)CUDAFN.CREATE_FILTERDESC, null);
                return (long)rg[0];
            }
        }

        /// <summary>
        /// Free a filter descriptor instance.
        /// </summary>
        /// <param name="h">Specifies the handle to the filter descriptor instance.</param>
        public void FreeFilterDesc(long h)
        {
            if (m_dt == DataType.DOUBLE)
                m_cuda.RunDouble((int)m_hKernel, (int)CUDAFN.FREE_FILTERDESC, m_param.AsDouble(h));
            else
                m_cuda.RunFloat((int)m_hKernel, (int)CUDAFN.FREE_FILTERDESC, m_param.AsFloat(h));
        }

        /// <summary>
        /// Sets the values of a filter descriptor.
        /// </summary>
        /// <param name="hHandle">Specifies the handle to the filter descriptor.</param>
        /// <param name="rgDim">Specifies the dimensions of the data.</param>
        /// <param name="bHalf">Optionally, specifies whether or not to use the FP16 half data type.</param>
        public void SetFilterNdDesc(long hHandle, int[] rgDim, bool bHalf = false)
        {
            if (m_dt == DataType.DOUBLE)
            {
                List<double> rgArg = new List<double>() { hHandle, (bHalf) ? 1 : 0, rgDim.Length };

                for (int i = 0; i < rgDim.Length; i++)
                {
                    rgArg.Add(rgDim[i]);
                }

                m_cuda.RunDouble((int)m_hKernel, (int)CUDAFN.SET_FILTERNDDESC, rgArg.ToArray());
            }
            else
            {
                List<float> rgArg = new List<float>() { hHandle, (bHalf) ? 1 : 0, rgDim.Length };

                for (int i = 0; i < rgDim.Length; i++)
                {
                    rgArg.Add(rgDim[i]);
                }

                m_cuda.RunFloat((int)m_hKernel, (int)CUDAFN.SET_FILTERNDDESC, rgArg.ToArray());
            }
        }

        /// <summary>
        /// Sets the values of a filter descriptor.
        /// </summary>
        /// <param name="hHandle">Specifies the handle to the filter descriptor.</param>
        /// <param name="n">Specifies the number of items.</param>
        /// <param name="c">Specifies the number of channels in each item.</param>
        /// <param name="h">Specifies the height of each item.</param>
        /// <param name="w">Specifies the width of each item.</param>
        /// <param name="bHalf">Optionally, specifies whether or not to use the FP16 half data type.</param>
        public void SetFilterDesc(long hHandle, int n, int c, int h, int w, bool bHalf = false)
        {
            if (m_dt == DataType.DOUBLE)
                m_cuda.RunDouble((int)m_hKernel, (int)CUDAFN.SET_FILTERDESC, m_param.AsDouble(hHandle, (bHalf) ? 1 : 0, n, c, h, w));
            else
                m_cuda.RunFloat((int)m_hKernel, (int)CUDAFN.SET_FILTERDESC, m_param.AsFloat(hHandle, (bHalf) ? 1 : 0, n, c, h, w));
        }

        /// <summary>
        /// Create a new instance of a convolution descriptor for use with [NVIDIA's cuDnn](https://developer.nvidia.com/cudnn).
        /// </summary>
        /// <returns>The convolution descriptor handle is returned.</returns>
        public long CreateConvolutionDesc()
        {
            if (m_dt == DataType.DOUBLE)
            {
                double[] rg = m_cuda.RunDouble((int)m_hKernel, (int)CUDAFN.CREATE_CONVDESC, null);
                return (long)rg[0];
            }
            else
            {
                float[] rg = m_cuda.RunFloat((int)m_hKernel, (int)CUDAFN.CREATE_CONVDESC, null);
                return (long)rg[0];
            }
        }

        /// <summary>
        /// Free a convolution descriptor instance.
        /// </summary>
        /// <param name="h">Specifies the handle to the convolution descriptor instance.</param>
        public void FreeConvolutionDesc(long h)
        {
            if (m_dt == DataType.DOUBLE)
                m_cuda.RunDouble((int)m_hKernel, (int)CUDAFN.FREE_CONVDESC, m_param.AsDouble(h));
            else
                m_cuda.RunFloat((int)m_hKernel, (int)CUDAFN.FREE_CONVDESC, m_param.AsFloat(h));
        }

        /// <summary>
        /// Set the values of a convolution descriptor.
        /// </summary>
        /// <param name="hHandle">Specifies the handle to the convolution descriptor.</param>
        /// <param name="hPad">Specifies the pad applied to the height.</param>
        /// <param name="wPad">Specifies the pad applied to the width.</param>
        /// <param name="hStride">Specifies the stride of the height.</param>
        /// <param name="wStride">Specifies the stride of the width.</param>
        /// <param name="hDilation">Specifies the dilation of the height (default = 1).</param>
        /// <param name="wDilation">Specifies the dilation of the width (default = 1).</param>
        /// <param name="bUseTensorCores">Optionally, specifies whether or not to use the Tensor Cores (if available).</param>
        /// <param name="bHalf">Optionally, specifies whether or not to use the FP16 half data type.</param>
        public void SetConvolutionDesc(long hHandle, int hPad, int wPad, int hStride, int wStride, int hDilation, int wDilation, bool bUseTensorCores, bool bHalf = false)
        {
            if (m_dt == DataType.DOUBLE)
                m_cuda.RunDouble((int)m_hKernel, (int)CUDAFN.SET_CONVDESC, m_param.AsDouble(hHandle, (bHalf) ? 1 : 0, hPad, wPad, hStride, wStride, hDilation, wDilation, (bUseTensorCores) ? 1 : 0));
            else
                m_cuda.RunFloat((int)m_hKernel, (int)CUDAFN.SET_CONVDESC, m_param.AsFloat(hHandle, (bHalf) ? 1 : 0, hPad, wPad, hStride, wStride, hDilation, wDilation, (bUseTensorCores) ? 1 : 0));
        }

        /// <summary>
        /// Queryies the algorithms and workspace sizes used for a given convolution descriptor.
        /// </summary>
        /// <param name="hCuDnn">Specifies a handle to the instance of cuDnn.</param>
        /// <param name="hBottomDesc">Specifies a handle to the bottom tensor descriptor.</param>
        /// <param name="hFilterDesc">Specifies a handle to the filter descriptor.</param>
        /// <param name="hConvDesc">Specifies a handle to the convolution descriptor.</param>
        /// <param name="hTopDesc">Specifies a handle to the top tensor descriptor.</param>
        /// <param name="lWorkspaceSizeLimitInBytes">Specifies the workspace limits (in bytes).</param>
        /// <param name="bUseTensorCores">Specifies whether or not to use tensor cores (this parameter must match the setting of the 'bUseTensorCores' specified in the 'SetConvolutionDesc' method.</param>
        /// <param name="algoFwd">Returns the algorithm used for the convolution foward.</param>
        /// <param name="lWsSizeFwd">Returns the workspace size (in bytes) for the convolution foward.</param>
        /// <param name="algoBwdFilter">Returns the algorithm used for the backward filter.</param>
        /// <param name="lWsSizeBwdFilter">Returns the workspace size (int bytes) for the backward filter.</param>
        /// <param name="algoBwdData">Returns the algorithm for the backward data.</param>
        /// <param name="lWsSizeBwdData">Returns the workspace (in bytes) for the backward data.</param>
        /// <param name="preferredFwdAlgo">Optionally, specifies a preferred forward algo to attempt to use for forward convolution.  The new algo is only used if the current device supports it.</param>
        public void GetConvolutionInfo(long hCuDnn, long hBottomDesc, long hFilterDesc, long hConvDesc, long hTopDesc, ulong lWorkspaceSizeLimitInBytes, bool bUseTensorCores, out CONV_FWD_ALGO algoFwd, out ulong lWsSizeFwd, out CONV_BWD_FILTER_ALGO algoBwdFilter, out ulong lWsSizeBwdFilter, out CONV_BWD_DATA_ALGO algoBwdData, out ulong lWsSizeBwdData, CONV_FWD_ALGO preferredFwdAlgo = CONV_FWD_ALGO.NONE)
        {
            if (m_dt == DataType.DOUBLE)
            {
                double[] rg = m_cuda.RunDouble((int)m_hKernel, (int)CUDAFN.GET_CONVINFO, m_param.AsDouble(hCuDnn, hBottomDesc, hFilterDesc, hConvDesc, hTopDesc, lWorkspaceSizeLimitInBytes, (bUseTensorCores) ? 1 : 0, (int)preferredFwdAlgo));
                algoFwd = (CONV_FWD_ALGO)rg[0];
                lWsSizeFwd = (ulong)rg[1];
                algoBwdFilter = (CONV_BWD_FILTER_ALGO)rg[2];
                lWsSizeBwdFilter = (ulong)rg[3];
                algoBwdData = (CONV_BWD_DATA_ALGO)rg[4];
                lWsSizeBwdData = (ulong)rg[5];
            }
            else
            {
                float[] rg = m_cuda.RunFloat((int)m_hKernel, (int)CUDAFN.GET_CONVINFO, m_param.AsFloat(hCuDnn, hBottomDesc, hFilterDesc, hConvDesc, hTopDesc, lWorkspaceSizeLimitInBytes, (bUseTensorCores) ? 1 : 0, (int)preferredFwdAlgo));
                algoFwd = (CONV_FWD_ALGO)rg[0];
                lWsSizeFwd = (ulong)rg[1];
                algoBwdFilter = (CONV_BWD_FILTER_ALGO)rg[2];
                lWsSizeBwdFilter = (ulong)rg[3];
                algoBwdData = (CONV_BWD_DATA_ALGO)rg[4];
                lWsSizeBwdData = (ulong)rg[5];
            }
        }

        /// <summary>
        /// Perform a convolution forward pass.
        /// </summary>
        /// <param name="hCuDnn">Specifies a handle to the instance of cuDnn.</param>
        /// <param name="hBottomDesc">Specifies a handle to the bottom tensor descriptor.</param>
        /// <param name="hBottomData">Specifies a handle to the bottom data in GPU memory.</param>
        /// <param name="nBottomOffset">Specifies an offset into the bottom memory (in items, not bytes).</param>
        /// <param name="hFilterDesc">Specifies a handle to the filter descriptor.</param>
        /// <param name="hWeight">Specifies a handle to the weight data in GPU memory.</param>
        /// <param name="nWeightOffset">Specifies an offset into the weight memory (in items, not bytes).</param>
        /// <param name="hConvDesc">Specifies a handle to the convolution descriptor.</param>
        /// <param name="algoFwd">Specifies the algorithm to use for the foward operation.</param>
        /// <param name="hWorkspace">Specifies a handle to the GPU memory to use for the workspace.</param>
        /// <param name="nWorkspaceOffset">Specifies an offset into the workspace memory.</param>
        /// <param name="lWorkspaceSize">Specifies the size of the workspace memory (in bytes).</param>
        /// <param name="hTopDesc">Specifies a handle to the top tensor descriptor.</param>
        /// <param name="hTopData">Specifies a handle to the top data in GPU memory.</param>
        /// <param name="nTopOffset">Specifies an offset into the top memory (in items, not bytes).</param>
        /// <param name="bSyncStream">Optionally, specifies whether or not to syncrhonize the stream. The default = <i>true</i>.</param>
        public void ConvolutionForward(long hCuDnn, long hBottomDesc, long hBottomData, int nBottomOffset, long hFilterDesc, long hWeight, int nWeightOffset, long hConvDesc, CONV_FWD_ALGO algoFwd, long hWorkspace, int nWorkspaceOffset, ulong lWorkspaceSize, long hTopDesc, long hTopData, int nTopOffset, bool bSyncStream = true)
        {
            ConvolutionForward(hCuDnn, m_tOne, hBottomDesc, hBottomData, nBottomOffset, hFilterDesc, hWeight, nWeightOffset, hConvDesc, algoFwd, hWeight, nWeightOffset, lWorkspaceSize, m_tZero, hTopDesc, hTopData, nTopOffset, bSyncStream);
        }

        /// <summary>
        /// Perform a convolution forward pass.
        /// </summary>
        /// <param name="hCuDnn">Specifies a handle to the instance of cuDnn.</param>
        /// <param name="fAlpha">Specifies a scaling factor applied to the result.</param>
        /// <param name="hBottomDesc">Specifies a handle to the bottom tensor descriptor.</param>
        /// <param name="hBottomData">Specifies a handle to the bottom data in GPU memory.</param>
        /// <param name="nBottomOffset">Specifies an offset into the bottom memory (in items, not bytes).</param>
        /// <param name="hFilterDesc">Specifies a handle to the filter descriptor.</param>
        /// <param name="hWeight">Specifies a handle to the weight data in GPU memory.</param>
        /// <param name="nWeightOffset">Specifies an offset into the weight memory (in items, not bytes).</param>
        /// <param name="hConvDesc">Specifies a handle to the convolution descriptor.</param>
        /// <param name="algoFwd">Specifies the algorithm to use for the foward operation.</param>
        /// <param name="hWorkspace">Specifies a handle to the GPU memory to use for the workspace.</param>
        /// <param name="nWorkspaceOffset">Specifies an offset into the workspace memory.</param>
        /// <param name="lWorkspaceSize">Specifies the size of the workspace memory (in bytes).</param>
        /// <param name="fBeta">Specifies a scaling factor applied to the prior destination value.</param>
        /// <param name="hTopDesc">Specifies a handle to the top tensor descriptor.</param>
        /// <param name="hTopData">Specifies a handle to the top data in GPU memory.</param>
        /// <param name="nTopOffset">Specifies an offset into the top memory (in items, not bytes).</param>
        /// <param name="bSyncStream">Optionally, specifies whether or not to syncrhonize the stream. The default = <i>true</i>.</param>
        public void ConvolutionForward(long hCuDnn, T fAlpha, long hBottomDesc, long hBottomData, int nBottomOffset, long hFilterDesc, long hWeight, int nWeightOffset, long hConvDesc, CONV_FWD_ALGO algoFwd, long hWorkspace, int nWorkspaceOffset, ulong lWorkspaceSize, T fBeta, long hTopDesc, long hTopData, int nTopOffset, bool bSyncStream = true)
        {
            if (m_dt == DataType.DOUBLE)
                m_cuda.RunDouble((int)m_hKernel, (int)CUDAFN.FWD_CONV, m_param.AsDouble(hCuDnn, convertD(fAlpha), hBottomDesc, hBottomData, nBottomOffset, hFilterDesc, hWeight, nWeightOffset, hConvDesc, (double)algoFwd, hWorkspace, nWorkspaceOffset, lWorkspaceSize, convertD(fBeta), hTopDesc, hTopData, nTopOffset, (bSyncStream) ? 1 : 0));
            else
                m_cuda.RunFloat((int)m_hKernel, (int)CUDAFN.FWD_CONV, m_param.AsFloat(hCuDnn, convertF(fAlpha), hBottomDesc, hBottomData, nBottomOffset, hFilterDesc, hWeight, nWeightOffset, hConvDesc, (float)algoFwd, hWorkspace, nWorkspaceOffset, lWorkspaceSize, convertF(fBeta), hTopDesc, hTopData, nTopOffset, (bSyncStream) ? 1 : 0));
        }

        /// <summary>
        /// Perform a convolution backward pass on the bias.
        /// </summary>
        /// <param name="hCuDnn">Specifies a handle to the instance of cuDnn.</param>
        /// <param name="hTopDesc">Specifies a handle to the top tensor descriptor.</param>
        /// <param name="hTopDiff">Specifies a handle to the top diff in GPU memory.</param>
        /// <param name="nTopOffset">Specifies an offset into the top memory (in items, not bytes).</param>
        /// <param name="hBiasDesc">Specifies a handle to the bias tensor descriptor.</param>
        /// <param name="hBiasDiff">Specifies a handle to the bias diff in GPU memory.</param>
        /// <param name="nBiasOffset">Specifies an offset into the diff memory (in items, not bytes).</param>
        /// <param name="bSyncStream">Optionally, specifies whether or not to syncrhonize the stream. The default = <i>true</i>.</param>
        public void ConvolutionBackwardBias(long hCuDnn, long hTopDesc, long hTopDiff, int nTopOffset, long hBiasDesc, long hBiasDiff, int nBiasOffset, bool bSyncStream = true)
        {
            ConvolutionBackwardBias(hCuDnn, m_tOne, hTopDesc, hTopDiff, nTopOffset, m_tOne, hBiasDesc, hBiasDiff, nBiasOffset, bSyncStream);
        }

        /// <summary>
        /// Perform a convolution backward pass on the bias.
        /// </summary>
        /// <param name="hCuDnn">Specifies a handle to the instance of cuDnn.</param>
        /// <param name="fAlpha">Specifies a scaling factor applied to the result.</param>
        /// <param name="hTopDesc">Specifies a handle to the top tensor descriptor.</param>
        /// <param name="hTopDiff">Specifies a handle to the top diff in GPU memory.</param>
        /// <param name="nTopOffset">Specifies an offset into the top memory (in items, not bytes).</param>
        /// <param name="fBeta">Specifies a scaling factor applied to the prior destination value.</param>
        /// <param name="hBiasDesc">Specifies a handle to the bias tensor descriptor.</param>
        /// <param name="hBiasDiff">Specifies a handle to the bias diff in GPU memory.</param>
        /// <param name="nBiasOffset">Specifies an offset into the diff memory (in items, not bytes).</param>
        /// <param name="bSyncStream">Optionally, specifies whether or not to syncrhonize the stream. The default = <i>true</i>.</param>
        public void ConvolutionBackwardBias(long hCuDnn, T fAlpha, long hTopDesc, long hTopDiff, int nTopOffset, T fBeta, long hBiasDesc, long hBiasDiff, int nBiasOffset, bool bSyncStream = true)
        {
            if (m_dt == DataType.DOUBLE)
                m_cuda.RunDouble((int)m_hKernel, (int)CUDAFN.BWD_CONV_BIAS, m_param.AsDouble(hCuDnn, convertD(fAlpha), hTopDesc, hTopDiff, nTopOffset, convertD(fBeta), hBiasDesc, hBiasDiff, nBiasOffset, (bSyncStream) ? 1 : 0));
            else
                m_cuda.RunFloat((int)m_hKernel, (int)CUDAFN.BWD_CONV_BIAS, m_param.AsFloat(hCuDnn, convertF(fAlpha), hTopDesc, hTopDiff, nTopOffset, convertF(fBeta), hBiasDesc, hBiasDiff, nBiasOffset, (bSyncStream) ? 1 : 0));
        }

        /// <summary>
        /// Perform a convolution backward pass on the filter.
        /// </summary>
        /// <param name="hCuDnn">Specifies a handle to the instance of cuDnn.</param>
        /// <param name="hBottomDesc">Specifies a handle to the bottom tensor descriptor.</param>
        /// <param name="hBottomData">Specifies a handle to the bottom data in GPU memory.</param>
        /// <param name="nBottomOffset">Specifies an offset into the bottom memory (in items, not bytes).</param>
        /// <param name="hTopDesc">Specifies a handle to the top tensor descriptor.</param>
        /// <param name="hTopDiff">Specifies a handle to the top diff in GPU memory.</param>
        /// <param name="nTopOffset">Specifies an offset into the top memory (in items, not bytes).</param>
        /// <param name="hConvDesc">Specifies a handle to the convolution descriptor.</param>
        /// <param name="algoBwd">Specifies the algorithm to use when performing the backward operation.</param>
        /// <param name="hWorkspace">Specifies a handle to the GPU memory to use for the workspace.</param>
        /// <param name="nWorkspaceOffset">Specifies an offset into the workspace memory.</param>
        /// <param name="lWorkspaceSize">Specifies the size of the workspace memory (in bytes).</param>
        /// <param name="hFilterDesc">Specifies a handle to the filter descriptor.</param>
        /// <param name="hWeightDiff">Specifies a handle to the weight diff in GPU memory.</param>
        /// <param name="nWeightOffset">Specifies an offset into the weight memory (in items, not bytes).</param>
        /// <param name="bSyncStream">Optionally, specifies whether or not to syncrhonize the stream. The default = <i>true</i>.</param>
        public void ConvolutionBackwardFilter(long hCuDnn, long hBottomDesc, long hBottomData, int nBottomOffset, long hTopDesc, long hTopDiff, int nTopOffset, long hConvDesc, CONV_BWD_FILTER_ALGO algoBwd, long hWorkspace, int nWorkspaceOffset, ulong lWorkspaceSize, long hFilterDesc, long hWeightDiff, int nWeightOffset, bool bSyncStream)
        {
            ConvolutionBackwardFilter(hCuDnn, m_tOne, hBottomDesc, hBottomData, nBottomOffset, hTopDesc, hTopDiff, nTopOffset, hConvDesc, algoBwd, hWorkspace, nWorkspaceOffset, lWorkspaceSize, m_tOne, hFilterDesc, hWeightDiff, nWeightOffset, bSyncStream);
        }

        /// <summary>
        /// Perform a convolution backward pass on the filter.
        /// </summary>
        /// <param name="hCuDnn">Specifies a handle to the instance of cuDnn.</param>
        /// <param name="fAlpha">Specifies a scaling factor applied to the result.</param>
        /// <param name="hBottomDesc">Specifies a handle to the bottom tensor descriptor.</param>
        /// <param name="hBottomData">Specifies a handle to the bottom data in GPU memory.</param>
        /// <param name="nBottomOffset">Specifies an offset into the bottom memory (in items, not bytes).</param>
        /// <param name="hTopDesc">Specifies a handle to the top tensor descriptor.</param>
        /// <param name="hTopDiff">Specifies a handle to the top diff in GPU memory.</param>
        /// <param name="nTopOffset">Specifies an offset into the top memory (in items, not bytes).</param>
        /// <param name="hConvDesc">Specifies a handle to the convolution descriptor.</param>
        /// <param name="algoBwd">Specifies the algorithm to use when performing the backward operation.</param>
        /// <param name="hWorkspace">Specifies a handle to the GPU memory to use for the workspace.</param>
        /// <param name="nWorkspaceOffset">Specifies an offset into the workspace memory.</param>
        /// <param name="lWorkspaceSize">Specifies the size of the workspace memory (in bytes).</param>
        /// <param name="fBeta">Specifies a scaling factor applied to the prior destination value.</param>
        /// <param name="hFilterDesc">Specifies a handle to the filter descriptor.</param>
        /// <param name="hWeightDiff">Specifies a handle to the weight diff in GPU memory.</param>
        /// <param name="nWeightOffset">Specifies an offset into the weight memory (in items, not bytes).</param>
        /// <param name="bSyncStream">Optionally, specifies whether or not to syncrhonize the stream. The default = <i>true</i>.</param>
        public void ConvolutionBackwardFilter(long hCuDnn, T fAlpha, long hBottomDesc, long hBottomData, int nBottomOffset, long hTopDesc, long hTopDiff, int nTopOffset, long hConvDesc, CONV_BWD_FILTER_ALGO algoBwd, long hWorkspace, int nWorkspaceOffset, ulong lWorkspaceSize, T fBeta, long hFilterDesc, long hWeightDiff, int nWeightOffset, bool bSyncStream = true)
        {
            if (m_dt == DataType.DOUBLE)
                m_cuda.RunDouble((int)m_hKernel, (int)CUDAFN.BWD_CONV_FILTER, m_param.AsDouble(hCuDnn, convertD(fAlpha), hBottomDesc, hBottomData, nBottomOffset, hTopDesc, hTopDiff, nTopOffset, hConvDesc, (double)algoBwd, hWorkspace, nWorkspaceOffset, lWorkspaceSize, convertD(fBeta), hFilterDesc, hWeightDiff, nWeightOffset, (bSyncStream) ? 1 : 0));
            else
                m_cuda.RunFloat((int)m_hKernel, (int)CUDAFN.BWD_CONV_FILTER, m_param.AsFloat(hCuDnn, convertF(fAlpha), hBottomDesc, hBottomData, nBottomOffset, hTopDesc, hTopDiff, nTopOffset, hConvDesc, (float)algoBwd, hWorkspace, nWorkspaceOffset, lWorkspaceSize, convertF(fBeta), hFilterDesc, hWeightDiff, nWeightOffset, (bSyncStream) ? 1 : 0));
        }

        /// <summary>
        /// Perform a convolution backward pass on the data.
        /// </summary>
        /// <param name="hCuDnn">Specifies a handle to the instance of cuDnn.</param>
        /// <param name="hFilterDesc">Specifies a handle to the filter descriptor.</param>
        /// <param name="hWeight">Specifies a handle to the weight data in GPU memory.</param>
        /// <param name="nWeightOffset">Specifies an offset into the weight memory (in items, not bytes).</param>
        /// <param name="hTopDesc">Specifies a handle to the top tensor descriptor.</param>
        /// <param name="hTopDiff">Specifies a handle to the top diff in GPU memory.</param>
        /// <param name="nTopOffset">Specifies an offset into the top memory (in items, not bytes).</param>
        /// <param name="hConvDesc">Specifies a handle to the convolution descriptor.</param>
        /// <param name="algoBwd">Specifies the algorithm to use when performing the backward operation.</param>
        /// <param name="hWorkspace">Specifies a handle to the GPU memory to use for the workspace.</param>
        /// <param name="nWorkspaceOffset">Specifies an offset into the workspace memory.</param>
        /// <param name="lWorkspaceSize">Specifies the size of the workspace memory (in bytes).</param>
        /// <param name="hBottomDesc">Specifies a handle to the bottom tensor descriptor.</param>
        /// <param name="hBottomDiff">Specifies a handle to the bottom diff in GPU memory.</param>
        /// <param name="nBottomOffset">Specifies an offset into the bottom memory (in items, not bytes).</param>
        /// <param name="bSyncStream">Optionally, specifies whether or not to syncrhonize the stream. The default = <i>true</i>.</param>
        public void ConvolutionBackwardData(long hCuDnn, long hFilterDesc, long hWeight, int nWeightOffset, long hTopDesc, long hTopDiff, int nTopOffset, long hConvDesc, CONV_BWD_DATA_ALGO algoBwd, long hWorkspace, int nWorkspaceOffset, ulong lWorkspaceSize, long hBottomDesc, long hBottomDiff, int nBottomOffset, bool bSyncStream = true)
        {
            ConvolutionBackwardData(hCuDnn, m_tOne, hFilterDesc, hWeight, nWeightOffset, hTopDesc, hTopDiff, nTopOffset, hConvDesc, algoBwd, hWorkspace, nWorkspaceOffset, lWorkspaceSize, m_tZero, hBottomDesc, hBottomDiff, nBottomOffset, bSyncStream);
        }

        /// <summary>
        /// Perform a convolution backward pass on the data.
        /// </summary>
        /// <param name="hCuDnn">Specifies a handle to the instance of cuDnn.</param>
        /// <param name="fAlpha">Specifies a scaling factor applied to the result.</param>
        /// <param name="hFilterDesc">Specifies a handle to the filter descriptor.</param>
        /// <param name="hWeight">Specifies a handle to the weight data in GPU memory.</param>
        /// <param name="nWeightOffset">Specifies an offset into the weight memory (in items, not bytes).</param>
        /// <param name="hTopDesc">Specifies a handle to the top tensor descriptor.</param>
        /// <param name="hTopDiff">Specifies a handle to the top diff in GPU memory.</param>
        /// <param name="nTopOffset">Specifies an offset into the top memory (in items, not bytes).</param>
        /// <param name="hConvDesc">Specifies a handle to the convolution descriptor.</param>
        /// <param name="algoBwd">Specifies the algorithm to use when performing the backward operation.</param>
        /// <param name="hWorkspace">Specifies a handle to the GPU memory to use for the workspace.</param>
        /// <param name="nWorkspaceOffset">Specifies an offset into the workspace memory.</param>
        /// <param name="lWorkspaceSize">Specifies the size of the workspace memory (in bytes).</param>
        /// <param name="fBeta">Specifies a scaling factor applied to the prior destination value.</param>
        /// <param name="hBottomDesc">Specifies a handle to the bottom tensor descriptor.</param>
        /// <param name="hBottomDiff">Specifies a handle to the bottom diff in GPU memory.</param>
        /// <param name="nBottomOffset">Specifies an offset into the bottom memory (in items, not bytes).</param>
        /// <param name="bSyncStream">Optionally, specifies whether or not to syncrhonize the stream. The default = <i>true</i>.</param>
        public void ConvolutionBackwardData(long hCuDnn, T fAlpha, long hFilterDesc, long hWeight, int nWeightOffset, long hTopDesc, long hTopDiff, int nTopOffset, long hConvDesc, CONV_BWD_DATA_ALGO algoBwd, long hWorkspace, int nWorkspaceOffset, ulong lWorkspaceSize, T fBeta, long hBottomDesc, long hBottomDiff, int nBottomOffset, bool bSyncStream = true)
        {
            if (m_dt == DataType.DOUBLE)
                m_cuda.RunDouble((int)m_hKernel, (int)CUDAFN.BWD_CONV_DATA, m_param.AsDouble(hCuDnn, convertD(fAlpha), hFilterDesc, hWeight, nWeightOffset, hTopDesc, hTopDiff, nTopOffset, hConvDesc, (double)algoBwd, hWorkspace, nWorkspaceOffset, lWorkspaceSize, convertD(fBeta), hBottomDesc, hBottomDiff, nBottomOffset, (bSyncStream) ? 1 : 0));
            else
                m_cuda.RunFloat((int)m_hKernel, (int)CUDAFN.BWD_CONV_DATA, m_param.AsFloat(hCuDnn, convertF(fAlpha), hFilterDesc, hWeight, nWeightOffset, hTopDesc, hTopDiff, nTopOffset, hConvDesc, (float)algoBwd, hWorkspace, nWorkspaceOffset, lWorkspaceSize, convertF(fBeta), hBottomDesc, hBottomDiff, nBottomOffset, (bSyncStream) ? 1 : 0));
        }

        /// <summary>
        /// Create a new instance of a pooling descriptor for use with [NVIDIA's cuDnn](https://developer.nvidia.com/cudnn).
        /// </summary>
        /// <returns>The pooling descriptor handle is returned.</returns>
        public long CreatePoolingDesc()
        {
            if (m_dt == DataType.DOUBLE)
            {
                double[] rg = m_cuda.RunDouble((int)m_hKernel, (int)CUDAFN.CREATE_POOLDESC, null);
                return (long)rg[0];
            }
            else
            {
                float[] rg = m_cuda.RunFloat((int)m_hKernel, (int)CUDAFN.CREATE_POOLDESC, null);
                return (long)rg[0];
            }
        }

        /// <summary>
        /// Free a pooling descriptor instance.
        /// </summary>
        /// <param name="h">Specifies the handle to the pooling descriptor instance.</param>
        public void FreePoolingDesc(long h)
        {
            if (m_dt == DataType.DOUBLE)
                m_cuda.RunDouble((int)m_hKernel, (int)CUDAFN.FREE_POOLDESC, m_param.AsDouble(h));
            else
                m_cuda.RunFloat((int)m_hKernel, (int)CUDAFN.FREE_POOLDESC, m_param.AsFloat(h));
        }

        /// <summary>
        /// Set the values of a pooling descriptor.
        /// </summary>
        /// <param name="hHandle">Specifies the handle to the convolution descriptor.</param>
        /// <param name="method">Specifies the pooling method to use.</param>
        /// <param name="h">Specifies the pooling area height.</param>
        /// <param name="w">Specifies the pooling area width.</param>
        /// <param name="hPad">Specifies the height padding.</param>
        /// <param name="wPad">Specifies the width padding.</param>
        /// <param name="hStride">Specifies the height stride.</param>
        /// <param name="wStride">Specifies the width stride.</param>
        public void SetPoolingDesc(long hHandle, PoolingMethod method, int h, int w, int hPad, int wPad, int hStride, int wStride)
        {
            if (m_dt == DataType.DOUBLE)
                m_cuda.RunDouble((int)m_hKernel, (int)CUDAFN.SET_POOLDESC, m_param.AsDouble(hHandle, (int)method, h, w, hPad, wPad, hStride, wStride));
            else
                m_cuda.RunFloat((int)m_hKernel, (int)CUDAFN.SET_POOLDESC, m_param.AsFloat(hHandle, (int)method, h, w, hPad, wPad, hStride, wStride));
        }

        /// <summary>
        /// Perform a pooling forward pass.
        /// </summary>
        /// <param name="hCuDnn">Specifies a handle to the instance of cuDnn.</param>
        /// <param name="hPoolingDesc">Specifies a handle to the pooling descriptor.</param>
        /// <param name="fAlpha">Specifies a scaling factor applied to the result.</param>
        /// <param name="hBottomDesc">Specifies a handle to the bottom tensor descriptor.</param>
        /// <param name="hBottomData">Specifies a handle to the bottom data in GPU memory.</param>
        /// <param name="fBeta">Specifies a scaling factor applied to the prior destination value.</param>
        /// <param name="hTopDesc">Specifies a handle to the top tensor descriptor.</param>
        /// <param name="hTopData">Specifies a handle to the top data in GPU memory.</param>
        public void PoolingForward(long hCuDnn, long hPoolingDesc, T fAlpha, long hBottomDesc, long hBottomData, T fBeta, long hTopDesc, long hTopData)
        {
            if (m_dt == DataType.DOUBLE)
                m_cuda.RunDouble((int)m_hKernel, (int)CUDAFN.FWD_POOL, m_param.AsDouble(hCuDnn, hPoolingDesc, convertD(fAlpha), hBottomDesc, hBottomData, convertD(fBeta), hTopDesc, hTopData));
            else
                m_cuda.RunFloat((int)m_hKernel, (int)CUDAFN.FWD_POOL, m_param.AsFloat(hCuDnn, hPoolingDesc, convertF(fAlpha), hBottomDesc, hBottomData, convertF(fBeta), hTopDesc, hTopData));
        }

        /// <summary>
        /// Perform a pooling backward pass.
        /// </summary>
        /// <param name="hCuDnn">Specifies a handle to the instance of cuDnn.</param>
        /// <param name="hPoolingDesc">Specifies a handle to the pooling descriptor.</param>
        /// <param name="fAlpha">Specifies a scaling factor applied to the result.</param>
        /// <param name="hTopDataDesc">Specifies a handle to the top data tensor descriptor.</param>
        /// <param name="hTopData">Specifies a handle to the top data in GPU memory.</param>
        /// <param name="hTopDiffDesc">Specifies a handle to the top diff tensor descriptor.</param>
        /// <param name="hTopDiff">Specifies a handle to the top diff in GPU memory.</param>
        /// <param name="hBottomDataDesc">Specifies a handle to the bottom data tensor descriptor.</param>
        /// <param name="hBottomData">Specifies a handle to the bottom data in GPU memory.</param>
        /// <param name="fBeta">Specifies a scaling factor applied to the prior destination value.</param>
        /// <param name="hBottomDiffDesc">Specifies a handle to the bottom diff tensor descriptor.</param>
        /// <param name="hBottomDiff">Specifies a handle to the bottom diff in GPU memory.</param>
        public void PoolingBackward(long hCuDnn, long hPoolingDesc, T fAlpha, long hTopDataDesc, long hTopData, long hTopDiffDesc, long hTopDiff, long hBottomDataDesc, long hBottomData, T fBeta, long hBottomDiffDesc, long hBottomDiff)
        {
            if (m_dt == DataType.DOUBLE)
                m_cuda.RunDouble((int)m_hKernel, (int)CUDAFN.BWD_POOL, m_param.AsDouble(hCuDnn, hPoolingDesc, convertD(fAlpha), hTopDataDesc, hTopData, hTopDiffDesc, hTopDiff, hBottomDataDesc, hBottomData, convertD(fBeta), hBottomDiffDesc, hBottomDiff));
            else
                m_cuda.RunFloat((int)m_hKernel, (int)CUDAFN.BWD_POOL, m_param.AsFloat(hCuDnn, hPoolingDesc, convertF(fAlpha), hTopDataDesc, hTopData, hTopDiffDesc, hTopDiff, hBottomDataDesc, hBottomData, convertF(fBeta), hBottomDiffDesc, hBottomDiff));
        }

        /// <summary>
        /// Derive the batch norm descriptors for both the forward and backward passes.
        /// </summary>
        /// <param name="hFwdScaleBiasMeanVarDesc">Specifies a handle to the scale bias mean var tensor descriptor for the forward pass.</param>
        /// <param name="hFwdBottomDesc">Specifies a handle to the forward bottom tensor descriptor.</param>
        /// <param name="hBwdScaleBiasMeanVarDesc">Specifies a handle to the scale bias mean var tensor descriptor for the backward pass.</param>
        /// <param name="hBwdBottomDesc">Specifies a handle to the backward bottom tensor descriptor.</param>
        /// <param name="mode"></param>
        public void DeriveBatchNormDesc(long hFwdScaleBiasMeanVarDesc, long hFwdBottomDesc, long hBwdScaleBiasMeanVarDesc, long hBwdBottomDesc, BATCHNORM_MODE mode)
        {
            if (m_dt == DataType.DOUBLE)
                m_cuda.RunDouble((int)m_hKernel, (int)CUDAFN.DERIVE_BNDESC, m_param.AsDouble(hFwdScaleBiasMeanVarDesc, hFwdBottomDesc, hBwdScaleBiasMeanVarDesc, hBwdBottomDesc, (int)mode));
            else
                m_cuda.RunFloat((int)m_hKernel, (int)CUDAFN.DERIVE_BNDESC, m_param.AsFloat(hFwdScaleBiasMeanVarDesc, hFwdBottomDesc, hBwdScaleBiasMeanVarDesc, hBwdBottomDesc, (int)mode));
        }

        /// <summary>
        /// Run the batch norm forward pass.
        /// </summary>
        /// <param name="hCuDnn">Specifies a handle to the instance of cuDnn.</param>
        /// <param name="mode">Specifies the batch normalization mode.</param>
        /// <param name="fAlpha">Specifies the alpha value.</param>
        /// <param name="fBeta">Specifies the beta value.</param>
        /// <param name="hFwdBottomDesc">Specifies a handle to the forward bottom tensor descriptor.</param>
        /// <param name="hBottomData">Specifies a handle to the bottom data tensor.</param>
        /// <param name="hFwdTopDesc">Specifies a handle to the forward top tensor descriptor.</param>
        /// <param name="hTopData">Specifies a handle to the top tensor.</param>
        /// <param name="hFwdScaleBiasMeanVarDesc">Specifies a handle to the forward scale bias mean variance descriptor.</param>
        /// <param name="hScaleData">Specifies a handle to the scale tensor.</param>
        /// <param name="hBiasData">Specifies a handle to the bias tensor.</param>
        /// <param name="dfFactor">Specifies a scaling factor.</param>
        /// <param name="hGlobalMean">Specifies a handle to the global mean tensor.</param>
        /// <param name="hGlobalVar">Specifies a handle to the global variance tensor.</param>
        /// <param name="dfEps">Specifies the epsilon value to avoid dividing by zero.</param>
        /// <param name="hSaveMean">Specifies a handle to the saved mean tensor.</param>
        /// <param name="hSaveInvVar">Specifies a handle to the saved variance tensor.</param>
        /// <param name="bTraining">Specifies that this is a training pass when <i>true</i>, and a testing pass when <i>false</i>.</param>
        public void BatchNormForward(long hCuDnn, BATCHNORM_MODE mode, T fAlpha, T fBeta, long hFwdBottomDesc, long hBottomData, long hFwdTopDesc, long hTopData, long hFwdScaleBiasMeanVarDesc, long hScaleData, long hBiasData, double dfFactor, long hGlobalMean, long hGlobalVar, double dfEps, long hSaveMean, long hSaveInvVar, bool bTraining)
        {
            if (m_dt == DataType.DOUBLE)
                m_cuda.RunDouble((int)m_hKernel, (int)CUDAFN.FWD_BN, m_param.AsDouble(hCuDnn, (int)mode, convertD(fAlpha), convertD(fBeta), hFwdBottomDesc, hBottomData, hFwdTopDesc, hTopData, hFwdScaleBiasMeanVarDesc, hScaleData, hBiasData, dfFactor, hGlobalMean, hGlobalVar, dfEps, hSaveMean, hSaveInvVar, (bTraining) ? 1 : 0));
            else
                m_cuda.RunFloat((int)m_hKernel, (int)CUDAFN.FWD_BN, m_param.AsFloat(hCuDnn, (int)mode, convertF(fAlpha), convertF(fBeta), hFwdBottomDesc, hBottomData, hFwdTopDesc, hTopData, hFwdScaleBiasMeanVarDesc, hScaleData, hBiasData, (float)dfFactor, hGlobalMean, hGlobalVar, (float)dfEps, hSaveMean, hSaveInvVar, (bTraining) ? 1 : 0));
        }

        /// <summary>
        /// Run the batch norm backward pass.
        /// </summary>
        /// <param name="hCuDnn">Specifies a handle to the instance of cuDnn.</param>
        /// <param name="mode">Specifies the batch normalization mode.</param>
        /// <param name="fAlphaDiff">Specifies the alpha value applied to the diff.</param>
        /// <param name="fBetaDiff">Specifies the beta value applied to the diff.</param>
        /// <param name="fAlphaParamDiff">Specifies the alpha value applied to the param diff.</param>
        /// <param name="fBetaParamDiff">Specifies the beta value applied to the param diff.</param>
        /// <param name="hBwdBottomDesc">Specifies a handle to the backward bottom tensor descriptor.</param>
        /// <param name="hBottomData">Specifies a handle to the bottom data tensor.</param>
        /// <param name="hTopDiffDesc">Specifies a handle to the top diff tensor descriptor.</param>
        /// <param name="hTopDiff">Specifies a handle to the top diff tensor.</param>
        /// <param name="hBottomDiffDesc">Specifies a handle to the bottom diff tensor descriptor.</param>
        /// <param name="hBottomDiff">Specifies a handle to the bottom diff tensor.</param>
        /// <param name="hBwdScaleBiasMeanVarDesc">Specifies a handle to the backward scale bias mean var descriptor.</param>
        /// <param name="hScaleData">Specifies a handle to the scale data tensor.</param>
        /// <param name="hScaleDiff">Specifies a handle to the scale diff tensor.</param>
        /// <param name="hBiasDiff">Specifies a handle to the bias diff tensor.</param>
        /// <param name="dfEps">Specifies the epsilon value.</param>
        /// <param name="hSaveMean">Specifies a handle to the saved mean tensor.</param>
        /// <param name="hSaveInvVar">Specifies a handle to the saved variance tensor.</param>
        public void BatchNormBackward(long hCuDnn, BATCHNORM_MODE mode, T fAlphaDiff, T fBetaDiff, T fAlphaParamDiff, T fBetaParamDiff, long hBwdBottomDesc, long hBottomData, long hTopDiffDesc, long hTopDiff, long hBottomDiffDesc, long hBottomDiff, long hBwdScaleBiasMeanVarDesc, long hScaleData, long hScaleDiff, long hBiasDiff, double dfEps, long hSaveMean, long hSaveInvVar)
        {
            if (m_dt == DataType.DOUBLE)
                m_cuda.RunDouble((int)m_hKernel, (int)CUDAFN.BWD_BN, m_param.AsDouble(hCuDnn, (int)mode, convertD(fAlphaDiff), convertD(fBetaDiff), convertD(fAlphaParamDiff), convertD(fBetaParamDiff), hBwdBottomDesc, hBottomData, hTopDiffDesc, hTopDiff, hBottomDiffDesc, hBottomDiff, hBwdScaleBiasMeanVarDesc, hScaleData, hScaleDiff, hBiasDiff, dfEps, hSaveMean, hSaveInvVar));
            else
                m_cuda.RunFloat((int)m_hKernel, (int)CUDAFN.BWD_BN, m_param.AsFloat(hCuDnn, (int)mode, convertF(fAlphaDiff), convertF(fBetaDiff), convertF(fAlphaParamDiff), convertF(fBetaParamDiff), hBwdBottomDesc, hBottomData, hTopDiffDesc, hTopDiff, hBottomDiffDesc, hBottomDiff, hBwdScaleBiasMeanVarDesc, hScaleData, hScaleDiff, hBiasDiff, (float)dfEps, hSaveMean, hSaveInvVar));
        }

        /// <summary>
        /// Create a new instance of a dropout descriptor for use with [NVIDIA's cuDnn](https://developer.nvidia.com/cudnn).
        /// </summary>
        /// <returns>The dropout descriptor handle is returned.</returns>
        public long CreateDropoutDesc()
        {
            if (m_dt == DataType.DOUBLE)
            {
                double[] rg = m_cuda.RunDouble((int)m_hKernel, (int)CUDAFN.CREATE_DROPOUTDESC, null);
                return (long)rg[0];
            }
            else
            {
                float[] rg = m_cuda.RunFloat((int)m_hKernel, (int)CUDAFN.CREATE_DROPOUTDESC, null);
                return (long)rg[0];
            }
        }

        /// <summary>
        /// Free a dropout descriptor instance.
        /// </summary>
        /// <param name="h">Specifies the handle to the dropout descriptor instance.</param>
        public void FreeDropoutDesc(long h)
        {
            if (m_dt == DataType.DOUBLE)
                m_cuda.RunDouble((int)m_hKernel, (int)CUDAFN.FREE_DROPOUTDESC, m_param.AsDouble(h));
            else
                m_cuda.RunFloat((int)m_hKernel, (int)CUDAFN.FREE_DROPOUTDESC, m_param.AsFloat(h));
        }

        /// <summary>
        /// Set the dropout descriptor values.
        /// </summary>
        /// <param name="hCuDnn">Specifies a handle to the instance of cuDnn.</param>
        /// <param name="hDropoutDesc">Specifies a handle to the dropout descriptor.</param>
        /// <param name="dfDropout">Specifies the droput probability (0.5 = 50%).</param>
        /// <param name="hStates">Specifies a handle to the state data in GPU memory.</param>
        /// <param name="lSeed">Specifies the random number-generator seed.</param>
        public void SetDropoutDesc(long hCuDnn, long hDropoutDesc, double dfDropout, long hStates, long lSeed)
        {
            if (m_dt == DataType.DOUBLE)
                m_cuda.RunDouble((int)m_hKernel, (int)CUDAFN.SET_DROPOUTDESC, m_param.AsDouble(hCuDnn, hDropoutDesc, dfDropout, hStates, lSeed));
            else
                m_cuda.RunFloat((int)m_hKernel, (int)CUDAFN.SET_DROPOUTDESC, m_param.AsFloat(hCuDnn, hDropoutDesc, (float)dfDropout, hStates, lSeed));
        }

        /// <summary>
        /// Query the dropout state and reserved counts.
        /// </summary>
        /// <param name="hCuDnn">Specifies a handle to the instance of cuDnn.</param>
        /// <param name="hBottomDesc">Specifies a handle to the bottom tensor descriptor.</param>
        /// <param name="ulStateCount">Returns the state count.</param>
        /// <param name="ulReservedCount">Returns the reserved count.</param>
        public void GetDropoutInfo(long hCuDnn, long hBottomDesc, out ulong ulStateCount, out ulong ulReservedCount)
        {
            if (m_dt == DataType.DOUBLE)
            {
                double[] rg = m_cuda.RunDouble((int)m_hKernel, (int)CUDAFN.GET_DROPOUT_INFO, m_param.AsDouble(hCuDnn, hBottomDesc));
                ulStateCount = (ulong)Math.Round(rg[0]/sizeof(double), 0, MidpointRounding.AwayFromZero);
                ulReservedCount = (ulong)Math.Round(rg[1]/sizeof(double), 0, MidpointRounding.AwayFromZero);  
            }
            else
            {
                float[] rg = m_cuda.RunFloat((int)m_hKernel, (int)CUDAFN.GET_DROPOUT_INFO, m_param.AsFloat(hCuDnn, hBottomDesc));
                ulStateCount = (ulong)Math.Round(rg[0] / sizeof(float), 0, MidpointRounding.AwayFromZero);
                ulReservedCount = (ulong)Math.Round(rg[1] / sizeof(float), 0, MidpointRounding.AwayFromZero);
            }
        }

        /// <summary>
        /// Performs a dropout forward pass.
        /// </summary>
        /// <param name="hCuDnn">Specifies a handle to the instance of cuDnn.</param>
        /// <param name="hDropoutDesc">Specifies a handle to the dropout descriptor.</param>
        /// <param name="hBottomDesc">Specifies a handle to the bottom tensor descriptor.</param>
        /// <param name="hBottomData">Specifies a handle to the bottom data in GPU memory.</param>
        /// <param name="hTopDesc">Specifies a handle to the top tensor descriptor.</param>
        /// <param name="hTopData">Specifies a handle to the top data in GPU memory.</param>
        /// <param name="hReserved">Specifies a handle to the reseved data in GPU memory.</param>
        public void DropoutForward(long hCuDnn, long hDropoutDesc, long hBottomDesc, long hBottomData, long hTopDesc, long hTopData, long hReserved)
        {
            if (m_dt == DataType.DOUBLE)
                m_cuda.RunDouble((int)m_hKernel, (int)CUDAFN.FWD_DROPOUT, m_param.AsDouble(hCuDnn, hDropoutDesc, hBottomDesc, hBottomData, hTopDesc, hTopData, hReserved));
            else
                m_cuda.RunFloat((int)m_hKernel, (int)CUDAFN.FWD_DROPOUT, m_param.AsFloat(hCuDnn, hDropoutDesc, hBottomDesc, hBottomData, hTopDesc, hTopData, hReserved));
        }

        /// <summary>
        /// Performs a dropout backward pass.
        /// </summary>
        /// <param name="hCuDnn">Specifies a handle to the instance of cuDnn.</param>
        /// <param name="hDropoutDesc">Specifies a handle to the dropout descriptor.</param>
        /// <param name="hTopDesc">Specifies a handle to the top tensor descriptor.</param>
        /// <param name="hTop">Specifies a handle to the top data in GPU memory.</param>
        /// <param name="hBottomDesc">Specifies a handle to the bottom tensor descriptor.</param>
        /// <param name="hBottom">Specifies a handle to the bottom data in GPU memory.</param>
        /// <param name="hReserved">Specifies a handle to the reseved data in GPU memory.</param>
        public void DropoutBackward(long hCuDnn, long hDropoutDesc, long hTopDesc, long hTop, long hBottomDesc, long hBottom, long hReserved)
        {
            if (m_dt == DataType.DOUBLE)
                m_cuda.RunDouble((int)m_hKernel, (int)CUDAFN.BWD_DROPOUT, m_param.AsDouble(hCuDnn, hDropoutDesc, hTopDesc, hTop, hBottomDesc, hBottom, hReserved));
            else
                m_cuda.RunFloat((int)m_hKernel, (int)CUDAFN.BWD_DROPOUT, m_param.AsFloat(hCuDnn, hDropoutDesc, hTopDesc, hTop, hBottomDesc, hBottom, hReserved));
        }

        /// <summary>
        /// Create a new instance of a LRN descriptor for use with [NVIDIA's cuDnn](https://developer.nvidia.com/cudnn).
        /// </summary>
        /// <returns>The LRN descriptor handle is returned.</returns>
        public long CreateLRNDesc()
        {
            if (m_dt == DataType.DOUBLE)
            {
                double[] rg = m_cuda.RunDouble((int)m_hKernel, (int)CUDAFN.CREATE_LRNDESC, null);
                return (long)rg[0];
            }
            else
            {
                float[] rg = m_cuda.RunFloat((int)m_hKernel, (int)CUDAFN.CREATE_LRNDESC, null);
                return (long)rg[0];
            }
        }

        /// <summary>
        /// Free a LRN descriptor instance.
        /// </summary>
        /// <param name="h">Specifies the handle to the LRN descriptor instance.</param>
        public void FreeLRNDesc(long h)
        {
            if (m_dt == DataType.DOUBLE)
                m_cuda.RunDouble((int)m_hKernel, (int)CUDAFN.FREE_LRNDESC, m_param.AsDouble(h));
            else
                m_cuda.RunFloat((int)m_hKernel, (int)CUDAFN.FREE_LRNDESC, m_param.AsFloat(h));
        }

        /// <summary>
        /// Set the LRN descriptor values.
        /// </summary>
        /// <param name="hHandle">Specifies a handle to an LRN descriptor.</param>
        /// <param name="nSize">Specifies the normalization window width.  Default = 5.</param>
        /// <param name="fAlpha">Specifies the alpha variance.  Caffe default = 1.0; cuDnn default = 1e-4.</param>
        /// <param name="fBeta">Specifies the beta power parameter.  Caffe and cuDnn default = 0.75.</param>
        /// <param name="fK">Specifies the normalization 'k' parameter.  Caffe default = 1.0; cuDnn default = 2.0.</param>
        public void SetLRNDesc(long hHandle, uint nSize, double fAlpha, double fBeta, double fK)
        {
            if (m_dt == DataType.DOUBLE)
                m_cuda.RunDouble((int)m_hKernel, (int)CUDAFN.SET_LRNDESC, m_param.AsDouble(hHandle, nSize, fAlpha, fBeta, fK));
            else
                m_cuda.RunFloat((int)m_hKernel, (int)CUDAFN.SET_LRNDESC, m_param.AsFloat(hHandle, nSize, (float)fAlpha, (float)fBeta, (float)fK));
        }

        /// <summary>
        /// Perform LRN cross channel forward pass.
        /// </summary>
        /// <param name="hCuDnn">Specifies a handle to the instance of cuDnn.</param>
        /// <param name="hNormDesc">Specifies a handle to an LRN descriptor.</param>
        /// <param name="fAlpha">Specifies a scaling factor applied to the result.</param>
        /// <param name="hBottomDesc">Specifies a handle to the bottom tensor descriptor.</param>
        /// <param name="hBottomData">Specifies a handle to the bottom data in GPU memory.</param>
        /// <param name="fBeta">Specifies a scaling factor applied to the prior destination value.</param>
        /// <param name="hTopDesc">Specifies a handle to the top tensor descriptor.</param>
        /// <param name="hTopData">Specifies a handle to the top data in GPU memory.</param>
        public void LRNCrossChannelForward(long hCuDnn, long hNormDesc, T fAlpha, long hBottomDesc, long hBottomData, T fBeta, long hTopDesc, long hTopData)
        {
            if (m_dt == DataType.DOUBLE)
                m_cuda.RunDouble((int)m_hKernel, (int)CUDAFN.LRN_CC_FWD, m_param.AsDouble(hCuDnn, hNormDesc, convertD(fAlpha), hBottomDesc, hBottomData, convertD(fBeta), hTopDesc, hTopData));
            else
                m_cuda.RunFloat((int)m_hKernel, (int)CUDAFN.LRN_CC_FWD, m_param.AsFloat(hCuDnn, hNormDesc, convertF(fAlpha), hBottomDesc, hBottomData, convertF(fBeta), hTopDesc, hTopData));
        }

        /// <summary>
        /// Perform LRN cross channel backward pass.
        /// </summary>
        /// <param name="hCuDnn">Specifies a handle to the instance of cuDnn.</param>
        /// <param name="hNormDesc">Specifies a handle to an LRN descriptor.</param>
        /// <param name="fAlpha">Specifies a scaling factor applied to the result.</param>
        /// <param name="hTopDataDesc">Specifies a handle to the top data tensor descriptor.</param>
        /// <param name="hTopData">Specifies a handle to the top data in GPU memory.</param>
        /// <param name="hTopDiffDesc">Specifies a handle to the top diff tensor descriptor.</param>
        /// <param name="hTopDiff">Specifies a handle to the top diff in GPU memory.</param>
        /// <param name="hBottomDataDesc">Specifies a handle to the bottom data tensor descriptor.</param>
        /// <param name="hBottomData">Specifies a handle to the bottom data in GPU memory.</param>
        /// <param name="fBeta">Specifies a scaling factor applied to the prior destination value.</param>
        /// <param name="hBottomDiffDesc">Specifies a handle to the bottom diff descriptor.</param>
        /// <param name="hBottomDiff">Specifies a handle to the bottom diff in GPU memory.</param>
        public void LRNCrossChannelBackward(long hCuDnn, long hNormDesc, T fAlpha, long hTopDataDesc, long hTopData, long hTopDiffDesc, long hTopDiff, long hBottomDataDesc, long hBottomData, T fBeta, long hBottomDiffDesc, long hBottomDiff)
        {
            if (m_dt == DataType.DOUBLE)
                m_cuda.RunDouble((int)m_hKernel, (int)CUDAFN.LRN_CC_BWD, m_param.AsDouble(hCuDnn, hNormDesc, convertD(fAlpha), hTopDataDesc, hTopData, hTopDiffDesc, hTopDiff, hBottomDataDesc, hBottomData, convertD(fBeta), hBottomDiffDesc, hBottomDiff));
            else
                m_cuda.RunFloat((int)m_hKernel, (int)CUDAFN.LRN_CC_BWD, m_param.AsFloat(hCuDnn, hNormDesc, convertF(fAlpha), hTopDataDesc, hTopData, hTopDiffDesc, hTopDiff, hBottomDataDesc, hBottomData, convertF(fBeta), hBottomDiffDesc, hBottomDiff));
        }

        /// <summary>
        /// Performs a Devisive Normalization forward pass.
        /// </summary>
        /// <remarks>
        /// See [What is the Best Feature Learning Procedure in Hierarchical Recognition Architectures?](https://arxiv.org/abs/1606.01535) by Jarrett, et al.
        /// </remarks>
        /// <param name="hCuDnn">Specifies a handle to the instance of cuDnn.</param>
        /// <param name="hNormDesc">Specifies a handle to an LRN descriptor.</param>
        /// <param name="fAlpha">Specifies a scaling factor applied to the result.</param>
        /// <param name="hBottomDataDesc">Specifies a handle to the bottom data tensor descriptor.</param>
        /// <param name="hBottomData">Specifies a handle to the bottom data in GPU memory.</param>
        /// <param name="hTemp1">Temporary data in GPU memory.</param>
        /// <param name="hTemp2">Temporary data in GPU memory.</param>
        /// <param name="fBeta">Specifies a scaling factor applied to the prior destination value.</param>
        /// <param name="hTopDataDesc">Specifies a handle to the top data tensor descriptor.</param>
        /// <param name="hTopData">Specifies a handle to the top data in GPU memory.</param>
        public void DivisiveNormalizationForward(long hCuDnn, long hNormDesc, T fAlpha, long hBottomDataDesc, long hBottomData, long hTemp1, long hTemp2, T fBeta, long hTopDataDesc, long hTopData)
        {
            if (m_dt == DataType.DOUBLE)
                m_cuda.RunDouble((int)m_hKernel, (int)CUDAFN.LCN_CC_FWD, m_param.AsDouble(hCuDnn, hNormDesc, convertD(fAlpha), hBottomDataDesc, hBottomData, hTemp1, hTemp2, convertD(fBeta), hTopDataDesc, hTopData));
            else
                m_cuda.RunFloat((int)m_hKernel, (int)CUDAFN.LCN_CC_FWD, m_param.AsFloat(hCuDnn, hNormDesc, convertF(fAlpha), hBottomDataDesc, hBottomData, hTemp1, hTemp2, convertF(fBeta), hTopDataDesc, hTopData));
        }

        /// <summary>
        /// Performs a Devisive Normalization backward pass.
        /// </summary>
        /// <remarks>
        /// See [What is the Best Feature Learning Procedure in Hierarchical Recognition Architectures?](https://arxiv.org/abs/1606.01535) by Jarrett, et al.
        /// </remarks>
        /// <param name="hCuDnn">Specifies a handle to the instance of cuDnn.</param>
        /// <param name="hNormDesc">Specifies a handle to an LRN descriptor.</param>
        /// <param name="fAlpha">Specifies a scaling factor applied to the result.</param>
        /// <param name="hBottomDataDesc">Specifies a handle to the bottom data tensor descriptor.</param>
        /// <param name="hBottomData">Specifies a handle to the bottom data in GPU memory.</param>
        /// <param name="hTopDiff">Specifies a handle to the top diff in GPU memory.</param>
        /// <param name="hTemp1">Temporary data in GPU memory.</param>
        /// <param name="hTemp2">Temporary data in GPU memory.</param>
        /// <param name="fBeta">Specifies a scaling factor applied to the prior destination value.</param>
        /// <param name="hBottomDiffDesc">Specifies a handle to the bottom diff tensor descriptor.</param>
        /// <param name="hBottomDiff">Specifies a handle to the bottom diff in GPU memory.</param>
        public void DivisiveNormalizationBackward(long hCuDnn, long hNormDesc, T fAlpha, long hBottomDataDesc, long hBottomData, long hTopDiff, long hTemp1, long hTemp2, T fBeta, long hBottomDiffDesc, long hBottomDiff)
        {
            if (m_dt == DataType.DOUBLE)
                m_cuda.RunDouble((int)m_hKernel, (int)CUDAFN.LCN_CC_BWD, m_param.AsDouble(hCuDnn, hNormDesc, convertD(fAlpha), hBottomDataDesc, hBottomData, hTopDiff, hTemp1, hTemp2, convertD(fBeta), hBottomDiffDesc, hBottomDiff));
            else
                m_cuda.RunFloat((int)m_hKernel, (int)CUDAFN.LCN_CC_BWD, m_param.AsFloat(hCuDnn, hNormDesc, convertF(fAlpha), hBottomDataDesc, hBottomData, hTopDiff, hTemp1, hTemp2, convertF(fBeta), hBottomDiffDesc, hBottomDiff));
        }

        /// <summary>
        /// Perform a Tanh forward pass.
        /// </summary>
        /// <param name="hCuDnn">Specifies a handle to the instance of cuDnn.</param>
        /// <param name="fAlpha">Specifies a scaling factor applied to the result.</param>
        /// <param name="hBottomDataDesc">Specifies a handle to the bottom data tensor descriptor.</param>
        /// <param name="hBottomData">Specifies a handle to the bottom data in GPU memory.</param>
        /// <param name="fBeta">Specifies a scaling factor applied to the prior destination value.</param>
        /// <param name="hTopDataDesc">Specifies a handle to the top data tensor descriptor.</param>
        /// <param name="hTopData">Specifies a handle to the top data in GPU memory.</param>
        public void TanhForward(long hCuDnn, T fAlpha, long hBottomDataDesc, long hBottomData, T fBeta, long hTopDataDesc, long hTopData)
        {
            if (m_dt == DataType.DOUBLE)
                m_cuda.RunDouble((int)m_hKernel, (int)CUDAFN.TANH_FWD, m_param.AsDouble(hCuDnn, convertD(fAlpha), hBottomDataDesc, hBottomData, convertD(fBeta), hTopDataDesc, hTopData));
            else
                m_cuda.RunFloat((int)m_hKernel, (int)CUDAFN.TANH_FWD, m_param.AsFloat(hCuDnn, convertF(fAlpha), hBottomDataDesc, hBottomData, convertF(fBeta), hTopDataDesc, hTopData));
        }

        /// <summary>
        /// Perform a Tanh backward pass.
        /// </summary>
        /// <param name="hCuDnn">Specifies a handle to the instance of cuDnn.</param>
        /// <param name="fAlpha">Specifies a scaling factor applied to the result.</param>
        /// <param name="hTopDataDesc">Specifies a handle to the top data tensor descriptor.</param>
        /// <param name="hTopData">Specifies a handle to the top data in GPU memory.</param>
        /// <param name="hTopDiffDesc">Specifies a handle to the top diff tensor descriptor</param>
        /// <param name="hTopDiff">Specifies a handle to the top diff in GPU memory.</param>
        /// <param name="hBottomDataDesc">Specifies a handle to the bottom data tensor descriptor.</param>
        /// <param name="hBottomData">Specifies a handle to the bottom data in GPU memory.</param>
        /// <param name="fBeta">Specifies a scaling factor applied to the prior destination value.</param>
        /// <param name="hBottomDiffDesc">Specifies a handle to the bottom diff tensor descriptor.</param>
        /// <param name="hBottomDiff">Specifies a handle to the bottom diff in GPU memory.</param>
        public void TanhBackward(long hCuDnn, T fAlpha, long hTopDataDesc, long hTopData, long hTopDiffDesc, long hTopDiff, long hBottomDataDesc, long hBottomData, T fBeta, long hBottomDiffDesc, long hBottomDiff)
        {
            if (m_dt == DataType.DOUBLE)
                m_cuda.RunDouble((int)m_hKernel, (int)CUDAFN.TANH_BWD, m_param.AsDouble(hCuDnn, convertD(fAlpha), hTopDataDesc, hTopData, hTopDiffDesc, hTopDiff, hBottomDataDesc, hBottomData, convertD(fBeta), hBottomDiffDesc, hBottomDiff));
            else
                m_cuda.RunFloat((int)m_hKernel, (int)CUDAFN.TANH_BWD, m_param.AsFloat(hCuDnn, convertF(fAlpha), hTopDataDesc, hTopData, hTopDiffDesc, hTopDiff, hBottomDataDesc, hBottomData, convertF(fBeta), hBottomDiffDesc, hBottomDiff));
        }

        /// <summary>
        /// Perform a Elu forward pass.
        /// </summary>
        /// <param name="hCuDnn">Specifies a handle to the instance of cuDnn.</param>
        /// <param name="fAlpha">Specifies a scaling factor applied to the result.</param>
        /// <param name="hBottomDataDesc">Specifies a handle to the bottom data tensor descriptor.</param>
        /// <param name="hBottomData">Specifies a handle to the bottom data in GPU memory.</param>
        /// <param name="fBeta">Specifies a scaling factor applied to the prior destination value.</param>
        /// <param name="hTopDataDesc">Specifies a handle to the top data tensor descriptor.</param>
        /// <param name="hTopData">Specifies a handle to the top data in GPU memory.</param>
        public void EluForward(long hCuDnn, T fAlpha, long hBottomDataDesc, long hBottomData, T fBeta, long hTopDataDesc, long hTopData)
        {
            if (m_dt == DataType.DOUBLE)
                m_cuda.RunDouble((int)m_hKernel, (int)CUDAFN.ELU_FWD, m_param.AsDouble(hCuDnn, convertD(fAlpha), hBottomDataDesc, hBottomData, convertD(fBeta), hTopDataDesc, hTopData));
            else
                m_cuda.RunFloat((int)m_hKernel, (int)CUDAFN.ELU_FWD, m_param.AsFloat(hCuDnn, convertF(fAlpha), hBottomDataDesc, hBottomData, convertF(fBeta), hTopDataDesc, hTopData));
        }

        /// <summary>
        /// Perform a Elu backward pass.
        /// </summary>
        /// <param name="hCuDnn">Specifies a handle to the instance of cuDnn.</param>
        /// <param name="fAlpha">Specifies a scaling factor applied to the result.</param>
        /// <param name="hTopDataDesc">Specifies a handle to the top data tensor descriptor.</param>
        /// <param name="hTopData">Specifies a handle to the top data in GPU memory.</param>
        /// <param name="hTopDiffDesc">Specifies a handle to the top diff tensor descriptor</param>
        /// <param name="hTopDiff">Specifies a handle to the top diff in GPU memory.</param>
        /// <param name="hBottomDataDesc">Specifies a handle to the bottom data tensor descriptor.</param>
        /// <param name="hBottomData">Specifies a handle to the bottom data in GPU memory.</param>
        /// <param name="fBeta">Specifies a scaling factor applied to the prior destination value.</param>
        /// <param name="hBottomDiffDesc">Specifies a handle to the bottom diff tensor descriptor.</param>
        /// <param name="hBottomDiff">Specifies a handle to the bottom diff in GPU memory.</param>
        public void EluBackward(long hCuDnn, T fAlpha, long hTopDataDesc, long hTopData, long hTopDiffDesc, long hTopDiff, long hBottomDataDesc, long hBottomData, T fBeta, long hBottomDiffDesc, long hBottomDiff)
        {
            if (m_dt == DataType.DOUBLE)
                m_cuda.RunDouble((int)m_hKernel, (int)CUDAFN.ELU_BWD, m_param.AsDouble(hCuDnn, convertD(fAlpha), hTopDataDesc, hTopData, hTopDiffDesc, hTopDiff, hBottomDataDesc, hBottomData, convertD(fBeta), hBottomDiffDesc, hBottomDiff));
            else
                m_cuda.RunFloat((int)m_hKernel, (int)CUDAFN.ELU_BWD, m_param.AsFloat(hCuDnn, convertF(fAlpha), hTopDataDesc, hTopData, hTopDiffDesc, hTopDiff, hBottomDataDesc, hBottomData, convertF(fBeta), hBottomDiffDesc, hBottomDiff));
        }

        /// <summary>
        /// Perform a Sigmoid forward pass.
        /// </summary>
        /// <param name="hCuDnn">Specifies a handle to the instance of cuDnn.</param>
        /// <param name="fAlpha">Specifies a scaling factor applied to the result.</param>
        /// <param name="hBottomDataDesc">Specifies a handle to the bottom data tensor descriptor.</param>
        /// <param name="hBottomData">Specifies a handle to the bottom data in GPU memory.</param>
        /// <param name="fBeta">Specifies a scaling factor applied to the prior destination value.</param>
        /// <param name="hTopDataDesc">Specifies a handle to the top data tensor descriptor.</param>
        /// <param name="hTopData">Specifies a handle to the top data in GPU memory.</param>
        public void SigmoidForward(long hCuDnn, T fAlpha, long hBottomDataDesc, long hBottomData, T fBeta, long hTopDataDesc, long hTopData)
        {
            if (m_dt == DataType.DOUBLE)
                m_cuda.RunDouble((int)m_hKernel, (int)CUDAFN.SIGMOID_FWD, m_param.AsDouble(hCuDnn, convertD(fAlpha), hBottomDataDesc, hBottomData, convertD(fBeta), hTopDataDesc, hTopData));
            else
                m_cuda.RunFloat((int)m_hKernel, (int)CUDAFN.SIGMOID_FWD, m_param.AsFloat(hCuDnn, convertF(fAlpha), hBottomDataDesc, hBottomData, convertF(fBeta), hTopDataDesc, hTopData));
        }

        /// <summary>
        /// Perform a Sigmoid backward pass.
        /// </summary>
        /// <param name="hCuDnn">Specifies a handle to the instance of cuDnn.</param>
        /// <param name="fAlpha">Specifies a scaling factor applied to the result.</param>
        /// <param name="hTopDataDesc">Specifies a handle to the top data tensor descriptor.</param>
        /// <param name="hTopData">Specifies a handle to the top data in GPU memory.</param>
        /// <param name="hTopDiffDesc">Specifies a handle to the top diff tensor descriptor</param>
        /// <param name="hTopDiff">Specifies a handle to the top diff in GPU memory.</param>
        /// <param name="hBottomDataDesc">Specifies a handle to the bottom data tensor descriptor.</param>
        /// <param name="hBottomData">Specifies a handle to the bottom data in GPU memory.</param>
        /// <param name="fBeta">Specifies a scaling factor applied to the prior destination value.</param>
        /// <param name="hBottomDiffDesc">Specifies a handle to the bottom diff tensor descriptor.</param>
        /// <param name="hBottomDiff">Specifies a handle to the bottom diff in GPU memory.</param>
        public void SigmoidBackward(long hCuDnn, T fAlpha, long hTopDataDesc, long hTopData, long hTopDiffDesc, long hTopDiff, long hBottomDataDesc, long hBottomData, T fBeta, long hBottomDiffDesc, long hBottomDiff)
        {
            if (m_dt == DataType.DOUBLE)
                m_cuda.RunDouble((int)m_hKernel, (int)CUDAFN.SIGMOID_BWD, m_param.AsDouble(hCuDnn, convertD(fAlpha), hTopDataDesc, hTopData, hTopDiffDesc, hTopDiff, hBottomDataDesc, hBottomData, convertD(fBeta), hBottomDiffDesc, hBottomDiff));
            else
                m_cuda.RunFloat((int)m_hKernel, (int)CUDAFN.SIGMOID_BWD, m_param.AsFloat(hCuDnn, convertF(fAlpha), hTopDataDesc, hTopData, hTopDiffDesc, hTopDiff, hBottomDataDesc, hBottomData, convertF(fBeta), hBottomDiffDesc, hBottomDiff));
        }

        /// <summary>
        /// Perform a ReLU forward pass.
        /// </summary>
        /// <remarks>
        /// See [Rectifier Nonlinearities Improve Neural Network Acoustic Models](https://www.semanticscholar.org/paper/Rectifier-Nonlinearities-Improve-Neural-Network-Maas-Hannun/367f2c63a6f6a10b3b64b8729d601e69337ee3cc) by 
        /// Maas, A. L., Hannun, A. Y., and Ng, A. Y. (2013),  In ICML Workshop on Deep Learning
        /// for Audio, Speech, and Language Processing.
        /// </remarks>
        /// <param name="hCuDnn">Specifies a handle to the instance of cuDnn.</param>
        /// <param name="fAlpha">Specifies a scaling factor applied to the result.</param>
        /// <param name="hBottomDataDesc">Specifies a handle to the bottom data tensor descriptor.</param>
        /// <param name="hBottomData">Specifies a handle to the bottom data in GPU memory.</param>
        /// <param name="fBeta">Specifies a scaling factor applied to the prior destination value.</param>
        /// <param name="hTopDataDesc">Specifies a handle to the top data tensor descriptor.</param>
        /// <param name="hTopData">Specifies a handle to the top data in GPU memory.</param>
        public void ReLUForward(long hCuDnn, T fAlpha, long hBottomDataDesc, long hBottomData, T fBeta, long hTopDataDesc, long hTopData)
        {
            if (m_dt == DataType.DOUBLE)
                m_cuda.RunDouble((int)m_hKernel, (int)CUDAFN.RELU_FWD, m_param.AsDouble(hCuDnn, convertD(fAlpha), hBottomDataDesc, hBottomData, convertD(fBeta), hTopDataDesc, hTopData));
            else
                m_cuda.RunFloat((int)m_hKernel, (int)CUDAFN.RELU_FWD, m_param.AsFloat(hCuDnn, convertF(fAlpha), hBottomDataDesc, hBottomData, convertF(fBeta), hTopDataDesc, hTopData));
        }

        /// <summary>
        /// Perform a ReLU backward pass.
        /// </summary>
        /// <param name="hCuDnn">Specifies a handle to the instance of cuDnn.</param>
        /// <param name="fAlpha">Specifies a scaling factor applied to the result.</param>
        /// <param name="hTopDataDesc">Specifies a handle to the top data tensor descriptor.</param>
        /// <param name="hTopData">Specifies a handle to the top data in GPU memory.</param>
        /// <param name="hTopDiffDesc">Specifies a handle to the top diff tensor descriptor</param>
        /// <param name="hTopDiff">Specifies a handle to the top diff in GPU memory.</param>
        /// <param name="hBottomDataDesc">Specifies a handle to the bottom data tensor descriptor.</param>
        /// <param name="hBottomData">Specifies a handle to the bottom data in GPU memory.</param>
        /// <param name="fBeta">Specifies a scaling factor applied to the prior destination value.</param>
        /// <param name="hBottomDiffDesc">Specifies a handle to the bottom diff tensor descriptor.</param>
        /// <param name="hBottomDiff">Specifies a handle to the bottom diff in GPU memory.</param>
        public void ReLUBackward(long hCuDnn, T fAlpha, long hTopDataDesc, long hTopData, long hTopDiffDesc, long hTopDiff, long hBottomDataDesc, long hBottomData, T fBeta, long hBottomDiffDesc, long hBottomDiff)
        {
            if (m_dt == DataType.DOUBLE)
                m_cuda.RunDouble((int)m_hKernel, (int)CUDAFN.RELU_BWD, m_param.AsDouble(hCuDnn, convertD(fAlpha), hTopDataDesc, hTopData, hTopDiffDesc, hTopDiff, hBottomDataDesc, hBottomData, convertD(fBeta), hBottomDiffDesc, hBottomDiff));
            else
                m_cuda.RunFloat((int)m_hKernel, (int)CUDAFN.RELU_BWD, m_param.AsFloat(hCuDnn, convertF(fAlpha), hTopDataDesc, hTopData, hTopDiffDesc, hTopDiff, hBottomDataDesc, hBottomData, convertF(fBeta), hBottomDiffDesc, hBottomDiff));
        }

        /// <summary>
        /// Perform a Softmax forward pass.
        /// </summary>
        /// <param name="hCuDnn">Specifies a handle to the instance of cuDnn.</param>
        /// <param name="fAlpha">Specifies a scaling factor applied to the result.</param>
        /// <param name="hBottomDataDesc">Specifies a handle to the bottom data tensor descriptor.</param>
        /// <param name="hBottomData">Specifies a handle to the bottom data in GPU memory.</param>
        /// <param name="fBeta">Specifies a scaling factor applied to the prior destination value.</param>
        /// <param name="hTopDataDesc">Specifies a handle to the top data tensor descriptor.</param>
        /// <param name="hTopData">Specifies a handle to the top data in GPU memory.</param>
        public void SoftmaxForward(long hCuDnn, T fAlpha, long hBottomDataDesc, long hBottomData, T fBeta, long hTopDataDesc, long hTopData)
        {
            if (m_dt == DataType.DOUBLE)
                m_cuda.RunDouble((int)m_hKernel, (int)CUDAFN.SOFTMAX_FWD, m_param.AsDouble(hCuDnn, convertD(fAlpha), hBottomDataDesc, hBottomData, convertD(fBeta), hTopDataDesc, hTopData));
            else
                m_cuda.RunFloat((int)m_hKernel, (int)CUDAFN.SOFTMAX_FWD, m_param.AsFloat(hCuDnn, convertF(fAlpha), hBottomDataDesc, hBottomData, convertF(fBeta), hTopDataDesc, hTopData));
        }

        /// <summary>
        /// Perform a Softmax backward pass.
        /// </summary>
        /// <param name="hCuDnn">Specifies a handle to the instance of cuDnn.</param>
        /// <param name="fAlpha">Specifies a scaling factor applied to the result.</param>
        /// <param name="hTopDataDesc">Specifies a handle to the top data tensor descriptor.</param>
        /// <param name="hTopData">Specifies a handle to the top data in GPU memory.</param>
        /// <param name="hTopDiffDesc">Specifies a handle to the top diff tensor descriptor.</param>
        /// <param name="hTopDiff">Specifies a handle to the top diff in GPU memory.</param>
        /// <param name="fBeta">Specifies a scaling factor applied to the prior destination value.</param>
        /// <param name="hBottomDiffDesc">Specifies a handle to the bottom diff tensor descriptor.</param>
        /// <param name="hBottomDiff">Specifies a handle to the bottom diff in GPU memory.</param>
        public void SoftmaxBackward(long hCuDnn, T fAlpha, long hTopDataDesc, long hTopData, long hTopDiffDesc, long hTopDiff, T fBeta, long hBottomDiffDesc, long hBottomDiff)
        {
            if (m_dt == DataType.DOUBLE)
                m_cuda.RunDouble((int)m_hKernel, (int)CUDAFN.SOFTMAX_BWD, m_param.AsDouble(hCuDnn, convertD(fAlpha), hTopDataDesc, hTopData, hTopDiffDesc, hTopDiff, convertD(fBeta), hBottomDiffDesc, hBottomDiff));
            else
                m_cuda.RunFloat((int)m_hKernel, (int)CUDAFN.SOFTMAX_BWD, m_param.AsFloat(hCuDnn, convertF(fAlpha), hTopDataDesc, hTopData, hTopDiffDesc, hTopDiff, convertF(fBeta), hBottomDiffDesc, hBottomDiff));
        }

        /// <summary>
        /// Create the RNN Data Descriptor.
        /// </summary>
        /// <returns>A handle to the RNN Data descriptor is returned.</returns>
        public long CreateRnnDataDesc()
        {
            int nFn = (m_bEnableRnnExtendedVersion) ? (int)CUDAFN.CREATE_RNN_DATA_DESCEX : (int)CUDAFN.CREATE_RNN_DATA_DESC;

            if (m_dt == DataType.DOUBLE)
            {
                double[] rg = m_cuda.RunDouble((int)m_hKernel, nFn, null);
                return (long)rg[0];
            }
            else
            {
                float[] rg = m_cuda.RunFloat((int)m_hKernel, nFn, null);
                return (long)rg[0];
            }
        }

        /// <summary>
        /// Free an existing RNN Data descriptor.
        /// </summary>
        /// <param name="h">Specifies the handle to the RNN Data descriptor created with CreateRnnDataDesc</param>
        public void FreeRnnDataDesc(long h)
        {
            int nFn = (m_bEnableRnnExtendedVersion) ? (int)CUDAFN.FREE_RNN_DATA_DESCEX : (int)CUDAFN.FREE_RNN_DATA_DESC;

            if (m_dt == DataType.DOUBLE)
                m_cuda.RunDouble((int)m_hKernel, nFn, m_param.AsDouble(h));
            else
                m_cuda.RunFloat((int)m_hKernel, nFn, m_param.AsFloat(h));
        }

        /// <summary>
        /// Sets the RNN Data Descriptor values.
        /// </summary>
        /// <param name="hRnnDataDesc">Specifies the handle to the RNN descriptor created with CreateRnnDesc</param>
        /// <param name="layout">Specifies the input data layout (either SEQUENCE major or BATCH major).</param>
        /// <param name="nMaxSeqLen">Specifies the maximum sequence length.</param>
        /// <param name="nBatchSize">Specifies the batch count.</param>
        /// <param name="nVectorSize">Specifies the input vector count.</param>
        /// <param name="bBidirectional">Specifies whether the Rnn is bidirectional or not (default = false).</param>
        /// <param name="rgSeqLen">Specifies the sequence lengths - currently this should be <i>null</i> which sets all sequence lengths to nMaxSeqLen.</param>
        public void SetRnnDataDesc(long hRnnDataDesc, RNN_DATALAYOUT layout, int nMaxSeqLen, int nBatchSize, int nVectorSize, bool bBidirectional = false, int[] rgSeqLen = null)
        {
            if (!m_bEnableRnnExtendedVersion && layout != RNN_DATALAYOUT.RNN_SEQ_MAJOR)
                throw new Exception("The non-extended functions only support RNN_SEQ_MAJOR ordering.");

            int nFn = (m_bEnableRnnExtendedVersion) ? (int)CUDAFN.SET_RNN_DATA_DESCEX : (int)CUDAFN.SET_RNN_DATA_DESC;

            if (m_dt == DataType.DOUBLE)
            {
                List<double> rgArg = new List<double>() { hRnnDataDesc, (double)layout, nMaxSeqLen, nBatchSize, nVectorSize, (bBidirectional) ? 1 : 0 };

                if (rgSeqLen != null)
                {
                    for (int i = 0; i < rgSeqLen.Length; i++)
                    {
                        rgArg.Add(rgSeqLen[i]);
                    }
                }

                m_cuda.RunDouble((int)m_hKernel, nFn, rgArg.ToArray());
            }
            else
            {
                List<float> rgArg = new List<float>() { hRnnDataDesc, (float)layout, nMaxSeqLen, nBatchSize, nVectorSize, (bBidirectional) ? 1 : 0 };

                if (rgSeqLen != null)
                {
                    for (int i = 0; i < rgSeqLen.Length; i++)
                    {
                        rgArg.Add(rgSeqLen[i]);
                    }
                }

                m_cuda.RunFloat((int)m_hKernel, nFn, rgArg.ToArray());
            }
        }

        /// <summary>
        /// Create the RNN Descriptor.
        /// </summary>
        /// <returns>A handle to the RNN descriptor is returned.</returns>
        public long CreateRnnDesc()
        {
            if (m_dt == DataType.DOUBLE)
            {
                double[] rg = m_cuda.RunDouble((int)m_hKernel, (int)CUDAFN.CREATE_RNN_DESC, null);
                return (long)rg[0];
            }
            else
            {
                float[] rg = m_cuda.RunFloat((int)m_hKernel, (int)CUDAFN.CREATE_RNN_DESC, null);
                return (long)rg[0];
            }
        }

        /// <summary>
        /// Free an existing RNN descriptor.
        /// </summary>
        /// <param name="h">Specifies the handle to the RNN descriptor created with CreateRnnDesc</param>
        public void FreeRnnDesc(long h)
        {
            if (m_dt == DataType.DOUBLE)
                m_cuda.RunDouble((int)m_hKernel, (int)CUDAFN.FREE_RNN_DESC, m_param.AsDouble(h));
            else
                m_cuda.RunFloat((int)m_hKernel, (int)CUDAFN.FREE_RNN_DESC, m_param.AsFloat(h));
        }

        /// <summary>
        /// Sets the RNN Descriptor values.
        /// </summary>
        /// <param name="hCuDnn">Specifies a handle to the instance of cuDnn.</param>
        /// <param name="hRnnDesc">Specifies the handle to the RNN descriptor created with CreateRnnDesc</param>
        /// <param name="nHiddenCount">Specifies the hidden input (typically the input) count.</param>
        /// <param name="nNumLayers">Specifies the number of layers.</param>
        /// <param name="hDropoutDesc">Specifies the handle to the Droput descriptor (or 0 to ignore).  The droput descriptor is only used with two or more layers.</param>
        /// <param name="mode">Specifies the RNN_MODE (LSTM, RNN_RELU, RNN_TANH) to use.</param>
        /// <param name="bUseTensorCores">Optionally, specifies whether or not to use the Tensor Cores (if available).</param>
        /// <param name="direction">Optionally, specifies the direction of the RNN; Unidirectional or BiDirectional.</param>
        public void SetRnnDesc(long hCuDnn, long hRnnDesc, int nHiddenCount, int nNumLayers, long hDropoutDesc, RNN_MODE mode, bool bUseTensorCores, RNN_DIRECTION direction = RNN_DIRECTION.RNN_UNIDIRECTIONAL)
        {
            if (m_dt == DataType.DOUBLE)
                m_cuda.RunDouble((int)m_hKernel, (int)CUDAFN.SET_RNN_DESC, m_param.AsDouble(hCuDnn, hRnnDesc, nHiddenCount, nNumLayers, hDropoutDesc, (int)mode, (bUseTensorCores) ? 1 : 0, (double)direction));
            else
                m_cuda.RunFloat((int)m_hKernel, (int)CUDAFN.SET_RNN_DESC, m_param.AsFloat(hCuDnn, hRnnDesc, nHiddenCount, nNumLayers, hDropoutDesc, (int)mode, (bUseTensorCores) ? 1 : 0, (float)direction));
        }

        /// <summary>
        /// Returns the RNN parameter count.
        /// </summary>
        /// <param name="hCuDnn">Specifies a handle to the instance of cuDnn.</param>
        /// <param name="hRnnDesc">Specifies the handle to the RNN descriptor created with CreateRnnDesc</param>
        /// <param name="hXDesc">Specifies the handle to the first X descriptor.</param>
        /// <returns>The number of parameters (weights) is returned.</returns>
        public int GetRnnParamCount(long hCuDnn, long hRnnDesc, long hXDesc)
        {
            if (m_dt == DataType.DOUBLE)
            {
                double[] rg = m_cuda.RunDouble((int)m_hKernel, (int)CUDAFN.GET_RNN_PARAMCOUNT, m_param.AsDouble(hCuDnn, hRnnDesc, hXDesc, (m_bEnableRnnExtendedVersion) ? 1 : 0));
                return (int)rg[0];
            }
            else
            {
                float[] rg = m_cuda.RunFloat((int)m_hKernel, (int)CUDAFN.GET_RNN_PARAMCOUNT, m_param.AsFloat(hCuDnn, hRnnDesc, hXDesc, (m_bEnableRnnExtendedVersion) ? 1 : 0));
                return (int)rg[0];
            }
        }

        /// <summary>
        /// Returns the workspace and reserved counts.
        /// </summary>
        /// <param name="hCuDnn">Specifies a handle to the instance of cuDnn.</param>
        /// <param name="hRnnDesc">Specifies the handle to the RNN descriptor created with CreateRnnDesc</param>
        /// <param name="hXDesc">Specifies a handle to the data descriptor created with CreateRnnDataDesc.</param>
        /// <param name="nReservedCount">Returns the reserved count needed.</param>
        /// <returns>Returns the workspace count needed.</returns>
        public ulong GetRnnWorkspaceCount(long hCuDnn, long hRnnDesc, long hXDesc, out ulong nReservedCount)
        {
            if (m_dt == DataType.DOUBLE)
            {
                double[] rg = m_cuda.RunDouble((int)m_hKernel, (int)CUDAFN.GET_RNN_WORKSPACECOUNT, m_param.AsDouble(hCuDnn, hRnnDesc, (m_bEnableRnnExtendedVersion) ? 1 : 0, hXDesc));
                nReservedCount = (ulong)rg[1];
                return (ulong)rg[0];
            }
            else
            {
                float[] rg = m_cuda.RunFloat((int)m_hKernel, (int)CUDAFN.GET_RNN_WORKSPACECOUNT, m_param.AsFloat(hCuDnn, hRnnDesc, (m_bEnableRnnExtendedVersion) ? 1 : 0, hXDesc));
                nReservedCount = (ulong)rg[1];
                return (ulong)rg[0];
            }
        }

        /// <summary>
        /// Returns the linear layer parameters (weights).
        /// </summary>
        /// <param name="hCuDnn">Specifies a handle to the instance of cuDnn.</param>
        /// <param name="hRnnDesc">Specifies the handle to the RNN descriptor created with CreateRnnDesc</param>
        /// <param name="nLayer">Specifies the current layer index.</param>
        /// <param name="hXDesc">Specifies the input data elelement descriptor.</param>
        /// <param name="hWtDesc">Specifies the weight descriptor.</param>
        /// <param name="hWtData">Specifies the weight memory containing all weights.</param>
        /// <param name="nLinLayer">Specifies the linear layer index (e.g. LSTM has 8 linear layers, RNN has 2)</param>
        /// <param name="nWtCount">Returns the number of weight items.</param>
        /// <param name="hWt">Returns a handle to the weight GPU memory.</param>
        /// <param name="nBiasCount">Returns the number of bias items.</param>
        /// <param name="hBias">Returns a handle to the bias GPU memory.</param>
        public void GetRnnLinLayerParams(long hCuDnn, long hRnnDesc, int nLayer, long hXDesc, long hWtDesc, long hWtData, int nLinLayer, out int nWtCount, out long hWt, out int nBiasCount, out long hBias)
        {
            if (m_dt == DataType.DOUBLE)
            {
                double[] rg = m_cuda.RunDouble((int)m_hKernel, (int)CUDAFN.GET_RNN_LINLAYERPARAMS, m_param.AsDouble(hCuDnn, hRnnDesc, nLayer, hXDesc, hWtDesc, hWtData, nLinLayer, (m_bEnableRnnExtendedVersion) ? 1 : 0));
                nWtCount = (int)rg[0];
                hWt = (long)rg[1];
                nBiasCount = (int)rg[2];
                hBias = (long)rg[3];
            }
            else
            {
                float[] rg = m_cuda.RunFloat((int)m_hKernel, (int)CUDAFN.GET_RNN_LINLAYERPARAMS, m_param.AsFloat(hCuDnn, hRnnDesc, nLayer, hXDesc, hWtDesc, hWtData, nLinLayer, (m_bEnableRnnExtendedVersion) ? 1 : 0));
                nWtCount = (int)rg[0];
                hWt = (long)rg[1];
                nBiasCount = (int)rg[2];
                hBias = (long)rg[3];
            }
        }

        /// <summary>
        /// Run the RNN through a forward pass.
        /// </summary>
        /// <param name="hCuDnn">Specifies a handle to the instance of cuDnn.</param>
        /// <param name="hRnnDesc">Specifies the handle to the RNN descriptor created with CreateRnnDesc</param>
        /// <param name="hXDesc">Specifies a handle to the input data descriptor.</param>
        /// <param name="hXData">Specifies a handle to the input GPU data.</param>
        /// <param name="hHxDesc">Specifies a handle to the hidden data descriptor.</param>
        /// <param name="hHxData">Specifies a handle to the hidden GPU data.</param>
        /// <param name="hCxDesc">Specifies a handle to the cont data descriptor.</param>
        /// <param name="hCxData">Specifies a handle to the cont GPU data.</param>
        /// <param name="hWtDesc">Specifies a handle to the weight descriptor.</param>
        /// <param name="hWtData">Specifies a handle to the weight data.</param>
        /// <param name="hYDesc">Specifies a handle to the output data descriptor.</param>
        /// <param name="hYData">Specifies a handle to the output GPU data.</param>
        /// <param name="hHyDesc">Specifies a handle to the output hidden descriptor.</param>
        /// <param name="hHyData">Specifies a handle to the output hidden data.</param>
        /// <param name="hCyDesc">Specifies a handle to the output cont descriptor.</param>
        /// <param name="hCyData">Specifies a handle to the output cont data.</param>
        /// <param name="hWorkspace">Specifies a handle to the workspace GPU memory.</param>
        /// <param name="nWsCount">Specifies the number of items within the workspace.</param>
        /// <param name="hReserved">Specifies a handle to the reserved GPU memory.</param>
        /// <param name="nResCount">Specifies the number of items within the reserved memory.</param>
        /// <param name="bTraining">Specifies the whether the forward pass is during taining or not.</param>
        public void RnnForward(long hCuDnn, long hRnnDesc, long hXDesc, long hXData, long hHxDesc, long hHxData, long hCxDesc, long hCxData, long hWtDesc, long hWtData, long hYDesc, long hYData, long hHyDesc, long hHyData, long hCyDesc, long hCyData, long hWorkspace, ulong nWsCount, long hReserved, ulong nResCount, bool bTraining)
        {
            if (m_dt == DataType.DOUBLE)
            {
                List<double> rgArg = new List<double>() { hCuDnn, hRnnDesc };

                rgArg.Add(hXDesc);
                rgArg.Add(hXData);

                rgArg.Add(hHxDesc);
                rgArg.Add(hHxData);
                rgArg.Add(hCxDesc);
                rgArg.Add(hCxData);

                rgArg.Add(hWtDesc);
                rgArg.Add(hWtData);

                rgArg.Add(hYDesc);
                rgArg.Add(hYData);

                rgArg.Add(hHyDesc);
                rgArg.Add(hHyData);
                rgArg.Add(hCyDesc);
                rgArg.Add(hCyData);

                rgArg.Add(hWorkspace);
                rgArg.Add(nWsCount);
                rgArg.Add(hReserved);
                rgArg.Add(nResCount);
                rgArg.Add((bTraining) ? 1 : 0);

                if (m_bEnableRnnExtendedVersion)
                    rgArg.Add(1);

                m_cuda.RunDouble((int)m_hKernel, (int)CUDAFN.FWD_RNN, rgArg.ToArray());
            }
            else
            {
                List<float> rgArg = new List<float>() { hCuDnn, hRnnDesc };

                rgArg.Add(hXDesc);
                rgArg.Add(hXData);

                rgArg.Add(hHxDesc);
                rgArg.Add(hHxData);
                rgArg.Add(hCxDesc);
                rgArg.Add(hCxData);

                rgArg.Add(hWtDesc);
                rgArg.Add(hWtData);

                rgArg.Add(hYDesc);
                rgArg.Add(hYData);

                rgArg.Add(hHyDesc);
                rgArg.Add(hHyData);
                rgArg.Add(hCyDesc);
                rgArg.Add(hCyData);

                rgArg.Add(hWorkspace);
                rgArg.Add(nWsCount);
                rgArg.Add(hReserved);
                rgArg.Add(nResCount);
                rgArg.Add((bTraining) ? 1 : 0);

                if (m_bEnableRnnExtendedVersion)
                    rgArg.Add(1);

                m_cuda.RunFloat((int)m_hKernel, (int)CUDAFN.FWD_RNN, rgArg.ToArray());
            }
        }

        /// <summary>
        /// Run the RNN backward pass through the data.
        /// </summary>
        /// <param name="hCuDnn">Specifies a handle to the instance of cuDnn.</param>
        /// <param name="hRnnDesc">Specifies the handle to the RNN descriptor created with CreateRnnDesc</param>
        /// <param name="hYDesc">Specifies a handle to the output data descriptor.</param>
        /// <param name="hYData">Specifies a handle to the output GPU data.</param>
        /// <param name="hYDiff">Specifies a handle to the output GPU gradients.</param>
        /// <param name="hHyDesc">Specifies a handle to the output hidden descriptor.</param>
        /// <param name="hHyDiff">Specifies a handle to the output hidden gradients.</param>
        /// <param name="hCyDesc">Specifies a handle to the output cont descriptor.</param>
        /// <param name="hCyDiff">Specifies a handle to the output cont gradients.</param>
        /// <param name="hWtDesc">Specifies a handle to the weight descriptor.</param>
        /// <param name="hWtData">Specifies a handle to the weight data.</param>
        /// <param name="hHxDesc">Specifies a handle to the hidden data descriptor.</param>
        /// <param name="hHxData">Specifies a handle to the hidden GPU data.</param>
        /// <param name="hCxDesc">Specifies a handle to the cont data descriptor.</param>
        /// <param name="hCxData">Specifies a handle to the cont GPU data.</param>
        /// <param name="hXDesc">Specifies a handle to the input data descriptor.</param>
        /// <param name="hXDiff">Specifies a handle to the input GPU gradients.</param>
        /// <param name="hdHxDesc">Specifies a handle to the input hidden descriptor for the gradients.</param>
        /// <param name="hHxDiff">Specifis a handle to the input hidden GPU gradients.</param>
        /// <param name="hdCxDesc">Specifies a handle to the input cont descriptor of the gradients.</param>
        /// <param name="hCxDiff">Specifies a handle to the input cont GPU gradients.</param>
        /// <param name="hWorkspace">Specifies a handle to the workspace GPU memory.</param>
        /// <param name="nWsCount">Specifies the number of items within the workspace.</param>
        /// <param name="hReserved">Specifies a handle to the reserved GPU memory.</param>
        /// <param name="nResCount">Specifies the number of items within the reserved memory.</param>
        public void RnnBackwardData(long hCuDnn, long hRnnDesc, long hYDesc, long hYData, long hYDiff, long hHyDesc, long hHyDiff, long hCyDesc, long hCyDiff, long hWtDesc, long hWtData, long hHxDesc, long hHxData, long hCxDesc, long hCxData, long hXDesc, long hXDiff, long hdHxDesc, long hHxDiff, long hdCxDesc, long hCxDiff, long hWorkspace, ulong nWsCount, long hReserved, ulong nResCount)
        {
            if (m_dt == DataType.DOUBLE)
            {
                List<double> rgArg = new List<double>() { hCuDnn, hRnnDesc };

                rgArg.Add(hYDesc);
                rgArg.Add(hYData);
                rgArg.Add(hYDiff);

                rgArg.Add(hHyDesc);
                rgArg.Add(hHyDiff);
                rgArg.Add(hCyDesc);
                rgArg.Add(hCyDiff);

                rgArg.Add(hWtDesc);
                rgArg.Add(hWtData);

                rgArg.Add(hHxDesc);
                rgArg.Add(hHxData);
                rgArg.Add(hCxDesc);
                rgArg.Add(hCxData);

                rgArg.Add(hXDesc);
                rgArg.Add(hXDiff);

                rgArg.Add(hdHxDesc);
                rgArg.Add(hHxDiff);
                rgArg.Add(hdCxDesc);
                rgArg.Add(hCxDiff);

                rgArg.Add(hWorkspace);
                rgArg.Add(nWsCount);
                rgArg.Add(hReserved);
                rgArg.Add(nResCount);

                if (m_bEnableRnnExtendedVersion)
                    rgArg.Add(1);

                m_cuda.RunDouble((int)m_hKernel, (int)CUDAFN.BWD_RNN_DATA, rgArg.ToArray());
            }
            else
            {
                List<float> rgArg = new List<float>() { hCuDnn, hRnnDesc };

                rgArg.Add(hYDesc);
                rgArg.Add(hYData);
                rgArg.Add(hYDiff);

                rgArg.Add(hHyDesc);
                rgArg.Add(hHyDiff);
                rgArg.Add(hCyDesc);
                rgArg.Add(hCyDiff);

                rgArg.Add(hWtDesc);
                rgArg.Add(hWtData);

                rgArg.Add(hHxDesc);
                rgArg.Add(hHxData);
                rgArg.Add(hCxDesc);
                rgArg.Add(hCxData);

                rgArg.Add(hXDesc);
                rgArg.Add(hXDiff);

                rgArg.Add(hdHxDesc);
                rgArg.Add(hHxDiff);
                rgArg.Add(hdCxDesc);
                rgArg.Add(hCxDiff);

                rgArg.Add(hWorkspace);
                rgArg.Add(nWsCount);
                rgArg.Add(hReserved);
                rgArg.Add(nResCount);

                if (m_bEnableRnnExtendedVersion)
                    rgArg.Add(1);

                m_cuda.RunFloat((int)m_hKernel, (int)CUDAFN.BWD_RNN_DATA, rgArg.ToArray());
            }
        }

        /// <summary>
        /// Run the RNN backward pass on the weights.
        /// </summary>
        /// <param name="hCuDnn">Specifies a handle to the instance of cuDnn.</param>
        /// <param name="hRnnDesc">Specifies the handle to the RNN descriptor created with CreateRnnDesc</param>
        /// <param name="hXDesc">Specifies a handle to the input data descriptor.</param>
        /// <param name="hXData">Specifies a handle to the input GPU data.</param>
        /// <param name="hHxDesc">Specifies a handle to the hidden data descriptor.</param>
        /// <param name="hHxData">Specifies a handle to the hidden GPU data.</param>
        /// <param name="hYDesc">Specifies a handle to the output data descriptor.</param>
        /// <param name="hYData">Specifies a handle to the output GPU data.</param>
        /// <param name="hWorkspace">Specifies a handle to the workspace GPU memory.</param>
        /// <param name="nWsCount">Specifies the number of items within the workspace.</param>
        /// <param name="hWtDesc">Specifies a handle to the weight descriptor.</param>
        /// <param name="hWtDiff">Specifies a handle to the weight gradients.</param>
        /// <param name="hReserved">Specifies a handle to the reserved GPU memory.</param>
        /// <param name="nResCount">Specifies the number of items within the reserved memory.</param>
        public void RnnBackwardWeights(long hCuDnn, long hRnnDesc, long hXDesc, long hXData, long hHxDesc, long hHxData, long hYDesc, long hYData, long hWorkspace, ulong nWsCount, long hWtDesc, long hWtDiff, long hReserved, ulong nResCount)
        {
            if (m_dt == DataType.DOUBLE)
            {
                List<double> rgArg = new List<double>() { hCuDnn, hRnnDesc };

                rgArg.Add(hXDesc);
                rgArg.Add(hXData);

                rgArg.Add(hHxDesc);
                rgArg.Add(hHxData);

                rgArg.Add(hYDesc);
                rgArg.Add(hYData);

                rgArg.Add(hWorkspace);
                rgArg.Add(nWsCount);

                rgArg.Add(hWtDesc);
                rgArg.Add(hWtDiff);

                rgArg.Add(hReserved);
                rgArg.Add(nResCount);

                if (m_bEnableRnnExtendedVersion)
                    rgArg.Add(1);

                m_cuda.RunDouble((int)m_hKernel, (int)CUDAFN.BWD_RNN_WTS, rgArg.ToArray());
            }
            else
            {
                List<float> rgArg = new List<float>() { hCuDnn, hRnnDesc };

                rgArg.Add(hXDesc);
                rgArg.Add(hXData);

                rgArg.Add(hHxDesc);
                rgArg.Add(hHxData);

                rgArg.Add(hYDesc);
                rgArg.Add(hYData);

                rgArg.Add(hWorkspace);
                rgArg.Add(nWsCount);

                rgArg.Add(hWtDesc);
                rgArg.Add(hWtDiff);

                rgArg.Add(hReserved);
                rgArg.Add(nResCount);

                if (m_bEnableRnnExtendedVersion)
                    rgArg.Add(1);

                m_cuda.RunFloat((int)m_hKernel, (int)CUDAFN.BWD_RNN_WTS, rgArg.ToArray());
            }
        }


        /// <summary>
        /// Allocates the GPU memory for the PCA Data.
        /// </summary>
        /// <remarks>
        /// See [Parallel GPU Implementation of Iterative PCA Algorithms](https://arxiv.org/abs/0811.1081) by Mircea Andrecut
        /// </remarks>
        /// <param name="nM">Specifies the data width (number of rows).</param>
        /// <param name="nN">Specifies the data height (number of columns).</param>
        /// <param name="nK">Specifies the number of components (K &lt;= N).</param>
        /// <param name="nCount">Returns the total number of items in the allocated data (nM * nN).</param>
        /// <returns></returns>
        public long AllocPCAData(int nM, int nN, int nK, out int nCount)
        {
            nCount = nM * nN;
            return AllocMemory(nCount);
        }

        /// <summary>
        /// Allocates the GPU memory for the PCA scores.
        /// </summary>
        /// <remarks>
        /// See [Parallel GPU Implementation of Iterative PCA Algorithms](https://arxiv.org/abs/0811.1081) by Mircea Andrecut
        /// </remarks>
        /// <param name="nM">Specifies the data width (number of rows).</param>
        /// <param name="nN">Specifies the data height (number of columns).</param>
        /// <param name="nK">Specifies the number of components (K &lt;= N).</param>
        /// <param name="nCount">Returns the total number of items in the allocated data (nM * nN).</param>
        /// <returns></returns>
        public long AllocPCAScores(int nM, int nN, int nK, out int nCount)
        {
            nCount = nM * nK;
            return AllocMemory(nCount);
        }

        /// <summary>
        /// Allocates the GPU memory for the PCA loads.
        /// </summary>
        /// <remarks>
        /// See [Parallel GPU Implementation of Iterative PCA Algorithms](https://arxiv.org/abs/0811.1081) by Mircea Andrecut
        /// </remarks>
        /// <param name="nM">Specifies the data width (number of rows).</param>
        /// <param name="nN">Specifies the data height (number of columns).</param>
        /// <param name="nK">Specifies the number of components (K &lt;= N).</param>
        /// <param name="nCount">Returns the total number of items in the allocated data (nM * nN).</param>
        /// <returns></returns>
        public long AllocPCALoads(int nM, int nN, int nK, out int nCount)
        {
            nCount = nN * nK;
            return AllocMemory(nCount);
        }

        /// <summary>
        /// Allocates the GPU memory for the PCA eigenvalues.
        /// </summary>
        /// <remarks>
        /// See [Parallel GPU Implementation of Iterative PCA Algorithms](https://arxiv.org/abs/0811.1081) by Mircea Andrecut
        /// </remarks>
        /// <param name="nM">Specifies the data width (number of rows).</param>
        /// <param name="nN">Specifies the data height (number of columns).</param>
        /// <param name="nK">Specifies the number of components (K &lt;= N).</param>
        /// <param name="nCount">Returns the total number of items in the allocated data (nM * nN).</param>
        /// <returns></returns>
        public long AllocPCAEigenvalues(int nM, int nN, int nK, out int nCount)
        {
            nCount = nK * 1;
            return AllocHostBuffer(nCount);
        }

        /// <summary>
        /// Creates a new PCA instance and returns the handle to it.
        /// </summary>
        /// <remarks>
        /// See [Parallel GPU Implementation of Iterative PCA Algorithms](https://arxiv.org/abs/0811.1081) by Mircea Andrecut
        /// </remarks>
        /// <param name="nMaxIterations">Specifies the number of iterations to run.</param>
        /// <param name="nM">Specifies the data width (number of rows).</param>
        /// <param name="nN">Specifies the data height (number of columns).</param>
        /// <param name="nK">Specifies the number of components (K less than or equal to N).</param>
        /// <param name="hData">Specifies a handle to the data allocated using AllocatePCAData.</param>
        /// <param name="hScoresResult">Specifies a handle to the data allocated using AllocatePCAScores.</param>
        /// <param name="hLoadsResult">Specifies a handle to the data allocated using AllocatePCALoads.</param>
        /// <param name="hResiduals">Specifies a handle to the data allocated using AllocatePCAData.</param>
        /// <param name="hEigenvalues">Specifies a handle to the data allocated using AllocatePCAEigenvalues.</param>
        /// <returns></returns>
        public long CreatePCA(int nMaxIterations, int nM, int nN, int nK, long hData, long hScoresResult, long hLoadsResult, long hResiduals = 0, long hEigenvalues = 0)
        {
            if (m_dt == DataType.DOUBLE)
            {
                double[] rg = m_cuda.RunDouble((int)m_hKernel, (int)CUDAFN.CUDA_CREATE_PCA, m_param.AsDouble(nMaxIterations, nM, nN, nK, hData, hScoresResult, hLoadsResult, hResiduals, hEigenvalues));
                return (long)rg[0];
            }
            else
            {
                float[] rg = m_cuda.RunFloat((int)m_hKernel, (int)CUDAFN.CUDA_CREATE_PCA, m_param.AsFloat(nMaxIterations, nM, nN, nK, hData, hScoresResult, hLoadsResult, hResiduals, hEigenvalues));
                return (long)rg[0];
            }
        }

        /// <summary>
        /// Runs a number of steps of the iterative PCA algorithm.
        /// </summary>
        /// <remarks>
        /// See [Parallel GPU Implementation of Iterative PCA Algorithms](https://arxiv.org/abs/0811.1081) by Mircea Andrecut
        /// </remarks>
        /// <param name="hPCA">Specifies a handle to the PCA instance to use.</param>
        /// <param name="nSteps">Specifies the number of steps to run.</param>
        /// <param name="nCurrentK">Returns the current component value.</param>
        /// <param name="nCurrentIteration">Returns the current iteration.</param>
        /// <returns><code>true</code> is returned when the maximum number of iterations have been run as specified in CreatePCA.</returns>
        public bool RunPCA(long hPCA, int nSteps, out int nCurrentK, out int nCurrentIteration)
        {
            bool bDone = false;

            if (m_dt == DataType.DOUBLE)
            {
                double[] rg = m_cuda.RunDouble((int)m_hKernel, (int)CUDAFN.CUDA_RUN_PCA, m_param.AsDouble(hPCA, nSteps));
                bDone = (rg[0] == 1.0) ? true : false;
                nCurrentIteration = (int)rg[1];
                nCurrentK = (int)rg[2];
            }
            else
            {
                float[] rg = m_cuda.RunFloat((int)m_hKernel, (int)CUDAFN.CUDA_RUN_PCA, m_param.AsFloat(hPCA, nSteps));
                bDone = (rg[0] == 1.0f) ? true : false;
                nCurrentIteration = (int)rg[1];
                nCurrentK = (int)rg[2];
            }

            return bDone;
        }

        /// <summary>
        /// Free the PCA instance associated with handle.
        /// </summary>
        /// <remarks>
        /// See [Parallel GPU Implementation of Iterative PCA Algorithms](https://arxiv.org/abs/0811.1081) by Mircea Andrecut
        /// </remarks>
        /// <param name="hPCA">Specifies a handle to the PCA instance to free.</param>
        public void FreePCA(long hPCA)
        {
            if (m_dt == DataType.DOUBLE)
                m_cuda.RunDouble((int)m_hKernel, (int)CUDAFN.CUDA_FREE_PCA, m_param.AsDouble(hPCA));
            else
                m_cuda.RunFloat((int)m_hKernel, (int)CUDAFN.CUDA_FREE_PCA, m_param.AsFloat(hPCA));
        }

        /// <summary>
        /// Create an instance of the SSD GPU support.
        /// </summary>
        /// <param name="nNumClasses">Specifies the number of classes.</param>
        /// <param name="bShareLocation">Specifies whether or not to share the location.</param>
        /// <param name="nLocClasses">Specifies the number of location classes.</param>
        /// <param name="nBackgroundLabelId">Specifies the background label ID.</param>
        /// <param name="bUseDiffcultGt">Specifies whether or not to use difficult ground truths.</param>
        /// <param name="miningType">Specifies the mining type to use.</param>
        /// <param name="matchType">Specifies the matching method to use.</param>
        /// <param name="fOverlapThreshold">Specifies the overlap threshold for each box.</param>
        /// <param name="bUsePriorForMatching">Specifies whether or not to use priors for matching.</param>
        /// <param name="codeType">Specifies the code type to use.</param>
        /// <param name="bEncodeVariantInTgt">Specifies whether or not to encode the variant in the target.</param>
        /// <param name="bBpInside">Specifies whether or not the BP is inside or not.</param>
        /// <param name="bIgnoreCrossBoundaryBbox">Specifies whether or not to ignore cross boundary boxes.</param>
        /// <param name="bUsePriorForNms">Specifies whether or not to use priors for NMS.</param>
        /// <param name="confLossType">Specifies the confidence loss type.</param>
        /// <param name="locLossType">Specifies the location loss type.</param>
        /// <param name="fNegPosRatio">Specifies the negative/positive ratio to use.</param>
        /// <param name="fNegOverlap">Specifies the negative overlap to use.</param>
        /// <param name="nSampleSize">Specifies the sample size.</param>
        /// <param name="bMapObjectToAgnostic">Specifies whether or not to map objects to agnostic or not.</param>
        /// <param name="bNmsParam">Specifies whether or not the NMS parameters are specified.</param>
        /// <param name="fNmsThreshold">Specifies the NMS threshold, which is only used when the 'bNmsParam' = true.</param>
        /// <param name="nNmsTopK">Specifies the NMS top-k selection, which is only used when the 'bNmsParam' = true.</param>
        /// <param name="fNmsEta">Specifies the NMS eta, which is only used when the 'bNmsParam' = true.</param>
        /// <returns>A handle to the SSD instance is returned.</returns>
        public long CreateSSD(int nNumClasses, bool bShareLocation, int nLocClasses, int nBackgroundLabelId, bool bUseDiffcultGt, SSD_MINING_TYPE miningType, SSD_MATCH_TYPE matchType, float fOverlapThreshold, bool bUsePriorForMatching, SSD_CODE_TYPE codeType, bool bEncodeVariantInTgt, bool bBpInside, bool bIgnoreCrossBoundaryBbox, bool bUsePriorForNms, SSD_CONF_LOSS_TYPE confLossType, SSD_LOC_LOSS_TYPE locLossType, float fNegPosRatio, float fNegOverlap, int nSampleSize, bool bMapObjectToAgnostic, bool bNmsParam, float? fNmsThreshold = null, int? nNmsTopK = null, float? fNmsEta = null)
        {
            int nGpuID = GetDeviceID();

            if (m_dt == DataType.DOUBLE)
            {
                List<double> rgArg = new List<double>();

                /* 0 */ rgArg.Add(nGpuID);
                /* 1 */ rgArg.Add(nNumClasses);
                /* 2 */ rgArg.Add((bShareLocation) ? 1 : 0);
                /* 3 */ rgArg.Add(nLocClasses);
                /* 4 */ rgArg.Add(nBackgroundLabelId);
                /* 5 */ rgArg.Add((bUseDiffcultGt) ? 1 : 0);
                /* 6 */ rgArg.Add((int)miningType);
                /* 7 */ rgArg.Add((int)matchType);
                /* 8 */ rgArg.Add(fOverlapThreshold);
                /* 9 */ rgArg.Add((bUsePriorForMatching) ? 1 : 0);
                /* 10 */ rgArg.Add((int)codeType);
                /* 11 */ rgArg.Add((bEncodeVariantInTgt) ? 1 : 0);
                /* 12 */ rgArg.Add((bBpInside) ? 1 : 0);
                /* 13 */ rgArg.Add((bIgnoreCrossBoundaryBbox) ? 1 : 0);
                /* 14 */ rgArg.Add((bUsePriorForNms) ? 1 : 0);
                /* 15 */ rgArg.Add((int)confLossType);
                /* 16 */ rgArg.Add((int)locLossType);
                /* 17 */ rgArg.Add(fNegPosRatio);
                /* 18 */ rgArg.Add(fNegOverlap);
                /* 19 */ rgArg.Add(nSampleSize);
                /* 20 */ rgArg.Add((bMapObjectToAgnostic) ? 1 : 0);
                /* 21 */ rgArg.Add((bNmsParam) ? 1 : 0);

                if (bNmsParam)
                {
                    if (!fNmsThreshold.HasValue)
                        throw new Exception("An NMS threshold must be specified when the 'bNmsParam' is true.");

                    /* 22 */ rgArg.Add(fNmsThreshold.GetValueOrDefault(0));
                    /* 23 */ rgArg.Add(nNmsTopK.GetValueOrDefault(-1));
                    /* 24 */ rgArg.Add(fNmsEta.GetValueOrDefault(1));
                }

                double[] rg = m_cuda.RunDouble((int)m_hKernel, (int)CUDAFN.CUDA_CREATE_SSD, rgArg.ToArray());
                return (long)rg[0];
            }
            else
            {
                List<float> rgArg = new List<float>();

                /* 0 */ rgArg.Add(nGpuID);
                /* 1 */ rgArg.Add(nNumClasses);
                /* 2 */ rgArg.Add((bShareLocation) ? 1 : 0);
                /* 3 */ rgArg.Add(nLocClasses);
                /* 4 */ rgArg.Add(nBackgroundLabelId);
                /* 5 */ rgArg.Add((bUseDiffcultGt) ? 1 : 0);
                /* 6 */ rgArg.Add((int)miningType);
                /* 7 */ rgArg.Add((int)matchType);
                /* 8 */ rgArg.Add(fOverlapThreshold);
                /* 9 */ rgArg.Add((bUsePriorForMatching) ? 1 : 0);
                /* 10 */ rgArg.Add((int)codeType);
                /* 11 */ rgArg.Add((bEncodeVariantInTgt) ? 1 : 0);
                /* 12 */ rgArg.Add((bBpInside) ? 1 : 0);
                /* 13 */ rgArg.Add((bIgnoreCrossBoundaryBbox) ? 1 : 0);
                /* 14 */ rgArg.Add((bUsePriorForNms) ? 1 : 0);
                /* 15 */ rgArg.Add((int)confLossType);
                /* 16 */ rgArg.Add((int)locLossType);
                /* 17 */ rgArg.Add(fNegPosRatio);
                /* 18 */ rgArg.Add(fNegOverlap);
                /* 19 */ rgArg.Add(nSampleSize);
                /* 20 */ rgArg.Add((bMapObjectToAgnostic) ? 1 : 0);
                /* 21 */ rgArg.Add((bNmsParam) ? 1 : 0);

                if (bNmsParam)
                {
                    if (!fNmsThreshold.HasValue)
                        throw new Exception("An NMS threshold must be specified when the 'bNmsParam' is true.");

                    /* 22 */ rgArg.Add(fNmsThreshold.GetValueOrDefault(0));
                    /* 23 */ rgArg.Add(nNmsTopK.GetValueOrDefault(-1));
                    /* 24 */ rgArg.Add(fNmsEta.GetValueOrDefault(1));
                }

                float[] rg = m_cuda.RunFloat((int)m_hKernel, (int)CUDAFN.CUDA_CREATE_SSD, rgArg.ToArray());
                return (long)rg[0];
            }
        }

        /// <summary>
        /// Setup the SSD GPU support.
        /// </summary>
        /// <param name="hSSD">Specifies the handle to the SSD instance.</param>
        /// <param name="nNum">Specifies the number of items.</param>
        /// <param name="nNumPriors">Specifies the number of priors.</param>
        /// <param name="nNumGt">Specifies the number of ground truths.</param>
        public void SetupSSD(long hSSD, int nNum, int nNumPriors, int nNumGt)
        {
            if (m_dt == DataType.DOUBLE)
                m_cuda.RunDouble((int)m_hKernel, (int)CUDAFN.CUDA_SETUP_SSD, m_param.AsDouble(hSSD, nNum, nNumPriors, nNumGt));
            else
                m_cuda.RunFloat((int)m_hKernel, (int)CUDAFN.CUDA_SETUP_SSD, m_param.AsFloat(hSSD, nNum, nNumPriors, nNumGt));
        }

        /// <summary>
        /// Free the instance of SSD GPU support.
        /// </summary>
        /// <param name="hSSD">Specifies the handle to the SSD instance.</param>
        public void FreeSSD(long hSSD)
        {
            if (m_dt == DataType.DOUBLE)
                m_cuda.RunDouble((int)m_hKernel, (int)CUDAFN.CUDA_FREE_SSD, m_param.AsDouble(hSSD));
            else
                m_cuda.RunFloat((int)m_hKernel, (int)CUDAFN.CUDA_FREE_SSD, m_param.AsFloat(hSSD));
        }

        /// <summary>
        /// Performs the SSD MultiBoxLoss forward operation.
        /// </summary>
        /// <param name="hSSD">Specifies the handle to the SSD instance.</param>
        /// <param name="nLocDataCount">Specifies the number of location data items.</param>
        /// <param name="hLocGpuData">Specifies the handle to the location data in GPU memory.</param>
        /// <param name="nConfDataCount">Specifies the number of confidence data items.</param>
        /// <param name="hConfGpuData">Specifies the handle to the confidence data in GPU memory.</param>
        /// <param name="nPriorDataCount">Specifies the number of prior box data.</param>
        /// <param name="hPriorGpuData">Specifies the prior box data in GPU memory.</param>
        /// <param name="nGtDataCount">Specifies the number of ground truth items.</param>
        /// <param name="hGtGpuData">Specifies the ground truth data in GPU memory.</param>
        /// <param name="rgAllMatchIndices">Returns all match indices found.</param>
        /// <param name="rgrgAllNegIndices">Returns all neg indices found.</param>
        /// <param name="nNumNegs">Returns the number of negatives.</param>
        /// <returns>The number of matches is returned.</returns>
        public int SsdMultiBoxLossForward(long hSSD, int nLocDataCount, long hLocGpuData, int nConfDataCount, long hConfGpuData, int nPriorDataCount, long hPriorGpuData, int nGtDataCount, long hGtGpuData, out List<DictionaryMap<List<int>>> rgAllMatchIndices, out List<List<int>> rgrgAllNegIndices, out int nNumNegs)
        {
            int nIdx = 0;
            int nMatchCount = 0;
            rgAllMatchIndices = new List<DictionaryMap<List<int>>>();
            rgrgAllNegIndices = new List<List<int>>();

            if (m_dt == DataType.DOUBLE)
            {
                double[] rg = m_cuda.RunDouble((int)m_hKernel, (int)CUDAFN.CUDA_SSD_FWD_MULTIBOXLOSS, m_param.AsDouble(hSSD, nLocDataCount, hLocGpuData, nConfDataCount, hConfGpuData, nPriorDataCount, hPriorGpuData, nGtDataCount, hGtGpuData));
                nMatchCount = (int)rg[nIdx];
                nIdx++;
                nNumNegs = (int)rg[nIdx];
                nIdx++;

                // Get the match indices.
                int nNumAllMatchIndices = (int)rg[nIdx];
                nIdx++;
                for (int i = 0; i < nNumAllMatchIndices; i++)
                {
                    DictionaryMap<List<int>> map = new DictionaryMap<List<int>>(null);

                    int nMapCount = (int)rg[nIdx];
                    nIdx++;
                    for (int j = 0; j < nMapCount; j++)
                    {
                        int nLabel = (int)rg[nIdx];
                        nIdx++;
                        List<int> rgIdx = new List<int>();

                        int nItemCount = (int)rg[nIdx];
                        nIdx++;
                        for (int k = 0; k < nItemCount; k++)
                        {
                            int nItemIdx = (int)rg[nIdx];
                            nIdx++;
                            rgIdx.Add(nItemIdx);
                        }

                        map[nLabel] = rgIdx;
                    }

                    rgAllMatchIndices.Add(map);
                }

                // Get the neg indices.
                int nNegListCount = (int)rg[nIdx];
                nIdx++;
                for (int i = 0; i < nNegListCount; i++)
                {
                    int nItemCount = (int)rg[nIdx];
                    nIdx++;
                    List<int> rgItems = new List<int>();

                    for (int j = 0; j < nItemCount; j++)
                    {
                        int nItemIdx = (int)rg[nIdx];
                        nIdx++;
                        rgItems.Add(nItemIdx);
                    }

                    rgrgAllNegIndices.Add(rgItems);
                }
            }
            else
            {
                float[] rg = m_cuda.RunFloat((int)m_hKernel, (int)CUDAFN.CUDA_SSD_FWD_MULTIBOXLOSS, m_param.AsFloat(hSSD, nLocDataCount, hLocGpuData, nConfDataCount, hConfGpuData, nPriorDataCount, hPriorGpuData, nGtDataCount, hGtGpuData));
                nMatchCount = (int)rg[nIdx];
                nIdx++;
                nNumNegs = (int)rg[nIdx];
                nIdx++;

                // Get the match indices.
                int nMapListCount = (int)rg[nIdx];
                nIdx++;
                for (int i = 0; i < nMapListCount; i++)
                {
                    DictionaryMap<List<int>> map = new DictionaryMap<List<int>>(null);

                    int nMapCount = (int)rg[nIdx];
                    nIdx++;
                    for (int j = 0; j < nMapCount; j++)
                    {
                        int nLabel = (int)rg[nIdx];
                        nIdx++;
                        List<int> rgIdx = new List<int>();

                        int nItemCount = (int)rg[nIdx];
                        nIdx++;
                        for (int k = 0; k < nItemCount; k++)
                        {
                            int nItemIdx = (int)rg[nIdx];
                            nIdx++;
                            rgIdx.Add(nItemIdx);
                        }

                        map[nLabel] = rgIdx;
                    }

                    rgAllMatchIndices.Add(map);
                }

                // Get the neg indices.
                int nNegListCount = (int)rg[nIdx];
                nIdx++;
                for (int i = 0; i < nNegListCount; i++)
                {
                    int nItemCount = (int)rg[nIdx];
                    nIdx++;
                    List<int> rgItems = new List<int>();

                    for (int j = 0; j < nItemCount; j++)
                    {
                        int nItemIdx = (int)rg[nIdx];
                        nIdx++;
                        rgItems.Add(nItemIdx);
                    }

                    rgrgAllNegIndices.Add(rgItems);
                }
            }

            return nMatchCount;
        }

        /// <summary>
        /// Encodes the SSD data into the location prediction and location ground truths.
        /// </summary>
        /// <param name="hSSD">Specifies the handle to the SSD instance.</param>
        /// <param name="nLocPredCount">Specifies the number of location prediction items.</param>
        /// <param name="hLocPred">Specifies the location prediction data in GPU memory.</param>
        /// <param name="nLocGtCount">Specifies the location ground truth items.</param>
        /// <param name="hLocGt">Specifies the location ground truth data in GPU memory.</param>
        public void SsdEncodeLocPrediction(long hSSD, int nLocPredCount, long hLocPred, int nLocGtCount, long hLocGt)
        {
            if (m_dt == DataType.DOUBLE)
                m_cuda.RunDouble((int)m_hKernel, (int)CUDAFN.CUDA_SSD_ENCODE_LOCPRED, m_param.AsDouble(hSSD, nLocPredCount, hLocPred, nLocGtCount, hLocGt));
            else
                m_cuda.RunFloat((int)m_hKernel, (int)CUDAFN.CUDA_SSD_ENCODE_LOCPRED, m_param.AsFloat(hSSD, nLocPredCount, hLocPred, nLocGtCount, hLocGt));
        }

        /// <summary>
        /// Encodes the SSD data into the confidence prediction and confidence ground truths.
        /// </summary>
        /// <param name="hSSD">Specifies the handle to the SSD instance.</param>
        /// <param name="nConfPredCount">Specifies the number of confidence prediction items.</param>
        /// <param name="hConfPred">Specifies the confidence prediction data in GPU memory.</param>
        /// <param name="nConfGtCount">Specifies the confidence ground truth items.</param>
        /// <param name="hConfGt">Specifies the confidence ground truth data in GPU memory.</param>
        public void SsdEncodeConfPrediction(long hSSD, int nConfPredCount, long hConfPred, int nConfGtCount, long hConfGt)
        {
            if (m_dt == DataType.DOUBLE)
                m_cuda.RunDouble((int)m_hKernel, (int)CUDAFN.CUDA_SSD_ENCODE_CONFPRED, m_param.AsDouble(hSSD, nConfPredCount, hConfPred, nConfGtCount, hConfGt));
            else
                m_cuda.RunFloat((int)m_hKernel, (int)CUDAFN.CUDA_SSD_ENCODE_CONFPRED, m_param.AsFloat(hSSD, nConfPredCount, hConfPred, nConfGtCount, hConfGt));
        }

        #endregion

        //---------------------------------------------------------------------
        //  ICudaMath Methods
        //---------------------------------------------------------------------
        #region ICudaMath Methods

        /// <summary>
        /// Set the values of GPU memory to a specified value of type <code>double</code>.
        /// </summary>
        /// <param name="nCount">Specifies the number of items to set.</param>
        /// <param name="hHandle">Specifies a handle to the memory on the GPU.</param>
        /// <param name="fVal">Specifies the value to set.</param>
        /// <param name="nIdx">When -1, all values in the GPU memory are set to the <i>fVal</i> value, otherwise, only the value at the index <i>nIdx</i> is set to the value.</param>
        public void set(int nCount, long hHandle, double fVal, int nIdx = -1)
        {
            set(nCount, hHandle, (T)Convert.ChangeType(fVal, typeof(T)), nIdx);
        }

        /// <summary>
        /// Set the values of GPU memory to a specified value of type <code>float</code>.
        /// </summary>
        /// <param name="nCount">Specifies the number of items to set.</param>
        /// <param name="hHandle">Specifies a handle to the memory on the GPU.</param>
        /// <param name="fVal">Specifies the value to set.</param>
        /// <param name="nIdx">When -1, all values in the GPU memory are set to the <i>fVal</i> value, otherwise, only the value at the index <i>nIdx</i> is set to the value.</param>
        public void set(int nCount, long hHandle, float fVal, int nIdx = -1)
        {
            set(nCount, hHandle, (T)Convert.ChangeType(fVal, typeof(T)), nIdx);
        }

        /// <summary>
        /// Set the values of GPU memory to a specified value of type 'T'.
        /// </summary>
        /// <param name="nCount">Specifies the number of items to set.</param>
        /// <param name="hHandle">Specifies a handle to the memory on the GPU.</param>
        /// <param name="fVal">Specifies the value to set.</param>
        /// <param name="nIdx">When -1, all values in the GPU memory are set to the <i>fVal</i> value, otherwise, only the value at the index <i>nIdx</i> is set to the value.</param>
        /// <param name="nXOff">Optionally specifies an offset into the GPU memory where the <i>set</i> starts.</param>
        public void set(int nCount, long hHandle, T fVal, int nIdx = -1, int nXOff = 0)
        {
            if (m_dt == DataType.DOUBLE)
            {
                if (m_rgGhostMemory == null)
                {
                    m_cuda.RunDouble((int)m_hKernel, (int)CUDAFN.CUDA_SET, m_param.AsDouble(nCount, hHandle, (double)Convert.ChangeType(fVal, typeof(double)), nIdx, nXOff));
                }
                else
                {
                    if (nIdx >= 0)
                        m_rgGhostMemory[hHandle][nIdx] = fVal;
                    else
                        Utility.Set<T>(m_rgGhostMemory[hHandle], fVal);
                }
            }
            else
            {
                if (m_rgGhostMemory == null)
                {
                    m_cuda.RunFloat((int)m_hKernel, (int)CUDAFN.CUDA_SET, m_param.AsFloat(nCount, hHandle, (float)Convert.ChangeType(fVal, typeof(float)), nIdx, nXOff));
                }
                else
                {
                    if (nIdx >= 0)
                        m_rgGhostMemory[hHandle][nIdx] = fVal;
                    else
                        Utility.Set<T>(m_rgGhostMemory[hHandle], fVal);
                }
            }
        }

        /// <summary>
        /// Queries the GPU memory by copying it into an array of <code>double</code>
        /// </summary>
        /// <param name="nCount">Specifies the number of items.</param>
        /// <param name="hHandle">Specifies a handle to GPU memory.</param>
        /// <param name="nIdx">When -1, all values in the GPU memory are queried, otherwise, only the value at the index <i>nIdx</i> is returned.</param>
        /// <returns>An array of <code>double</code> is returned.</returns>
        public double[] get_double(int nCount, long hHandle, int nIdx = -1)
        {
            return convertD(get(nCount, hHandle, nIdx));
        }

        /// <summary>
        /// Queries the GPU memory by copying it into an array of <code>float</code>
        /// </summary>
        /// <param name="nCount">Specifies the number of items.</param>
        /// <param name="hHandle">Specifies a handle to GPU memory.</param>
        /// <param name="nIdx">When -1, all values in the GPU memory are queried, otherwise, only the value at the index <i>nIdx</i> is returned.</param>
        /// <returns>An array of <code>float</code> is returned.</returns>
        public float[] get_float(int nCount, long hHandle, int nIdx = -1)
        {
            return convertF(get(nCount, hHandle, nIdx));
        }

        /// <summary>
        /// Queries the GPU memory by copying it into an array of type 'T'.
        /// </summary>
        /// <param name="nCount">Specifies the number of items.</param>
        /// <param name="hHandle">Specifies a handle to GPU memory.</param>
        /// <param name="nIdx">When -1, all values in the GPU memory are queried, otherwise, only the value at the index <i>nIdx</i> is returned.</param>
        /// <returns>An array of <code>T</code> is returned.</returns>
        public T[] get(int nCount, long hHandle, int nIdx = -1)
        {
            if (m_dt == DataType.DOUBLE)
                return convert(m_cuda.RunDouble((int)m_hKernel, (int)CUDAFN.CUDA_GET, m_param.AsDouble(nCount, hHandle, nIdx)));
            else
                return convert(m_cuda.RunFloat((int)m_hKernel, (int)CUDAFN.CUDA_GET, m_param.AsFloat(nCount, hHandle, nIdx)));
        }

        /// <summary>
        /// Copy data from one block of GPU memory to another.
        /// </summary>
        /// <remarks>
        /// This function uses [NVIDIA's cuBlas](https://developer.nvidia.com/cublas) but with a different parameter ordering.
        /// </remarks>
        /// <param name="nCount">Specifies the number of items (not bytes) to copy.</param>
        /// <param name="hSrc">Specifies a handle to GPU memory containing the source data.</param>
        /// <param name="hDst">Specifies a handle to GPU memory containing the destination data.</param>
        /// <param name="nSrcOffset">Optionally specifies the offset into the source data where the copying starts.</param>
        /// <param name="nDstOffset">Optionally specifies the offset into the destination data where the copying starts.</param>
        /// <param name="hStream">Optionally, specifies a handle to a stream to use for the operation.</param>
        /// <param name="bSrcHalfSizeOverride">Optionally, specifies and override for the half size state of the source (default = null, which is ignored).</param>
        /// <param name="bDstHalfSizeOverride">Optionally, specifies and override for the half size state of the destination (default = null, which is ignored).</param>
        public void copy(int nCount, long hSrc, long hDst, int nSrcOffset = 0, int nDstOffset = 0, long hStream = -1, bool? bSrcHalfSizeOverride = null, bool? bDstHalfSizeOverride = null)
        {
            int nSrcHalfSizeOverride = -1;
            int nDstHalfSizeOverride = -1;

            if (bSrcHalfSizeOverride.HasValue)
                nSrcHalfSizeOverride = (bSrcHalfSizeOverride.Value) ? 1 : 0;

            if (bDstHalfSizeOverride.HasValue)
                nDstHalfSizeOverride = (bDstHalfSizeOverride.Value) ? 1 : 0;

            if (m_dt == DataType.DOUBLE)
                m_cuda.RunDouble((int)m_hKernel, (int)CUDAFN.CUDA_COPY, m_param.AsDouble(nCount, hSrc, hDst, nSrcOffset, nDstOffset, hStream, nSrcHalfSizeOverride, nDstHalfSizeOverride));
            else
                m_cuda.RunFloat((int)m_hKernel, (int)CUDAFN.CUDA_COPY, m_param.AsFloat(nCount, hSrc, hDst, nSrcOffset, nDstOffset, hStream, nSrcHalfSizeOverride, nDstHalfSizeOverride));
        }

        /// <summary>
        /// Copy similar items of length 'nDim' from hSrc1 (where hSimilar(i) = 1) and dissimilar items of length 'nDim' from hSrc2 (where hSimilar(i) = 0).
        /// </summary>
        /// <param name="nCount">Specifies the total data length of hSrc1, hSrc2 and hDst.</param>
        /// <param name="nNum">Specifis the number of outer items in hSrc1, hSrc2, hDst, and the number of elements in hSimilar.</param>
        /// <param name="nDim">Specifies the inner dimension of hSrc1, hSrc2 and hDst.</param>
        /// <param name="hSrc1">Specifies a handle to the GPU memory of source 1.</param>
        /// <param name="hSrc2">Specifies a handle to the GPU memory of source 2.</param>
        /// <param name="hDst">Specifies a handle to the GPU memory of the destination.</param>
        /// <param name="hSimilar">Specifies a handle to the GPU memory of the similar data.</param>
        /// <param name="bInvert">Optionally, specifies whether or not to invert the similar values (e.g. copy when similar = 0 instead of similar = 1)</param>
        public void copy(int nCount, int nNum, int nDim, long hSrc1, long hSrc2, long hDst, long hSimilar, bool bInvert = false)
        {
            if (m_dt == DataType.DOUBLE)
                m_cuda.RunDouble((int)m_hKernel, (int)CUDAFN.CUDA_COPY_SIM, m_param.AsDouble(nCount, nNum, nDim, hSrc1, hSrc2, hDst, hSimilar, (bInvert) ? 1 : 0));
            else
                m_cuda.RunFloat((int)m_hKernel, (int)CUDAFN.CUDA_COPY_SIM, m_param.AsFloat(nCount, nNum, nDim, hSrc1, hSrc2, hDst, hSimilar, (bInvert) ? 1 : 0));
        }

        /// <summary>
        /// Copy a batch of labeled items into a cache organized by label where older data is removed and replaced by newer data.
        /// </summary>
        /// <param name="nCount">Specifies the total data length of hSrc.</param>
        /// <param name="nNum">Specifis the number of outer items in hSrc1, hSrc2, hDst, and the number of elements in hSimilar.</param>
        /// <param name="nDim">Specifies the inner dimension of hSrc1, hSrc2 and hDst.</param>
        /// <param name="hSrcData">Specifies a handle to the GPU memory of source data.</param>
        /// <param name="hSrcLbl">Specifies a handle to the GPU memory of source labels.</param>
        /// <param name="nDstCount">Specifies the total data length of the hDstCache</param>
        /// <param name="hDstCache">Specifies a handle to the GPU memory of the destination cache.</param>
        /// <param name="hWorkDevData">Specifies a handle to the GPU memory of the device work data that is the same size as the hDstCache.</param>
        /// <param name="nLabelStart">Specifies the first label of all possible labels.</param>
        /// <param name="nLabelCount">Specifies the total number of labels (expects labels to be sequential from 'nLabelStart').</param>
        /// <param name="nCacheSize">Specifies the size of each labeled data cache.</param>
        /// <param name="hCacheHostCursors">Specifies a handle to host memmory (allocated using AllocateHostBuffer) containing the label cursors - there should be 'nLabelCount' cursors.</param>
        /// <param name="hWorkDataHost">Specifies a handle to host memory (allocated using AllocateHostBuffer) used for work - must be nNum in item length.</param>
        /// <remarks>
        /// NOTE: The cache size must be set at a sufficient size that covers the maximum number items for any given label within a batch, otherwise cached items will be overwritten for items in the current batch.
        /// </remarks>
        public void copy_batch(int nCount, int nNum, int nDim, long hSrcData, long hSrcLbl, int nDstCount, long hDstCache, long hWorkDevData, int nLabelStart, int nLabelCount, int nCacheSize, long hCacheHostCursors, long hWorkDataHost)
        {
            if (m_dt == DataType.DOUBLE)
                m_cuda.RunDouble((int)m_hKernel, (int)CUDAFN.CUDA_COPY_BATCH, m_param.AsDouble(nCount, nNum, nDim, hSrcData, hSrcLbl, nDstCount, hDstCache, hWorkDevData, nLabelStart, nLabelCount, nCacheSize, hCacheHostCursors, hWorkDataHost));
            else
                m_cuda.RunFloat((int)m_hKernel, (int)CUDAFN.CUDA_COPY_BATCH, m_param.AsFloat(nCount, nNum, nDim, hSrcData, hSrcLbl, nDstCount, hDstCache, hWorkDevData, nLabelStart, nLabelCount, nCacheSize, hCacheHostCursors, hWorkDataHost));
        }

        /// <summary>
        /// Copy a sequence of cached items, organized by label, into an anchor, positive (if nK > 0), and negative blobs.
        /// </summary>
        /// <param name="nK">Specifies the output type expected where: nK = 0, outputs to 2 tops (anchor and one negative), or nK > 0, outputs to 2 + nK tops (anchor, positive, nK negatives).  The rghTop and rgnTopCount must be sized accordingly.</param>
        /// <param name="nNum">Specifis the number of outer items in hSrc1, hSrc2, hDst, and the number of elements in hSimilar.</param>
        /// <param name="nDim">Specifies the inner dimension of hSrc1, hSrc2 and hDst.</param>
        /// <param name="hSrcData">Specifies a handle to the GPU memory of source data.</param>
        /// <param name="hSrcLbl">Specifies a handle to the GPU memory of source labels.</param>
        /// <param name="nSrcCacheCount">Specifis the number of items in hSrcCache (nCacheSize * nLabelCount).</param>
        /// <param name="hSrcCache">Specifies a handle to the cached labeled data.</param>
        /// <param name="nLabelStart">Specifies the first label of all possible labels.</param>
        /// <param name="nLabelCount">Specifies the total number of labels (expects labels to be sequential from 'nLabelStart').</param>
        /// <param name="nCacheSize">Specifies the size of each labeled data cache.</param>
        /// <param name="hCacheHostCursors">Specifies a handle to host memmory containing the label cursors - there should be 'nLabelCount' cursors.</param>
        /// <param name="bOutputLabels">Specifies whether or not to output labels or not.  When true, one additional top is expected for the labels.</param>
        /// <param name="rghTop">Specifies a list of the GPU memory for each top item.  The number of top items expected depends on the 'nK' value.</param>
        /// <param name="rgnTopCount">Specifies a list of the item count for each top item.  The number of top items expected depends on the 'nK' value.</param>
        /// <param name="hWorkDataHost">Specifies a handle to host memory (allocated using AllocateHostBuffer) used for work - must be nNum in item length and must be the same hWorkDataHost passed to 'copy_batch'.</param>
        /// <param name="bCombinePositiveAndNegative">Optionally, specifies to combine the positive and negative items by alternating between each and placing both in Top[1], while also making sure the output labels reflect the alternation.</param>
        /// <param name="nSeed">Optionally, specifies a seed for the random number generator (default = 0, which igores this parameter).</param>
        /// <remarks>
        /// Receiving an error ERROR_BATCH_TOO_SMALL indicates that the batch size is too small and does not have enough labels to choose from.  Each batch should have at least two instances of each labeled item.
        /// 
        /// NOTE: When 'nK' = 1 and 'bCombinePositiveAndNegative' = true, the label output has a dimension of 2, and and the tops used are as follows: top(0) = anchor; top(1) = alternating negative/positive, top(2) = labels if 'bOutputLabels' = true.
        /// </remarks>
        public void copy_sequence(int nK, int nNum, int nDim, long hSrcData, long hSrcLbl, int nSrcCacheCount, long hSrcCache, int nLabelStart, int nLabelCount, int nCacheSize, long hCacheHostCursors, bool bOutputLabels, List<long> rghTop, List<int> rgnTopCount, long hWorkDataHost, bool bCombinePositiveAndNegative = false, int nSeed = 0)
        {
            int nTopCount = 2 + nK;

            if (bOutputLabels)
                nTopCount++;

            if (bCombinePositiveAndNegative && nK != 0)
                throw new ArgumentOutOfRangeException("nK", "When using 'bCombinePositiveAndNegative', nK should be 0.");

            if (nK < 0 || nK > 10)
                throw new ArgumentOutOfRangeException("nK", "The 'nK' parameter must be within the range [0,10]!");

            if (rghTop.Count != nTopCount)
                throw new ArgumentOutOfRangeException("rghTop", "The 'rghTop' count must equal '" + nTopCount.ToString() + "' given nK = " + nK.ToString() + " and bOutputLabels = " + bOutputLabels.ToString() + "!");

            if (rgnTopCount.Count != rghTop.Count)
                throw new ArgumentOutOfRangeException("rgnTopCount", "The 'rgnTopCount' count must equal the 'rghTop' count!");

            if (m_dt == DataType.DOUBLE)
            {
                List<double> rgarg = new List<double>() { nK, nNum, nDim, hSrcData, hSrcLbl, nSrcCacheCount, hSrcCache, nLabelStart, nLabelCount, nCacheSize, hCacheHostCursors, (bOutputLabels) ? 1 : 0, hWorkDataHost, (bCombinePositiveAndNegative) ? 1 : 0, nSeed };

                for (int i = 0; i < rghTop.Count; i++)
                {
                    rgarg.Add(rghTop[i]);
                }

                for (int i = 0; i < rgnTopCount.Count; i++)
                {
                    rgarg.Add(rgnTopCount[i]);
                }

                m_cuda.RunDouble((int)m_hKernel, (int)CUDAFN.CUDA_COPY_SEQUENCE, rgarg.ToArray());
            }
            else
            {
                List<float> rgarg = new List<float>() { nK, nNum, nDim, hSrcData, hSrcLbl, nSrcCacheCount, hSrcCache, nLabelStart, nLabelCount, nCacheSize, hCacheHostCursors, (bOutputLabels) ? 1 : 0, hWorkDataHost, (bCombinePositiveAndNegative) ? 1 : 0, nSeed };

                for (int i = 0; i < rghTop.Count; i++)
                {
                    rgarg.Add(rghTop[i]);
                }

                for (int i = 0; i < rgnTopCount.Count; i++)
                {
                    rgarg.Add(rgnTopCount[i]);
                }

                m_cuda.RunFloat((int)m_hKernel, (int)CUDAFN.CUDA_COPY_SEQUENCE, rgarg.ToArray());
            }
        }

        /// <summary>
        /// Copy a sequence from a source to a destination and allow for skip steps.
        /// </summary>
        /// <param name="n">Specifies the total number of items in src.</param>
        /// <param name="hSrc">Specifies a handle to the source GPU memory.</param>
        /// <param name="nSrcStep">Specifies the stepping used across the source.</param>
        /// <param name="nSrcStartIdx">Specifies the starting index into the source.</param>
        /// <param name="nCopyCount">Specifies the number of items to copy.</param>
        /// <param name="nCopyDim">Specifies the dimension to copy (which x spatial dim = total copy amount).</param>
        /// <param name="hDst">Specifies a handle to the destination GPU memory.</param>
        /// <param name="nDstStep">Specifies the steping used across the desination.</param>
        /// <param name="nDstStartIdx">Specifies the starting index where data is to be copied in the destination.</param>
        /// <param name="nSrcSpatialDim">Specifies the src spatial dim of each item copied.  Src and Dst spatial dims should be equal when nSpatialDimCount is not used.</param>
        /// <param name="nDstSpatialDim">Specifies the dst spatial dim of each item copied.  Src and Dst spatial dims should be equal when nSpatialDimCount is not used.</param>
        /// <param name="nSrcSpatialDimStartIdx">Optionally, specifies the start index within the source spatial dim to start the copy (default = 0)</param>
        /// <param name="nDstSpatialDimStartIdx">Optionally, specifies the start index within the destination spatial dim to start the copy (default = 0)</param>
        /// <param name="nSpatialDimCount">Optionally, specifies the number of items to copy from within the spatial dim (default = -1, copy all)</param>
        public void copy_sequence(int n, long hSrc, int nSrcStep, int nSrcStartIdx, int nCopyCount, int nCopyDim, long hDst, int nDstStep, int nDstStartIdx, int nSrcSpatialDim, int nDstSpatialDim, int nSrcSpatialDimStartIdx = 0, int nDstSpatialDimStartIdx = 0, int nSpatialDimCount = -1)
        {
            if (m_dt == DataType.DOUBLE)
                m_cuda.RunDouble((int)m_hKernel, (int)CUDAFN.CUDA_COPY_SEQUENCE2, m_param.AsDouble(n, hSrc, nSrcStep, nSrcStartIdx, nCopyCount, nCopyDim, hDst, nDstStep, nDstStartIdx, nSrcSpatialDim, nDstSpatialDim, nSrcSpatialDimStartIdx, nDstSpatialDimStartIdx, nSpatialDimCount));
            else
                m_cuda.RunFloat((int)m_hKernel, (int)CUDAFN.CUDA_COPY_SEQUENCE2, m_param.AsFloat(n, hSrc, nSrcStep, nSrcStartIdx, nCopyCount, nCopyDim, hDst, nDstStep, nDstStartIdx, nSrcSpatialDim, nDstSpatialDim, nSrcSpatialDimStartIdx, nDstSpatialDimStartIdx, nSpatialDimCount));
        }

        /// <summary>
        /// Expand a vector of length 'nNum' into a matrix of size 'nNum' x 'nDim' by copying each value of the vector
        /// into all elements of the corresponding matrix row.
        /// </summary>
        /// <param name="n">Specifies the total number of items in the matrix 'A'</param>
        /// <param name="nNum">Specifies the total number of rows in the matrix 'A' and the total number of items in the vector 'X'.</param>
        /// <param name="nDim">Specifies the total number of columns in the matrix 'A'.</param>
        /// <param name="hX">Specifies the 'nNum' length vector to expand.</param>
        /// <param name="hA">Specifies the 'nNum' x 'nDim' matrix.</param>
        public void copy_expand(int n, int nNum, int nDim, long hX, long hA)
        {
            if (m_dt == DataType.DOUBLE)
                m_cuda.RunDouble((int)m_hKernel, (int)CUDAFN.CUDA_COPY_EXPAND, m_param.AsDouble(n, nNum, nDim, hX, hA));
            else
                m_cuda.RunFloat((int)m_hKernel, (int)CUDAFN.CUDA_COPY_EXPAND, m_param.AsFloat(n, nNum, nDim, hX, hA));
        }

        /// <summary>
        /// Fill data from the source data 'n' times in the destination.
        /// </summary>
        /// <param name="n">Specifies the number of times to copy the source data.</param>
        /// <param name="nDim">Specifies the number of source items to copy.</param>
        /// <param name="hSrc">Specifies a handle to the GPU memory of the source data.</param>
        /// <param name="nSrcOff">Specifies an offset into the GPU memory where the source data copy starts.</param>
        /// <param name="nCount">Specifies the total number of items in the destination.  This value must be >= n * nDim.</param>
        /// <param name="hDst">Specifies the handle to the GPU memory where the data is to be copied.</param>
        public void fill(int n, int nDim, long hSrc, int nSrcOff, int nCount, long hDst)
        {
            if (m_dt == DataType.DOUBLE)
                m_cuda.RunDouble((int)m_hKernel, (int)CUDAFN.CUDA_COPY_FILL, m_param.AsDouble(n, nDim, hSrc, nSrcOff, nCount, hDst));
            else
                m_cuda.RunFloat((int)m_hKernel, (int)CUDAFN.CUDA_COPY_FILL, m_param.AsFloat(n, nDim, hSrc, nSrcOff, nCount, hDst));
        }

        /// <summary>
        /// Sort the data in the GPU memory specified.
        /// </summary>
        /// <param name="nCount">Specifies the total number of items in the memory.</param>
        /// <param name="hY">Specifies the handle to the GPU memory of data to sort.</param>
        public void sort(int nCount, long hY)
        {
            if (m_dt == DataType.DOUBLE)
                m_cuda.RunDouble((int)m_hKernel, (int)CUDAFN.CUDA_SORT, m_param.AsDouble(nCount, hY));
            else
                m_cuda.RunFloat((int)m_hKernel, (int)CUDAFN.CUDA_SORT, m_param.AsFloat(nCount, hY));
        }

        /// <summary>
        /// Perform a matrix-matrix multiplication operation: C = alpha transB (B) transA (A) + beta C 
        /// </summary>
        /// <remarks>
        /// This function uses [NVIDIA's cuBlas](https://developer.nvidia.com/cublas) but with a different parameter ordering.
        /// </remarks>
        /// <param name="bTransA">Specifies whether or not to transpose A.</param>
        /// <param name="bTransB">Specifies whether or not to transpose B.</param>
        /// <param name="m">Specifies the width (number of columns) of A and C.</param>
        /// <param name="n">Specifies the height (number of rows) of B and C.</param>
        /// <param name="k">Specifies the width (number of columns) of A and B.</param>
        /// <param name="fAlpha">Specifies a scalar multiplied by the data where the scalar is of type <code>double</code></param>
        /// <param name="hA">Specifies a handle to the data for A in GPU memory.</param>
        /// <param name="hB">Specifies a handle to the data for B in GPU memory.</param>
        /// <param name="fBeta">Specifies a scalar multiplied by C where the scalar is of type <code>double</code></param>
        /// <param name="hC">Specifies a handle to the data for C in GPU memory.</param>
        public void gemm(bool bTransA, bool bTransB, int m, int n, int k, double fAlpha, long hA, long hB, double fBeta, long hC)
        {
            gemm(bTransA, bTransB, m, n, k, (T)Convert.ChangeType(fAlpha, typeof(T)), hA, hB, (T)Convert.ChangeType(fBeta, typeof(T)), hC);
        }

        /// <summary>
        /// Perform a matrix-matrix multiplication operation: C = alpha transB (B) transA (A) + beta C 
        /// </summary>
        /// <remarks>
        /// This function uses [NVIDIA's cuBlas](https://developer.nvidia.com/cublas) but with a different parameter ordering.
        /// </remarks>
        /// <param name="bTransA">Specifies whether or not to transpose A.</param>
        /// <param name="bTransB">Specifies whether or not to transpose B.</param>
        /// <param name="m">Specifies the width (number of columns) of A and C.</param>
        /// <param name="n">Specifies the height (number of rows) of B and C.</param>
        /// <param name="k">Specifies the width (number of columns) of A and B.</param>
        /// <param name="fAlpha">Specifies a scalar multiplied by the data where the scalar is of type <code>float</code></param>
        /// <param name="hA">Specifies a handle to the data for matrix A in GPU memory.</param>
        /// <param name="hB">Specifies a handle to the data for matrix B in GPU memory.</param>
        /// <param name="fBeta">Specifies a scalar multiplied by C where the scalar is of type <code>float</code></param>
        /// <param name="hC">Specifies a handle to the data for matrix C in GPU memory.</param>
        public void gemm(bool bTransA, bool bTransB, int m, int n, int k, float fAlpha, long hA, long hB, float fBeta, long hC)
        {
            gemm(bTransA, bTransB, m, n, k, (T)Convert.ChangeType(fAlpha, typeof(T)), hA, hB, (T)Convert.ChangeType(fBeta, typeof(T)), hC);
        }

        /// <summary>
        /// Perform a matrix-matrix multiplication operation: C = alpha transB (B) transA (A) + beta C 
        /// </summary>
        /// <remarks>
        /// This function uses [NVIDIA's cuBlas](https://developer.nvidia.com/cublas) but with a different parameter ordering.
        /// </remarks>
        /// <param name="bTransA">Specifies whether or not to transpose A.</param>
        /// <param name="bTransB">Specifies whether or not to transpose B.</param>
        /// <param name="m">Specifies the width (number of columns) of A and C.</param>
        /// <param name="n">Specifies the height (number of rows) of B and C.</param>
        /// <param name="k">Specifies the width (number of columns) of A and B.</param>
        /// <param name="fAlpha">Specifies a scalar multiplied by the data where the scalar is of type 'T'.</param>
        /// <param name="hA">Specifies a handle to the data for matrix A in GPU memory.</param>
        /// <param name="hB">Specifies a handle to the data for matrix B in GPU memory.</param>
        /// <param name="fBeta">Specifies a scalar multiplied by C where the scalar is of type 'T'.</param>
        /// <param name="hC">Specifies a handle to the data for matrix C in GPU memory.</param>
        /// <param name="nAOffset">Specifies an offset (in items, not bytes) into the memory of A.</param>
        /// <param name="nBOffset">Specifies an offset (in items, not bytes) into the memory of B.</param>
        /// <param name="nCOffset">Specifies an offset (in items, not bytes) into the memory of C.</param>
        public void gemm(bool bTransA, bool bTransB, int m, int n, int k, T fAlpha, long hA, long hB, T fBeta, long hC, int nAOffset = 0, int nBOffset = 0, int nCOffset = 0)
        {
            if (m_dt == DataType.DOUBLE)
                m_cuda.RunDouble((int)m_hKernel, (int)CUDAFN.CUDA_GEMM, m_param.AsDouble((bTransA) ? 1.0 : 0.0, (bTransB) ? 1.0 : 0.0, m, n, k, convertD(fAlpha), hA, hB, convertD(fBeta), hC, nAOffset, nBOffset, nCOffset));
            else
                m_cuda.RunFloat((int)m_hKernel, (int)CUDAFN.CUDA_GEMM, m_param.AsFloat((bTransA) ? 1.0f : 0.0f, (bTransB) ? 1.0f : 0.0f, m, n, k, convertF(fAlpha), hA, hB, convertF(fBeta), hC, nAOffset, nBOffset, nCOffset));
        }

        /// <summary>
        /// Perform a matrix-matrix multiplication operation: C = alpha transB (B) transA (A) + beta C 
        /// </summary>
        /// <remarks>
        /// This function uses [NVIDIA's cuBlas](https://developer.nvidia.com/cublas) but with a different parameter ordering.
        /// </remarks>
        /// <param name="bTransA">Specifies whether or not to transpose A.</param>
        /// <param name="bTransB">Specifies whether or not to transpose B.</param>
        /// <param name="m">Specifies the width (number of columns) of A and C.</param>
        /// <param name="n">Specifies the height (number of rows) of B and C.</param>
        /// <param name="k">Specifies the width (number of columns) of A and B.</param>
        /// <param name="fAlpha">Specifies a scalar multiplied by the data where the scalar is of type 'T'.</param>
        /// <param name="hA">Specifies a handle to the data for matrix A in GPU memory.</param>
        /// <param name="hB">Specifies a handle to the data for matrix B in GPU memory.</param>
        /// <param name="fBeta">Specifies a scalar multiplied by C where the scalar is of type 'T'.</param>
        /// <param name="hC">Specifies a handle to the data for matrix C in GPU memory.</param>
        /// <param name="lda">Specifies the leading dimension of A.</param>
        /// <param name="ldb">Specifies the leading dimension of B.</param>
        /// <param name="ldc">Specifies the leading dimension of C.</param>
        public void gemm(bool bTransA, bool bTransB, int m, int n, int k, double fAlpha, long hA, long hB, double fBeta, long hC, uint lda, uint ldb, uint ldc)
        {
            if (m_dt == DataType.DOUBLE)
                m_cuda.RunDouble((int)m_hKernel, (int)CUDAFN.CUDA_GEMM2, m_param.AsDouble((bTransA) ? 1.0 : 0.0, (bTransB) ? 1.0 : 0.0, m, n, k, fAlpha, hA, hB, fBeta, hC, lda, ldb, ldc));
            else
                m_cuda.RunFloat((int)m_hKernel, (int)CUDAFN.CUDA_GEMM2, m_param.AsFloat((bTransA) ? 1.0f : 0.0f, (bTransB) ? 1.0f : 0.0f, m, n, k, (float)fAlpha, hA, hB, (float)fBeta, hC, lda, ldb, ldc));
        }

        /// <summary>
        /// Perform a matrix-matrix addition/transposition operation: C = alpha transA (A) + beta transB (B) 
        /// </summary>
        /// <remarks>
        /// This function uses [NVIDIA's cuBlas](https://developer.nvidia.com/cublas) but with a different parameter ordering.
        /// </remarks>
        /// <param name="bTransA">Specifies whether or not to transpose A.</param>
        /// <param name="bTransB">Specifies whether or not to transpose B.</param>
        /// <param name="m">Specifies the width (number of columns) of A, B and C.</param>
        /// <param name="n">Specifies the height (number of rows) of A, B and C.</param>
        /// <param name="fAlpha">Specifies a scalar multiplied by the data where the scalar is of type <code>double</code></param>
        /// <param name="hA">Specifies a handle to the data for A in GPU memory.</param>
        /// <param name="hB">Specifies a handle to the data for B in GPU memory.</param>
        /// <param name="fBeta">Specifies a scalar multiplied by C where the scalar is of type <code>double</code></param>
        /// <param name="hC">Specifies a handle to the data for C in GPU memory.</param>
        public void geam(bool bTransA, bool bTransB, int m, int n, double fAlpha, long hA, long hB, double fBeta, long hC)
        {
            geam(bTransA, bTransB, m, n, (T)Convert.ChangeType(fAlpha, typeof(T)), hA, hB, (T)Convert.ChangeType(fBeta, typeof(T)), hC);
        }

        /// <summary>
        /// Perform a matrix-matrix addition/transposition operation: C = alpha transA (A) + beta transB (B) 
        /// </summary>
        /// <remarks>
        /// This function uses [NVIDIA's cuBlas](https://developer.nvidia.com/cublas) but with a different parameter ordering.
        /// </remarks>
        /// <param name="bTransA">Specifies whether or not to transpose A.</param>
        /// <param name="bTransB">Specifies whether or not to transpose B.</param>
        /// <param name="m">Specifies the width (number of columns) of A, B and C.</param>
        /// <param name="n">Specifies the height (number of rows) of A, B and C.</param>
        /// <param name="fAlpha">Specifies a scalar multiplied by the data where the scalar is of type <code>double</code></param>
        /// <param name="hA">Specifies a handle to the data for A in GPU memory.</param>
        /// <param name="hB">Specifies a handle to the data for B in GPU memory.</param>
        /// <param name="fBeta">Specifies a scalar multiplied by C where the scalar is of type <code>double</code></param>
        /// <param name="hC">Specifies a handle to the data for C in GPU memory.</param>
        public void geam(bool bTransA, bool bTransB, int m, int n, float fAlpha, long hA, long hB, float fBeta, long hC)
        {
            geam(bTransA, bTransB, m, n, (T)Convert.ChangeType(fAlpha, typeof(T)), hA, hB, (T)Convert.ChangeType(fBeta, typeof(T)), hC);
        }

        /// <summary>
        /// Perform a matrix-matrix multiplication operation: C = alpha transB (B) transA (A) + beta C 
        /// </summary>
        /// <remarks>
        /// This function uses [NVIDIA's cuBlas](https://developer.nvidia.com/cublas) but with a different parameter ordering.
        /// </remarks>
        /// <param name="bTransA">Specifies whether or not to transpose A.</param>
        /// <param name="bTransB">Specifies whether or not to transpose B.</param>
        /// <param name="m">Specifies the width (number of columns) of A and C.</param>
        /// <param name="n">Specifies the height (number of rows) of B and C.</param>
        /// <param name="fAlpha">Specifies a scalar multiplied by the data where the scalar is of type 'T'.</param>
        /// <param name="hA">Specifies a handle to the data for matrix A in GPU memory.</param>
        /// <param name="hB">Specifies a handle to the data for matrix B in GPU memory.</param>
        /// <param name="fBeta">Specifies a scalar multiplied by C where the scalar is of type 'T'.</param>
        /// <param name="hC">Specifies a handle to the data for matrix C in GPU memory.</param>
        /// <param name="nAOffset">Specifies an offset (in items, not bytes) into the memory of A.</param>
        /// <param name="nBOffset">Specifies an offset (in items, not bytes) into the memory of B.</param>
        /// <param name="nCOffset">Specifies an offset (in items, not bytes) into the memory of C.</param>
        public void geam(bool bTransA, bool bTransB, int m, int n, T fAlpha, long hA, long hB, T fBeta, long hC, int nAOffset = 0, int nBOffset = 0, int nCOffset = 0)
        {
            if (m_dt == DataType.DOUBLE)
                m_cuda.RunDouble((int)m_hKernel, (int)CUDAFN.CUDA_GEAM, m_param.AsDouble((bTransA) ? 1.0 : 0.0, (bTransB) ? 1.0 : 0.0, m, n, convertD(fAlpha), hA, hB, convertD(fBeta), hC, nAOffset, nBOffset, nCOffset));
            else
                m_cuda.RunFloat((int)m_hKernel, (int)CUDAFN.CUDA_GEAM, m_param.AsFloat((bTransA) ? 1.0f : 0.0f, (bTransB) ? 1.0f : 0.0f, m, n, convertF(fAlpha), hA, hB, convertF(fBeta), hC, nAOffset, nBOffset, nCOffset));
        }



        /// <summary>
        /// Perform a matrix-vector multiplication operation: y = alpha transA (A) x + beta y (where x and y are vectors)
        /// </summary>
        /// <remarks>
        /// This function uses [NVIDIA's cuBlas](https://developer.nvidia.com/cublas) but with a different parameter ordering.
        /// </remarks>
        /// <param name="bTransA">Specifies whether or not to transpose A.</param>
        /// <param name="m">Specifies the width (number of columns) of A.</param>
        /// <param name="n">Specifies the height (number of rows) of A.</param>
        /// <param name="fAlpha">Specifies a scalar multiplied by the data where the scalar is of type <code>double</code></param>
        /// <param name="hA">Specifies a handle to the data for matrix A in GPU memory.</param>
        /// <param name="hX">Specifies a handle to the data for vector x in GPU memory.</param>
        /// <param name="fBeta">Specifies a scalar multiplied by y where the scalar is of type <code>double</code></param>
        /// <param name="hY">Specifies a handle to the data for vectory y in GPU memory.</param>
        public void gemv(bool bTransA, int m, int n, double fAlpha, long hA, long hX, double fBeta, long hY)
        {
            gemv(bTransA, m, n, (T)Convert.ChangeType(fAlpha, typeof(T)), hA, hX, (T)Convert.ChangeType(fBeta, typeof(T)), hY);
        }

        /// <summary>
        /// Perform a matrix-vector multiplication operation: y = alpha transA (A) x + beta y (where x and y are vectors)
        /// </summary>
        /// <remarks>
        /// This function uses [NVIDIA's cuBlas](https://developer.nvidia.com/cublas) but with a different parameter ordering.
        /// </remarks>
        /// <param name="bTransA">Specifies whether or not to transpose A.</param>
        /// <param name="m">Specifies the width (number of columns) of A.</param>
        /// <param name="n">Specifies the height (number of rows) of A.</param>
        /// <param name="fAlpha">Specifies a scalar multiplied by the data where the scalar is of type <code>float</code></param>
        /// <param name="hA">Specifies a handle to the data for matrix A in GPU memory.</param>
        /// <param name="hX">Specifies a handle to the data for vector x in GPU memory.</param>
        /// <param name="fBeta">Specifies a scalar multiplied by y where the scalar is of type <code>float</code></param>
        /// <param name="hY">Specifies a handle to the data for vectory y in GPU memory.</param>
        public void gemv(bool bTransA, int m, int n, float fAlpha, long hA, long hX, float fBeta, long hY)
        {
            gemv(bTransA, m, n, (T)Convert.ChangeType(fAlpha, typeof(T)), hA, hX, (T)Convert.ChangeType(fBeta, typeof(T)), hY);
        }

        /// <summary>
        /// Perform a matrix-vector multiplication operation: y = alpha transA (A) x + beta y (where x and y are vectors)
        /// </summary>
        /// <remarks>
        /// This function uses [NVIDIA's cuBlas](https://developer.nvidia.com/cublas) but with a different parameter ordering.
        /// </remarks>
        /// <param name="bTransA">Specifies whether or not to transpose A.</param>
        /// <param name="m">Specifies the width (number of columns) of A.</param>
        /// <param name="n">Specifies the height (number of rows) of A.</param>
        /// <param name="fAlpha">Specifies a scalar multiplied by the data where the scalar is of type 'T'.</param>
        /// <param name="hA">Specifies a handle to the data for matrix A in GPU memory.</param>
        /// <param name="hX">Specifies a handle to the data for vector X in GPU memory.</param>
        /// <param name="fBeta">Specifies a scalar multiplied by Y where the scalar is of type 'T'</param>
        /// <param name="hY">Specifies a handle to the data for vectory y in GPU memory.</param>
        /// <param name="nAOffset">Specifies an offset (in items, not bytes) into the memory of A.</param>
        /// <param name="nXOffset">Specifies an offset (in items, not bytes) into the memory of X.</param>
        /// <param name="nYOffset">Specifies an offset (in items, not bytes) into the memory of Y.</param>
        public void gemv(bool bTransA, int m, int n, T fAlpha, long hA, long hX, T fBeta, long hY, int nAOffset = 0, int nXOffset = 0, int nYOffset = 0)
        {
            if (m_dt == DataType.DOUBLE)
                m_cuda.RunDouble((int)m_hKernel, (int)CUDAFN.CUDA_GEMV, m_param.AsDouble((bTransA) ? 1.0 : 0.0, m, n, convertD(fAlpha), hA, hX, convertD(fBeta), hY, nAOffset, nXOffset, nYOffset));
            else
                m_cuda.RunFloat((int)m_hKernel, (int)CUDAFN.CUDA_GEMV, m_param.AsFloat((bTransA) ? 1.0f : 0.0f, m, n, convertF(fAlpha), hA, hX, convertF(fBeta), hY, nAOffset, nXOffset, nYOffset));
        }

        /// <summary>
        /// Perform a vector-vector multiplication operation: A = x * (fAlpha * y) (where x and y are vectors and A is an m x n Matrix)
        /// </summary>
        /// <remarks>
        /// This function uses [NVIDIA's cuBlas](https://developer.nvidia.com/cublas) but with a different parameter ordering.
        /// </remarks>
        /// <param name="m">Specifies the length of X and rows in A (m x n).</param>
        /// <param name="n">Specifies the length of Y and cols in A (m x n).</param>
        /// <param name="fAlpha">Specifies a scalar multiplied by y where the scalar is of type 'T'.</param>
        /// <param name="hX">Specifies a handle to the data for matrix X (m in length) in GPU memory.</param>
        /// <param name="hY">Specifies a handle to the data for vector Y (n in length) in GPU memory.</param>
        /// <param name="hA">Specifies a handle to the data for matrix A (m x n) in GPU memory.</param>
        public void ger(int m, int n, double fAlpha, long hX, long hY, long hA)
        {
            ger(m, n, (T)Convert.ChangeType(fAlpha, typeof(T)), hX, hY, hA);
        }

        /// <summary>
        /// Perform a vector-vector multiplication operation: A = x * (fAlpha * y) (where x and y are vectors and A is an m x n Matrix)
        /// </summary>
        /// <remarks>
        /// This function uses [NVIDIA's cuBlas](https://developer.nvidia.com/cublas) but with a different parameter ordering.
        /// </remarks>
        /// <param name="m">Specifies the length of X and rows in A (m x n).</param>
        /// <param name="n">Specifies the length of Y and cols in A (m x n).</param>
        /// <param name="fAlpha">Specifies a scalar multiplied by y where the scalar is of type 'T'.</param>
        /// <param name="hX">Specifies a handle to the data for matrix X (m in length) in GPU memory.</param>
        /// <param name="hY">Specifies a handle to the data for vector Y (n in length) in GPU memory.</param>
        /// <param name="hA">Specifies a handle to the data for matrix A (m x n) in GPU memory.</param>
        public void ger(int m, int n, float fAlpha, long hX, long hY, long hA)
        {
            ger(m, n, (T)Convert.ChangeType(fAlpha, typeof(T)), hX, hY, hA);
        }

        /// <summary>
        /// Perform a vector-vector multiplication operation: A = x * (fAlpha * y) (where x and y are vectors and A is an m x n Matrix)
        /// </summary>
        /// <remarks>
        /// This function uses [NVIDIA's cuBlas](https://developer.nvidia.com/cublas) but with a different parameter ordering.
        /// </remarks>
        /// <param name="m">Specifies the length of X and rows in A (m x n).</param>
        /// <param name="n">Specifies the length of Y and cols in A (m x n).</param>
        /// <param name="fAlpha">Specifies a scalar multiplied by y where the scalar is of type 'T'.</param>
        /// <param name="hX">Specifies a handle to the data for matrix X (m in length) in GPU memory.</param>
        /// <param name="hY">Specifies a handle to the data for vector Y (n in length) in GPU memory.</param>
        /// <param name="hA">Specifies a handle to the data for matrix A (m x n) in GPU memory.</param>
        public void ger(int m, int n, T fAlpha, long hX, long hY, long hA)
        {
            if (m_dt == DataType.DOUBLE)
                m_cuda.RunDouble((int)m_hKernel, (int)CUDAFN.CUDA_GER, m_param.AsDouble(m, n, convertD(fAlpha), hX, hY, hA));
            else
                m_cuda.RunFloat((int)m_hKernel, (int)CUDAFN.CUDA_GER, m_param.AsFloat(m, n, convertF(fAlpha), hX, hY, hA));
        }

        /// <summary>
        /// Multiply the vector X by a scalar and add the result to the vector Y.
        /// </summary>
        /// <remarks>
        /// This function uses [NVIDIA's cuBlas](https://developer.nvidia.com/cublas).
        /// </remarks>
        /// <param name="n">Specifies the number of items (not bytes) in the vector X and Y.</param>
        /// <param name="fAlpha">Specifies the scalar to multiply where the scalar is of type <code>double</code></param>
        /// <param name="hX">Specifies a handle to the vector X in GPU memory.</param>
        /// <param name="hY">Specifies a handle to the vector Y in GPU memory.</param>
        public void axpy(int n, double fAlpha, long hX, long hY)
        {
            axpy(n, (T)Convert.ChangeType(fAlpha, typeof(T)), hX, hY);
        }

        /// <summary>
        /// Multiply the vector X by a scalar and add the result to the vector Y.
        /// </summary>
        /// <remarks>
        /// This function uses [NVIDIA's cuBlas](https://developer.nvidia.com/cublas).
        /// </remarks>
        /// <param name="n">Specifies the number of items (not bytes) in the vector X and Y.</param>
        /// <param name="fAlpha">Specifies the scalar to multiply where the scalar is of type <code>float</code></param>
        /// <param name="hX">Specifies a handle to the vector X in GPU memory.</param>
        /// <param name="hY">Specifies a handle to the vector Y in GPU memory.</param>
        public void axpy(int n, float fAlpha, long hX, long hY)
        {
            axpy(n, (T)Convert.ChangeType(fAlpha, typeof(T)), hX, hY);
        }

        /// <summary>
        /// Multiply the vector X by a scalar and add the result to the vector Y.
        /// </summary>
        /// <remarks>
        /// This function uses [NVIDIA's cuBlas](https://developer.nvidia.com/cublas).
        /// </remarks>
        /// <param name="n">Specifies the number of items (not bytes) in the vector X and Y.</param>
        /// <param name="fAlpha">Specifies the scalar to multiply where the scalar is of type 'T'.</param>
        /// <param name="hX">Specifies a handle to the vector X in GPU memory.</param>
        /// <param name="hY">Specifies a handle to the vector Y in GPU memory.</param>
        /// <param name="nXOff">Optionally, specifies an offset (in items, not bytes) into the memory of X.</param>
        /// <param name="nYOff">Optionally, specifies an offset (in items, not bytes) into the memory of Y.</param>
        public void axpy(int n, T fAlpha, long hX, long hY, int nXOff = 0, int nYOff = 0)
        {
            if (m_dt == DataType.DOUBLE)
                m_cuda.RunDouble((int)m_hKernel, (int)CUDAFN.CUDA_AXPY, m_param.AsDouble(n, convertD(fAlpha), hX, hY, nXOff, nYOff));
            else
                m_cuda.RunFloat((int)m_hKernel, (int)CUDAFN.CUDA_AXPY, m_param.AsFloat(n, convertF(fAlpha), hX, hY, nXOff, nYOff));
        }

        /// <summary>
        /// Scale the vector x and then multiply the vector X by a scalar and add the result to the vector Y.
        /// </summary>
        /// <remarks>
        /// This function uses [NVIDIA's cuBlas](https://developer.nvidia.com/cublas).
        /// </remarks>
        /// <param name="n">Specifies the number of items (not bytes) in the vector X and Y.</param>
        /// <param name="fAlpha">Specifies the scalar to multiply where the scalar is of type <code>double</code></param>
        /// <param name="hX">Specifies a handle to the vector X in GPU memory.</param>
        /// <param name="fBeta">Specifies the scaling factor to apply to vector X, where the scaling factor is of type <code>double</code></param>
        /// <param name="hY">Specifies a handle to the vector Y in GPU memory.</param>
        public void axpby(int n, double fAlpha, long hX, double fBeta, long hY)
        {
            axpby(n, (T)Convert.ChangeType(fAlpha, typeof(T)), hX, (T)Convert.ChangeType(fBeta, typeof(T)), hY);
        }

        /// <summary>
        /// Scale the vector x and then multiply the vector X by a scalar and add the result to the vector Y.
        /// </summary>
        /// <remarks>
        /// This function uses [NVIDIA's cuBlas](https://developer.nvidia.com/cublas).
        /// </remarks>
        /// <param name="n">Specifies the number of items (not bytes) in the vector X and Y.</param>
        /// <param name="fAlpha">Specifies the scalar to multiply where the scalar is of type <code>float</code></param>
        /// <param name="hX">Specifies a handle to the vector X in GPU memory.</param>
        /// <param name="fBeta">Specifies the scaling factor to apply to vector X, where the scaling factor is of type <code>float</code></param>
        /// <param name="hY">Specifies a handle to the vector Y in GPU memory.</param>
        public void axpby(int n, float fAlpha, long hX, float fBeta, long hY)
        {
            axpby(n, (T)Convert.ChangeType(fAlpha, typeof(T)), hX, (T)Convert.ChangeType(fBeta, typeof(T)), hY);
        }

        /// <summary>
        /// Scale the vector x by Alpha and scale vector y by Beta and then add both together.
        /// 
        /// Y = (X * fAlpha) + (Y * fBeta)
        /// </summary>
        /// <remarks>
        /// This function uses [NVIDIA's cuBlas](https://developer.nvidia.com/cublas).
        /// </remarks>
        /// <param name="n">Specifies the number of items (not bytes) in the vector X and Y.</param>
        /// <param name="fAlpha">Specifies the scalar to multiply where the scalar is of type 'T'.</param>
        /// <param name="hX">Specifies a handle to the vector X in GPU memory.</param>
        /// <param name="fBeta">Specifies the scaling factor to apply to vector X, where the scaling factor is of type 'T'.</param>
        /// <param name="hY">Specifies a handle to the vector Y in GPU memory.</param>
        public void axpby(int n, T fAlpha, long hX, T fBeta, long hY)
        {
            if (m_dt == DataType.DOUBLE)
                m_cuda.RunDouble((int)m_hKernel, (int)CUDAFN.CUDA_AXPBY, m_param.AsDouble(n, convertD(fAlpha), hX, convertD(fBeta), hY));
            else
                m_cuda.RunFloat((int)m_hKernel, (int)CUDAFN.CUDA_AXPBY, m_param.AsFloat(n, convertF(fAlpha), hX, convertF(fBeta), hY));
        }

        /// <summary>
        /// Multiply a matrix with a vector.
        /// </summary>
        /// <param name="n">Specifies the number of items.</param>
        /// <param name="hA">Specifies the matrix to multiply.</param>
        /// <param name="nAOff">Specifies the offset to apply to the GPU memory of hA.</param>
        /// <param name="hX">Specifies the vector to multiply.</param>
        /// <param name="nXOff">Specifies the offset to apply to the GPU memory of hX.</param>
        /// <param name="nC">Specifies the number of channels.</param>
        /// <param name="nSpatialDim">Specifies the spatial dimension.</param>
        /// <param name="bTranspose">Specifies whether or not to transpose the matrix.</param>
        /// <param name="hB">Specifies the output matrix.</param>
        /// <param name="nBOff">Specifies the offset to apply to the GPU memory of hB.</param>
        public void mulbsx(int n, long hA, int nAOff, long hX, int nXOff, int nC, int nSpatialDim, bool bTranspose, long hB, int nBOff)
        {
            if (m_dt == DataType.DOUBLE)
                m_cuda.RunDouble((int)m_hKernel, (int)CUDAFN.CUDA_MULBSX, m_param.AsDouble(n, hA, nAOff, hX, nXOff, nC, nSpatialDim, (bTranspose) ? 1 : 0, hB, nBOff));
            else
                m_cuda.RunFloat((int)m_hKernel, (int)CUDAFN.CUDA_MULBSX, m_param.AsFloat(n, hA, nAOff, hX, nXOff, nC, nSpatialDim, (bTranspose) ? 1 : 0, hB, nBOff));
        }

        /// <summary>
        /// Divide a matrix by a vector.
        /// </summary>
        /// <param name="n">Specifies the number of items.</param>
        /// <param name="hA">Specifies the matrix to divide.</param>
        /// <param name="nAOff">Specifies the offset to apply to the GPU memory of hA.</param>
        /// <param name="hX">Specifies the divisor vector.</param>
        /// <param name="nXOff">Specifies the offset to apply to the GPU memory of hX.</param>
        /// <param name="nC">Specifies the number of channels.</param>
        /// <param name="nSpatialDim">Specifies the spatial dimension.</param>
        /// <param name="bTranspose">Specifies whether or not to transpose the matrix.</param>
        /// <param name="hB">Specifies the output matrix.</param>
        /// <param name="nBOff">Specifies the offset to apply to the GPU memory of hB.</param>
        public void divbsx(int n, long hA, int nAOff, long hX, int nXOff, int nC, int nSpatialDim, bool bTranspose, long hB, int nBOff)
        {
            if (m_dt == DataType.DOUBLE)
                m_cuda.RunDouble((int)m_hKernel, (int)CUDAFN.CUDA_DIVBSX, m_param.AsDouble(n, hA, nAOff, hX, nXOff, nC, nSpatialDim, (bTranspose) ? 1 : 0, hB, nBOff));
            else
                m_cuda.RunFloat((int)m_hKernel, (int)CUDAFN.CUDA_DIVBSX, m_param.AsFloat(n, hA, nAOff, hX, nXOff, nC, nSpatialDim, (bTranspose) ? 1 : 0, hB, nBOff));
        }

        /// <summary>
        /// Set the bounds of all items within the data to a set range of values.
        /// </summary>
        /// <param name="n">Specifies the number of items.</param>
        /// <param name="dfMin">Specifies the minimum value.</param>
        /// <param name="dfMax">Specifies the maximum value.</param>
        /// <param name="hX">Specifies a handle to the GPU data to be bound.</param>
        public void set_bounds(int n, double dfMin, double dfMax, long hX)
        {
            if (m_dt == DataType.DOUBLE)
            {
                m_cuda.RunDouble((int)m_hKernel, (int)CUDAFN.CUDA_SET_BOUNDS, m_param.AsDouble(n, dfMin, dfMax, hX));
            }
            else
            {
                float fMin = -float.MaxValue;
                float fMax = float.MaxValue;

                if (dfMin > -float.MaxValue && dfMin < float.MaxValue)
                    fMin = (float)dfMin;
                else if (dfMin > float.MaxValue)
                    fMin = float.MaxValue;

                if (dfMax > -float.MaxValue && dfMax < float.MaxValue)
                    fMax = (float)dfMax;
                else if (dfMin < -float.MaxValue)
                    fMax = -float.MaxValue;

                m_cuda.RunFloat((int)m_hKernel, (int)CUDAFN.CUDA_SET_BOUNDS, m_param.AsFloat(n, fMin, fMax, hX));
            }
        }

        /// <summary>
        /// Scales the data in X by a scaling factor.
        /// </summary>
        /// <remarks>
        /// This function uses [NVIDIA's cuBlas](https://developer.nvidia.com/cublas).
        /// </remarks>
        /// <param name="n">Specifies the number of items (not bytes) in the vector X and Y.</param>
        /// <param name="fAlpha">Specifies the scaling factor to apply to vector X, where the scaling factor is of type <code>double</code></param>
        /// <param name="hX">Specifies a handle to the vector X in GPU memory.</param>
        /// <param name="nXOff">Specifies an offset (in items, not bytes) into the memory of X.</param>
        public void scal(int n, double fAlpha, long hX, int nXOff = 0)
        {
            scal(n, (T)Convert.ChangeType(fAlpha, typeof(T)), hX, nXOff);
        }

        /// <summary>
        /// Scales the data in X by a scaling factor.
        /// </summary>
        /// <remarks>
        /// This function uses [NVIDIA's cuBlas](https://developer.nvidia.com/cublas).
        /// </remarks>
        /// <param name="n">Specifies the number of items (not bytes) in the vector X and Y.</param>
        /// <param name="fAlpha">Specifies the scaling factor to apply to vector X, where the scaling factor is of type <code>float</code></param>
        /// <param name="hX">Specifies a handle to the vector X in GPU memory.</param>
        /// <param name="nXOff">Specifies an offset (in items, not bytes) into the memory of X.</param>
        public void scal(int n, float fAlpha, long hX, int nXOff = 0)
        {
            scal(n, (T)Convert.ChangeType(fAlpha, typeof(T)), hX, nXOff);
        }

        /// <summary>
        /// Scales the data in X by a scaling factor.
        /// </summary>
        /// <remarks>
        /// This function uses [NVIDIA's cuBlas](https://developer.nvidia.com/cublas).
        /// </remarks>
        /// <param name="n">Specifies the number of items (not bytes) in the vector X and Y.</param>
        /// <param name="fAlpha">Specifies the scaling factor to apply to vector X, where the scaling factor is of type 'T'.</param>
        /// <param name="hX">Specifies a handle to the vector X in GPU memory.</param>
        /// <param name="nXOff">Specifies an offset (in items, not bytes) into the memory of X.</param>
        public void scal(int n, T fAlpha, long hX, int nXOff = 0)
        {
            if (m_dt == DataType.DOUBLE)
                m_cuda.RunDouble((int)m_hKernel, (int)CUDAFN.CUDA_SCAL, m_param.AsDouble(n, convertD(fAlpha), hX, nXOff));
            else
                m_cuda.RunFloat((int)m_hKernel, (int)CUDAFN.CUDA_SCAL, m_param.AsFloat(n, convertF(fAlpha), hX, nXOff));
        }

        /// <summary>
        /// Computes the dot product of X and Y.
        /// </summary>
        /// <remarks>
        /// This function uses [NVIDIA's cuBlas](https://developer.nvidia.com/cublas).
        /// </remarks>
        /// <param name="n">Specifies the number of items (not bytes) in the vector X and Y.</param>
        /// <param name="hX">Specifies a handle to the vector X in GPU memory.</param>
        /// <param name="hY">Specifies a handle to the vector Y in GPU memory.</param>
        /// <returns>The dot product is returned as a type <code>double</code></returns>
        public double dot_double(int n, long hX, long hY)
        {
            return (double)Convert.ChangeType(dot(n, hX, hY), typeof(double));
        }

        /// <summary>
        /// Computes the dot product of X and Y.
        /// </summary>
        /// <remarks>
        /// This function uses [NVIDIA's cuBlas](https://developer.nvidia.com/cublas).
        /// </remarks>
        /// <param name="n">Specifies the number of items (not bytes) in the vector X and Y.</param>
        /// <param name="hX">Specifies a handle to the vector X in GPU memory.</param>
        /// <param name="hY">Specifies a handle to the vector Y in GPU memory.</param>
        /// <returns>The dot product is returned as a type <code>float</code></returns>
        public float dot_float(int n, long hX, long hY)
        {
            return (float)Convert.ChangeType(dot(n, hX, hY), typeof(float));
        }

        /// <summary>
        /// Computes the dot product of X and Y.
        /// </summary>
        /// <remarks>
        /// This function uses [NVIDIA's cuBlas](https://developer.nvidia.com/cublas).
        /// </remarks>
        /// <param name="n">Specifies the number of items (not bytes) in the vector X and Y.</param>
        /// <param name="hX">Specifies a handle to the vector X in GPU memory.</param>
        /// <param name="hY">Specifies a handle to the vector Y in GPU memory.</param>
        /// <param name="nXOff">Optionally, specifies an offset (in items, not bytes) into the memory of X.</param>
        /// <param name="nYOff">Optionally, specifies an offset (in items, not bytes) into the memory of Y.</param>
        /// <returns>The dot product is returned as a type 'T'.</returns>
        public T dot(int n, long hX, long hY, int nXOff = 0, int nYOff = 0)
        {
            if (m_dt == DataType.DOUBLE)
            {
                double[] rg = m_cuda.RunDouble((int)m_hKernel, (int)CUDAFN.CUDA_DOT, m_param.AsDouble(n, hX, hY, nXOff, nYOff));
                return (T)Convert.ChangeType(rg[0], typeof(T));
            }
            else
            {
                float[] rg = m_cuda.RunFloat((int)m_hKernel, (int)CUDAFN.CUDA_DOT, m_param.AsFloat(n, hX, hY, nXOff, nYOff));
                return (T)Convert.ChangeType(rg[0], typeof(T));
            }
        }

        /// <summary>
        /// Computes the sum of absolute values in X.
        /// </summary>
        /// <remarks>
        /// This function uses [NVIDIA's cuBlas](https://developer.nvidia.com/cublas).
        /// </remarks>
        /// <param name="n">Specifies the number of items (not bytes) in the vector X.</param>
        /// <param name="hX">Specifies a handle to the vector X in GPU memory.</param>
        /// <param name="nXOff">Optionally, specifies an offset (in items, not bytes) into the memory of X.</param>
        /// <returns>the absolute sum is returned as a type <code>double</code></returns>
        public double asum_double(int n, long hX, int nXOff = 0)
        {
            return (double)Convert.ChangeType(asum(n, hX, nXOff), typeof(double));
        }

        /// <summary>
        /// Computes the sum of absolute values in X.
        /// </summary>
        /// <remarks>
        /// This function uses [NVIDIA's cuBlas](https://developer.nvidia.com/cublas).
        /// </remarks>
        /// <param name="n">Specifies the number of items (not bytes) in the vector X.</param>
        /// <param name="hX">Specifies a handle to the vector X in GPU memory.</param>
        /// <param name="nXOff">Optionally, specifies an offset (in items, not bytes) into the memory of X.</param>
        /// <returns>the absolute sum is returned as a type <code>float</code></returns>
        public float asum_float(int n, long hX, int nXOff = 0)
        {
            return (float)Convert.ChangeType(asum(n, hX, nXOff), typeof(float));
        }

        /// <summary>
        /// Computes the sum of absolute values in X.
        /// </summary>
        /// <remarks>
        /// This function uses [NVIDIA's cuBlas](https://developer.nvidia.com/cublas).
        /// </remarks>
        /// <param name="n">Specifies the number of items (not bytes) in the vector X.</param>
        /// <param name="hX">Specifies a handle to the vector X in GPU memory.</param>
        /// <param name="nXOff">Optionally, specifies an offset (in items, not bytes) into the memory of X.</param>
        /// <returns>the absolute value sum is returned as a type 'T'.</returns>
        public T asum(int n, long hX, int nXOff = 0)
        {
            if (m_dt == DataType.DOUBLE)
            {
                double[] rg = m_cuda.RunDouble((int)m_hKernel, (int)CUDAFN.CUDA_ASUM, m_param.AsDouble(n, hX, nXOff));
                return (T)Convert.ChangeType(rg[0], typeof(T));
            }
            else
            {
                float[] rg = m_cuda.RunFloat((int)m_hKernel, (int)CUDAFN.CUDA_ASUM, m_param.AsFloat(n, hX, nXOff));
                return (T)Convert.ChangeType(rg[0], typeof(T));
            }
        }

        /// <summary>
        /// Scales the values in X and places them in Y.
        /// </summary>
        /// <remarks>
        /// This function uses [NVIDIA's cuBlas](https://developer.nvidia.com/cublas).
        /// </remarks>
        /// <param name="n">Specifies the number of items (not bytes) in the vector X and Y.</param>
        /// <param name="fAlpha">Specifies the scale value in type <code>double</code></param>
        /// <param name="hX">Specifies a handle to the vector X in GPU memory.</param>
        /// <param name="hY">Specifies a handle to the vector Y in GPU memory.</param>
        public void scale(int n, double fAlpha, long hX, long hY)
        {
            scale(n, (T)Convert.ChangeType(fAlpha, typeof(T)), hX, hY);
        }

        /// <summary>
        /// Scales the values in X and places them in Y.
        /// </summary>
        /// <remarks>
        /// This function uses [NVIDIA's cuBlas](https://developer.nvidia.com/cublas).
        /// </remarks>
        /// <param name="n">Specifies the number of items (not bytes) in the vector X and Y.</param>
        /// <param name="fAlpha">Specifies the scale value in type <code>float</code></param>
        /// <param name="hX">Specifies a handle to the vector X in GPU memory.</param>
        /// <param name="hY">Specifies a handle to the vector Y in GPU memory.</param>
        public void scale(int n, float fAlpha, long hX, long hY)
        {
            scale(n, (T)Convert.ChangeType(fAlpha, typeof(T)), hX, hY);
        }

        /// <summary>
        /// Scales the values in X and places them in Y.
        /// </summary>
        /// <remarks>
        /// This function uses [NVIDIA's cuBlas](https://developer.nvidia.com/cublas).
        /// </remarks>
        /// <param name="n">Specifies the number of items (not bytes) in the vector X and Y.</param>
        /// <param name="fAlpha">Specifies the scale value in type 'T'.</param>
        /// <param name="hX">Specifies a handle to the vector X in GPU memory.</param>
        /// <param name="hY">Specifies a handle to the vector Y in GPU memory.</param>
        /// <param name="nXOff">Optionally, specifies an offset (in items, not bytes) into the memory of X.</param>
        /// <param name="nYOff">Optionally, specifies an offset (in items, not bytes) into the memory of Y.</param>
        public void scale(int n, T fAlpha, long hX, long hY, int nXOff = 0, int nYOff = 0)
        {
            if (m_dt == DataType.DOUBLE)
                m_cuda.RunDouble((int)m_hKernel, (int)CUDAFN.CUDA_SCALE, m_param.AsDouble(n, convertD(fAlpha), hX, hY, nXOff, nYOff));
            else
                m_cuda.RunFloat((int)m_hKernel, (int)CUDAFN.CUDA_SCALE, m_param.AsFloat(n, convertF(fAlpha), hX, hY, nXOff, nYOff));
        }

        /// <summary>
        /// Scales the values in X and places the result in Y (can also run inline where X = Y).
        /// </summary>
        /// <param name="n">Specifies the number of items (not bytes) in the vector X.</param>
        /// <param name="hX">Specifies a handle to the vector X in GPU memory.</param>
        /// <param name="hY">Specifies a handle to the vector Y in GPU memory.</param>
        /// <param name="fMin">Specifies the minimum of the new range.</param>
        /// <param name="fMax">Specifies the maximum of the new range.</param>
        public void scale_to_range(int n, long hX, long hY, double fMin, double fMax)
        {
            if (m_dt == DataType.DOUBLE)
                m_cuda.RunDouble((int)m_hKernel, (int)CUDAFN.CUDA_SCALE_TO_RANGE, m_param.AsDouble(n, hX, hY, fMin, fMax));
            else
                m_cuda.RunFloat((int)m_hKernel, (int)CUDAFN.CUDA_SCALE_TO_RANGE, m_param.AsFloat(n, hX, hY, (float)fMin, (float)fMax));
        }

        /// <summary>
        /// Adds a scalar value to each element of Y.
        /// </summary>
        /// <remarks>
        /// Y = Y + alpha
        /// </remarks>
        /// <param name="n">Specifies the number of items (not bytes) in the vector Y.</param>
        /// <param name="fAlpha">Specifies the scalar value in type <code>double</code></param>
        /// <param name="hY">Specifies a handle to the vector Y in GPU memory.</param>
        public void add_scalar(int n, double fAlpha, long hY)
        {
            add_scalar(n, (T)Convert.ChangeType(fAlpha, typeof(T)), hY);
        }

        /// <summary>
        /// Adds a scalar value to each element of Y.
        /// </summary>
        /// <remarks>
        /// Y = Y + alpha
        /// </remarks>
        /// <param name="n">Specifies the number of items (not bytes) in the vector Y.</param>
        /// <param name="fAlpha">Specifies the scalar value in type <code>float</code></param>
        /// <param name="hY">Specifies a handle to the vector Y in GPU memory.</param>
        public void add_scalar(int n, float fAlpha, long hY)
        {
            add_scalar(n, (T)Convert.ChangeType(fAlpha, typeof(T)), hY);
        }

        /// <summary>
        /// Adds a scalar value to each element of Y.
        /// </summary>
        /// <remarks>
        /// Y = Y + alpha
        /// </remarks>
        /// <param name="n">Specifies the number of items (not bytes) in the vector Y.</param>
        /// <param name="fAlpha">Specifies the scalar value in type 'T'.</param>
        /// <param name="hY">Specifies a handle to the vector Y in GPU memory.</param>
        /// <param name="nYOff">Optionally, specifies an offset into Y.  The default is 0.</param>
        public void add_scalar(int n, T fAlpha, long hY, int nYOff = 0)
        {
            if (m_dt == DataType.DOUBLE)
                m_cuda.RunDouble((int)m_hKernel, (int)CUDAFN.CUDA_ADD_SCALAR, m_param.AsDouble(n, convertD(fAlpha), hY, nYOff));
            else
                m_cuda.RunFloat((int)m_hKernel, (int)CUDAFN.CUDA_ADD_SCALAR, m_param.AsFloat(n, convertF(fAlpha), hY, nYOff));
        }

        /// <summary>
        /// Adds A to B and places the result in Y.
        /// </summary>
        /// <remarks>
        /// Y = A + B
        /// </remarks>
        /// <param name="n">Specifies the number of items (not bytes) in the vectors A, B and Y.</param>
        /// <param name="hA">Specifies a handle to the vector A in GPU memory.</param>
        /// <param name="hB">Specifies a handle to the vector B in GPU memory.</param>
        /// <param name="hY">Specifies a handle to the vector Y in GPU memory.</param>
        public void add(int n, long hA, long hB, long hY)
        {
            if (m_dt == DataType.DOUBLE)
                m_cuda.RunDouble((int)m_hKernel, (int)CUDAFN.CUDA_ADD, m_param.AsDouble(n, hA, hB, hY));
            else
                m_cuda.RunFloat((int)m_hKernel, (int)CUDAFN.CUDA_ADD, m_param.AsFloat(n, hA, hB, hY));
        }

        /// <summary>
        /// Adds A to (B times scalar) and places the result in Y. 
        /// </summary>
        /// <remarks>
        /// Y = A + (B * alpha)
        /// </remarks>
        /// <param name="n">Specifies the number of items (not bytes) in the vectors A, B and Y.</param>
        /// <param name="hA">Specifies a handle to the vector A in GPU memory.</param>
        /// <param name="hB">Specifies a handle to the vector B in GPU memory.</param>
        /// <param name="hY">Specifies a handle to the vector Y in GPU memory.</param>
        /// <param name="dfAlpha">Specifies a scalar int type <code>double</code></param>
        public void add(int n, long hA, long hB, long hY, double dfAlpha)
        {
            if (m_dt == DataType.DOUBLE)
                m_cuda.RunDouble((int)m_hKernel, (int)CUDAFN.CUDA_ADD, m_param.AsDouble(n, hA, hB, hY, dfAlpha));
            else
                m_cuda.RunFloat((int)m_hKernel, (int)CUDAFN.CUDA_ADD, m_param.AsFloat(n, hA, hB, hY, (float)dfAlpha));
        }

        /// <summary>
        /// Adds A to (B times scalar) and places the result in Y.
        /// </summary>
        /// <remarks>
        /// Y = A + (B * alpha)
        /// </remarks>
        /// <param name="n">Specifies the number of items (not bytes) in the vectors A, B and Y.</param>
        /// <param name="hA">Specifies a handle to the vector A in GPU memory.</param>
        /// <param name="hB">Specifies a handle to the vector B in GPU memory.</param>
        /// <param name="hY">Specifies a handle to the vector Y in GPU memory.</param>
        /// <param name="fAlpha">Specifies a scalar int type <code>float</code></param>
        public void add(int n, long hA, long hB, long hY, float fAlpha)
        {
            if (m_dt == DataType.DOUBLE)
                m_cuda.RunDouble((int)m_hKernel, (int)CUDAFN.CUDA_ADD, m_param.AsDouble(n, hA, hB, hY, fAlpha));
            else
                m_cuda.RunFloat((int)m_hKernel, (int)CUDAFN.CUDA_ADD, m_param.AsFloat(n, hA, hB, hY, fAlpha));
        }

        /// <summary>
        /// Adds A to (B times scalar) and places the result in Y.
        /// </summary>
        /// <remarks>
        /// Y = A + (B * alpha)
        /// </remarks>
        /// <param name="n">Specifies the number of items (not bytes) in the vectors A, B and Y.</param>
        /// <param name="hA">Specifies a handle to the vector A in GPU memory.</param>
        /// <param name="hB">Specifies a handle to the vector B in GPU memory.</param>
        /// <param name="hY">Specifies a handle to the vector Y in GPU memory.</param>
        /// <param name="dfAlphaA">Specifies a scalar int type 'T' applied to A.</param>
        /// <param name="dfAlphaB">Specifies a scalar int type 'T' applied to B.</param>
        /// <param name="nAOff">Optionally, specifies an offset (in items, not bytes) into the memory of A.</param>
        /// <param name="nBOff">Optionally, specifies an offset (in items, not bytes) into the memory of B.</param>
        /// <param name="nYOff">Optionally, specifies an offset (in items, not bytes) into the memory of Y.</param>
        public void add(int n, long hA, long hB, long hY, double dfAlphaA, double dfAlphaB, int nAOff = 0, int nBOff = 0, int nYOff = 0)
        {
            if (m_dt == DataType.DOUBLE)
                m_cuda.RunDouble((int)m_hKernel, (int)CUDAFN.CUDA_ADD2, m_param.AsDouble(n, hA, hB, hY, dfAlphaA, dfAlphaB, nAOff, nBOff, nYOff));
            else
                m_cuda.RunFloat((int)m_hKernel, (int)CUDAFN.CUDA_ADD2, m_param.AsFloat(n, hA, hB, hY, (float)dfAlphaA, (float)dfAlphaB, nAOff, nBOff, nYOff));
        }

        /// <summary>
        /// Subtracts B from A and places the result in Y.
        /// </summary>
        /// <remarks>
        /// Y = A - B
        /// </remarks>
        /// <param name="n">Specifies the number of items (not bytes) in the vectors A, B and Y.</param>
        /// <param name="hA">Specifies a handle to the vector A in GPU memory.</param>
        /// <param name="hB">Specifies a handle to the vector B in GPU memory.</param>
        /// <param name="hY">Specifies a handle to the vector Y in GPU memory.</param>
        /// <param name="nAOff">Optionally, specifies an offset (in items, not bytes) into the memory of A.</param>
        /// <param name="nBOff">Optionally, specifies an offset (in items, not bytes) into the memory of B.</param>
        /// <param name="nYOff">Optionally, specifies an offset (in items, not bytes) into the memory of Y.</param>
        /// <param name="nB">Optionally, specifies a number of 'B' items to subtract (default = 0 which causes ALL items in B to be subtracted).
        /// When 'nB' > 0, it must be a factor of 'n' and causes that number of B items to be subtracted as a block from A.
        /// </param>
        public void sub(int n, long hA, long hB, long hY, int nAOff = 0, int nBOff = 0, int nYOff = 0, int nB = 0)
        {
            if (m_dt == DataType.DOUBLE)
                m_cuda.RunDouble((int)m_hKernel, (int)CUDAFN.CUDA_SUB, m_param.AsDouble(n, hA, hB, hY, nAOff, nBOff, nYOff, nB));
            else
                m_cuda.RunFloat((int)m_hKernel, (int)CUDAFN.CUDA_SUB, m_param.AsFloat(n, hA, hB, hY, nAOff, nBOff, nYOff, nB));
        }


        /// <summary>
        /// Multiplies each element of A with each element of B and places the result in Y.
        /// </summary>
        /// <remarks>
        /// Y = A * B (element by element)
        /// </remarks>
        /// <param name="n">Specifies the number of items (not bytes) in the vectors A, B and Y.</param>
        /// <param name="hA">Specifies a handle to the vector A in GPU memory.</param>
        /// <param name="hB">Specifies a handle to the vector B in GPU memory.</param>
        /// <param name="hY">Specifies a handle to the vector Y in GPU memory.</param>
        /// <param name="nAOff">Optionally, specifies an offset (in items, not bytes) into the memory of A.</param>
        /// <param name="nBOff">Optionally, specifies an offset (in items, not bytes) into the memory of B.</param>
        /// <param name="nYOff">Optionally, specifies an offset (in items, not bytes) into the memory of Y.</param>
        public void mul(int n, long hA, long hB, long hY, int nAOff = 0, int nBOff = 0, int nYOff = 0)
        {
            if (m_dt == DataType.DOUBLE)
                m_cuda.RunDouble((int)m_hKernel, (int)CUDAFN.CUDA_MUL, m_param.AsDouble(n, hA, hB, hY, nAOff, nBOff, nYOff));
            else
                m_cuda.RunFloat((int)m_hKernel, (int)CUDAFN.CUDA_MUL, m_param.AsFloat(n, hA, hB, hY, nAOff, nBOff, nYOff));
        }

        /// <summary>
        /// Subtracts every <i>nInnterNum</i> element of B from A and performs a dot product on the result.
        /// </summary>
        /// <remarks>
        /// Y[i] = (A[i] - B[i%nInnerNum]) * (A[i] - B[i%nInnerNum])
        /// </remarks>
        /// <param name="n">Specifies the number of items (not bytes) in the vectors A, B and Y.</param>
        /// <param name="nN">Specifies the inner count.</param>
        /// <param name="nInnerNum">Specifies the dimension.</param>
        /// <param name="hA">Specifies a handle to the vector A in GPU memory.</param>
        /// <param name="hB">Specifies a handle to the vector B in GPU memory.</param>
        /// <param name="hY">Specifies a handle to the vector Y in GPU memory.</param>
        /// <param name="nAOff">Optionally, specifies an offset (in items, not bytes) into the memory of A.</param>
        /// <param name="nBOff">Optionally, specifies an offset (in items, not bytes) into the memory of B.</param>
        /// <param name="nYOff">Optionally, specifies an offset (in items, not bytes) into the memory of Y.</param>
        public void sub_and_dot(int n, int nN, int nInnerNum, long hA, long hB, long hY, int nAOff, int nBOff, int nYOff)
        {
            if (m_dt == DataType.DOUBLE)
                m_cuda.RunDouble((int)m_hKernel, (int)CUDAFN.CUDA_SUB_AND_DOT, m_param.AsDouble(n, nN, nInnerNum, hA, hB, hY, nAOff, nBOff, nYOff));
            else
                m_cuda.RunFloat((int)m_hKernel, (int)CUDAFN.CUDA_SUB_AND_DOT, m_param.AsFloat(n, nN, nInnerNum, hA, hB, hY, nAOff, nBOff, nYOff));
        }

        /// <summary>
        /// Mutlipy each element of Y by a scalar.
        /// </summary>
        /// <remarks>
        /// Y = Y * alpha
        /// </remarks>
        /// <param name="n">Specifies the number of items (not bytes) in the vectors Y.</param>
        /// <param name="fAlpha">Specifies the scalar in type <code>double</code></param>
        /// <param name="hY">Specifies a handle to the vector Y in GPU memory.</param>
        public void mul_scalar(int n, double fAlpha, long hY)
        {
            mul_scalar(n, (T)Convert.ChangeType(fAlpha, typeof(T)), hY);
        }

        /// <summary>
        /// Mutlipy each element of Y by a scalar.
        /// </summary>
        /// <remarks>
        /// Y = Y * alpha
        /// </remarks>
        /// <param name="n">Specifies the number of items (not bytes) in the vectors Y.</param>
        /// <param name="fAlpha">Specifies the scalar in type <code>float</code></param>
        /// <param name="hY">Specifies a handle to the vector Y in GPU memory.</param>
        public void mul_scalar(int n, float fAlpha, long hY)
        {
            mul_scalar(n, (T)Convert.ChangeType(fAlpha, typeof(T)), hY);
        }

        /// <summary>
        /// Mutlipy each element of Y by a scalar.
        /// </summary>
        /// <remarks>
        /// Y = Y * alpha
        /// </remarks>
        /// <param name="n">Specifies the number of items (not bytes) in the vectors Y.</param>
        /// <param name="fAlpha">Specifies the scalar in type 'T'.</param>
        /// <param name="hY">Specifies a handle to the vector Y in GPU memory.</param>
        public void mul_scalar(int n, T fAlpha, long hY)
        {
            if (m_dt == DataType.DOUBLE)
                m_cuda.RunDouble((int)m_hKernel, (int)CUDAFN.CUDA_MUL_SCALAR, m_param.AsDouble(n, convertD(fAlpha), hY));
            else
                m_cuda.RunFloat((int)m_hKernel, (int)CUDAFN.CUDA_MUL_SCALAR, m_param.AsFloat(n, convertF(fAlpha), hY));
        }

        /// <summary>
        /// Divides each element of A by each element of B and places the result in Y.
        /// </summary>
        /// <remarks>
        /// Y = A / B (element by element)
        /// </remarks>
        /// <param name="n">Specifies the number of items (not bytes) in the vectors A, B and Y.</param>
        /// <param name="hA">Specifies a handle to the vector A in GPU memory.</param>
        /// <param name="hB">Specifies a handle to the vector B in GPU memory.</param>
        /// <param name="hY">Specifies a handle to the vector Y in GPU memory.</param>
        public void div(int n, long hA, long hB, long hY)
        {
            if (m_dt == DataType.DOUBLE)
                m_cuda.RunDouble((int)m_hKernel, (int)CUDAFN.CUDA_DIV, m_param.AsDouble(n, hA, hB, hY));
            else
                m_cuda.RunFloat((int)m_hKernel, (int)CUDAFN.CUDA_DIV, m_param.AsFloat(n, hA, hB, hY));
        }

        /// <summary>
        /// Calculates the absolute value of A and places the result in Y.
        /// </summary>
        /// <remarks>
        /// Y = abs(X)
        /// </remarks>
        /// <param name="n">Specifies the number of items (not bytes) in the vectors A and Y.</param>
        /// <param name="hA">Specifies a handle to the vector A in GPU memory.</param>
        /// <param name="hY">Specifies a handle to the vector Y in GPU memory.</param>
        public void abs(int n, long hA, long hY)
        {
            if (m_dt == DataType.DOUBLE)
                m_cuda.RunDouble((int)m_hKernel, (int)CUDAFN.CUDA_ABS, m_param.AsDouble(n, hA, hY));
            else
                m_cuda.RunFloat((int)m_hKernel, (int)CUDAFN.CUDA_ABS, m_param.AsFloat(n, hA, hY));
        }

        /// <summary>
        /// Calculates the exponent value of A and places the result in Y.
        /// </summary>
        /// <remarks>
        /// @f$ f(x) = exp(x) @f$
        /// </remarks>
        /// <param name="n">Specifies the number of items (not bytes) in the vectors A and Y.</param>
        /// <param name="hA">Specifies a handle to the vector A in GPU memory.</param>
        /// <param name="hY">Specifies a handle to the vector Y in GPU memory.</param>
        public void exp(int n, long hA, long hY)
        {
            exp(n, hA, hY, 0, 0, 1.0);
        }

        /// <summary>
        /// Calculates the exponent value of A * beta and places the result in Y.
        /// </summary>
        /// <remarks>
        /// @f$ f(x) = exp(x * \beta) @f$
        /// </remarks>
        /// <param name="n">Specifies the number of items (not bytes) in the vectors A and Y.</param>
        /// <param name="hA">Specifies a handle to the vector A in GPU memory.</param>
        /// <param name="hY">Specifies a handle to the vector Y in GPU memory.</param>
        /// <param name="nAOff">Specifies an offset (in items, not bytes) into the memory of A.</param>
        /// <param name="nYOff">Specifies an offset (in items, not bytes) into the memory of Y.</param>
        /// <param name="dfBeta">Specifies the scalar as type <code>double</code></param>
        public void exp(int n, long hA, long hY, int nAOff, int nYOff, double dfBeta)
        {
            if (m_dt == DataType.DOUBLE)
                m_cuda.RunDouble((int)m_hKernel, (int)CUDAFN.CUDA_EXP, m_param.AsDouble(n, hA, hY, nAOff, nYOff, dfBeta));
            else
                m_cuda.RunFloat((int)m_hKernel, (int)CUDAFN.CUDA_EXP, m_param.AsFloat(n, hA, hY, nAOff, nYOff, (float)dfBeta));
        }

        /// <summary>
        /// Calculates the log value of A and places the result in Y.
        /// </summary>
        /// <remarks>
        /// @f$ f(x) = log(x) @f$
        /// </remarks>
        /// <param name="n">Specifies the number of items (not bytes) in the vectors A and Y.</param>
        /// <param name="hA">Specifies a handle to the vector A in GPU memory.</param>
        /// <param name="hY">Specifies a handle to the vector Y in GPU memory.</param>
        public void log(int n, long hA, long hY)
        {
            log(n, hA, hY, 1.0, 0.0);
        }

        /// <summary>
        /// Calculates the log value of (A * beta) + alpha, and places the result in Y.
        /// </summary>
        /// <remarks>
        /// @f$ f(x) = \ln((x * \beta) + \alpha) @f$
        /// </remarks>
        /// <param name="n">Specifies the number of items (not bytes) in the vectors A and Y.</param>
        /// <param name="hA">Specifies a handle to the vector A in GPU memory.</param>
        /// <param name="hY">Specifies a handle to the vector Y in GPU memory.</param>
        /// <param name="dfBeta">Specifies the scalar as type <code>double</code> that is multiplied with the log.</param>
        /// <param name="dfAlpha">Optionally, specifies a scalar added to the value before taking the log.</param>
        public void log(int n, long hA, long hY, double dfBeta, double dfAlpha = 0)
        {
            if (m_dt == DataType.DOUBLE)
                m_cuda.RunDouble((int)m_hKernel, (int)CUDAFN.CUDA_LOG, m_param.AsDouble(n, hA, hY, dfBeta, dfAlpha));
            else
                m_cuda.RunFloat((int)m_hKernel, (int)CUDAFN.CUDA_LOG, m_param.AsFloat(n, hA, hY, (float)dfBeta, (float)dfAlpha));
        }

        /// <summary>
        /// Calculates the A raised to the power alpha and places the result in Y.
        /// </summary>
        /// <remarks>
        /// @f$ f(x) = x^\alpha @f$
        /// </remarks>
        /// <param name="n">Specifies the number of items (not bytes) in the vectors A and Y.</param>
        /// <param name="hA">Specifies a handle to the vector A in GPU memory.</param>
        /// <param name="fAlpha">Specifies the scalar in type <code>double</code></param>
        /// <param name="hY">Specifies a handle to the vector Y in GPU memory.</param>
        /// <param name="nAOff">Optionally, specifies the offset for hA memory (default = 0).</param>
        /// <param name="nYOff">Optionally, specifies the offset for hY memory (default = 0).</param>
        public void powx(int n, long hA, double fAlpha, long hY, int nAOff = 0, int nYOff = 0)
        {
            powx(n, hA, (T)Convert.ChangeType(fAlpha, typeof(T)), hY, nAOff, nYOff);
        }

        /// <summary>
        /// Calculates the A raised to the power alpha and places the result in Y.
        /// </summary>
        /// <remarks>
        /// @f$ f(x) = x^\alpha @f$
        /// </remarks>
        /// <param name="n">Specifies the number of items (not bytes) in the vectors A and Y.</param>
        /// <param name="hA">Specifies a handle to the vector A in GPU memory.</param>
        /// <param name="fAlpha">Specifies the scalar in type <code>float</code></param>
        /// <param name="hY">Specifies a handle to the vector Y in GPU memory.</param>
        /// <param name="nAOff">Optionally, specifies the offset for hA memory (default = 0).</param>
        /// <param name="nYOff">Optionally, specifies the offset for hY memory (default = 0).</param>
        public void powx(int n, long hA, float fAlpha, long hY, int nAOff = 0, int nYOff = 0)
        {
            powx(n, hA, (T)Convert.ChangeType(fAlpha, typeof(T)), hY, nAOff, nYOff);
        }

        /// <summary>
        /// Calculates the A raised to the power alpha and places the result in Y.
        /// </summary>
        /// <remarks>
        /// @f$ f(x) = x^\alpha @f$
        /// </remarks>
        /// <param name="n">Specifies the number of items (not bytes) in the vectors A and Y.</param>
        /// <param name="hA">Specifies a handle to the vector A in GPU memory.</param>
        /// <param name="fAlpha">Specifies the scalar in type 'T'.</param>
        /// <param name="hY">Specifies a handle to the vector Y in GPU memory.</param>
        /// <param name="nAOff">Optionally, specifies the offset for hA memory (default = 0).</param>
        /// <param name="nYOff">Optionally, specifies the offset for hY memory (default = 0).</param>
        public void powx(int n, long hA, T fAlpha, long hY, int nAOff = 0, int nYOff = 0)
        {
            if (m_dt == DataType.DOUBLE)
                m_cuda.RunDouble((int)m_hKernel, (int)CUDAFN.CUDA_POWX, m_param.AsDouble(n, hA, convertD(fAlpha), hY, nAOff, nYOff));
            else
                m_cuda.RunFloat((int)m_hKernel, (int)CUDAFN.CUDA_POWX, m_param.AsFloat(n, hA, convertF(fAlpha), hY, nAOff, nYOff));
        }

        /// <summary>
        /// Computes the sign of each element of X and places the result in Y.
        /// </summary>
        /// <param name="n">Specifies the number of items (not bytes) in the vectors A and Y.</param>
        /// <param name="hX">Specifies a handle to the vector X in GPU memory.</param>
        /// <param name="hY">Specifies a handle to the vector Y in GPU memory.</param>
        /// <param name="nXOff">Specifies an offset (in items, not bytes) into the memory of X.</param>
        /// <param name="nYOff">Specifies an offset (in items, not bytes) into the memory of Y.</param>
        public void sign(int n, long hX, long hY, int nXOff = 0, int nYOff = 0)
        {
            if (m_dt == DataType.DOUBLE)
                m_cuda.RunDouble((int)m_hKernel, (int)CUDAFN.CUDA_SIGN, m_param.AsDouble(n, hX, hY, nXOff, nYOff));
            else
                m_cuda.RunFloat((int)m_hKernel, (int)CUDAFN.CUDA_SIGN, m_param.AsFloat(n, hX, hY, nXOff, nYOff));
        }

#pragma warning disable 1591

        public void student(int n, long hX, long hY) /** @private */
        {
            if (m_dt == DataType.DOUBLE)
                m_cuda.RunDouble((int)m_hKernel, (int)CUDAFN.CUDA_STUDENT, m_param.AsDouble(n, hX, hY));
            else
                m_cuda.RunFloat((int)m_hKernel, (int)CUDAFN.CUDA_STUDENT, m_param.AsFloat(n, hX, hY));
        }

        public void logistic1(int n, long hX, long hY) /** @private */
        {
            if (m_dt == DataType.DOUBLE)
                m_cuda.RunDouble((int)m_hKernel, (int)CUDAFN.CUDA_LOGISTIC1, m_param.AsDouble(n, hX, hY));
            else
                m_cuda.RunFloat((int)m_hKernel, (int)CUDAFN.CUDA_LOGISTIC1, m_param.AsFloat(n, hX, hY));
        }

        public void logistic2(int n, long hX, long hY) /** @private */
        {
            if (m_dt == DataType.DOUBLE)
                m_cuda.RunDouble((int)m_hKernel, (int)CUDAFN.CUDA_LOGISTIC2, m_param.AsDouble(n, hX, hY));
            else
                m_cuda.RunFloat((int)m_hKernel, (int)CUDAFN.CUDA_LOGISTIC2, m_param.AsFloat(n, hX, hY));
        }

        public void reciprocol(int n, long hX, long hY) /** @private */
        {
            if (m_dt == DataType.DOUBLE)
                m_cuda.RunDouble((int)m_hKernel, (int)CUDAFN.CUDA_RECIPROCOL, m_param.AsDouble(n, hX, hY));
            else
                m_cuda.RunFloat((int)m_hKernel, (int)CUDAFN.CUDA_RECIPROCOL, m_param.AsFloat(n, hX, hY));
        }

#pragma warning restore 1591

        /// <summary>
        /// Computes the square root of each element of X and places the result in Y.
        /// </summary>
        /// <param name="n">Specifies the number of items (not bytes) in the vectors A and Y.</param>
        /// <param name="hX">Specifies a handle to the vector X in GPU memory.</param>
        /// <param name="hY">Specifies a handle to the vector Y in GPU memory.</param>
        public void sqrt(int n, long hX, long hY)
        {
            if (m_dt == DataType.DOUBLE)
                m_cuda.RunDouble((int)m_hKernel, (int)CUDAFN.CUDA_SQRT, m_param.AsDouble(n, hX, hY));
            else
                m_cuda.RunFloat((int)m_hKernel, (int)CUDAFN.CUDA_SQRT, m_param.AsFloat(n, hX, hY));
        }

        /// <summary>
        /// Scale the data by the sqrt of the data.  y = sqrt(abs(x)) * sign(x)
        /// </summary>
        /// <param name="nCount">Specifies the number of elements.</param>
        /// <param name="hX">Specifies a handle to the vector X in GPU memory.</param>
        /// <param name="hY">Specifies a handle to the vector Y in GPU memory.</param>
        public void sqrt_scale(int nCount, long hX, long hY)
        {
            if (m_dt == DataType.DOUBLE)
                m_cuda.RunDouble((int)m_hKernel, (int)CUDAFN.CUDA_SQRT_SCALE, m_param.AsDouble(nCount, hX, hY));
            else
                m_cuda.RunFloat((int)m_hKernel, (int)CUDAFN.CUDA_SQRT_SCALE, m_param.AsFloat(nCount, hX, hY));
        }

        /// <summary>
        /// Compares the signs of each value in A and B and places the result in Y.
        /// </summary>
        /// <param name="n">Specifies the number of items (not bytes) in the vectors A, B and Y.</param>
        /// <param name="hA">Specifies a handle to the vector A in GPU memory.</param>
        /// <param name="hB">Specifies a handle to the vector B in GPU memory.</param>
        /// <param name="hY">Specifies a handle to the vector Y in GPU memory.</param>
        public void compare_signs(int n, long hA, long hB, long hY)
        {
            if (m_dt == DataType.DOUBLE)
                m_cuda.RunDouble((int)m_hKernel, (int)CUDAFN.CUDA_COMPARE_SIGNS, m_param.AsDouble(n, hA, hB, hY));
            else
                m_cuda.RunFloat((int)m_hKernel, (int)CUDAFN.CUDA_COMPARE_SIGNS, m_param.AsFloat(n, hA, hB, hY));
        }

        /// <summary>
        /// Finds the maximum value of A.
        /// </summary>
        /// <remarks>
        /// This function uses [NVIDIA's Thrust](https://developer.nvidia.com/thrust).
        /// </remarks>
        /// <param name="n">Specifies the number of items (not bytes) in the vectors A.</param>
        /// <param name="hA">Specifies a handle to the vector A in GPU memory.</param>
        /// <param name="lPos">Returns the position of the maximum value.</param>
        /// <param name="nAOff">Optionally, specifies an offset (in items, not bytes) into the memory of A (default = 0).</param>
        /// <returns>The maximum value is returned as type <code>double</code></returns>
        public double max(int n, long hA, out long lPos, int nAOff = 0)
        {
            if (m_dt == DataType.DOUBLE)
            {
                double[] rg = m_cuda.RunDouble((int)m_hKernel, (int)CUDAFN.CUDA_MAXVAL, m_param.AsDouble(n, hA, nAOff));
                lPos = (long)rg[1];
                return rg[0];
            }
            else
            {
                float[] rg = m_cuda.RunFloat((int)m_hKernel, (int)CUDAFN.CUDA_MAXVAL, m_param.AsFloat(n, hA, nAOff));
                lPos = (long)rg[1];
                return rg[0];
            }
        }

        /// <summary>
        /// Finds the minimum value of A.
        /// </summary>
        /// <remarks>
        /// This function uses [NVIDIA's Thrust](https://developer.nvidia.com/thrust).
        /// </remarks>
        /// <param name="n">Specifies the number of items (not bytes) in the vectors A.</param>
        /// <param name="hA">Specifies a handle to the vector A in GPU memory.</param>
        /// <param name="lPos">Returns the position of the minimum value.</param>
        /// <param name="nAOff">Optionally, specifies an offset (in items, not bytes) into the memory of A (default = 0).</param>
        /// <returns>The minimum value is returned as type <code>double</code></returns>
        public double min(int n, long hA, out long lPos, int nAOff = 0)
        {
            if (m_dt == DataType.DOUBLE)
            {
                double[] rg = m_cuda.RunDouble((int)m_hKernel, (int)CUDAFN.CUDA_MINVAL, m_param.AsDouble(n, hA, nAOff));
                lPos = (long)rg[1];
                return rg[0];
            }
            else
            {
                float[] rg = m_cuda.RunFloat((int)m_hKernel, (int)CUDAFN.CUDA_MINVAL, m_param.AsFloat(n, hA, nAOff));
                lPos = (long)rg[1];
                return rg[0];
            }
        }

        /// <summary>
        /// Finds the minimum and maximum values within A.
        /// </summary>
        /// <param name="n">Specifies the number of items (not bytes) in the vector A.</param>
        /// <param name="hA">Specifies a handle to the vector A in GPU memory.</param>
        /// <param name="hWork1">Specifies a handle to workspace data in GPU memory.  To get the size of the workspace memory, call this function with hA = 0.</param>
        /// <param name="hWork2">Specifies a handle to workspace data in GPU memory.  To get the size of the workspace memory, call this function with hA = 0.</param>
        /// <param name="bDetectNans">Optionally, specifies whether or not to detect Nans.</param>
        /// <param name="nAOff">Optionally, specifies an offset (in items, not bytes) into the memory of A.</param>
        /// <returns>A four element tuple is returned where the first item contains the minimum, the second item contains the maximum, the third contains the number
        /// of NaN values and the fourth contains the number of Infinity values.  
        /// When calling this function with <code>hA = 0</code> the function instead returns the required size of <i>hWork1</i>, <i>hWork2</i>, 0, 0 (in items, not bytes).</returns>
        public Tuple<double, double, double, double> minmax(int n, long hA, long hWork1, long hWork2, bool bDetectNans = false, int nAOff = 0)
        {
            if (m_dt == DataType.DOUBLE)
            {
                double[] rg = m_cuda.RunDouble((int)m_hKernel, (int)CUDAFN.CUDA_MINMAXVAL, m_param.AsDouble(n, hA, hWork1, hWork2, (bDetectNans) ? 1 : 0, nAOff));
                return new Tuple<double, double, double, double>(rg[0], rg[1], rg[2], rg[3]);
            }
            else
            {
                float[] rg = m_cuda.RunFloat((int)m_hKernel, (int)CUDAFN.CUDA_MINMAXVAL, m_param.AsFloat(n, hA, hWork1, hWork2, (bDetectNans) ? 1 : 0, nAOff));
                return new Tuple<double, double, double, double>(rg[0], rg[1], rg[2], rg[3]);
            }
        }

        /// <summary>
        /// Finds up to 'nK' minimum and maximum values within A.
        /// </summary>
        /// <param name="n">Specifies the number of items (not bytes) in the vector A.</param>
        /// <param name="hA">Specifies a handle to the vector A in GPU memory.</param>
        /// <param name="hWork1">Specifies a handle to workspace data in GPU memory.  To get the size of the workspace memory, call this function with hA = 0.</param>
        /// <param name="hWork2">Specifies a handle to workspace data in GPU memory.  To get the size of the workspace memory, call this function with hA = 0.</param>
        /// <param name="nK">Specifies the number of min and max values to find.</param>
        /// <param name="hMin">Specifies a handle to host memory allocated with AllocHostBuffer in the length 'nK' where the min values are placed.</param>
        /// <param name="hMax">Specifies a handle to host memory allocated with AllocHostBuffer in the length 'nK' where the min values are placed.</param>
        /// <param name="bNonZeroOnly">Specifies whether or not to exclude zero from the min and max calculations.</param>
        public void minmax(int n, long hA, long hWork1, long hWork2, int nK, long hMin, long hMax, bool bNonZeroOnly)
        {
            if (m_dt == DataType.DOUBLE)
                m_cuda.RunDouble((int)m_hKernel, (int)CUDAFN.CUDA_MINMAXVEC, m_param.AsDouble(n, hA, hWork1, hWork2, nK, hMin, hMax, (bNonZeroOnly) ? 1 : 0));
            else
                m_cuda.RunFloat((int)m_hKernel, (int)CUDAFN.CUDA_MINMAXVEC, m_param.AsFloat(n, hA, hWork1, hWork2, nK, hMin, hMax, (bNonZeroOnly) ? 1 : 0));
        }

        /// <summary>
        /// Perform a transpose on X producing Y, similar to the numpy.transpose operation.
        /// </summary>
        /// <param name="n">Specifies the number of items in both hX and hY (must be the same).</param>
        /// <param name="hX">Specifies a handle to the input data in gpu memory.</param>
        /// <param name="hY">Specifies a handle to the output data in gpu memory.</param>
        /// <param name="hXCounts">Specifies a handle to the input counts in gpu memory.</param>
        /// <param name="hYCounts">Specifies a handle to the output counts in gpu memory.</param>
        /// <param name="hMapping">Specifies a handle to the mappings of each axis.</param>
        /// <param name="nNumAxes">Specifies the number of axes.</param>
        /// <param name="hBuffer">Specifies a handle to the buffer that should have 'n' * nNumAxes number of items.</param>
        public void transpose(int n, long hX, long hY, long hXCounts, long hYCounts, long hMapping, int nNumAxes, long hBuffer)
        {
            if (m_dt == DataType.DOUBLE)
                m_cuda.RunDouble((int)m_hKernel, (int)CUDAFN.CUDA_TRANSPOSE, m_param.AsDouble(n, hX, hY, hXCounts, hYCounts, hMapping, nNumAxes, hBuffer));
            else
                m_cuda.RunFloat((int)m_hKernel, (int)CUDAFN.CUDA_TRANSPOSE, m_param.AsFloat(n, hX, hY, hXCounts, hYCounts, hMapping, nNumAxes, hBuffer));
        }

        /// <summary>
        /// Calculates the sum of squares of A.
        /// </summary>
        /// <param name="n">Specifies the number of items (not bytes) in the vectors A and W.</param>
        /// <param name="hW">Specifies a handle to workspace data in GPU memory.</param>
        /// <param name="hA">Specifies a handle to the vector A in GPU memory.</param>
        /// <param name="nAOff">Specifies an offset (in items, not bytes) into the memory of A.</param>
        /// <returns>The sum of squares of A is returned as type <code>double</code></returns>
        public double sumsq(int n, long hW, long hA, int nAOff = 0)
        {
            if (m_dt == DataType.DOUBLE)
            {
                double[] rg = m_cuda.RunDouble((int)m_hKernel, (int)CUDAFN.CUDA_SUMSQ, m_param.AsDouble(n, hW, hA, nAOff));
                return rg[0];
            }
            else
            {
                float[] rg = m_cuda.RunFloat((int)m_hKernel, (int)CUDAFN.CUDA_SUMSQ, m_param.AsFloat(n, hW, hA, nAOff));
                return rg[0];
            }
        }

        /// <summary>
        /// Calculates the sum of squares of differences between A and B
        /// </summary>
        /// <param name="n">Specifies the number of items (not bytes) in the vectors A, B and W.</param>
        /// <param name="hW">Specifies a handle to workspace data in GPU memory.</param>
        /// <param name="hA">Specifies a handle to the vector A in GPU memory.</param>
        /// <param name="hB">Specifies a handle to the vector B in GPU memory.</param>
        /// <param name="nAOff">Specifies an offset (in items, not bytes) into the memory of A.</param>
        /// <param name="nBOff">Specifies an offset (in items, not bytes) into the memory of B.</param>
        /// <returns>The sum of squared differences between A and B are returned as type <code>double</code></returns>
        public double sumsqdiff(int n, long hW, long hA, long hB, int nAOff = 0, int nBOff = 0)
        {
            if (m_dt == DataType.DOUBLE)
            {
                double[] rg = m_cuda.RunDouble((int)m_hKernel, (int)CUDAFN.CUDA_SUMSQDIFF, m_param.AsDouble(n, hW, hA, hB, nAOff, nBOff));
                return rg[0];
            }
            else
            {
                float[] rg = m_cuda.RunFloat((int)m_hKernel, (int)CUDAFN.CUDA_SUMSQDIFF, m_param.AsFloat(n, hW, hA, hB, nAOff, nBOff));
                return rg[0];
            }
        }

        /// <summary>
        /// Calculates the width values.
        /// </summary>
        /// <param name="n">Specifies the number of items.</param>
        /// <param name="hMean">Specifies a handle to the mean values in GPU memory.</param>
        /// <param name="hMin">Specifies a handle to the min values in GPU memory.</param>
        /// <param name="hMax">Specifies a handle to the max values in GPU memory.</param>
        /// <param name="dfAlpha">Specifies the alpha value.</param>
        /// <param name="hWidth">Specifies the GPU memory where the width values are placed.</param>
        public void width(int n, long hMean, long hMin, long hMax, double dfAlpha, long hWidth) 
        {
            if (m_dt == DataType.DOUBLE)
                m_cuda.RunDouble((int)m_hKernel, (int)CUDAFN.CUDA_WIDTH, m_param.AsDouble(n, hMean, hMin, hMax, dfAlpha, hWidth));
            else
                m_cuda.RunFloat((int)m_hKernel, (int)CUDAFN.CUDA_WIDTH, m_param.AsFloat(n, hMean, hMin, hMax, (float)dfAlpha, hWidth));
        }

        /// <summary>
        /// Returns true if the point is contained within the bounds.
        /// </summary>
        /// <param name="n">Specifies the number of items.</param>
        /// <param name="hMean">Specifies a handle to the mean values in GPU memory.</param>
        /// <param name="hWidth">Specifies a handle to the width values in GPU memory.</param>
        /// <param name="hX">Specifies a handle to the X values in GPU memory.</param>
        /// <param name="hWork">Specifies a handle to the work data in GPU memory.</param>
        /// <param name="nXOff">Optionally, specifies an offset into the X vector (default = 0).</param>
        /// <returns>If the X values are within the bounds, <i>true</i> is returned, otherwise <i>false</i>.</returns>
        public bool contains_point(int n, long hMean, long hWidth, long hX, long hWork, int nXOff = 0) 
        {
            if (m_dt == DataType.DOUBLE)
            {
                double[] rg = m_cuda.RunDouble((int)m_hKernel, (int)CUDAFN.CUDA_CONTAINS_POINT, m_param.AsDouble(n, hMean, hWidth, hX, hWork, nXOff));
                return (rg[0] == 0) ? false : true;
            }
            else
            {
                float[] rg = m_cuda.RunFloat((int)m_hKernel, (int)CUDAFN.CUDA_CONTAINS_POINT, m_param.AsFloat(n, hMean, hWidth, hX, hWork, nXOff));
                return (rg[0] == 0) ? false : true;
            }
        }

        /// <summary>
        /// Replaces all NAN values witin X with a replacement value.
        /// </summary>
        /// <param name="n">Specifies the number of items (not bytes) in the vector X.</param>
        /// <param name="hX">Specifies a handle to the vector X in GPU memory.</param>
        /// <param name="dfReplacement">Specifies the replacement value.</param>
        public void denan(int n, long hX, double dfReplacement)
        {
            if (m_dt == DataType.DOUBLE)
                m_cuda.RunDouble((int)m_hKernel, (int)CUDAFN.CUDA_DENAN, m_param.AsDouble(n, hX, dfReplacement));
            else
                m_cuda.RunFloat((int)m_hKernel, (int)CUDAFN.CUDA_DENAN, m_param.AsFloat(n, hX, (float)dfReplacement));
        }

        /// <summary>
        /// Rearranges image blocks into columns.
        /// </summary>
        /// <param name="hDataIm">Specifies a handle to the image block in GPU memory.</param>
        /// <param name="nDataImOffset">Specifies an offset into the image block memory.</param>
        /// <param name="nChannels">Specifies the number of channels in the image.</param>
        /// <param name="nHeight">Specifies the height of the image.</param>
        /// <param name="nWidth">Specifies the width of the image.</param>
        /// <param name="nKernelH">Specifies the kernel height.</param>
        /// <param name="nKernelW">Specifies the kernel width.</param>
        /// <param name="nPadH">Specifies the pad applied to the height.</param>
        /// <param name="nPadW">Specifies the pad applied to the width.</param>
        /// <param name="nStrideH">Specifies the stride along the height.</param>
        /// <param name="nStrideW">Specifies the stride along the width.</param>
        /// <param name="nDilationH">Specifies the dilation along the height.</param>
        /// <param name="nDilationW">Specifies the dilation along the width.</param>
        /// <param name="hDataCol">Specifies a handle to the column data in GPU memory.</param>
        /// <param name="nDataColOffset">Specifies an offset into the column memory.</param>
        public void im2col(long hDataIm, int nDataImOffset, int nChannels, int nHeight, int nWidth, int nKernelH, int nKernelW, int nPadH, int nPadW, int nStrideH, int nStrideW, int nDilationH, int nDilationW, long hDataCol, int nDataColOffset)
        {
            if (m_dt == DataType.DOUBLE)
                m_cuda.RunDouble((int)m_hKernel, (int)CUDAFN.CUDA_IM2COL, m_param.AsDouble(hDataIm, nDataImOffset, nChannels, nHeight, nWidth, nKernelH, nKernelW, nPadH, nPadW, nStrideH, nStrideW, nDilationH, nDilationW, hDataCol, nDataColOffset));
            else
                m_cuda.RunFloat((int)m_hKernel, (int)CUDAFN.CUDA_IM2COL, m_param.AsFloat(hDataIm, nDataImOffset, nChannels, nHeight, nWidth, nKernelH, nKernelW, nPadH, nPadW, nStrideH, nStrideW, nDilationH, nDilationW, hDataCol, nDataColOffset));
        }

        /// <summary>
        /// Rearranges image blocks into columns.
        /// </summary>
        /// <param name="hDataIm">Specifies a handle to the image block in GPU memory.</param>
        /// <param name="nDataImOffset">Specifies an offset into the image block memory.</param>
        /// <param name="nNumSpatialAxes">Specifies the number of spatial axes.</param>
        /// <param name="nImCount">Specifies the number of kernels.</param>
        /// <param name="nChannelAxis">Specifies the axis containing the channel.</param>
        /// <param name="hImShape">Specifies a handle to the image shape data in GPU memory.</param>
        /// <param name="hColShape">Specifies a handle to the column shape data in GPU memory.</param>
        /// <param name="hKernelShape">Specifies a handle to the kernel shape data in GPU memory.</param>
        /// <param name="hPad">Specifies a handle to the pad data in GPU memory.</param>
        /// <param name="hStride">Specifies a handle to the stride data in GPU memory.</param>
        /// <param name="hDilation">Specifies a handle to the dilation data in GPU memory.</param>
        /// <param name="hDataCol">Specifies a handle to the column data in GPU memory.</param>
        /// <param name="nDataColOffset">Specifies an offset into the column memory.</param>
        public void im2col_nd(long hDataIm, int nDataImOffset, int nNumSpatialAxes, int nImCount, int nChannelAxis, long hImShape, long hColShape, long hKernelShape, long hPad, long hStride, long hDilation, long hDataCol, int nDataColOffset)
        {
            if (m_dt == DataType.DOUBLE)
                m_cuda.RunDouble((int)m_hKernel, (int)CUDAFN.CUDA_IM2COL_ND, m_param.AsDouble(hDataIm, nDataImOffset, nNumSpatialAxes, nImCount, nChannelAxis, hImShape, hColShape, hKernelShape, hPad, hStride, hDilation, hDataCol, nDataColOffset));
            else
                m_cuda.RunFloat((int)m_hKernel, (int)CUDAFN.CUDA_IM2COL_ND, m_param.AsFloat(hDataIm, nDataImOffset, nNumSpatialAxes, nImCount, nChannelAxis, hImShape, hColShape, hKernelShape, hPad, hStride, hDilation, hDataCol, nDataColOffset));
        }

        /// <summary>
        /// Rearranges the columns into image blocks.
        /// </summary>
        /// <param name="hDataCol">Specifies a handle to the column data in GPU memory.</param>
        /// <param name="nDataColOffset">Specifies an offset into the column memory.</param>
        /// <param name="nChannels">Specifies the number of channels in the image.</param>
        /// <param name="nHeight">Specifies the height of the image.</param>
        /// <param name="nWidth">Specifies the width of the image.</param>
        /// <param name="nKernelH">Specifies the kernel height.</param>
        /// <param name="nKernelW">Specifies the kernel width.</param>
        /// <param name="nPadH">Specifies the pad applied to the height.</param>
        /// <param name="nPadW">Specifies the pad applied to the width.</param>
        /// <param name="nStrideH">Specifies the stride along the height.</param>
        /// <param name="nStrideW">Specifies the stride along the width.</param>
        /// <param name="nDilationH">Specifies the dilation along the height.</param>
        /// <param name="nDilationW">Specifies the dilation along the width.</param>
        /// <param name="hDataIm">Specifies a handle to the image block in GPU memory.</param>
        /// <param name="nDataImOffset">Specifies an offset into the image block memory.</param>
        public void col2im(long hDataCol, int nDataColOffset, int nChannels, int nHeight, int nWidth, int nKernelH, int nKernelW, int nPadH, int nPadW, int nStrideH, int nStrideW, int nDilationH, int nDilationW, long hDataIm, int nDataImOffset)
        {
            if (m_dt == DataType.DOUBLE)
                m_cuda.RunDouble((int)m_hKernel, (int)CUDAFN.CUDA_COL2IM, m_param.AsDouble(hDataCol, nDataColOffset, nChannels, nHeight, nWidth, nKernelH, nKernelW, nPadH, nPadW, nStrideH, nStrideW, nDilationH, nDilationW, hDataIm, nDataImOffset));
            else
                m_cuda.RunFloat((int)m_hKernel, (int)CUDAFN.CUDA_COL2IM, m_param.AsFloat(hDataCol, nDataColOffset, nChannels, nHeight, nWidth, nKernelH, nKernelW, nPadH, nPadW, nStrideH, nStrideW, nDilationH, nDilationW, hDataIm, nDataImOffset));
        }

        /// <summary>
        /// Rearranges the columns into image blocks.
        /// </summary>
        /// <param name="hDataCol">Specifies a handle to the column data in GPU memory.</param>
        /// <param name="nDataColOffset">Specifies an offset into the column memory.</param>
        /// <param name="nNumSpatialAxes">Specifies the number of spatial axes.</param>
        /// <param name="nColCount">Specifies the number of kernels.</param>
        /// <param name="nChannelAxis">Specifies the axis containing the channel.</param>
        /// <param name="hImShape">Specifies a handle to the image shape data in GPU memory.</param>
        /// <param name="hColShape">Specifies a handle to the column shape data in GPU memory.</param>
        /// <param name="hKernelShape">Specifies a handle to the kernel shape data in GPU memory.</param>
        /// <param name="hPad">Specifies a handle to the pad data in GPU memory.</param>
        /// <param name="hStride">Specifies a handle to the stride data in GPU memory.</param>
        /// <param name="hDilation">Specifies a handle to the dilation data in GPU memory.</param>
        /// <param name="hDataIm">Specifies a handle to the image block in GPU memory.</param>
        /// <param name="nDataImOffset">Specifies an offset into the image block memory.</param>
        public void col2im_nd(long hDataCol, int nDataColOffset, int nNumSpatialAxes, int nColCount, int nChannelAxis, long hImShape, long hColShape, long hKernelShape, long hPad, long hStride, long hDilation, long hDataIm, int nDataImOffset)
        {
            if (m_dt == DataType.DOUBLE)
                m_cuda.RunDouble((int)m_hKernel, (int)CUDAFN.CUDA_COL2IM_ND, m_param.AsDouble(hDataCol, nDataColOffset, nNumSpatialAxes, nColCount, nChannelAxis, hImShape, hColShape, hKernelShape, hPad, hStride, hDilation, hDataIm, nDataImOffset));
            else
                m_cuda.RunFloat((int)m_hKernel, (int)CUDAFN.CUDA_COL2IM_ND, m_param.AsFloat(hDataCol, nDataColOffset, nNumSpatialAxes, nColCount, nChannelAxis, hImShape, hColShape, hKernelShape, hPad, hStride, hDilation, hDataIm, nDataImOffset));
        }

        /// <summary>
        /// Calculates the minimum value within each channel of X and places the result in Y.
        /// </summary>
        /// <param name="nCount">Specifies the number of elements in X.</param>
        /// <param name="nOuterNum">Specifies the number of images within X.</param>
        /// <param name="nChannels">Specifies the number of channels per image of X.</param>
        /// <param name="nInnerNum">Specifies the dimension of each image in X.</param>
        /// <param name="hX">Specifies a handle to the vector X in GPU memory.</param>
        /// <param name="hY">Specifies a handle to the vector Y in GPU memory.</param>
        public void channel_min(int nCount, int nOuterNum, int nChannels, int nInnerNum, long hX, long hY)
        {
            if (m_dt == DataType.DOUBLE)
                m_cuda.RunDouble((int)m_hKernel, (int)CUDAFN.CUDA_CHANNEL_MIN, m_param.AsDouble(nCount, nOuterNum, nChannels, nInnerNum, hX, hY));
            else
                m_cuda.RunFloat((int)m_hKernel, (int)CUDAFN.CUDA_CHANNEL_MIN, m_param.AsFloat(nCount, nOuterNum, nChannels, nInnerNum, hX, hY));
        }

        /// <summary>
        /// Calculates the maximum value within each channel of X and places the result in Y.
        /// </summary>
        /// <param name="nCount">Specifies the number of elements in X.</param>
        /// <param name="nOuterNum">Specifies the number of images within X.</param>
        /// <param name="nChannels">Specifies the number of channels per image of X.</param>
        /// <param name="nInnerNum">Specifies the dimension of each image in X.</param>
        /// <param name="hX">Specifies a handle to the vector X in GPU memory.</param>
        /// <param name="hY">Specifies a handle to the vector Y in GPU memory.</param>
        public void channel_max(int nCount, int nOuterNum, int nChannels, int nInnerNum, long hX, long hY)
        {
            if (m_dt == DataType.DOUBLE)
                m_cuda.RunDouble((int)m_hKernel, (int)CUDAFN.CUDA_CHANNEL_MAX, m_param.AsDouble(nCount, nOuterNum, nChannels, nInnerNum, hX, hY));
            else
                m_cuda.RunFloat((int)m_hKernel, (int)CUDAFN.CUDA_CHANNEL_MAX, m_param.AsFloat(nCount, nOuterNum, nChannels, nInnerNum, hX, hY));
        }

        /// <summary>
        /// Compares the values of the channels from X and places the result in Y where 1 is set if the values are equal otherwise 0 is set.
        /// </summary>
        /// <param name="nCount">Specifies the number of elements in X.</param>
        /// <param name="nOuterNum">Specifies the number of images within X.</param>
        /// <param name="nChannels">Specifies the number of channels per image of X.</param>
        /// <param name="nInnerNum">Specifies the dimension of each image in X.</param>
        /// <param name="hX">Specifies a handle to the vector X in GPU memory.</param>
        /// <param name="hY">Specifies a handle to the vector Y in GPU memory of length nOuterNum.</param>
        public void channel_compare(int nCount, int nOuterNum, int nChannels, int nInnerNum, long hX, long hY)
        {
            if (m_dt == DataType.DOUBLE)
                m_cuda.RunDouble((int)m_hKernel, (int)CUDAFN.CUDA_CHANNEL_COMPARE, m_param.AsDouble(nCount, nOuterNum, nChannels, nInnerNum, hX, hY));
            else
                m_cuda.RunFloat((int)m_hKernel, (int)CUDAFN.CUDA_CHANNEL_COMPARE, m_param.AsFloat(nCount, nOuterNum, nChannels, nInnerNum, hX, hY));
        }

        /// <summary>
        /// Fills each channel with the channel item of Y with the data of X matching the label index specified by hLabels.
        /// </summary>
        /// <param name="nCount">Specifies the number of items in Y.</param>
        /// <param name="nOuterNum">Specifies the num of Y and Labels.</param>
        /// <param name="nChannels">Specifies the channel size of Y and X.</param>
        /// <param name="nInnerNum">Specifies the spatial dimension of X and Y, but is normally 1.</param>
        /// <param name="hX">Specifies the GPU memory containing the encodings (usually centroids) of each label 0, ... max label.</param>
        /// <param name="nLabelDim">Specifies the dimension of the label channels.  A value > 1 indicates that more than one label are stored per channel in which case only the first label is used.</param>
        /// <param name="hLabels">Specifies the label ordering that determines how Y is filled using data from X.</param>
        /// <param name="hY">Specifies the GPU memory of the output data.</param>
        /// <remarks>
        /// This function is used to fill a blob with data matching a set of labels.  For example in a 3 item encoding based system with
        /// 4 labels:
        /// X = 4 channels of 3 items each (e.g. an encoding for each label).
        /// The values of hLabels show the ordering for which to fill hY with the labeled encodings.  So if hLabels = 0, 2, 1, 3, 1, then
        /// Y = size { 5, 3, 1, 1 }, 5 items each with encoding sizes of 3 items which are then filled with the encoding at position 0,
        /// (for label 0), followed by the encoding for label 2, then 1, 3 and ending with the encoding for 1 as specified by the labels.
        /// </remarks>
        public void channel_fill(int nCount, int nOuterNum, int nChannels, int nInnerNum, long hX, int nLabelDim, long hLabels, long hY)
        {
            if (m_dt == DataType.DOUBLE)
                m_cuda.RunDouble((int)m_hKernel, (int)CUDAFN.CUDA_CHANNEL_FILL, m_param.AsDouble(nCount, nOuterNum, nChannels, nInnerNum, hX, nLabelDim, hLabels, hY));
            else
                m_cuda.RunFloat((int)m_hKernel, (int)CUDAFN.CUDA_CHANNEL_FILL, m_param.AsFloat(nCount, nOuterNum, nChannels, nInnerNum, hX, nLabelDim, hLabels, hY));
        }

        /// <summary>
        /// Subtracts the values across the channels from X and places the result in Y.
        /// </summary>
        /// <param name="nCount">Specifies the number of elements in X.</param>
        /// <param name="nOuterNum">Specifies the number of images within X.</param>
        /// <param name="nChannels">Specifies the number of channels per image of X.</param>
        /// <param name="nInnerNum">Specifies the dimension of each image in X.</param>
        /// <param name="hX">Specifies a handle to the vector X in GPU memory.</param>
        /// <param name="hY">Specifies a handle to the vector Y in GPU memory.</param>
        public void channel_sub(int nCount, int nOuterNum, int nChannels, int nInnerNum, long hX, long hY)
        {
            if (m_dt == DataType.DOUBLE)
                m_cuda.RunDouble((int)m_hKernel, (int)CUDAFN.CUDA_CHANNEL_SUB, m_param.AsDouble(nCount, nOuterNum, nChannels, nInnerNum, hX, hY));
            else
                m_cuda.RunFloat((int)m_hKernel, (int)CUDAFN.CUDA_CHANNEL_SUB, m_param.AsFloat(nCount, nOuterNum, nChannels, nInnerNum, hX, hY));
        }

        /// <summary>
        /// Calculates the sum the the values either across or within each channel (depending on bSumAcrossChannels setting) of X and places the result in Y.
        /// </summary>
        /// <param name="nCount">Specifies the number of elements in X.</param>
        /// <param name="nOuterNum">Specifies the number of images within X.</param>
        /// <param name="nChannels">Specifies the number of channels per image of X.</param>
        /// <param name="nInnerNum">Specifies the dimension of each image in X.</param>
        /// <param name="hX">Specifies a handle to the vector X in GPU memory.</param>
        /// <param name="hY">Specifies a handle to the vector Y in GPU memory.</param>
        public void channel_sum(int nCount, int nOuterNum, int nChannels, int nInnerNum, long hX, long hY)
        {
            if (m_dt == DataType.DOUBLE)
                m_cuda.RunDouble((int)m_hKernel, (int)CUDAFN.CUDA_CHANNEL_SUM, m_param.AsDouble(nCount, nOuterNum, nChannels, nInnerNum, hX, hY));
            else
                m_cuda.RunFloat((int)m_hKernel, (int)CUDAFN.CUDA_CHANNEL_SUM, m_param.AsFloat(nCount, nOuterNum, nChannels, nInnerNum, hX, hY));
        }

        /// <summary>
        /// Divides the values of the channels from X and places the result in Y.
        /// </summary>
        /// <param name="nCount">Specifies the number of elements in X.</param>
        /// <param name="nOuterNum">Specifies the number of images within X.</param>
        /// <param name="nChannels">Specifies the number of channels per image of X.</param>
        /// <param name="nInnerNum">Specifies the dimension of each image in X.</param>
        /// <param name="hX">Specifies a handle to the vector X in GPU memory.</param>
        /// <param name="hY">Specifies a handle to the vector Y in GPU memory.</param>
        /// <param name="nMethod">Specifies the method of traversing the channel, <i>nMethod</i> = 1 (the default) is used by the SoftmaxLayer and <i>nMethod</i> = 2 is used by the GRNLayer.</param>
        public void channel_div(int nCount, int nOuterNum, int nChannels, int nInnerNum, long hX, long hY, int nMethod = 1)
        {
            if (m_dt == DataType.DOUBLE)
                m_cuda.RunDouble((int)m_hKernel, (int)CUDAFN.CUDA_CHANNEL_DIV, m_param.AsDouble(nCount, nOuterNum, nChannels, nInnerNum, hX, hY, nMethod));
            else
                m_cuda.RunFloat((int)m_hKernel, (int)CUDAFN.CUDA_CHANNEL_DIV, m_param.AsFloat(nCount, nOuterNum, nChannels, nInnerNum, hX, hY, nMethod));
        }

        /// <summary>
        /// Multiplies the values of the channels from X and places the result in Y.
        /// </summary>
        /// <param name="nCount">Specifies the number of elements in X.</param>
        /// <param name="nOuterNum">Specifies the number of images within X.</param>
        /// <param name="nChannels">Specifies the number of channels per image of X.</param>
        /// <param name="nInnerNum">Specifies the dimension of each image in X.</param>
        /// <param name="hX">Specifies a handle to the vector X in GPU memory.</param>
        /// <param name="hY">Specifies a handle to the vector Y in GPU memory.</param>
        /// <param name="nMethod">Specifies the method of traversing the channel, <i>nMethod</i> = 1 (the default) is used by the SoftmaxLayer and <i>nMethod</i> = 2 is used by the GRNLayer.</param>
        public void channel_mul(int nCount, int nOuterNum, int nChannels, int nInnerNum, long hX, long hY, int nMethod = 1)
        {
            if (m_dt == DataType.DOUBLE)
                m_cuda.RunDouble((int)m_hKernel, (int)CUDAFN.CUDA_CHANNEL_MUL, m_param.AsDouble(nCount, nOuterNum, nChannels, nInnerNum, hX, hY, nMethod));
            else
                m_cuda.RunFloat((int)m_hKernel, (int)CUDAFN.CUDA_CHANNEL_MUL, m_param.AsFloat(nCount, nOuterNum, nChannels, nInnerNum, hX, hY, nMethod));
        }

        /// <summary>
        /// Multiplies the values in vector X by each channel in matrix A and places the result in matrix C.
        /// </summary>
        /// <param name="nCount">Specifies the number of elements in X.</param>
        /// <param name="nOuterNum">Specifies the number of images within X.</param>
        /// <param name="nChannels">Specifies the number of channels per image of X.</param>
        /// <param name="nInnerNum">Specifies the dimension of each image in X.</param>
        /// <param name="hA">Specifies a handle to the matrix X in GPU memory.</param>
        /// <param name="hX">Specifies a handle to the vector X in GPU memory (must be of length nInnerDim).</param>
        /// <param name="hC">Specifies a handle to the matrix C in GPU memory where the results are placed.</param>
        public void channel_mulv(int nCount, int nOuterNum, int nChannels, int nInnerNum, long hA, long hX, long hC)
        {
            if (m_dt == DataType.DOUBLE)
                m_cuda.RunDouble((int)m_hKernel, (int)CUDAFN.CUDA_CHANNEL_MULV, m_param.AsDouble(nCount, nOuterNum, nChannels, nInnerNum, hA, hX, hC));
            else
                m_cuda.RunFloat((int)m_hKernel, (int)CUDAFN.CUDA_CHANNEL_MULV, m_param.AsFloat(nCount, nOuterNum, nChannels, nInnerNum, hA, hX, hC));
        }

        /// <summary>
        /// Multiplies the values of the channels from X with the scalar values in B and places the result in Y.
        /// </summary>
        /// <param name="nCount">Specifies the number of elements in X.</param>
        /// <param name="nOuterNum">Specifies the number of items within X and B.</param>
        /// <param name="nChannels">Specifies the number of channels per item of X and B.</param>
        /// <param name="nInnerNum">Specifies the dimension of each data item in X (B should have data dimension = 1).</param>
        /// <param name="hX">Specifies a handle to the vector X in GPU memory.</param>
        /// <param name="hA">Specifies a handle to the vector B containing the scalar values, one per num * channel.</param>
        /// <param name="hY">Specifies a handle to the vector Y in GPU memory.</param>
        public void channel_scale(int nCount, int nOuterNum, int nChannels, int nInnerNum, long hX, long hA, long hY)
        {
            if (m_dt == DataType.DOUBLE)
                m_cuda.RunDouble((int)m_hKernel, (int)CUDAFN.CUDA_CHANNEL_SCALE, m_param.AsDouble(nCount, nOuterNum, nChannels, nInnerNum, hX, hA, hY));
            else
                m_cuda.RunFloat((int)m_hKernel, (int)CUDAFN.CUDA_CHANNEL_SCALE, m_param.AsFloat(nCount, nOuterNum, nChannels, nInnerNum, hX, hA, hY));
        }

        /// <summary>
        /// Calculates the dot product the the values within each channel of X and places the result in Y.
        /// </summary>
        /// <param name="nCount">Specifies the number of elements.</param>
        /// <param name="nOuterNum">Specifies the number of images.</param>
        /// <param name="nChannels">Specifies the number of channels per image.</param>
        /// <param name="nInnerNum">Specifies the dimension of each image.</param>
        /// <param name="hX">Specifies a handle to the vector X in GPU memory.</param>
        /// <param name="hA">Specifies a handle to the vector A in GPU memory.</param>
        /// <param name="hY">Specifies a handle to the vector Y in GPU memory.</param>
        public void channel_dot(int nCount, int nOuterNum, int nChannels, int nInnerNum, long hX, long hA, long hY)
        {
            if (m_dt == DataType.DOUBLE)
                m_cuda.RunDouble((int)m_hKernel, (int)CUDAFN.CUDA_CHANNEL_DOT, m_param.AsDouble(nCount, nOuterNum, nChannels, nInnerNum, hX, hA, hY));
            else
                m_cuda.RunFloat((int)m_hKernel, (int)CUDAFN.CUDA_CHANNEL_DOT, m_param.AsFloat(nCount, nOuterNum, nChannels, nInnerNum, hX, hA, hY));
        }

        /// <summary>
        /// Calculates the sum of inner values of X and places the result in Y.
        /// </summary>
        /// <param name="nCount">Specifies the number of elements in X.</param>
        /// <param name="nOuterNum">Specifies the number of outer items within X.</param>
        /// <param name="nInnerNum">Specifies the dimension of items to sum in X.</param>
        /// <param name="hX">Specifies a handle to the vector X in GPU memory.</param>
        /// <param name="hY">Specifies a handle to the vector Y in GPU memory.</param>
        public void sum(int nCount, int nOuterNum, int nInnerNum, long hX, long hY)
        {
            if (m_dt == DataType.DOUBLE)
                m_cuda.RunDouble((int)m_hKernel, (int)CUDAFN.CUDA_SUM, m_param.AsDouble(nCount, nOuterNum, nInnerNum, hX, hY));
            else
                m_cuda.RunFloat((int)m_hKernel, (int)CUDAFN.CUDA_SUM, m_param.AsFloat(nCount, nOuterNum, nInnerNum, hX, hY));
        }

        /// <summary>
        /// Sets the random number generator seed used by random number operations.
        /// </summary>
        /// <remarks>
        /// This function uses [NVIDIA's cuRand](https://developer.nvidia.com/curand)
        /// </remarks>
        /// <param name="lSeed">Specifies the random number generator seed.</param>
        public void rng_setseed(long lSeed)
        {
            if (m_dt == DataType.DOUBLE)
                m_cuda.RunDouble((int)m_hKernel, (int)CUDAFN.CUDA_RNG_SETSEED, m_param.AsDouble(lSeed));
            else
                m_cuda.RunFloat((int)m_hKernel, (int)CUDAFN.CUDA_RNG_SETSEED, m_param.AsFloat(lSeed));
        }

        /// <summary>
        /// Fill Y with random numbers using a uniform random distribution.
        /// </summary>
        /// <remarks>
        /// This function uses [NVIDIA's cuRand](https://developer.nvidia.com/curand).  See also [Uniform Distribution](https://en.wikipedia.org/wiki/Uniform_distribution_(continuous)).
        /// </remarks>
        /// <param name="n">Specifies the number of items (not bytes) in the vector X.</param>
        /// <param name="fMin">Specifies the minimum value of the distribution with a type of <code>double</code></param>
        /// <param name="fMax">Specifies the maximum value of the distribution with a type of <code>double</code></param>
        /// <param name="hY">Specifies a handle to the vector Y in GPU memory.</param>
        public void rng_uniform(int n, double fMin, double fMax, long hY)
        {
            rng_uniform(n, (T)Convert.ChangeType(fMin, typeof(T)), (T)Convert.ChangeType(fMax, typeof(T)), hY);
        }

        /// <summary>
        /// Fill Y with random numbers using a uniform random distribution.
        /// </summary>
        /// <remarks>
        /// This function uses [NVIDIA's cuRand](https://developer.nvidia.com/curand).  See also [Uniform Distribution](https://en.wikipedia.org/wiki/Uniform_distribution_(continuous)).
        /// </remarks>
        /// <param name="n">Specifies the number of items (not bytes) in the vector X.</param>
        /// <param name="fMin">Specifies the minimum value of the distribution with a type of <code>float</code></param>
        /// <param name="fMax">Specifies the maximum value of the distribution with a type of <code>float</code></param>
        /// <param name="hY">Specifies a handle to the vector Y in GPU memory.</param>
        public void rng_uniform(int n, float fMin, float fMax, long hY)
        {
            rng_uniform(n, (T)Convert.ChangeType(fMin, typeof(T)), (T)Convert.ChangeType(fMax, typeof(T)), hY);
        }

        /// <summary>
        /// Fill Y with random numbers using a uniform random distribution.
        /// </summary>
        /// <remarks>
        /// This function uses [NVIDIA's cuRand](https://developer.nvidia.com/curand).  See also [Uniform Distribution](https://en.wikipedia.org/wiki/Uniform_distribution_(continuous)).
        /// </remarks>
        /// <param name="n">Specifies the number of items (not bytes) in the vector X.</param>
        /// <param name="fMin">Specifies the minimum value of the distribution with a type of 'T'.</param>
        /// <param name="fMax">Specifies the maximum value of the distribution with a type of 'T'.</param>
        /// <param name="hY">Specifies a handle to the vector Y in GPU memory.</param>
        public void rng_uniform(int n, T fMin, T fMax, long hY)
        {
            if (m_dt == DataType.DOUBLE)
            {
                if (m_rgGhostMemory == null || !m_bGhostMemoryEnabled)
                    m_cuda.RunDouble((int)m_hKernel, (int)CUDAFN.CUDA_RNG_UNIFORM, m_param.AsDouble(n, (double)Convert.ChangeType(fMin, typeof(double)), (double)Convert.ChangeType(fMax, typeof(double)), hY));
            }
            else
            {
                if (m_rgGhostMemory == null || !m_bGhostMemoryEnabled)
                    m_cuda.RunFloat((int)m_hKernel, (int)CUDAFN.CUDA_RNG_UNIFORM, m_param.AsFloat(n, (float)Convert.ChangeType(fMin, typeof(float)), (float)Convert.ChangeType(fMax, typeof(float)), hY));
            }
        }

        /// <summary>
        /// Fill Y with random numbers using a gaussian random distribution.
        /// </summary>
        /// <remarks>
        /// This function uses [NVIDIA's cuRand](https://developer.nvidia.com/curand).  See also [Guassian Distribution](https://en.wikipedia.org/wiki/Normal_distribution).
        /// </remarks>
        /// <param name="n">Specifies the number of items (not bytes) in the vector X.</param>
        /// <param name="fMu">Specifies the mean of the distribution with a type of <code>double</code></param>
        /// <param name="fSigma">Specifies the standard deviation of the distribution with a type of <code>double</code></param>
        /// <param name="hY">Specifies a handle to the vector Y in GPU memory.</param>
        public void rng_gaussian(int n, double fMu, double fSigma, long hY)
        {
            rng_gaussian(n, (T)Convert.ChangeType(fMu, typeof(T)), (T)Convert.ChangeType(fSigma, typeof(T)), hY);
        }

        /// <summary>
        /// Fill Y with random numbers using a gaussian random distribution.
        /// </summary>
        /// <remarks>
        /// This function uses [NVIDIA's cuRand](https://developer.nvidia.com/curand).  See also [Guassian Distribution](https://en.wikipedia.org/wiki/Normal_distribution).
        /// </remarks>
        /// <param name="n">Specifies the number of items (not bytes) in the vector X.</param>
        /// <param name="fMu">Specifies the mean of the distribution with a type of <code>float</code></param>
        /// <param name="fSigma">Specifies the standard deviation of the distribution with a type of <code>float</code></param>
        /// <param name="hY">Specifies a handle to the vector Y in GPU memory.</param>
        public void rng_gaussian(int n, float fMu, float fSigma, long hY)
        {
            rng_gaussian(n, (T)Convert.ChangeType(fMu, typeof(T)), (T)Convert.ChangeType(fSigma, typeof(T)), hY);
        }

        /// <summary>
        /// Fill Y with random numbers using a gaussian random distribution.
        /// </summary>
        /// <remarks>
        /// This function uses [NVIDIA's cuRand](https://developer.nvidia.com/curand).  See also [Guassian Distribution](https://en.wikipedia.org/wiki/Normal_distribution).
        /// </remarks>
        /// <param name="n">Specifies the number of items (not bytes) in the vector X.</param>
        /// <param name="fMu">Specifies the mean of the distribution with a type of 'T'.</param>
        /// <param name="fSigma">Specifies the standard deviation of the distribution with a type of 'T'.</param>
        /// <param name="hY">Specifies a handle to the vector Y in GPU memory.</param>
        public void rng_gaussian(int n, T fMu, T fSigma, long hY)
        {
            if (m_dt == DataType.DOUBLE)
            {
                if (m_rgGhostMemory == null || !m_bGhostMemoryEnabled)
                    m_cuda.RunDouble((int)m_hKernel, (int)CUDAFN.CUDA_RNG_GAUSSIAN, m_param.AsDouble(n, (double)Convert.ChangeType(fMu, typeof(double)), (double)Convert.ChangeType(fSigma, typeof(double)), hY));
            }
            else
            {
                if (m_rgGhostMemory == null || !m_bGhostMemoryEnabled)
                    m_cuda.RunFloat((int)m_hKernel, (int)CUDAFN.CUDA_RNG_GAUSSIAN, m_param.AsFloat(n, (float)Convert.ChangeType(fMu, typeof(float)), (float)Convert.ChangeType(fSigma, typeof(float)), hY));
            }
        }

        /// <summary>
        /// Fill Y with random numbers using a bernoulli random distribution.
        /// </summary>
        /// <remarks>
        /// See [Bernoulli Distribution](https://en.wikipedia.org/wiki/Bernoulli_distribution).
        /// </remarks>
        /// <param name="n">Specifies the number of items (not bytes) in the vector X.</param>
        /// <param name="fNonZeroProb">Specifies the probability that a given value is set to non zero.</param>
        /// <param name="hY">Specifies a handle to the vector Y in GPU memory.</param>
        public void rng_bernoulli(int n, double fNonZeroProb, long hY)
        {
            rng_bernoulli(n, (T)Convert.ChangeType(fNonZeroProb, typeof(T)), hY);
        }

        /// <summary>
        /// Fill Y with random numbers using a bernoulli random distribution.
        /// </summary>
        /// <remarks>
        /// See [Bernoulli Distribution](https://en.wikipedia.org/wiki/Bernoulli_distribution).
        /// </remarks>
        /// <param name="n">Specifies the number of items (not bytes) in the vector X.</param>
        /// <param name="fNonZeroProb">Specifies the probability that a given value is set to non zero.</param>
        /// <param name="hY">Specifies a handle to the vector Y in GPU memory.</param>
        public void rng_bernoulli(int n, float fNonZeroProb, long hY)
        {
            rng_bernoulli(n, (T)Convert.ChangeType(fNonZeroProb, typeof(T)), hY);
        }

        /// <summary>
        /// Fill Y with random numbers using a bernoulli random distribution.
        /// </summary>
        /// <remarks>
        /// See [Bernoulli Distribution](https://en.wikipedia.org/wiki/Bernoulli_distribution).
        /// </remarks>
        /// <param name="n">Specifies the number of items (not bytes) in the vector X.</param>
        /// <param name="fNonZeroProb">Specifies the probability that a given value is set to non zero.</param>
        /// <param name="hY">Specifies a handle to the vector Y in GPU memory.</param>
        public void rng_bernoulli(int n, T fNonZeroProb, long hY)
        {
            //if (m_dt == DataType.DOUBLE)
            //    m_cuda.RunDouble((int)m_hKernel, (int)CUDAFN.CUDA_RNG_BERNOULLI, new double[] { n, (double)Convert.ChangeType(fNonZeroProb, typeof(double)), hY });
            //else
            //    m_cuda.RunFloat((int)m_hKernel, (int)CUDAFN.CUDA_RNG_BERNOULLI, new float[] { n, (float)Convert.ChangeType(fNonZeroProb, typeof(float)), hY });

            T[] rg = GetMemory(hY);
            fill_random(fNonZeroProb, rg);
            SetMemory(hY, rg);
        }

#pragma warning disable 1591

        public void fill_random(T fNonZeroProb, T[] rg) /** @private */
        {
            double dfNonZeroProb = Utility.ConvertVal<T>(fNonZeroProb);

            for (int i = 0; i < rg.Length; i++)
            {
                double dfRand = m_random.NextDouble();
                rg[i] = (dfRand <= dfNonZeroProb) ? m_tOne : m_tZero;
            }
        }

#pragma warning restore 1591

        
        /// <summary>
        /// Performs the forward pass for the accuracy layer
        /// </summary>
        /// <param name="nCount">Specifies the number of items.</param>
        /// <param name="hBottomData">Specifies a handle to the bottom data in GPU memory.</param>
        /// <param name="hBottomLabel">Specifies a handle to the bottom labels in GPU memory.</param>
        /// <param name="hAccData">Specifies a handle to temporary accuracy data in GPU memory.</param>
        /// <param name="nOuterNum">Specifies the outer count.</param>
        /// <param name="nDim">Specifies the dimension.</param>
        /// <param name="nInnerNum">Specifies the inner count.</param>
        /// <param name="nNumLabels">Specifies the number of labels.</param>
        /// <param name="nTopK">Specifies the top items to include in the accuracy.</param>
        /// <param name="hCounts">Specifies a handle to the counts data in GPU memory.</param>
        /// <param name="bPerClass">Specifies whether (true) to caculate the accuracy for each class, or (false) globally.</param>
        /// <param name="nIgnoreLabel">Optionally, specifies a label to ignore, or <i>null</i> to ignore.</param>
        public void accuracy_fwd(int nCount, long hBottomData, long hBottomLabel, long hAccData, int nOuterNum, int nDim, int nInnerNum, int nNumLabels, int nTopK, long hCounts, bool bPerClass, int? nIgnoreLabel = null)
        {
            if (m_dt == DataType.DOUBLE)
            {
                List<double> rgArg = new List<double>() { nCount, hBottomData, hBottomLabel, hAccData, nOuterNum, nDim, nInnerNum, nNumLabels, nTopK, hCounts, (bPerClass) ? 1 : 0 };
                if (nIgnoreLabel.HasValue)
                    rgArg.Add(nIgnoreLabel.Value);
                m_cuda.RunDouble((int)m_hKernel, (int)CUDAFN.CUDA_ACCURACY_FWD, rgArg.ToArray());
            }
            else
            {
                List<float> rgArg = new List<float>() { nCount, hBottomData, hBottomLabel, hAccData, nOuterNum, nDim, nInnerNum, nNumLabels, nTopK, hCounts, (bPerClass) ? 1 : 0 };
                if (nIgnoreLabel.HasValue)
                    rgArg.Add(nIgnoreLabel.Value);
                m_cuda.RunFloat((int)m_hKernel, (int)CUDAFN.CUDA_ACCURACY_FWD, rgArg.ToArray());
            }
        }


        /// <summary>
        /// Performs the forward pass for batch re-index
        /// </summary>
        /// <param name="nCount">Specifies the number of items.</param>
        /// <param name="nInnerDim">Specifies the inner dimension.</param>
        /// <param name="hBottomData">Specifies a handle to the bottom data in GPU memory.</param>
        /// <param name="hPermutData">Specifies a handle to the permuation data in GPU memory.</param>
        /// <param name="hTopData">Specifies a handle to the top data in GPU memory.</param>
        public void batchreidx_fwd(int nCount, int nInnerDim, long hBottomData, long hPermutData, long hTopData)
        {
            if (m_dt == DataType.DOUBLE)
                m_cuda.RunDouble((int)m_hKernel, (int)CUDAFN.CUDA_BATCHREIDX_FWD, m_param.AsDouble(nCount, nInnerDim, hBottomData, hPermutData, hTopData));
            else
                m_cuda.RunFloat((int)m_hKernel, (int)CUDAFN.CUDA_BATCHREIDX_FWD, m_param.AsFloat(nCount, nInnerDim, hBottomData, hPermutData, hTopData));
        }

        /// <summary>
        /// Performs the backward pass for batch re-index
        /// </summary>
        /// <param name="nCount">Specifies the number of items.</param>
        /// <param name="nInnerDim">Specifies the inner dimension.</param>
        /// <param name="hTopDiff">Specifies a handle to the top diff in GPU memory.</param>
        /// <param name="hTopIdx">Specifies a handle to the top indexes in GPU memory.</param>
        /// <param name="hBegins">Specifies a handle to the begin data in GPU memory.</param>
        /// <param name="hCounts">Specifies a handle to the counts in GPU memory.</param>
        /// <param name="hBottomDiff">Specifies a handle to the bottom diff in GPU memory.</param>
        public void batchreidx_bwd(int nCount, int nInnerDim, long hTopDiff, long hTopIdx, long hBegins, long hCounts, long hBottomDiff)
        {
            if (m_dt == DataType.DOUBLE)
                m_cuda.RunDouble((int)m_hKernel, (int)CUDAFN.CUDA_BATCHREIDX_BWD, m_param.AsDouble(nCount, nInnerDim, hTopDiff, hTopIdx, hBegins, hCounts, hBottomDiff));
            else
                m_cuda.RunFloat((int)m_hKernel, (int)CUDAFN.CUDA_BATCHREIDX_BWD, m_param.AsFloat(nCount, nInnerDim, hTopDiff, hTopIdx, hBegins, hCounts, hBottomDiff));
        }

        /// <summary>
        /// Performs the forward pass for embed 
        /// </summary>
        /// <param name="nCount">Specifies the number of items in the bottom data.</param>
        /// <param name="hBottomData">Specifies a handle to the bottom data in GPU memory.</param>
        /// <param name="hWeight">Specifies a handle to the weight data in GPU memory.</param>
        /// <param name="nM"><b><i>NEEDS REVIEW</i></b></param>
        /// <param name="nN"><b><i>NEEDS REVIEW</i></b></param>
        /// <param name="nK"><b><i>NEEDS REVIEW</i></b></param>
        /// <param name="hTopData">Specifies a handle to the top data in GPU memory.</param>
        public void embed_fwd(int nCount, long hBottomData, long hWeight, int nM, int nN, int nK, long hTopData)
        {
            if (m_dt == DataType.DOUBLE)
                m_cuda.RunDouble((int)m_hKernel, (int)CUDAFN.CUDA_EMBED_FWD, m_param.AsDouble(nCount, hBottomData, hWeight, nM, nN, nK, hTopData));
            else
                m_cuda.RunFloat((int)m_hKernel, (int)CUDAFN.CUDA_EMBED_FWD, m_param.AsFloat(nCount, hBottomData, hWeight, nM, nN, nK, hTopData));
        }

        /// <summary>
        /// Performs the backward pass for embed
        /// </summary>
        /// <param name="nCount">Specifies the number of items in the bottom data.</param>
        /// <param name="hBottomData">Specifies a handle to the bottom data in GPU memory.</param>
        /// <param name="hTopDiff">Specifies a handle to the top diff in GPU memory.</param>
        /// <param name="nM"><b><i>NEEDS REVIEW</i></b></param>
        /// <param name="nN"><b><i>NEEDS REVIEW</i></b></param>
        /// <param name="nK"><b><i>NEEDS REVIEW</i></b></param>
        /// <param name="hWeightDiff">Specifies a handle to the weight diff in GPU memory.</param>
        public void embed_bwd(int nCount, long hBottomData, long hTopDiff, int nM, int nN, int nK, long hWeightDiff)
        {
            if (m_dt == DataType.DOUBLE)
                m_cuda.RunDouble((int)m_hKernel, (int)CUDAFN.CUDA_EMBED_BWD, m_param.AsDouble(nCount, hBottomData, hTopDiff, nM, nN, nK, hWeightDiff));
            else
                m_cuda.RunFloat((int)m_hKernel, (int)CUDAFN.CUDA_EMBED_BWD, m_param.AsFloat(nCount, hBottomData, hTopDiff, nM, nN, nK, hWeightDiff));
        }

        /// <summary>
        /// Performs the forward pass for pooling using Cuda
        /// </summary>
        /// <param name="method">Specifies the pooling method.</param>
        /// <param name="nCount">Specifies the number of items in the bottom data.</param>
        /// <param name="hBottomData">Specifies a handle to the bottom data in GPU memory.</param>
        /// <param name="num">Specifies the number of inputs.</param>
        /// <param name="nChannels">Specifies the number of channels per input.</param>
        /// <param name="nHeight">Specifies the height of each input.</param>
        /// <param name="nWidth">Specifies the width of each input.</param>
        /// <param name="nPooledHeight">Specifies the height of the pooled data.</param>
        /// <param name="nPooledWidth">Specifies the width of the pooled data.</param>
        /// <param name="nKernelH">Specifies the height of the pooling kernel.</param>
        /// <param name="nKernelW">Specifies the width of the pooling kernel.</param>
        /// <param name="nStrideH">Specifies the stride along the height.</param>
        /// <param name="nStrideW">Specifies the stride along the width.</param>
        /// <param name="nPadH">Specifies the pad applied to the height.</param>
        /// <param name="nPadW">Specifies the pad applied to the width.</param>
        /// <param name="hTopData">Specifies a handle to the top data in GPU memory.</param>
        /// <param name="hMask">Specifies a handle to the mask data in GPU memory.</param>
        /// <param name="hTopMask">Specifies a handle to the top mask data in GPU memory.</param>
        public void pooling_fwd(POOLING_METHOD method, int nCount, long hBottomData, int num, int nChannels, int nHeight, int nWidth, int nPooledHeight, int nPooledWidth, int nKernelH, int nKernelW, int nStrideH, int nStrideW, int nPadH, int nPadW, long hTopData, long hMask, long hTopMask)
        {
            if (m_dt == DataType.DOUBLE)
                m_cuda.RunDouble((int)m_hKernel, (int)CUDAFN.CUDA_POOL_FWD, m_param.AsDouble((int)method, nCount, hBottomData, num, nChannels, nHeight, nWidth, nPooledHeight, nPooledWidth, nKernelH, nKernelW, nStrideH, nStrideW, nPadH, nPadW, hTopData, hMask, hTopMask));
            else
                m_cuda.RunFloat((int)m_hKernel, (int)CUDAFN.CUDA_POOL_FWD, m_param.AsFloat((int)method, nCount, hBottomData, num, nChannels, nHeight, nWidth, nPooledHeight, nPooledWidth, nKernelH, nKernelW, nStrideH, nStrideW, nPadH, nPadW, hTopData, hMask, hTopMask));
        }

        /// <summary>
        /// Performs the backward pass for pooling using Cuda
        /// </summary>
        /// <param name="method">Specifies the pooling method.</param>
        /// <param name="nCount">Specifies the number of items in the bottom data.</param>
        /// <param name="hTopDiff">Specifies a handle to the top diff in GPU memory.</param>
        /// <param name="num">Specifies the number of inputs.</param>
        /// <param name="nChannels">Specifies the number of channels per input.</param>
        /// <param name="nHeight">Specifies the height of each input.</param>
        /// <param name="nWidth">Specifies the width of each input.</param>
        /// <param name="nPooledHeight">Specifies the height of the pooled data.</param>
        /// <param name="nPooledWidth">Specifies the width of the pooled data.</param>
        /// <param name="nKernelH">Specifies the height of the pooling kernel.</param>
        /// <param name="nKernelW">Specifies the width of the pooling kernel.</param>
        /// <param name="nStrideH">Specifies the stride along the height.</param>
        /// <param name="nStrideW">Specifies the stride along the width.</param>
        /// <param name="nPadH">Specifies the pad applied to the height.</param>
        /// <param name="nPadW">Specifies the pad applied to the width.</param>
        /// <param name="hBottomDiff">Specifies a handle to the bottom diff in GPU memory.</param>
        /// <param name="hMask">Specifies a handle to the mask data in GPU memory.</param>
        /// <param name="hTopMask">Specifies a handle to the top mask data in GPU memory.</param>
        public void pooling_bwd(POOLING_METHOD method, int nCount, long hTopDiff, int num, int nChannels, int nHeight, int nWidth, int nPooledHeight, int nPooledWidth, int nKernelH, int nKernelW, int nStrideH, int nStrideW, int nPadH, int nPadW, long hBottomDiff, long hMask, long hTopMask)
        {
            if (m_dt == DataType.DOUBLE)
                m_cuda.RunDouble((int)m_hKernel, (int)CUDAFN.CUDA_POOL_BWD, m_param.AsDouble((int)method, nCount, hTopDiff, num, nChannels, nHeight, nWidth, nPooledHeight, nPooledWidth, nKernelH, nKernelW, nStrideH, nStrideW, nPadH, nPadW, hBottomDiff, hMask, hTopMask));
            else
                m_cuda.RunFloat((int)m_hKernel, (int)CUDAFN.CUDA_POOL_BWD, m_param.AsFloat((int)method, nCount, hTopDiff, num, nChannels, nHeight, nWidth, nPooledHeight, nPooledWidth, nKernelH, nKernelW, nStrideH, nStrideW, nPadH, nPadW, hBottomDiff, hMask, hTopMask));
        }

        /// <summary>
        /// Performs the forward pass for unpooling using Cuda
        /// </summary>
        /// <param name="method">Specifies the pooling method.</param>
        /// <param name="nCount">Specifies the number of items in the bottom data.</param>
        /// <param name="hBottomData">Specifies a handle to the bottom data in GPU memory.</param>
        /// <param name="num">Specifies the number of inputs.</param>
        /// <param name="nChannels">Specifies the number of channels per input.</param>
        /// <param name="nHeight">Specifies the height of each input.</param>
        /// <param name="nWidth">Specifies the width of each input.</param>
        /// <param name="nPooledHeight">Specifies the height of the pooled data.</param>
        /// <param name="nPooledWidth">Specifies the width of the pooled data.</param>
        /// <param name="nKernelH">Specifies the height of the pooling kernel.</param>
        /// <param name="nKernelW">Specifies the width of the pooling kernel.</param>
        /// <param name="nStrideH">Specifies the stride along the height.</param>
        /// <param name="nStrideW">Specifies the stride along the width.</param>
        /// <param name="nPadH">Specifies the pad applied to the height.</param>
        /// <param name="nPadW">Specifies the pad applied to the width.</param>
        /// <param name="hTopData">Specifies a handle to the top data in GPU memory.</param>
        /// <param name="hMask">Specifies a handle to the mask data in GPU memory.</param>
        public void unpooling_fwd(POOLING_METHOD method, int nCount, long hBottomData, int num, int nChannels, int nHeight, int nWidth, int nPooledHeight, int nPooledWidth, int nKernelH, int nKernelW, int nStrideH, int nStrideW, int nPadH, int nPadW, long hTopData, long hMask)
        {
            if (m_dt == DataType.DOUBLE)
                m_cuda.RunDouble((int)m_hKernel, (int)CUDAFN.CUDA_UNPOOL_FWD, m_param.AsDouble((int)method, nCount, hBottomData, num, nChannels, nHeight, nWidth, nPooledHeight, nPooledWidth, nKernelH, nKernelW, nStrideH, nStrideW, nPadH, nPadW, hTopData, hMask));
            else
                m_cuda.RunFloat((int)m_hKernel, (int)CUDAFN.CUDA_UNPOOL_FWD, m_param.AsFloat((int)method, nCount, hBottomData, num, nChannels, nHeight, nWidth, nPooledHeight, nPooledWidth, nKernelH, nKernelW, nStrideH, nStrideW, nPadH, nPadW, hTopData, hMask));
        }

        /// <summary>
        /// Performs the backward pass for unpooling using Cuda
        /// </summary>
        /// <param name="method">Specifies the pooling method.</param>
        /// <param name="nCount">Specifies the number of items in the bottom data.</param>
        /// <param name="hTopDiff">Specifies a handle to the top diff in GPU memory.</param>
        /// <param name="num">Specifies the number of inputs.</param>
        /// <param name="nChannels">Specifies the number of channels per input.</param>
        /// <param name="nHeight">Specifies the height of each input.</param>
        /// <param name="nWidth">Specifies the width of each input.</param>
        /// <param name="nPooledHeight">Specifies the height of the pooled data.</param>
        /// <param name="nPooledWidth">Specifies the width of the pooled data.</param>
        /// <param name="nKernelH">Specifies the height of the pooling kernel.</param>
        /// <param name="nKernelW">Specifies the width of the pooling kernel.</param>
        /// <param name="nStrideH">Specifies the stride along the height.</param>
        /// <param name="nStrideW">Specifies the stride along the width.</param>
        /// <param name="nPadH">Specifies the pad applied to the height.</param>
        /// <param name="nPadW">Specifies the pad applied to the width.</param>
        /// <param name="hBottomDiff">Specifies a handle to the bottom diff in GPU memory.</param>
        /// <param name="hMask">Specifies a handle to the mask data in GPU memory.</param>
        public void unpooling_bwd(POOLING_METHOD method, int nCount, long hTopDiff, int num, int nChannels, int nHeight, int nWidth, int nPooledHeight, int nPooledWidth, int nKernelH, int nKernelW, int nStrideH, int nStrideW, int nPadH, int nPadW, long hBottomDiff, long hMask)
        {
            if (m_dt == DataType.DOUBLE)
                m_cuda.RunDouble((int)m_hKernel, (int)CUDAFN.CUDA_UNPOOL_BWD, m_param.AsDouble((int)method, nCount, hTopDiff, num, nChannels, nHeight, nWidth, nPooledHeight, nPooledWidth, nKernelH, nKernelW, nStrideH, nStrideW, nPadH, nPadW, hBottomDiff, hMask));
            else
                m_cuda.RunFloat((int)m_hKernel, (int)CUDAFN.CUDA_UNPOOL_BWD, m_param.AsFloat((int)method, nCount, hTopDiff, num, nChannels, nHeight, nWidth, nPooledHeight, nPooledWidth, nKernelH, nKernelW, nStrideH, nStrideW, nPadH, nPadW, hBottomDiff, hMask));
        }

        /// <summary>
        /// Performs a Clip forward pass in Cuda.
        /// </summary>
        /// <remarks>
        /// Calculation @f$ Y[i] = \max(min, \min(max,X[i])) @f$
        /// </remarks>
        /// <param name="nCount">Specifies the number of items in the bottom and top data.</param>
        /// <param name="hBottomData">Specifies a handle to the bottom data in GPU memory.</param>
        /// <param name="hTopData">Specifies a handle to the top data in GPU memory.</param>
        /// <param name="fMin">Specifies the bottom value to clip to.</param>
        /// <param name="fMax">Specifies the top value to clip to.</param>
        public void clip_fwd(int nCount, long hBottomData, long hTopData, T fMin, T fMax)
        {
            if (m_dt == DataType.DOUBLE)
                m_cuda.RunDouble((int)m_hKernel, (int)CUDAFN.CUDA_CLIP_FWD, m_param.AsDouble(nCount, hBottomData, hTopData, convertD1(fMin), convertD1(fMax)));
            else
                m_cuda.RunFloat((int)m_hKernel, (int)CUDAFN.CUDA_CLIP_FWD, m_param.AsFloat(nCount, hBottomData, hTopData, convertF1(fMin), convertF1(fMax)));
        }

        /// <summary>
        /// Performs a Clip backward pass in Cuda.
        /// </summary>
        /// <param name="nCount">Specifies the number of items.</param>
        /// <param name="hTopDiff">Specifies a handle to the top diff in GPU memory.</param>
        /// <param name="hBottomData">Specifies a handle to the bottom data in GPU memory.</param>
        /// <param name="hBottomDiff">Specifies a handle to the bottom diff in GPU memory.</param>
        /// <param name="fMin">Specifies the bottom value to clip to.</param>
        /// <param name="fMax">Specifies the top value to clip to.</param>
        public void clip_bwd(int nCount, long hTopDiff, long hBottomData, long hBottomDiff, T fMin, T fMax)
        {
            if (m_dt == DataType.DOUBLE)
                m_cuda.RunDouble((int)m_hKernel, (int)CUDAFN.CUDA_CLIP_BWD, m_param.AsDouble(nCount, hTopDiff, hBottomData, hBottomDiff, convertD1(fMin), convertD1(fMax)));
            else
                m_cuda.RunFloat((int)m_hKernel, (int)CUDAFN.CUDA_CLIP_BWD, m_param.AsFloat(nCount, hTopDiff, hBottomData, hBottomDiff, convertF1(fMin), convertF1(fMax)));
        }

        /// <summary>
        /// Performs a Math function forward pass in Cuda.
        /// </summary>
        /// <remarks>
        /// Calculation @f$ Y[i] = function(X[i]) @f$
        /// </remarks>
        /// <param name="nCount">Specifies the number of items in the bottom and top data.</param>
        /// <param name="hBottomData">Specifies a handle to the bottom data in GPU memory.</param>
        /// <param name="hTopData">Specifies a handle to the top data in GPU memory.</param>
        /// <param name="function">Specifies the mathematical function to use.</param>
        public void math_fwd(int nCount, long hBottomData, long hTopData, MATH_FUNCTION function)
        {
            if (m_dt == DataType.DOUBLE)
                m_cuda.RunDouble((int)m_hKernel, (int)CUDAFN.CUDA_MATH_FWD, m_param.AsDouble(nCount, hBottomData, hTopData, (int)function));
            else
                m_cuda.RunFloat((int)m_hKernel, (int)CUDAFN.CUDA_MATH_FWD, m_param.AsFloat(nCount, hBottomData, hTopData, (int)function));
        }

        /// <summary>
        /// Performs a Math function backward pass in Cuda.
        /// </summary>
        /// <param name="nCount">Specifies the number of items.</param>
        /// <param name="hTopDiff">Specifies a handle to the top diff in GPU memory.</param>
        /// <param name="hTopData">Specifies a handle to the top data in GPU memory.</param>
        /// <param name="hBottomDiff">Specifies a handle to the bottom diff in GPU memory.</param>
        /// <param name="hBottomData">Specifies a handle tot he bottom data in GPU memory.</param>
        /// <param name="function">Specifies the mathematical function to use.</param>
        public void math_bwd(int nCount, long hTopDiff, long hTopData, long hBottomDiff, long hBottomData, MATH_FUNCTION function)
        {
            if (m_dt == DataType.DOUBLE)
                m_cuda.RunDouble((int)m_hKernel, (int)CUDAFN.CUDA_MATH_BWD, m_param.AsDouble(nCount, hTopDiff, hTopData, hBottomDiff, hBottomData, (int)function));
            else
                m_cuda.RunFloat((int)m_hKernel, (int)CUDAFN.CUDA_MATH_BWD, m_param.AsFloat(nCount, hTopDiff, hTopData, hBottomDiff, hBottomData, (int)function));
        }

        /// <summary>
        /// Performs a MAE Loss backward pass in Cuda.
        /// </summary>
        /// <remarks>
        /// The gradient is set to:
        ///     +1 when predicted greater than target,
        ///     -1 when predicted less than target,
        ///      0 when predicted equal to target.
        /// if propagate_down[1] == true.
        /// 
        /// @see [Mean Absolute Error (MAE) derivative](https://stats.stackexchange.com/questions/312737/mean-absolute-error-mae-derivative)
        /// </remarks>
        /// <param name="nCount">Specifies the number of items.</param>
        /// <param name="hPredicted">Specifies a handle to the predicted data in GPU memory.</param>
        /// <param name="hTarget">Specifies a handle to the target data in GPU memory.</param>
        /// <param name="hBottomDiff">Specifies a handle to the bottom diff in GPU memory.</param>
        public void mae_loss_bwd(int nCount, long hPredicted, long hTarget, long hBottomDiff)
        {
            if (m_dt == DataType.DOUBLE)
                m_cuda.RunDouble((int)m_hKernel, (int)CUDAFN.CUDA_MAE_LOSS_BWD, m_param.AsDouble(nCount, hPredicted, hTarget, hBottomDiff));
            else
                m_cuda.RunFloat((int)m_hKernel, (int)CUDAFN.CUDA_MAE_LOSS_BWD, m_param.AsFloat(nCount, hPredicted, hTarget, hBottomDiff));
        }


        /// <summary>
        /// Performs a Mish forward pass in Cuda.
        /// </summary>
        /// <remarks>
        /// Computes the mish non-linearity @f$ f(x)  = x * tanh(ln( 1 + e^x )) @f$.
        /// 
        /// @see [Mish: A Self Regularized Non-Monotonic Neural Activation Function](https://arxiv.org/abs/1908.08681v1) by Diganta Misra, 2019.
        /// </remarks>
        /// <param name="nCount">Specifies the number of items in the bottom and top data.</param>
        /// <param name="hBottomData">Specifies a handle to the bottom data in GPU memory.</param>
        /// <param name="hTopData">Specifies a handle to the top data in GPU memory.</param>
        /// <param name="dfThreshold">Specifies the threshold value.</param>
        public void mish_fwd(int nCount, long hBottomData, long hTopData, double dfThreshold)
        {
            if (m_dt == DataType.DOUBLE)
                m_cuda.RunDouble((int)m_hKernel, (int)CUDAFN.CUDA_MISH_FWD, m_param.AsDouble(nCount, hBottomData, hTopData, dfThreshold));
            else
                m_cuda.RunFloat((int)m_hKernel, (int)CUDAFN.CUDA_MISH_FWD, m_param.AsFloat(nCount, hBottomData, hTopData, (float)dfThreshold));
        }

        /// <summary>
        /// Performs a Mish backward pass in Cuda.
        /// </summary>
        /// <remarks>
        /// Computes the mish gradient @f$ f(x)' = \frac{ exp(x) * (4*e^x * x + 4*x + 6*e^x + 4*e^2x + e^3x + 4) }{ (2*e^x + e^2x + 2)^2 } @f$
        /// Note, see Wolfram Alpha with 'derivative of x * tanh(ln(1 + e^x))'                                         
        /// 
        /// @see [Mish: A Self Regularized Non-Monotonic Neural Activation Function](https://arxiv.org/abs/1908.08681v1) by Diganta Misra, 2019.
        /// </remarks>
        /// <param name="nCount">Specifies the number of items.</param>
        /// <param name="hTopDiff">Specifies a handle to the top diff in GPU memory.</param>
        /// <param name="hTopData">Specifies a handle to the top data in GPU memory.</param>
        /// <param name="hBottomDiff">Specifies a handle to the bottom diff in GPU memory.</param>
        /// <param name="hBottomData">Specifies a handle tot he bottom data in GPU memory.</param>
        /// <param name="dfThreshold">Specifies the threshold value.</param>
        /// <param name="nMethod">Optionally, specifies to run the new implementation when > 0.</param>
        public void mish_bwd(int nCount, long hTopDiff, long hTopData, long hBottomDiff, long hBottomData, double dfThreshold, int nMethod = 0)
        {
            if (m_dt == DataType.DOUBLE)
                m_cuda.RunDouble((int)m_hKernel, (int)CUDAFN.CUDA_MISH_BWD, m_param.AsDouble(nCount, hTopDiff, hTopData, hBottomDiff, hBottomData, dfThreshold, nMethod));
            else
                m_cuda.RunFloat((int)m_hKernel, (int)CUDAFN.CUDA_MISH_BWD, m_param.AsFloat(nCount, hTopDiff, hTopData, hBottomDiff, hBottomData, (float)dfThreshold, nMethod));
        }

        /// <summary>
        /// Performs a TanH forward pass in Cuda.
        /// </summary>
        /// <remarks>
        /// Calculation @f$ f(x) = tanh(x) @f$
        /// 
        /// @see [Hyperbolic Function](https://en.wikipedia.org/wiki/Hyperbolic_function).
        /// </remarks>
        /// <param name="nCount">Specifies the number of items in the bottom and top data.</param>
        /// <param name="hBottomData">Specifies a handle to the bottom data in GPU memory.</param>
        /// <param name="hTopData">Specifies a handle to the top data in GPU memory.</param>
        public void tanh_fwd(int nCount, long hBottomData, long hTopData)
        {
            if (m_dt == DataType.DOUBLE)
                m_cuda.RunDouble((int)m_hKernel, (int)CUDAFN.CUDA_TANH_FWD, m_param.AsDouble(nCount, hBottomData, hTopData));
            else
                m_cuda.RunFloat((int)m_hKernel, (int)CUDAFN.CUDA_TANH_FWD, m_param.AsFloat(nCount, hBottomData, hTopData));
        }

        /// <summary>
        /// Performs a TanH backward pass in Cuda.
        /// </summary>
        /// <remarks>
        /// @see [Hyperbolic Function](https://en.wikipedia.org/wiki/Hyperbolic_function).
        /// </remarks>
        /// <param name="nCount">Specifies the number of items.</param>
        /// <param name="hTopDiff">Specifies a handle to the top diff in GPU memory.</param>
        /// <param name="hTopData">Specifies a handle to the top data in GPU memory.</param>
        /// <param name="hBottomDiff">Specifies a handle to the bottom diff in GPU memory.</param>
        public void tanh_bwd(int nCount, long hTopDiff, long hTopData, long hBottomDiff)
        {
            if (m_dt == DataType.DOUBLE)
                m_cuda.RunDouble((int)m_hKernel, (int)CUDAFN.CUDA_TANH_BWD, m_param.AsDouble(nCount, hTopDiff, hTopData, hBottomDiff));
            else
                m_cuda.RunFloat((int)m_hKernel, (int)CUDAFN.CUDA_TANH_BWD, m_param.AsFloat(nCount, hTopDiff, hTopData, hBottomDiff));
        }

        /// <summary>
        /// Performs a Sigmoid forward pass in Cuda.
        /// </summary>
        /// <remarks>
        /// Calcuation @f$ f(x) = 1.0 / (1.0 + e^-x) @f$
        /// 
        /// @see [Sigmoid Function](https://en.wikipedia.org/wiki/Sigmoid_function).
        /// </remarks>
        /// <param name="nCount">Specifies the number of items in the bottom and top data.</param>
        /// <param name="hBottomData">Specifies a handle to the bottom data in GPU memory.</param>
        /// <param name="hTopData">Specifies a handle to the top data in GPU memory.</param>
        public void sigmoid_fwd(int nCount, long hBottomData, long hTopData)
        {
            if (m_dt == DataType.DOUBLE)
                m_cuda.RunDouble((int)m_hKernel, (int)CUDAFN.CUDA_SIGMOID_FWD, m_param.AsDouble(nCount, hBottomData, hTopData));
            else
                m_cuda.RunFloat((int)m_hKernel, (int)CUDAFN.CUDA_SIGMOID_FWD, m_param.AsFloat(nCount, hBottomData, hTopData));
        }

        /// <summary>
        /// Performs a Sigmoid backward pass in Cuda.
        /// </summary>
        /// <remarks>
        /// @see [Sigmoid Function](https://en.wikipedia.org/wiki/Sigmoid_function).
        /// </remarks>
        /// <param name="nCount">Specifies the number of items.</param>
        /// <param name="hTopDiff">Specifies a handle to the top diff in GPU memory.</param>
        /// <param name="hTopData">Specifies a handle to the top data in GPU memory.</param>
        /// <param name="hBottomDiff">Specifies a handle to the bottom diff in GPU memory.</param>
        public void sigmoid_bwd(int nCount, long hTopDiff, long hTopData, long hBottomDiff)
        {
            if (m_dt == DataType.DOUBLE)
                m_cuda.RunDouble((int)m_hKernel, (int)CUDAFN.CUDA_SIGMOID_BWD, m_param.AsDouble(nCount, hTopDiff, hTopData, hBottomDiff));
            else
                m_cuda.RunFloat((int)m_hKernel, (int)CUDAFN.CUDA_SIGMOID_BWD, m_param.AsFloat(nCount, hTopDiff, hTopData, hBottomDiff));
        }

        /// <summary>
        /// Performs a Swish backward pass in Cuda.
        /// </summary>
        /// <remarks>
        /// @see [Activation Functions](https://arxiv.org/abs/1710.05941v2) by Prajit Ramachandran, Barret Zoph, Quoc V. Le., 2017.
        /// </remarks>
        /// <param name="nCount">Specifies the number of items.</param>
        /// <param name="hTopDiff">Specifies a handle to the top diff in GPU memory.</param>
        /// <param name="hTopData">Specifies a handle to the top data in GPU memory.</param>
        /// <param name="hSigmoidOutputData">Specifies a handle to the sigmoid output data in GPU memory.</param>
        /// <param name="hBottomDiff">Specifies a handle to the bottom diff in GPU memory.</param>
        /// <param name="dfBeta">Specifies the 'beta' value applied to the output.</param>
        public void swish_bwd(int nCount, long hTopDiff, long hTopData, long hSigmoidOutputData, long hBottomDiff, double dfBeta)
        {
            if (m_dt == DataType.DOUBLE)
                m_cuda.RunDouble((int)m_hKernel, (int)CUDAFN.CUDA_SWISH_BWD, m_param.AsDouble(nCount, hTopDiff, hTopData, hSigmoidOutputData, hBottomDiff, dfBeta));
            else
                m_cuda.RunFloat((int)m_hKernel, (int)CUDAFN.CUDA_SWISH_BWD, m_param.AsFloat(nCount, hTopDiff, hTopData, hSigmoidOutputData, hBottomDiff, (float)dfBeta));
        }

        /// <summary>
        /// Performs a Rectifier Linear Unit (ReLU) forward pass in Cuda.
        /// </summary>
        /// <remarks>
        /// Calculation @f$ f(x) = (x > 0) ? x : x * negativeSlope @f$
        /// 
        /// @see [Rectifier](https://en.wikipedia.org/wiki/Rectifier_(neural_networks)), and
        /// @see [Understanding Deep Neural Networks with Rectified Linear Units](https://arxiv.org/abs/1611.01491) by Arora, et al., 2016,
        /// @see [Delving Deep into Rectifiers: Surpassing Human-Level Performance on ImageNet Classification](https://arxiv.org/abs/1502.01852v1) by He, et al., 2015
        /// </remarks>
        /// <param name="nCount">Specifies the number of items in the bottom and top data.</param>
        /// <param name="hBottomData">Specifies a handle to the bottom data in GPU memory.</param>
        /// <param name="hTopData">Specifies a handle to the top data in GPU memory.</param>
        /// <param name="fNegativeSlope">Specifies the negative slope.</param>
        public void relu_fwd(int nCount, long hBottomData, long hTopData, T fNegativeSlope)
        {
            if (m_dt == DataType.DOUBLE)
                m_cuda.RunDouble((int)m_hKernel, (int)CUDAFN.CUDA_RELU_FWD, new double[] { nCount, hBottomData, hTopData, convertD(fNegativeSlope) });
            else
                m_cuda.RunFloat((int)m_hKernel, (int)CUDAFN.CUDA_RELU_FWD, new float[] { nCount, hBottomData, hTopData, convertF(fNegativeSlope) });
        }

        /// <summary>
        /// Performs a Rectifier Linear Unit (ReLU) backward pass in Cuda.
        /// </summary>
        /// <remarks>
        /// @see [Rectifier](https://en.wikipedia.org/wiki/Rectifier_(neural_networks)), and
        /// @see [Understanding Deep Neural Networks with Rectified Linear Units](https://arxiv.org/abs/1611.01491) by Arora, et al., 2016,
        /// @see [Delving Deep into Rectifiers: Surpassing Human-Level Performance on ImageNet Classification](https://arxiv.org/abs/1502.01852v1) by He, et al., 2015
        /// </remarks>
        /// <param name="nCount">Specifies the number of items.</param>
        /// <param name="hTopDiff">Specifies a handle to the top diff in GPU memory.</param>
        /// <param name="hTopData">Specifies a handle to the top data in GPU memory.</param>
        /// <param name="hBottomDiff">Specifies a handle to the bottom diff in GPU memory.</param>
        /// <param name="fNegativeSlope">Specifies the negative slope.</param>
        public void relu_bwd(int nCount, long hTopDiff, long hTopData, long hBottomDiff, T fNegativeSlope)
        {
            if (m_dt == DataType.DOUBLE)
                m_cuda.RunDouble((int)m_hKernel, (int)CUDAFN.CUDA_RELU_BWD, m_param.AsDouble(nCount, hTopDiff, hTopData, hBottomDiff, convertD(fNegativeSlope)));
            else
                m_cuda.RunFloat((int)m_hKernel, (int)CUDAFN.CUDA_RELU_BWD, m_param.AsFloat(nCount, hTopDiff, hTopData, hBottomDiff, convertF(fNegativeSlope)));
        }

        /// <summary>
        /// Performs a Exponential Linear Unit (ELU) forward pass in Cuda.
        /// </summary>
        /// <remarks>
        /// Calculates @f$ f(x) = (x > 0) ? x : \alpha * (e^x - 1) @f$
        /// 
        /// @see [Deep Residual Networks with Exponential Linear Unit](https://arxiv.org/abs/1604.04112) by Shah, et al., 2016
        /// </remarks>
        /// <param name="nCount">Specifies the number of items in the bottom and top data.</param>
        /// <param name="hBottomData">Specifies a handle to the bottom data in GPU memory.</param>
        /// <param name="hTopData">Specifies a handle to the top data in GPU memory.</param>
        /// <param name="dfAlpha">Specifies the alpha value.</param>
        public void elu_fwd(int nCount, long hBottomData, long hTopData, double dfAlpha)
        {
            if (m_dt == DataType.DOUBLE)
                m_cuda.RunDouble((int)m_hKernel, (int)CUDAFN.CUDA_ELU_FWD, m_param.AsDouble(nCount, hBottomData, hTopData, dfAlpha));
            else
                m_cuda.RunFloat((int)m_hKernel, (int)CUDAFN.CUDA_ELU_FWD, m_param.AsFloat(nCount, hBottomData, hTopData, (float)dfAlpha));
        }

        /// <summary>
        /// Performs a Exponential Linear Unit (ELU) backward pass in Cuda.
        /// </summary>
        /// <remarks>
        /// @see [Deep Residual Networks with Exponential Linear Unit](https://arxiv.org/abs/1604.04112) by Shah, et al., 2016
        /// </remarks>
        /// <param name="nCount">Specifies the number of items.</param>
        /// <param name="hTopDiff">Specifies a handle to the top diff in GPU memory.</param>
        /// <param name="hTopData">Specifies a handle to the top data in GPU memory.</param>
        /// <param name="hBottomData">Specifies a handle to the bottom data in GPU memory.</param>
        /// <param name="hBottomDiff">Specifies a handle to the bottom diff in GPU memory.</param>
        /// <param name="dfAlpha">Specifies the alpha value.</param>
        public void elu_bwd(int nCount, long hTopDiff, long hTopData, long hBottomData, long hBottomDiff, double dfAlpha)
        {
            if (m_dt == DataType.DOUBLE)
                m_cuda.RunDouble((int)m_hKernel, (int)CUDAFN.CUDA_ELU_BWD, m_param.AsDouble(nCount, hTopDiff, hTopData, hBottomData, hBottomDiff, dfAlpha));
            else
                m_cuda.RunFloat((int)m_hKernel, (int)CUDAFN.CUDA_ELU_BWD, m_param.AsFloat(nCount, hTopDiff, hTopData, hBottomData, hBottomDiff, (float)dfAlpha));
        }

        /// <summary>
        /// Performs a dropout forward pass in Cuda.
        /// </summary>
        /// <remarks>
        /// @see [Improving neural networks by preventing co-adaptation of feature detectors](https://arxiv.org/abs/1207.0580) by Hinton, et al., 2012
        /// </remarks>
        /// <param name="nCount">Specifies the number of items.</param>
        /// <param name="hBottomData">Specifies a handle to the bottom data in GPU memory.</param>
        /// <param name="hMask">Specifies a handle to the mask data in GPU memory.</param>
        /// <param name="uiThreshold">Specifies the threshold value: when mask value are less than the threshold, the data item is 'dropped out' by setting the data item to zero.</param>
        /// <param name="fScale">Specifies a scale value applied to each item that is not dropped out.</param>
        /// <param name="hTopData">Specifies a handle to the top data in GPU memory.</param>
        public void dropout_fwd(int nCount, long hBottomData, long hMask, uint uiThreshold, T fScale, long hTopData)
        {
            if (m_dt == DataType.DOUBLE)
                m_cuda.RunDouble((int)m_hKernel, (int)CUDAFN.CUDA_DROPOUT_FWD, m_param.AsDouble(nCount, hBottomData, hMask, uiThreshold, convertD(fScale), hTopData));
            else
                m_cuda.RunFloat((int)m_hKernel, (int)CUDAFN.CUDA_DROPOUT_FWD, m_param.AsFloat(nCount, hBottomData, hMask, uiThreshold, convertF(fScale), hTopData));
        }

        /// <summary>
        /// Performs a dropout backward pass in Cuda.
        /// </summary>
        /// <remarks>
        /// @see [Improving neural networks by preventing co-adaptation of feature detectors](https://arxiv.org/abs/1207.0580) by Hinton, et al., 2012
        /// </remarks>
        /// <param name="nCount">Specifies the number of items.</param>
        /// <param name="hTopDiff">Specifies a handle to the top diff in GPU memory.</param>
        /// <param name="hMask">Specifies a handle to the mask data in GPU memory.</param>
        /// <param name="uiThreshold">Specifies the threshold value: when mask value are less than the threshold, the data item is 'dropped out' by setting the data item to zero.</param>
        /// <param name="fScale">Specifies a scale value applied to each item that is not dropped out.</param>
        /// <param name="hBottomDiff">Specifies a handle to the bottom diff in GPU memory.</param>
        public void dropout_bwd(int nCount, long hTopDiff, long hMask, uint uiThreshold, T fScale, long hBottomDiff)
        {
            if (m_dt == DataType.DOUBLE)
                m_cuda.RunDouble((int)m_hKernel, (int)CUDAFN.CUDA_DROPOUT_BWD, m_param.AsDouble(nCount, hTopDiff, hMask, uiThreshold, convertD(fScale), hBottomDiff));
            else
                m_cuda.RunFloat((int)m_hKernel, (int)CUDAFN.CUDA_DROPOUT_BWD, m_param.AsFloat(nCount, hTopDiff, hMask, uiThreshold, convertF(fScale), hBottomDiff));
        }

        /// <summary>
        /// Performs a binomial normal log liklihod (BNLL) forward pass in Cuda.
        /// </summary>
        /// <remarks>
        /// Computes @f$ f(x) = ln(1 + e^x) @f$
        /// </remarks>
        /// <param name="nCount">Specifies the number of items.</param>
        /// <param name="hBottomData">Specifies a handle to the bottom data in GPU memory.</param>
        /// <param name="hTopData">Specifies a handle to the top data in GPU memory.</param>
        public void bnll_fwd(int nCount, long hBottomData, long hTopData)
        {
            if (m_dt == DataType.DOUBLE)
                m_cuda.RunDouble((int)m_hKernel, (int)CUDAFN.CUDA_BNLL_FWD, m_param.AsDouble(nCount, hBottomData, hTopData));
            else
                m_cuda.RunFloat((int)m_hKernel, (int)CUDAFN.CUDA_BNLL_FWD, m_param.AsFloat(nCount, hBottomData, hTopData));
        }

        /// <summary>
        /// Performs a binomial normal log liklihod (BNLL) backward pass in Cuda.
        /// </summary>
        /// <param name="nCount">Specifies the number of items.</param>
        /// <param name="hTopDiff">Specifies a handle to the top diff in GPU memory.</param>
        /// <param name="hBottomData">Specifies a handle to the bottom data in GPU memory.</param>
        /// <param name="hBottomDiff">Specifies a handle to the bottom diff in GPU memory.</param>
        public void bnll_bwd(int nCount, long hTopDiff, long hBottomData, long hBottomDiff)
        {
            if (m_dt == DataType.DOUBLE)
                m_cuda.RunDouble((int)m_hKernel, (int)CUDAFN.CUDA_BNLL_BWD, m_param.AsDouble(nCount, hTopDiff, hBottomData, hBottomDiff));
            else
                m_cuda.RunFloat((int)m_hKernel, (int)CUDAFN.CUDA_BNLL_BWD, m_param.AsFloat(nCount, hTopDiff, hBottomData, hBottomDiff));
        }

        /// <summary>
        /// Performs Parameterized Rectifier Linear Unit (ReLU) forward pass in Cuda.
        /// </summary>
        /// <remarks>
        /// Calculation @f$ f(x) = (x > 0) ? x : x * slopeData @f$
        /// 
        /// @see [Understanding Deep Neural Networks with Rectified Linear Units](https://arxiv.org/abs/1611.01491) by Arora, et al., 2016,
        /// @see [Delving Deep into Rectifiers: Surpassing Human-Level Performance on ImageNet Classification](https://arxiv.org/abs/1502.01852v1) by He, et al., 2015
        /// </remarks>
        /// <param name="nCount">Specifies the number of items.</param>
        /// <param name="nChannels">Specifies the channels per input.</param>
        /// <param name="nDim">Specifies the dimension of each input.</param>
        /// <param name="hBottomData">Specifies a handle to the bottom data in GPU memory.</param>
        /// <param name="hTopData">Specifies a handle to the top data in GPU memory.</param>
        /// <param name="hSlopeData">Specifies a handle to the slope data in GPU memory.</param>
        /// <param name="nDivFactor">Specifies the div factor applied to the channels.</param>
        public void prelu_fwd(int nCount, int nChannels, int nDim, long hBottomData, long hTopData, long hSlopeData, int nDivFactor)
        {
            if (m_dt == DataType.DOUBLE)
                m_cuda.RunDouble((int)m_hKernel, (int)CUDAFN.CUDA_PRELU_FWD, m_param.AsDouble(nCount, nChannels, nDim, hBottomData, hTopData, hSlopeData, nDivFactor));
            else
                m_cuda.RunFloat((int)m_hKernel, (int)CUDAFN.CUDA_PRELU_FWD, m_param.AsFloat(nCount, nChannels, nDim, hBottomData, hTopData, hSlopeData, nDivFactor));
        }


        /// <summary>
        /// Performs Parameterized Rectifier Linear Unit (ReLU) backward param pass in Cuda.
        /// </summary>
        /// <remarks>
        /// @see [Understanding Deep Neural Networks with Rectified Linear Units](https://arxiv.org/abs/1611.01491) by Arora, et al., 2016,
        /// @see [Delving Deep into Rectifiers: Surpassing Human-Level Performance on ImageNet Classification](https://arxiv.org/abs/1502.01852v1) by He, et al., 2015
        /// </remarks>
        /// <param name="nCDim"><b><i>NEEDS REVIEW</i></b></param>
        /// <param name="nNum"><b><i>NEEDS REVIEW</i></b></param>
        /// <param name="nTopOffset"><b><i>NEEDS REVIEW</i></b></param>
        /// <param name="hTopDiff">Specifies a handle to the top diff in GPU memory.</param>
        /// <param name="hBottomData">Specifies a handle to the bottom data in GPU memory.</param>
        /// <param name="hBackBuffDiff">Specifies a handle to the back buffer diff in GPU memory.</param>
        public void prelu_bwd_param(int nCDim, int nNum, int nTopOffset, long hTopDiff, long hBottomData, long hBackBuffDiff)
        {
            if (m_dt == DataType.DOUBLE)
                m_cuda.RunDouble((int)m_hKernel, (int)CUDAFN.CUDA_PRELU_BWD_PARAM, m_param.AsDouble(nCDim, nNum, nTopOffset, hTopDiff, hBottomData, hBackBuffDiff));
            else
                m_cuda.RunFloat((int)m_hKernel, (int)CUDAFN.CUDA_PRELU_BWD_PARAM, m_param.AsFloat(nCDim, nNum, nTopOffset, hTopDiff, hBottomData, hBackBuffDiff));
        }

        /// <summary>
        /// Performs Parameterized Rectifier Linear Unit (ReLU) backward pass in Cuda.
        /// </summary>
        /// <remarks>
        /// @see [Understanding Deep Neural Networks with Rectified Linear Units](https://arxiv.org/abs/1611.01491) by Arora, et al., 2016,
        /// @see [Delving Deep into Rectifiers: Surpassing Human-Level Performance on ImageNet Classification](https://arxiv.org/abs/1502.01852v1) by He, et al., 2015
        /// </remarks>
        /// <param name="nCount">Specifies the number of items.</param>
        /// <param name="nChannels">Specifies the channels per input.</param>
        /// <param name="nDim">Specifies the dimension of each input.</param>
        /// <param name="hTopDiff">Specifies a handle to the top diff in GPU memory.</param>
        /// <param name="hBottomData">Specifies a handle to the bottom data in GPU memory.</param>
        /// <param name="hBottomDiff">Specifies a handle to the bottom diff in GPU memory.</param>
        /// <param name="hSlopeData">Specifies a handle to the slope data in GPU memory.</param>
        /// <param name="nDivFactor">Specifies the div factor applied to the channels.</param>
        public void prelu_bwd(int nCount, int nChannels, int nDim, long hTopDiff, long hBottomData, long hBottomDiff, long hSlopeData, int nDivFactor)
        {
            if (m_dt == DataType.DOUBLE)
                m_cuda.RunDouble((int)m_hKernel, (int)CUDAFN.CUDA_PRELU_BWD, m_param.AsDouble(nCount, nChannels, nDim, hTopDiff, hBottomData, hBottomDiff, hSlopeData, nDivFactor));
            else
                m_cuda.RunFloat((int)m_hKernel, (int)CUDAFN.CUDA_PRELU_BWD, m_param.AsFloat(nCount, nChannels, nDim, hTopDiff, hBottomData, hBottomDiff, hSlopeData, nDivFactor));
        }

        /// <summary>
        /// Performs Softmax Loss forward pass in Cuda.
        /// </summary>
        /// <param name="nCount">Specifies the number of items.</param>
        /// <param name="hProbData">Specifies a handle to the probability data in GPU memory.</param>
        /// <param name="hLabel">Specifies a handle to the label data in GPU memory.</param>
        /// <param name="hLossData">Specifies a handle to the loss data in GPU memory.</param>
        /// <param name="nOuterNum"><b><i>NEEDS REVIEW</i></b></param>
        /// <param name="nDim"><b><i>NEEDS REVIEW</i></b></param>
        /// <param name="nInnerNum"><b><i>NEEDS REVIEW</i></b></param>
        /// <param name="hCounts">Specifies a handle to the counts in GPU memory.</param>
        /// <param name="nIgnoreLabel">Optionally, specifies a label to ignore.</param>
        public void softmaxloss_fwd(int nCount, long hProbData, long hLabel, long hLossData, int nOuterNum, int nDim, int nInnerNum, long hCounts, int? nIgnoreLabel)
        {
            if (m_dt == DataType.DOUBLE)
            {
                List<double> rg = new List<double>() { nCount, hProbData, hLabel, hLossData, nOuterNum, nDim, nInnerNum, hCounts };

                if (nIgnoreLabel.HasValue)
                    rg.Add(nIgnoreLabel.Value);

                m_cuda.RunDouble((int)m_hKernel, (int)CUDAFN.CUDA_SOFTMAXLOSS_FWD, rg.ToArray());
            }
            else
            {
                List<float> rg = new List<float>() { nCount, hProbData, hLabel, hLossData, nOuterNum, nDim, nInnerNum, hCounts };

                if (nIgnoreLabel.HasValue)
                    rg.Add(nIgnoreLabel.Value);

                m_cuda.RunFloat((int)m_hKernel, (int)CUDAFN.CUDA_SOFTMAXLOSS_FWD, rg.ToArray());
            }
        }

        /// <summary>
        /// Performs Softmax Loss backward pass in Cuda.
        /// </summary>
        /// <param name="nCount">Specifies the number of items.</param>
        /// <param name="hTopData">Specifies a handle to the top data in GPU memory.</param>
        /// <param name="hLabel">Specifies a handle to the label data in GPU memory.</param>
        /// <param name="hBottomDiff">Specifies a handle to the bottom diff in GPU memory.</param>
        /// <param name="nOuterNum"><b><i>NEEDS REVIEW</i></b></param>
        /// <param name="nDim"><b><i>NEEDS REVIEW</i></b></param>
        /// <param name="nInnerNum"><b><i>NEEDS REVIEW</i></b></param>
        /// <param name="hCounts">Specifies a handle to the counts in GPU memory.</param>
        /// <param name="nIgnoreLabel">Optionally, specifies a label to ignore.</param>
        public void softmaxloss_bwd(int nCount, long hTopData, long hLabel, long hBottomDiff, int nOuterNum, int nDim, int nInnerNum, long hCounts, int? nIgnoreLabel)
        {
            if (m_dt == DataType.DOUBLE)
            {
                List<double> rg = new List<double>() { nCount, hTopData, hLabel, hBottomDiff, nOuterNum, nDim, nInnerNum, hCounts };

                if (nIgnoreLabel.HasValue)
                    rg.Add(nIgnoreLabel.Value);

                m_cuda.RunDouble((int)m_hKernel, (int)CUDAFN.CUDA_SOFTMAXLOSS_BWD, rg.ToArray());
            }
            else
            {
                List<float> rg = new List<float>() { nCount, hTopData, hLabel, hBottomDiff, nOuterNum, nDim, nInnerNum, hCounts };

                if (nIgnoreLabel.HasValue)
                    rg.Add(nIgnoreLabel.Value);

                m_cuda.RunFloat((int)m_hKernel, (int)CUDAFN.CUDA_SOFTMAXLOSS_BWD, rg.ToArray());
            }
        }


        /// <summary>
        /// Performs a max forward pass in Cuda.
        /// </summary>
        /// <remarks>
        /// Calculation: @f$ Y[i] = max(A[i], B[i]) @f$
        /// </remarks>
        /// <param name="nCount">Specifies the number of items.</param>
        /// <param name="hBottomDataA">Specifies a handle to the Bottom A data in GPU memory.</param>
        /// <param name="hBottomDataB">Specifies a handle to the Bottom B data in GPU memory.</param>
        /// <param name="nIdx">Specifies the blob index used to set the mask.</param>
        /// <param name="hTopData">Specifies a handle to the Top data in GPU memory.</param>
        /// <param name="hMask">Specifies a handle to the mask data in GPU.</param>
        public void max_fwd(int nCount, long hBottomDataA, long hBottomDataB, int nIdx, long hTopData, long hMask)
        {
            if (m_dt == DataType.DOUBLE)
                m_cuda.RunDouble((int)m_hKernel, (int)CUDAFN.CUDA_MAX_FWD, m_param.AsDouble(nCount, hBottomDataA, hBottomDataB, nIdx, hTopData, hMask));
            else
                m_cuda.RunFloat((int)m_hKernel, (int)CUDAFN.CUDA_MAX_FWD, m_param.AsFloat(nCount, hBottomDataA, hBottomDataB, nIdx, hTopData, hMask));
        }

        /// <summary>
        /// Performs a max backward pass in Cuda.
        /// </summary>
        /// <param name="nCount">Specifies the number of items.</param>
        /// <param name="hTopDiff">Specifies a handle to the top diff in GPU memory.</param>
        /// <param name="nIdx">Specifies the blob index used to test the mask.</param>
        /// <param name="hMask">Specifies a handle to the mask data in GPU.</param>
        /// <param name="hBottomDiff">Specifies a handle to the bottom diff in GPU memory.</param>
        public void max_bwd(int nCount, long hTopDiff, int nIdx, long hMask, long hBottomDiff)
        {
            if (m_dt == DataType.DOUBLE)
                m_cuda.RunDouble((int)m_hKernel, (int)CUDAFN.CUDA_MAX_BWD, m_param.AsDouble(nCount, hTopDiff, nIdx, hMask, hBottomDiff));
            else
                m_cuda.RunFloat((int)m_hKernel, (int)CUDAFN.CUDA_MAX_BWD, m_param.AsFloat(nCount, hTopDiff, nIdx, hMask, hBottomDiff));
        }

        /// <summary>
        /// Performs a min forward pass in Cuda.
        /// </summary>
        /// <remarks>
        /// Calculation: @f$ Y[i] = min(A[i], B[i]) @f$
        /// </remarks>
        /// <param name="nCount">Specifies the number of items.</param>
        /// <param name="hBottomDataA">Specifies a handle to the Bottom A data in GPU memory.</param>
        /// <param name="hBottomDataB">Specifies a handle to the Bottom B data in GPU memory.</param>
        /// <param name="nIdx">Specifies the blob index used to set the mask.</param>
        /// <param name="hTopData">Specifies a handle to the Top data in GPU memory.</param>
        /// <param name="hMask">Specifies a handle to the mask data in GPU.</param>
        public void min_fwd(int nCount, long hBottomDataA, long hBottomDataB, int nIdx, long hTopData, long hMask)
        {
            if (m_dt == DataType.DOUBLE)
                m_cuda.RunDouble((int)m_hKernel, (int)CUDAFN.CUDA_MIN_FWD, m_param.AsDouble(nCount, hBottomDataA, hBottomDataB, nIdx, hTopData, hMask));
            else
                m_cuda.RunFloat((int)m_hKernel, (int)CUDAFN.CUDA_MIN_FWD, m_param.AsFloat(nCount, hBottomDataA, hBottomDataB, nIdx, hTopData, hMask));
        }

        /// <summary>
        /// Performs a min backward pass in Cuda.
        /// </summary>
        /// <param name="nCount">Specifies the number of items.</param>
        /// <param name="hTopDiff">Specifies a handle to the top diff in GPU memory.</param>
        /// <param name="nIdx">Specifies the blob index used to test the mask.</param>
        /// <param name="hMask">Specifies a handle to the mask data in GPU.</param>
        /// <param name="hBottomDiff">Specifies a handle to the bottom diff in GPU memory.</param>
        public void min_bwd(int nCount, long hTopDiff, int nIdx, long hMask, long hBottomDiff)
        {
            if (m_dt == DataType.DOUBLE)
                m_cuda.RunDouble((int)m_hKernel, (int)CUDAFN.CUDA_MIN_BWD, m_param.AsDouble(nCount, hTopDiff, nIdx, hMask, hBottomDiff));
            else
                m_cuda.RunFloat((int)m_hKernel, (int)CUDAFN.CUDA_MIN_BWD, m_param.AsFloat(nCount, hTopDiff, nIdx, hMask, hBottomDiff));
        }

        /// <summary>
        /// Performs the crop forward operation.
        /// </summary>
        /// <param name="nCount">Specifies the count.</param>
        /// <param name="nNumAxes">Specifies the number of axes in the bottom.</param>
        /// <param name="hSrcStrides">Specifies a handle to the GPU memory containing the source strides.</param>
        /// <param name="hDstStrides">Specifies a handle to the GPU memory containing the destination strides.</param>
        /// <param name="hOffsets">Specifies a handle to the GPU memory containing the offsets.</param>
        /// <param name="hBottomData">Specifies a handle to the bottom data in GPU memory.</param>
        /// <param name="hTopData">Specifies a handle to the top data in GPU memory.</param>
        public void crop_fwd(int nCount, int nNumAxes, long hSrcStrides, long hDstStrides, long hOffsets, long hBottomData, long hTopData)
        {
            if (m_dt == DataType.DOUBLE)
                m_cuda.RunDouble((int)m_hKernel, (int)CUDAFN.CUDA_CROP_FWD, m_param.AsDouble(nCount, nNumAxes, hSrcStrides, hDstStrides, hOffsets, hBottomData, hTopData));
            else
                m_cuda.RunFloat((int)m_hKernel, (int)CUDAFN.CUDA_CROP_FWD, m_param.AsFloat(nCount, nNumAxes, hSrcStrides, hDstStrides, hOffsets, hBottomData, hTopData));
        }

        /// <summary>
        /// Performs the crop backward operation.
        /// </summary>
        /// <param name="nCount">Specifies the count.</param>
        /// <param name="nNumAxes">Specifies the number of axes in the bottom.</param>
        /// <param name="hSrcStrides">Specifies a handle to the GPU memory containing the source strides.</param>
        /// <param name="hDstStrides">Specifies a handle to the GPU memory containing the destination strides.</param>
        /// <param name="hOffsets">Specifies a handle to the GPU memory containing the offsets.</param>
        /// <param name="hBottomDiff">Specifies a handle to the bottom data in GPU memory.</param>
        /// <param name="hTopDiff">Specifies a handle to the top data in GPU memory.</param>
        public void crop_bwd(int nCount, int nNumAxes, long hSrcStrides, long hDstStrides, long hOffsets, long hBottomDiff, long hTopDiff)
        {
            if (m_dt == DataType.DOUBLE)
                m_cuda.RunDouble((int)m_hKernel, (int)CUDAFN.CUDA_CROP_BWD, m_param.AsDouble(nCount, nNumAxes, hSrcStrides, hDstStrides, hOffsets, hBottomDiff, hTopDiff));
            else
                m_cuda.RunFloat((int)m_hKernel, (int)CUDAFN.CUDA_CROP_BWD, m_param.AsFloat(nCount, nNumAxes, hSrcStrides, hDstStrides, hOffsets, hBottomDiff, hTopDiff));
        }

        /// <summary>
        /// Performs a concat forward pass in Cuda.
        /// </summary>
        /// <param name="nCount">Specifies the number of items.</param>
        /// <param name="hBottomData">Specifies a handle to the Bottom data in GPU memory.</param>
        /// <param name="nNumConcats">Specifies the number of concatenations.</param>
        /// <param name="nConcatInputSize">Specifies the concatenation input size.</param>
        /// <param name="nTopConcatAxis">Specifies the top axis to concatenate.</param>
        /// <param name="nBottomConcatAxis"><b><i>NEEDS REVIEW</i></b></param>
        /// <param name="nOffsetConcatAxis"><b><i>NEEDS REVIEW</i></b></param>
        /// <param name="hTopData">Specifies a handle to the top data in GPU memory.</param>
        public void concat_fwd(int nCount, long hBottomData, int nNumConcats, int nConcatInputSize, int nTopConcatAxis, int nBottomConcatAxis, int nOffsetConcatAxis, long hTopData)
        {
            if (m_dt == DataType.DOUBLE)
                m_cuda.RunDouble((int)m_hKernel, (int)CUDAFN.CUDA_CONCAT_FWD, m_param.AsDouble(nCount, hBottomData, nNumConcats, nConcatInputSize, nTopConcatAxis, nBottomConcatAxis, nOffsetConcatAxis, hTopData));
            else
                m_cuda.RunFloat((int)m_hKernel, (int)CUDAFN.CUDA_CONCAT_FWD, m_param.AsFloat(nCount, hBottomData, nNumConcats, nConcatInputSize, nTopConcatAxis, nBottomConcatAxis, nOffsetConcatAxis, hTopData));
        }


        /// <summary>
        /// Performs a concat backward pass in Cuda.
        /// </summary>
        /// <param name="nCount">Specifies the number of items.</param>
        /// <param name="hTopDiff">Specifies a handle to the top diff in GPU memory.</param>
        /// <param name="nNumConcats">Specifies the number of concatenations.</param>
        /// <param name="nConcatInputSize">Specifies the concatenation input size.</param>
        /// <param name="nTopConcatAxis"><b><i>NEEDS REVIEW</i></b></param>
        /// <param name="nBottomConcatAxis"><b><i>NEEDS REVIEW</i></b></param>
        /// <param name="nOffsetConcatAxis"><b><i>NEEDS REVIEW</i></b></param>
        /// <param name="hBottomDiff">Specifies a handle to the Bottom diff in GPU memory.</param>
        public void concat_bwd(int nCount, long hTopDiff, int nNumConcats, int nConcatInputSize, int nTopConcatAxis, int nBottomConcatAxis, int nOffsetConcatAxis, long hBottomDiff)
        {
            if (m_dt == DataType.DOUBLE)
                m_cuda.RunDouble((int)m_hKernel, (int)CUDAFN.CUDA_CONCAT_BWD, m_param.AsDouble(nCount, hTopDiff, nNumConcats, nConcatInputSize, nTopConcatAxis, nBottomConcatAxis, nOffsetConcatAxis, hBottomDiff));
            else
                m_cuda.RunFloat((int)m_hKernel, (int)CUDAFN.CUDA_CONCAT_BWD, m_param.AsFloat(nCount, hTopDiff, nNumConcats, nConcatInputSize, nTopConcatAxis, nBottomConcatAxis, nOffsetConcatAxis, hBottomDiff));
        }

        /// <summary>
        /// Performs a slice forward pass in Cuda.
        /// </summary>
        /// <param name="nCount">Specifies the number of items.</param>
        /// <param name="hBottomData">Specifies a handle to the Bottom data in GPU memory.</param>
        /// <param name="nNumSlices">Specifies the number of slices.</param>
        /// <param name="nSliceSize">Specifies the slice size.</param>
        /// <param name="nBottomSliceAxis"><b><i>NEEDS REVIEW</i></b></param>
        /// <param name="nTopSliceAxis"><b><i>NEEDS REVIEW</i></b></param>
        /// <param name="nOffsetSliceAxis"><b><i>NEEDS REVIEW</i></b></param>
        /// <param name="hTopData">Specifies a handle to the top data in GPU memory.</param>
        public void slice_fwd(int nCount, long hBottomData, int nNumSlices, int nSliceSize, int nBottomSliceAxis, int nTopSliceAxis, int nOffsetSliceAxis, long hTopData)
        {
            if (m_dt == DataType.DOUBLE)
                m_cuda.RunDouble((int)m_hKernel, (int)CUDAFN.CUDA_SLICE_FWD, m_param.AsDouble(nCount, hBottomData, nNumSlices, nSliceSize, nBottomSliceAxis, nTopSliceAxis, nOffsetSliceAxis, hTopData));
            else
                m_cuda.RunFloat((int)m_hKernel, (int)CUDAFN.CUDA_SLICE_FWD, m_param.AsFloat(nCount, hBottomData, nNumSlices, nSliceSize, nBottomSliceAxis, nTopSliceAxis, nOffsetSliceAxis, hTopData));
        }

        /// <summary>
        /// Performs a slice backward pass in Cuda.
        /// </summary>
        /// <param name="nCount">Specifies the number of items.</param>
        /// <param name="hTopDiff">Specifies a handle to the top diff in GPU memory.</param>
        /// <param name="nNumSlices">Specifies the number of slices.</param>
        /// <param name="nSliceSize">Specifies the slice size.</param>
        /// <param name="nBottomSliceAxis">Specifies the bottom axis to concatenate.</param>
        /// <param name="nTopSliceAxis"><b><i>NEEDS REVIEW</i></b></param>
        /// <param name="nOffsetSliceAxis"><b><i>NEEDS REVIEW</i></b></param>
        /// <param name="hBottomDiff">Specifies a handle to the Bottom diff in GPU memory.</param>
        public void slice_bwd(int nCount, long hTopDiff, int nNumSlices, int nSliceSize, int nBottomSliceAxis, int nTopSliceAxis, int nOffsetSliceAxis, long hBottomDiff)
        {
            if (m_dt == DataType.DOUBLE)
                m_cuda.RunDouble((int)m_hKernel, (int)CUDAFN.CUDA_SLICE_BWD, m_param.AsDouble(nCount, hTopDiff, nNumSlices, nSliceSize, nBottomSliceAxis, nTopSliceAxis, nOffsetSliceAxis, hBottomDiff));
            else
                m_cuda.RunFloat((int)m_hKernel, (int)CUDAFN.CUDA_SLICE_BWD, m_param.AsFloat(nCount, hTopDiff, nNumSlices, nSliceSize, nBottomSliceAxis, nTopSliceAxis, nOffsetSliceAxis, hBottomDiff));
        }

        /// <summary>
        /// Performs a tile forward pass in Cuda.
        /// </summary>
        /// <param name="nCount">Specifies the number of items.</param>
        /// <param name="hBottomData">Specifies a handle to the Bottom data in GPU memory.</param>
        /// <param name="nInnerDim"><b><i>NEEDS REVIEW</i></b></param>
        /// <param name="nTiles">Specifies the number of tiles.</param>
        /// <param name="nBottomTileAxis"><b><i>NEEDS REVIEW</i></b></param>
        /// <param name="hTopData">Specifies a handle to the top data in GPU memory.</param>
        public void tile_fwd(int nCount, long hBottomData, int nInnerDim, int nTiles, int nBottomTileAxis, long hTopData)
        {
            if (m_dt == DataType.DOUBLE)
                m_cuda.RunDouble((int)m_hKernel, (int)CUDAFN.CUDA_TILE_FWD, m_param.AsDouble(nCount, hBottomData, nInnerDim, nTiles, nBottomTileAxis, hTopData));
            else
                m_cuda.RunFloat((int)m_hKernel, (int)CUDAFN.CUDA_TILE_FWD, m_param.AsFloat(nCount, hBottomData, nInnerDim, nTiles, nBottomTileAxis, hTopData));
        }

        /// <summary>
        /// Performs a tile backward pass in Cuda.
        /// </summary>
        /// <param name="nCount">Specifies the number of items.</param>
        /// <param name="hTopDiff">Specifies a handle to the top diff in GPU memory.</param>
        /// <param name="nTileSize">Specifies the size of each tile.</param>
        /// <param name="nTiles">Specifies the number of tiles.</param>
        /// <param name="nBottomTileAxis"><b><i>NEEDS REVIEW</i></b></param>
        /// <param name="hBottomDiff">Specifies a handle to the Bottom diff in GPU memory.</param>
        public void tile_bwd(int nCount, long hTopDiff, int nTileSize, int nTiles, int nBottomTileAxis, long hBottomDiff)
        {
            if (m_dt == DataType.DOUBLE)
                m_cuda.RunDouble((int)m_hKernel, (int)CUDAFN.CUDA_TILE_BWD, m_param.AsDouble(nCount, hTopDiff, nTileSize, nTiles, nBottomTileAxis, hBottomDiff));
            else
                m_cuda.RunFloat((int)m_hKernel, (int)CUDAFN.CUDA_TILE_BWD, m_param.AsFloat(nCount, hTopDiff, nTileSize, nTiles, nBottomTileAxis, hBottomDiff));
        }

        /// <summary>
        /// Performs a bias forward pass in Cuda.
        /// </summary>
        /// <param name="nCount">Specifies the number of items.</param>
        /// <param name="hBottomData">Specifies a handle to the Bottom data in GPU memory.</param>
        /// <param name="hBiasData">Specifies a handle to the bias data in GPU memory.</param>
        /// <param name="nBiasDim">Specifies the bias dimension.</param>
        /// <param name="nInnerDim"><b><i>NEEDS REVIEW</i></b></param>
        /// <param name="hTopData">Specifies a handle to the top data in GPU memory.</param>
        public void bias_fwd(int nCount, long hBottomData, long hBiasData, int nBiasDim, int nInnerDim, long hTopData)
        {
            if (m_dt == DataType.DOUBLE)
                m_cuda.RunDouble((int)m_hKernel, (int)CUDAFN.CUDA_BIAS_FWD, m_param.AsDouble(nCount, hBottomData, hBiasData, nBiasDim, nInnerDim, hTopData));
            else
                m_cuda.RunFloat((int)m_hKernel, (int)CUDAFN.CUDA_BIAS_FWD, m_param.AsFloat(nCount, hBottomData, hBiasData, nBiasDim, nInnerDim, hTopData));
        }

        /// <summary>
        /// Performs a scale forward pass in Cuda.
        /// </summary>
        /// <remarks>
        /// Calculation: @f$ f(x) = 
        /// \begin{cases}
        ///     x * scaleData[(i / nInnerDim) \mod nScaleDim],                                           & \text{if } hBias == 0\\
        ///     x * scaleData[(i / nInnerDim) \mod nScaleDim] + biasData[(i / nInnerDim) \mod nScaleDim] & \text{otherwise}
        /// \end{cases} @f$
        /// </remarks>
        /// <param name="nCount">Specifies the number of items.</param>
        /// <param name="hX">Specifies the input data X in GPU memory.</param>
        /// <param name="hScaleData"></param>
        /// <param name="nScaleDim"></param>
        /// <param name="nInnerDim"></param>
        /// <param name="hY">Specifies the output data Y in GPU memory.</param>
        /// <param name="hBiasData">Optionally, specifies the bias data in GPU memory.</param>
        public void scale_fwd(int nCount, long hX, long hScaleData, int nScaleDim, int nInnerDim, long hY, long hBiasData = 0)
        {
            if (m_dt == DataType.DOUBLE)
                m_cuda.RunDouble((int)m_hKernel, (int)CUDAFN.CUDA_SCALE_FWD, m_param.AsDouble(nCount, hX, hScaleData, nScaleDim, nInnerDim, hY, hBiasData));
            else
                m_cuda.RunFloat((int)m_hKernel, (int)CUDAFN.CUDA_SCALE_FWD, m_param.AsFloat(nCount, hX, hScaleData, nScaleDim, nInnerDim, hY, hBiasData));
        }

        /// <summary>
        /// Performs a threshold pass in Cuda.
        /// </summary>
        /// <remarks>
        /// Calculation: @f$ Y[i] = (X[i] > threshold) ? 1 : 0 @f$
        /// </remarks>
        /// <param name="nCount">Specifies the number of items.</param>
        /// <param name="dfThreshold">Specifies the threshold value.</param>
        /// <param name="hX">Specifies the input data X in GPU memory.</param>
        /// <param name="hY">Specifies the output data Y in GPU memory.</param>
        public void threshold_fwd(int nCount, double dfThreshold, long hX, long hY)
        {
            if (m_dt == DataType.DOUBLE)
                m_cuda.RunDouble((int)m_hKernel, (int)CUDAFN.CUDA_THRESHOLD_FWD, m_param.AsDouble(nCount, dfThreshold, hX, hY));
            else
                m_cuda.RunFloat((int)m_hKernel, (int)CUDAFN.CUDA_THRESHOLD_FWD, m_param.AsFloat(nCount, (float)dfThreshold, hX, hY));
        }

        /// <summary>
        /// Performs a contrastive loss layer backward pass in Cuda.
        /// </summary>
        /// <remarks>
        /// See [Dimensionality Reduction by Learning an Invariant Mapping](http://yann.lecun.com/exdb/publis/pdf/hadsell-chopra-lecun-06.pdf) by Hadsel, et al., 2006
        /// </remarks>
        /// <param name="nCount">Specifies the number of items.</param>
        /// <param name="nChannels">Specifies the number of channels.</param>
        /// <param name="dfMargin"><b><i>Specifies the margin to use.  The default is 1.0.</i></b></param>
        /// <param name="bLegacyVersion">When <code>false</code> the calculation proposed by Hadsell, et al., 2006 is used where @f$ (margin - d)^2 @f$, 
        /// otherwise the legacy version is used where @f$ (margin - d^2) @f$.  The default is <code>false</code></param>
        /// <param name="dfAlpha"><b><i>NEEDS REVIEW</i></b></param>
        /// <param name="hY">Specifies the Y data in GPU memory used to determine similar pairs.</param>
        /// <param name="hDiff">Specifies the diff in GPU memory.</param>
        /// <param name="hDistSq">Specifies the distance squared data in GPU memory.</param>
        /// <param name="hBottomDiff">Specifies the bottom diff in GPU memory.</param>
        public void cll_bwd(int nCount, int nChannels, double dfMargin, bool bLegacyVersion, double dfAlpha, long hY, long hDiff, long hDistSq, long hBottomDiff)
        {
            if (m_dt == DataType.DOUBLE)
                m_cuda.RunDouble((int)m_hKernel, (int)CUDAFN.CUDA_CLL_BWD, m_param.AsDouble(nCount, nChannels, dfMargin, (bLegacyVersion) ? 1.0 : 0.0, dfAlpha, hY, hDiff, hDistSq, hBottomDiff));
            else
                m_cuda.RunFloat((int)m_hKernel, (int)CUDAFN.CUDA_CLL_BWD, m_param.AsFloat(nCount, nChannels, (float)dfMargin, (bLegacyVersion) ? 1.0f : 0.0f, (float)dfAlpha, hY, hDiff, hDistSq, hBottomDiff));
        }

        /// <summary>
        /// Performs the forward operation for the SmoothL1 loss.
        /// </summary>
        /// <remarks>
        /// Calculation: 
        ///     f(x) = 0.5 * x^2, if |x| lt 1
        ///          = |x| - 0.5, otherwise
        /// </remarks>
        /// <param name="nCount">Specifies the number of items.</param>
        /// <param name="hX">Specifies the input data X in GPU memory.</param>
        /// <param name="hY">Specifies the output data Y in GPU memory.</param>
        public void smoothl1_fwd(int nCount, long hX, long hY)
        {
            if (m_dt == DataType.DOUBLE)
                m_cuda.RunDouble((int)m_hKernel, (int)CUDAFN.CUDA_SMOOTHL1_FWD, m_param.AsDouble(nCount, hX, hY));
            else
                m_cuda.RunFloat((int)m_hKernel, (int)CUDAFN.CUDA_SMOOTHL1_FWD, m_param.AsFloat( nCount, hX, hY));
        }

        /// <summary>
        /// Performs the backward operation for the SmoothL1 loss.
        /// </summary>
        /// <remarks>
        /// Calculation: 
        ///     f'(x) = x, if |x| lt 1
        ///           = sign(x), otherwise
        /// </remarks>
        /// <param name="nCount">Specifies the number of items.</param>
        /// <param name="hX">Specifies the input data X in GPU memory.</param>
        /// <param name="hY">Specifies the output data Y in GPU memory.</param>
        public void smoothl1_bwd(int nCount, long hX, long hY)
        {
            if (m_dt == DataType.DOUBLE)
                m_cuda.RunDouble((int)m_hKernel, (int)CUDAFN.CUDA_SMOOTHL1_BWD, m_param.AsDouble(nCount, hX, hY));
            else
                m_cuda.RunFloat((int)m_hKernel, (int)CUDAFN.CUDA_SMOOTHL1_BWD, m_param.AsFloat( nCount, hX, hY));
        }

        /// <summary>
        /// Performs data permutation on the input and reorders the data which is placed in the output.
        /// </summary>
        /// <param name="nCount">Specifies the number of items.</param>
        /// <param name="hBottom">Specifies the input data.</param>
        /// <param name="bFwd">Specifies whether or not this is a forward (<i>true</i>) or backwards (<i>true</i>) operation.</param>
        /// <param name="hPermuteOrder">Specifies the permuation order values in GPU memory.</param>
        /// <param name="hOldSteps">Specifies the old step values in GPU memory.</param>
        /// <param name="hNewSteps">Specifies the new step values in GPU memory.</param>
        /// <param name="nNumAxes">Specifies the number of axes.</param>
        /// <param name="hTop">Specifies the output data.</param>
        public void permute(int nCount, long hBottom, bool bFwd, long hPermuteOrder, long hOldSteps, long hNewSteps, int nNumAxes, long hTop)
        {
            if (m_dt == DataType.DOUBLE)
                m_cuda.RunDouble((int)m_hKernel, (int)CUDAFN.CUDA_PERMUTE, m_param.AsDouble(nCount, hBottom, (bFwd) ? 1 : 0, hPermuteOrder, hOldSteps, hNewSteps, nNumAxes, hTop));
            else
                m_cuda.RunFloat((int)m_hKernel, (int)CUDAFN.CUDA_PERMUTE, m_param.AsFloat( nCount, hBottom, (bFwd) ? 1 : 0, hPermuteOrder, hOldSteps, hNewSteps, nNumAxes, hTop));
        }

        /// <summary>
        /// Performs a gather forward pass where data at specifies indexes along a given axis are copied to the output data.
        /// </summary>
        /// <param name="nCount">Specifies the number of items.</param>
        /// <param name="hBottom">Specifies the input data.</param>
        /// <param name="hTop">Specifies the output data.</param>
        /// <param name="nAxis">Specifies the axis along which to copy.</param>
        /// <param name="nDim">Specifies the dimension of each item at each index.</param>
        /// <param name="nDimAtAxis">Specifies the dimension at the axis.</param>
        /// <param name="nM">Specifies the M dimension.</param>
        /// <param name="nN">Specifies the M dimension.</param>
        /// <param name="hIdx">Specifies the indexes of the data to gather.</param>
        public void gather_fwd(int nCount, long hBottom, long hTop, int nAxis, int nDim, int nDimAtAxis, int nM, int nN, long hIdx)
        {
            if (m_dt == DataType.DOUBLE)
                m_cuda.RunDouble((int)m_hKernel, (int)CUDAFN.CUDA_GATHER_FWD, m_param.AsDouble(nCount, hBottom, hTop, nAxis, nDim, nDimAtAxis, nM, nN, hIdx));
            else
                m_cuda.RunFloat((int)m_hKernel, (int)CUDAFN.CUDA_GATHER_FWD, m_param.AsFloat( nCount, hBottom, hTop, nAxis, nDim, nDimAtAxis, nM, nN, hIdx));
        }

        /// <summary>
        /// Performs a gather backward pass where data at specifies indexes along a given axis are copied to the output data.
        /// </summary>
        /// <param name="nCount">Specifies the number of items.</param>
        /// <param name="hTop">Specifies the input data.</param>
        /// <param name="hBottom">Specifies the output data.</param>
        /// <param name="nAxis">Specifies the axis along which to copy.</param>
        /// <param name="nDim">Specifies the dimension of each item at each index.</param>
        /// <param name="nDimAtAxis">Specifies the dimension at the axis.</param>
        /// <param name="nM">Specifies the M dimension.</param>
        /// <param name="nN">Specifies the M dimension.</param>
        /// <param name="hIdx">Specifies the indexes of the data to gather.</param>
        public void gather_bwd(int nCount, long hTop, long hBottom, int nAxis, int nDim, int nDimAtAxis, int nM, int nN, long hIdx)
        {
            if (m_dt == DataType.DOUBLE)
                m_cuda.RunDouble((int)m_hKernel, (int)CUDAFN.CUDA_GATHER_BWD, m_param.AsDouble(nCount, hTop, hBottom, nAxis, nDim, nDimAtAxis, nM, nN, hIdx));
            else
                m_cuda.RunFloat((int)m_hKernel, (int)CUDAFN.CUDA_GATHER_BWD, m_param.AsFloat( nCount, hTop, hBottom, nAxis, nDim, nDimAtAxis, nM, nN, hIdx));
        }

        /// <summary>
        /// Performs the fill scale operation used to calculate the LRN cross channel forward pass in Cuda.
        /// </summary>
        /// <param name="nCount">Specifies the number of items.</param>
        /// <param name="hBottomData">Specifies a handle to the Bottom data in GPU memory.</param>
        /// <param name="nNum">Specifies the number of input items.</param>
        /// <param name="nChannels">Specifies the number of channels per input item.</param>
        /// <param name="nHeight">Specifies the height of each input item.</param>
        /// <param name="nWidth">Specifies the width of each input item.</param>
        /// <param name="nSize"><b><i>NEEDS REVIEW</i></b></param>
        /// <param name="fAlphaOverSize">Specifies the alpha value over the size.</param>
        /// <param name="fK">Specifies the k value.</param>
        /// <param name="hScaleData">Specifies a handle to the scale data in GPU memory.</param>
        public void lrn_fillscale(int nCount, long hBottomData, int nNum, int nChannels, int nHeight, int nWidth, int nSize, T fAlphaOverSize, T fK, long hScaleData)
        {
            if (m_dt == DataType.DOUBLE)
                m_cuda.RunDouble((int)m_hKernel, (int)CUDAFN.CUDA_LRN_FILLSCALE, m_param.AsDouble(nCount, hBottomData, nNum, nChannels, nHeight, nWidth, nSize, convertD(fAlphaOverSize), convertD(fK), hScaleData));
            else
                m_cuda.RunFloat((int)m_hKernel, (int)CUDAFN.CUDA_LRN_FILLSCALE, m_param.AsFloat( nCount, hBottomData, nNum, nChannels, nHeight, nWidth, nSize, convertF(fAlphaOverSize), convertF(fK), hScaleData));
        }

        /// <summary>
        /// Computes the output used to calculate the LRN cross channel forward pass in Cuda.
        /// </summary>
        /// <param name="nCount">Specifies the number of items.</param>
        /// <param name="hBottomData">Specifies a handle to the Bottom data in GPU memory.</param>
        /// <param name="hScaleData">Specifies a handle to the scale data in GPU memory.</param>
        /// <param name="fNegativeBeta">Specifies the negative beta value.</param>
        /// <param name="hTopData">Specifies a handle to the top data in GPU memory.</param>
        public void lrn_computeoutput(int nCount, long hBottomData, long hScaleData, T fNegativeBeta, long hTopData)
        {
            if (m_dt == DataType.DOUBLE)
                m_cuda.RunDouble((int)m_hKernel, (int)CUDAFN.CUDA_LRN_COMPUTEOUTPUT, m_param.AsDouble(nCount, hBottomData, hScaleData, convertD(fNegativeBeta), hTopData));
            else
                m_cuda.RunFloat((int)m_hKernel, (int)CUDAFN.CUDA_LRN_COMPUTEOUTPUT, m_param.AsFloat( nCount, hBottomData, hScaleData, convertF(fNegativeBeta), hTopData));
        }


        /// <summary>
        /// Computes the diff used to calculate the LRN cross channel backward pass in Cuda.
        /// </summary>
        /// <param name="nCount">Specifies the number of items.</param>
        /// <param name="hBottomData">Specifies a handle to the Bottom data in GPU memory.</param>
        /// <param name="hTopData">Specifies a handle to the top data in GPU memory.</param>
        /// <param name="hScaleData">Specifies a handle to the scale data in GPU memory.</param>
        /// <param name="hTopDiff">Specifies a handle to the top diff in GPU memory.</param>
        /// <param name="nNum">Specifies the number of input items.</param>
        /// <param name="nChannels">Specifies the number of channels per input item.</param>
        /// <param name="nHeight">Specifies the height of each input item.</param>
        /// <param name="nWidth">Specifies the width of each input item.</param>
        /// <param name="nSize"><b><i>NEEDS REVIEW</i></b></param>
        /// <param name="fNegativeBeta">Specifies the negative beta value.</param>
        /// <param name="fCacheRatio"><b><i>NEEDS REVIEW</i></b></param>
        /// <param name="hBottomDiff">Specifies a handle to the bottom diff in GPU memory.</param>
        public void lrn_computediff(int nCount, long hBottomData, long hTopData, long hScaleData, long hTopDiff, int nNum, int nChannels, int nHeight, int nWidth, int nSize, T fNegativeBeta, T fCacheRatio, long hBottomDiff)
        {
            if (m_dt == DataType.DOUBLE)
                m_cuda.RunDouble((int)m_hKernel, (int)CUDAFN.CUDA_LRN_COMPUTEDIFF, m_param.AsDouble(nCount, hBottomData, hTopData, hScaleData, hTopDiff, nNum, nChannels, nHeight, nWidth, nSize, convertD(fNegativeBeta), convertD(fCacheRatio), hBottomDiff));
            else
                m_cuda.RunFloat((int)m_hKernel, (int)CUDAFN.CUDA_LRN_COMPUTEDIFF, m_param.AsFloat( nCount, hBottomData, hTopData, hScaleData, hTopDiff, nNum, nChannels, nHeight, nWidth, nSize, convertF(fNegativeBeta), convertF(fCacheRatio), hBottomDiff));
        }

        /// <summary>
        /// Perform the Stochastic Gradient Descent (SGD) update
        /// </summary>
        /// <remarks>
        /// See [Stochastic Gradient Descent](https://en.wikipedia.org/wiki/Stochastic_gradient_descent).
        /// </remarks>
        /// <param name="nCount">Specifies the number of items.</param>
        /// <param name="hNetParamsDiff">Specifies a handle to the net params diff in GPU memory.</param>
        /// <param name="hHistoryData">Specifies a handle to the history data in GPU memory.</param>
        /// <param name="fMomentum">Specifies the momentum value.</param>
        /// <param name="fLocalRate">Specifies the local learning rate.</param>
        public void sgd_update(int nCount, long hNetParamsDiff, long hHistoryData, T fMomentum, T fLocalRate)
        {
            if (m_dt == DataType.DOUBLE)
                m_cuda.RunDouble((int)m_hKernel, (int)CUDAFN.CUDA_SGD_UPDATE, m_param.AsDouble(nCount, hNetParamsDiff, hHistoryData, convertD(fMomentum), convertD(fLocalRate)));
            else
                m_cuda.RunFloat((int)m_hKernel, (int)CUDAFN.CUDA_SGD_UPDATE, m_param.AsFloat( nCount, hNetParamsDiff, hHistoryData, convertF(fMomentum), convertF(fLocalRate)));
        }

        /// <summary>
        /// Perform the Nesterov update
        /// </summary>
        /// <remarks>
        /// See [Lecture 6c The momentum method](http://www.cs.toronto.edu/~tijmen/csc321/slides/lecture_slides_lec6.pdf) by Hinton, et al., 2012,
        /// and [Nesterov's Accelerated Gradient and Momentum as approximations to Regularised Update Descent](https://arxiv.org/abs/1607.01981) by Botev, et al., 2016
        /// </remarks>
        /// <param name="nCount">Specifies the number of items.</param>
        /// <param name="hNetParamsDiff">Specifies a handle to the net params diff in GPU memory.</param>
        /// <param name="hHistoryData">Specifies a handle to the history data in GPU memory.</param>
        /// <param name="fMomentum">Specifies the momentum value.</param>
        /// <param name="fLocalRate">Specifies the local learning rate.</param>
        public void nesterov_update(int nCount, long hNetParamsDiff, long hHistoryData, T fMomentum, T fLocalRate)
        {
            if (m_dt == DataType.DOUBLE)
                m_cuda.RunDouble((int)m_hKernel, (int)CUDAFN.CUDA_NESTEROV_UPDATE, m_param.AsDouble(nCount, hNetParamsDiff, hHistoryData, convertD(fMomentum), convertD(fLocalRate)));
            else
                m_cuda.RunFloat((int)m_hKernel, (int)CUDAFN.CUDA_NESTEROV_UPDATE, m_param.AsFloat( nCount, hNetParamsDiff, hHistoryData, convertF(fMomentum), convertF(fLocalRate)));
        }


        /// <summary>
        /// Perform the AdaGrad update
        /// </summary>
        /// <remarks>
        /// See [Adaptive Subgradient Methods for Online Learning and Stochastic Optimization](http://www.jmlr.org/papers/volume12/duchi11a/duchi11a.pdf) by Duchi, et al., 2011
        /// </remarks>
        /// <param name="nCount">Specifies the number of items.</param>
        /// <param name="hNetParamsDiff">Specifies a handle to the net params diff in GPU memory.</param>
        /// <param name="hHistoryData">Specifies a handle to the history data in GPU memory.</param>
        /// <param name="fDelta">Specifies the numerical stability factor.</param>
        /// <param name="fLocalRate">Specifies the local learning rate.</param>
        public void adagrad_update(int nCount, long hNetParamsDiff, long hHistoryData, T fDelta, T fLocalRate)
        {
            if (m_dt == DataType.DOUBLE)
                m_cuda.RunDouble((int)m_hKernel, (int)CUDAFN.CUDA_ADAGRAD_UPDATE, m_param.AsDouble(nCount, hNetParamsDiff, hHistoryData, convertD(fDelta), convertD(fLocalRate)));
            else
                m_cuda.RunFloat((int)m_hKernel, (int)CUDAFN.CUDA_ADAGRAD_UPDATE, m_param.AsFloat( nCount, hNetParamsDiff, hHistoryData, convertF(fDelta), convertF(fLocalRate)));
        }

        /// <summary>
        /// Perform the AdaDelta update
        /// </summary>
        /// <remarks>
        /// See [ADADELTA: An Adaptive Learning Rate Method](https://arxiv.org/abs/1212.5701) by Zeiler, 2012
        /// </remarks>
        /// <param name="nCount">Specifies the number of items.</param>
        /// <param name="hNetParamsDiff">Specifies a handle to the net params diff in GPU memory.</param>
        /// <param name="hHistoryData1">Specifies a handle to history data in GPU memory.</param>
        /// <param name="hHistoryData2">Specifies a handle to history data in GPU memory.</param>
        /// <param name="fMomentum">Specifies the momentum to use.</param>
        /// <param name="fDelta">Specifies the numerical stability factor.</param>
        /// <param name="fLocalRate">Specifies the local learning rate.</param>
        public void adadelta_update(int nCount, long hNetParamsDiff, long hHistoryData1, long hHistoryData2, T fMomentum, T fDelta, T fLocalRate)
        {
            if (m_dt == DataType.DOUBLE)
                m_cuda.RunDouble((int)m_hKernel, (int)CUDAFN.CUDA_ADADELTA_UPDATE, m_param.AsDouble(nCount, hNetParamsDiff, hHistoryData1, hHistoryData2, convertD(fMomentum), convertD(fDelta), convertD(fLocalRate)));
            else
                m_cuda.RunFloat((int)m_hKernel, (int)CUDAFN.CUDA_ADADELTA_UPDATE, m_param.AsFloat( nCount, hNetParamsDiff, hHistoryData1, hHistoryData2, convertF(fMomentum), convertF(fDelta), convertF(fLocalRate)));
        }

        /// <summary>
        /// Perform the Adam update
        /// </summary>
        /// <remarks>
        /// See [Adam: A Method for Stochastic Optimization](https://arxiv.org/abs/1412.6980v9) by Kingma, et al., 2014
        /// </remarks>
        /// <param name="nCount">Specifies the number of items.</param>
        /// <param name="hNetParamsDiff">Specifies a handle to the net params diff in GPU memory.</param>
        /// <param name="hValM"><b><i>NEEDS REVIEW</i></b></param>
        /// <param name="hValV"><b><i>NEEDS REVIEW</i></b></param>
        /// <param name="fBeta1"><b><i>NEEDS REVIEW</i></b></param>
        /// <param name="fBeta2"><b><i>NEEDS REVIEW</i></b></param>
        /// <param name="fEpsHat"><b><i>NEEDS REVIEW</i></b></param>
        /// <param name="fCorrectedLocalRate"><b><i>NEEDS REVIEW</i></b></param>
        public void adam_update(int nCount, long hNetParamsDiff, long hValM, long hValV, T fBeta1, T fBeta2, T fEpsHat, T fCorrectedLocalRate)
        {
            if (m_dt == DataType.DOUBLE)
                m_cuda.RunDouble((int)m_hKernel, (int)CUDAFN.CUDA_ADAM_UPDATE, m_param.AsDouble(nCount, hNetParamsDiff, hValM, hValV, convertD(fBeta1), convertD(fBeta2), convertD(fEpsHat), convertD(fCorrectedLocalRate)));
            else
                m_cuda.RunFloat((int)m_hKernel, (int)CUDAFN.CUDA_ADAM_UPDATE, m_param.AsFloat( nCount, hNetParamsDiff, hValM, hValV, convertF(fBeta1), convertF(fBeta2), convertF(fEpsHat), convertF(fCorrectedLocalRate)));
        }

        /// <summary>
        /// Perform the RMSProp update
        /// </summary>
        /// <remarks>
        /// See [Lecture 6e	rmsprop: Divide the gradient by a running average of its recent magnitude](http://www.cs.toronto.edu/~tijmen/csc321/slides/lecture_slides_lec6.pdf) by Tieleman and Hinton, 2012,
        /// and [RMSProp and equilibrated adaptive learning rates for non-convex optimization](https://arxiv.org/abs/1502.04390v1) by Dauphin, et al., 2015
        /// </remarks>
        /// <param name="nCount">Specifies the number of items.</param>
        /// <param name="hNetParamsDiff">Specifies a handle to the net params diff in GPU memory.</param>
        /// <param name="hHistoryData">Specifies a handle to the history data in GPU memory.</param>
        /// <param name="fRmsDecay">Specifies the decay value used by the Solver.  MeanSquare(t) = 'rms_decay' * MeanSquare(t-1) + (1 - 'rms_decay') * SquareGradient(t).</param>
        /// <param name="fDelta">Specifies the numerical stability factor.</param>
        /// <param name="fLocalRate">Specifies the local learning rate.</param>
        public void rmsprop_update(int nCount, long hNetParamsDiff, long hHistoryData, T fRmsDecay, T fDelta, T fLocalRate)
        {
            if (m_dt == DataType.DOUBLE)
                m_cuda.RunDouble((int)m_hKernel, (int)CUDAFN.CUDA_RMSPROP_UPDATE, m_param.AsDouble(nCount, hNetParamsDiff, hHistoryData, convertD(fRmsDecay), convertD(fDelta), convertD(fLocalRate)));
            else
                m_cuda.RunFloat((int)m_hKernel, (int)CUDAFN.CUDA_RMSPROP_UPDATE, m_param.AsFloat( nCount, hNetParamsDiff, hHistoryData, convertF(fRmsDecay), convertF(fDelta), convertF(fLocalRate)));
        }

        /// <summary>
        /// Peforms the simple LSTM foward pass in Cuda.
        /// </summary>
        /// <remarks>
        /// See [LSTM with Working Memory](https://arxiv.org/abs/1605.01988) by Pulver, et al., 2016
        /// </remarks>
        /// <param name="t">Specifies the step within the sequence.</param>
        /// <param name="nN">Specifies the batch size.</param>
        /// <param name="nH">Specifies the number of hidden units.</param>
        /// <param name="nI">Specifies the number the input size.</param>
        /// <param name="hWeight_h">Specifies a handle to the GPU memory holding the 'h' weights.</param>
        /// <param name="hWeight_i">Specifies a handle to the GPU memory holding the 'i' weights.</param>
        /// <param name="hClipData">Specifies a handle to the GPU memory holding the clip data.</param>
        /// <param name="nClipOffset">Specifies the clip offset for this step within the sequence.</param>
        /// <param name="hTopData">Specifies a handle to the top data in GPU memory.</param>
        /// <param name="nTopOffset">Specifies an offset into the top data memory.</param>
        /// <param name="hCellData">Specifies a handle to the GPU memory holding the 'c_t' data.</param>
        /// <param name="nCellOffset">Specifies the c_t offset for this step within the sequence.</param>
        /// <param name="hPreGateData">Specifies a handle to the GPU memory holding the pre-gate data.</param>
        /// <param name="nPreGateOffset">Specifies the pre-gate offset for this step within the sequence.</param>
        /// <param name="hGateData">Specifies a handle to the GPU memory holding the gate data.</param>
        /// <param name="nGateOffset">Specifies the gate data offset for this step within the sequence.</param>
        /// <param name="hHT1Data">Specifies a handle to the GPU memory holding the HT1 data.</param>
        /// <param name="nHT1Offset">Specifies the HT1 offset for this step within the sequence.</param>
        /// <param name="hCT1Data">Specifies a handle to the GPU memory holding the CT1 data.</param>
        /// <param name="nCT1Offset">Specifies the CT1 offset for this step within the sequence.</param>
        /// <param name="hHtoGateData">Specifies a handle to the GPU memory holding the H to Gate data.</param>
        /// <param name="hContext">Optionally, specifies the attention context, or 0 when not used.</param>
        /// <param name="hWeight_c">Optionally, specifies the attention context weights, or 0 when not used.</param>
        /// <param name="hCtoGetData">Optionally, specifies the attention context to gate data, or 0 when not used.</param>
        public void lstm_fwd(int t, int nN, int nH, int nI, long hWeight_h, long hWeight_i, long hClipData, int nClipOffset, long hTopData, int nTopOffset, long hCellData, int nCellOffset, long hPreGateData, int nPreGateOffset, long hGateData, int nGateOffset, long hHT1Data, int nHT1Offset, long hCT1Data, int nCT1Offset, long hHtoGateData, long hContext = 0, long hWeight_c = 0, long hCtoGetData = 0)
        {
            if (m_dt == DataType.DOUBLE)
                m_cuda.RunDouble((int)m_hKernel, (int)CUDAFN.CUDA_LSTM_FWD, m_param.AsDouble(t, nN, nH, nI, hWeight_h, hWeight_i, hClipData, nClipOffset, hTopData, nTopOffset, hCellData, nCellOffset, hPreGateData, nPreGateOffset, hGateData, nGateOffset, hHT1Data, nHT1Offset, hCT1Data, nCT1Offset, hHtoGateData, hContext, hWeight_c, hCtoGetData));
            else
                m_cuda.RunFloat((int)m_hKernel, (int)CUDAFN.CUDA_LSTM_FWD, m_param.AsFloat( t, nN, nH, nI, hWeight_h, hWeight_i, hClipData, nClipOffset, hTopData, nTopOffset, hCellData, nCellOffset, hPreGateData, nPreGateOffset, hGateData, nGateOffset, hHT1Data, nHT1Offset, hCT1Data, nCT1Offset, hHtoGateData, hContext, hWeight_c, hCtoGetData));
        }

        /// <summary>
        /// Peforms the simple LSTM backward pass in Cuda.
        /// </summary>
        /// <remarks>
        /// See [LSTM with Working Memory](https://arxiv.org/abs/1605.01988) by Pulver, et al., 2016
        /// </remarks>
        /// <param name="t">Specifies the step within the sequence.</param>
        /// <param name="nN">Specifies the batch size.</param>
        /// <param name="nH">Specifies the number of hidden units.</param>
        /// <param name="nI">Specifies the number the input size.</param>
        /// <param name="dfClippingThreshold"></param>
        /// <param name="hWeight_h">Specifies a handle to the GPU memory holding the 'h' weights.</param>
        /// <param name="hClipData">Specifies a handle to the GPU memory holding the clip data.</param>
        /// <param name="nClipOffset">Specifies the clip offset for this step within the sequence.</param>
        /// <param name="hTopDiff">Specifies a handle to the top diff in GPU memory.</param>
        /// <param name="nTopOffset">Specifies an offset into the top diff memory.</param>
        /// <param name="hCellData">Specifies a handle to the GPU memory holding the 'c_t' data.</param>
        /// <param name="hCellDiff">Specifies a handle to the GPU memory holding the 'c_t' gradients.</param>
        /// <param name="nCellOffset">Specifies the c_t offset for this step within the sequence.</param>
        /// <param name="hPreGateDiff">Specifies a handle to the GPU memory holding the pre-gate gradients.</param>
        /// <param name="nPreGateOffset">Specifies the pre-gate offset for this step within the sequence.</param>
        /// <param name="hGateData">Specifies a handle to the GPU memory holding the gate data.</param>
        /// <param name="hGateDiff">Specifies a handle to the GPU memory holding the gate gradients.</param>
        /// <param name="nGateOffset">Specifies the gate data offset for this step within the sequence.</param>
        /// <param name="hCT1Data">Specifies a handle to the GPU memory holding the CT1 data.</param>
        /// <param name="nCT1Offset">Specifies the CT1 offset for this step within the sequence.</param>
        /// <param name="hDHT1Diff">Specifies a handle to the GPU DHT1 gradients.</param>
        /// <param name="nDHT1Offset">Specifies the DHT1 offset for this step within the sequence.</param>
        /// <param name="hDCT1Diff">Specifies a handle to the DCT1 gradients.</param>
        /// <param name="nDCT1Offset">Specifies the DCT1 offset for this step within the sequence.</param>
        /// <param name="hHtoHData">Specifies a handle to the GPU memory holding the H to H data.</param>
        /// <param name="hContextDiff">Optionally, specifies the handle to the GPU memory holding the context diff, or 0 when not used.</param>
        /// <param name="hWeight_c">Optionally, specifies the handle to the GPU memory holding the 'c' weights, or 0 when not used.</param>
        public void lstm_bwd(int t, int nN, int nH, int nI, double dfClippingThreshold, long hWeight_h, long hClipData, int nClipOffset, long hTopDiff, int nTopOffset, long hCellData, long hCellDiff, int nCellOffset, long hPreGateDiff, int nPreGateOffset, long hGateData, long hGateDiff, int nGateOffset, long hCT1Data, int nCT1Offset, long hDHT1Diff, int nDHT1Offset, long hDCT1Diff, int nDCT1Offset, long hHtoHData, long hContextDiff = 0, long hWeight_c = 0)
        {
            if (m_dt == DataType.DOUBLE)
                m_cuda.RunDouble((int)m_hKernel, (int)CUDAFN.CUDA_LSTM_BWD, m_param.AsDouble(t, nN, nH, nI, dfClippingThreshold, hWeight_h, hClipData, nClipOffset, hTopDiff, nTopOffset, hCellData, hCellDiff, nCellOffset, hPreGateDiff, nPreGateOffset, hGateData, hGateDiff, nGateOffset, hCT1Data, nCT1Offset, hDHT1Diff, nDHT1Offset, hDCT1Diff, nDCT1Offset, hHtoHData, hContextDiff, hWeight_c));
            else
                m_cuda.RunFloat((int)m_hKernel, (int)CUDAFN.CUDA_LSTM_BWD, m_param.AsFloat( t, nN, nH, nI, (float)dfClippingThreshold, hWeight_h, hClipData, nClipOffset, hTopDiff, nTopOffset, hCellData, hCellDiff, nCellOffset, hPreGateDiff, nPreGateOffset, hGateData, hGateDiff, nGateOffset, hCT1Data, nCT1Offset, hDHT1Diff, nDHT1Offset, hDCT1Diff, nDCT1Offset, hHtoHData, hContextDiff, hWeight_c));
        }

        /// <summary>
        /// Peforms the simple LSTM foward pass in Cuda for a given LSTM unit.
        /// </summary>
        /// <remarks>
        /// See [LSTM with Working Memory](https://arxiv.org/abs/1605.01988) by Pulver, et al., 2016
        /// </remarks>
        /// <param name="nCount"><b><i>NEEDS REVIEW</i></b></param>
        /// <param name="nHiddenDim"><b><i>NEEDS REVIEW</i></b></param>
        /// <param name="nXCount"><b><i>NEEDS REVIEW</i></b></param>
        /// <param name="hX"><b><i>NEEDS REVIEW</i></b></param>
        /// <param name="hX_acts"><b><i>NEEDS REVIEW</i></b></param>
        /// <param name="hC_prev"><b><i>NEEDS REVIEW</i></b></param>
        /// <param name="hCont"><b><i>NEEDS REVIEW</i></b></param>
        /// <param name="hC"><b><i>NEEDS REVIEW</i></b></param>
        /// <param name="hH"><b><i>NEEDS REVIEW</i></b></param>
        public void lstm_unit_fwd(int nCount, int nHiddenDim, int nXCount, long hX, long hX_acts, long hC_prev, long hCont, long hC, long hH)
        {
            if (m_dt == DataType.DOUBLE)
                m_cuda.RunDouble((int)m_hKernel, (int)CUDAFN.CUDA_LSTM_UNIT_FWD, m_param.AsDouble(nCount, nHiddenDim, nXCount, hX, hX_acts, hC_prev, hCont, hC, hH));
            else
                m_cuda.RunFloat((int)m_hKernel, (int)CUDAFN.CUDA_LSTM_UNIT_FWD, m_param.AsFloat( nCount, nHiddenDim, nXCount, hX, hX_acts, hC_prev, hCont, hC, hH));
        }

        /// <summary>
        /// Peforms the simple LSTM backward pass in Cuda for a given LSTM unit.
        /// </summary>
        /// <remarks>
        /// See [LSTM with Working Memory](https://arxiv.org/abs/1605.01988) by Pulver, et al., 2016
        /// </remarks>
        /// <param name="nCount"><b><i>NEEDS REVIEW</i></b></param>
        /// <param name="nHiddenDim"><b><i>NEEDS REVIEW</i></b></param>
        /// <param name="nXCount"><b><i>NEEDS REVIEW</i></b></param>
        /// <param name="hC_prev"><b><i>NEEDS REVIEW</i></b></param>
        /// <param name="hX_acts"><b><i>NEEDS REVIEW</i></b></param>
        /// <param name="hC"><b><i>NEEDS REVIEW</i></b></param>
        /// <param name="hH"><b><i>NEEDS REVIEW</i></b></param>
        /// <param name="hCont"><b><i>NEEDS REVIEW</i></b></param>
        /// <param name="hC_diff"><b><i>NEEDS REVIEW</i></b></param>
        /// <param name="hH_diff"><b><i>NEEDS REVIEW</i></b></param>
        /// <param name="hC_prev_diff"><b><i>NEEDS REVIEW</i></b></param>
        /// <param name="hX_acts_diff"><b><i>NEEDS REVIEW</i></b></param>
        /// <param name="hX_diff"><b><i>NEEDS REVIEW</i></b></param>
        public void lstm_unit_bwd(int nCount, int nHiddenDim, int nXCount, long hC_prev, long hX_acts, long hC, long hH, long hCont, long hC_diff, long hH_diff, long hC_prev_diff, long hX_acts_diff, long hX_diff)
        {
            if (m_dt == DataType.DOUBLE)
                m_cuda.RunDouble((int)m_hKernel, (int)CUDAFN.CUDA_LSTM_UNIT_BWD, m_param.AsDouble(nCount, nHiddenDim, nXCount, hC_prev, hX_acts, hC, hH, hCont, hC_diff, hH_diff, hC_prev_diff, hX_acts_diff, hX_diff));
            else
                m_cuda.RunFloat((int)m_hKernel, (int)CUDAFN.CUDA_LSTM_UNIT_BWD, m_param.AsFloat( nCount, nHiddenDim, nXCount, hC_prev, hX_acts, hC, hH, hCont, hC_diff, hH_diff, hC_prev_diff, hX_acts_diff, hX_diff));
        }

        /// <summary>
        /// Performs a coefficient sum foward pass in Cuda.
        /// </summary>
        /// <param name="nCount">Specifies the number of items.</param>
        /// <param name="nDim"><b><i>Specifies the dimension of the data where the data is sized 'num' x 'dim'.</i></b></param>
        /// <param name="nNumOffset">Specifies the offset applied to the coefficent indexing.</param>
        /// <param name="dfCoeff">Specifies a primary coefficient value applied to each input before summing.</param>
        /// <param name="hCoeffData">Optionally specifies a handle to coefficient data that is applied to the primary coefficient.</param>
        /// <param name="hBottom">Specifies a handle to the bottom data in GPU memory.</param>
        /// <param name="hTop">Specifies a handle to the top data in GPU memory.</param>
        public void coeff_sum_fwd(int nCount, int nDim, int nNumOffset, double dfCoeff, long hCoeffData, long hBottom, long hTop)
        {
            if (m_dt == DataType.DOUBLE)
                m_cuda.RunDouble((int)m_hKernel, (int)CUDAFN.CUDA_COEFF_SUM_FWD, m_param.AsDouble(nCount, nDim, nNumOffset, dfCoeff, hCoeffData, hBottom, hTop));
            else
                m_cuda.RunFloat((int)m_hKernel, (int)CUDAFN.CUDA_COEFF_SUM_FWD, m_param.AsFloat( nCount, nDim, nNumOffset, (float)dfCoeff, hCoeffData, hBottom, hTop));
        }


        /// <summary>
        /// Performs a coefficient sum backward pass in Cuda.
        /// </summary>
        /// <param name="nCount">Specifies the number of items.</param>
        /// <param name="nDim"><b><i>Specifies the dimension of the data where the data is sized 'num' x 'dim'.</i></b></param>
        /// <param name="nNumOffset">Specifies the offset applied to the coefficent indexing.</param>
        /// <param name="dfCoeff">Specifies a primary coefficient value applied to each input before summing.</param>
        /// <param name="hCoeffData">Optionally specifies a handle to coefficient data that is applied to the primary coefficient.</param>
        /// <param name="hTopDiff">Specifies a handle to the top diff in GPU memory.</param>
        /// <param name="hBottomDiff">Specifies a handle to the bottom diff in GPU memory.</param>
        public void coeff_sum_bwd(int nCount, int nDim, int nNumOffset, double dfCoeff, long hCoeffData, long hTopDiff, long hBottomDiff)
        {
            if (m_dt == DataType.DOUBLE)
                m_cuda.RunDouble((int)m_hKernel, (int)CUDAFN.CUDA_COEFF_SUM_BWD, m_param.AsDouble(nCount, nDim, nNumOffset, dfCoeff, hCoeffData, hTopDiff, hBottomDiff));
            else
                m_cuda.RunFloat((int)m_hKernel, (int)CUDAFN.CUDA_COEFF_SUM_BWD, m_param.AsFloat( nCount, nDim, nNumOffset, (float)dfCoeff, hCoeffData, hTopDiff, hBottomDiff));
        }

        /// <summary>
        /// Performs a coefficient sub foward pass in Cuda.
        /// </summary>
        /// <param name="nCount">Specifies the number of items.</param>
        /// <param name="nDim"><b><i>Specifies the dimension of the data where the data is sized 'num' x 'dim'.</i></b></param>
        /// <param name="nNumOffset">Specifies the offset applied to the coefficent indexing.</param>
        /// <param name="dfCoeff">Specifies a primary coefficient value applied to each input before summing.</param>
        /// <param name="hCoeffData">Optionally specifies a handle to coefficient data that is applied to the primary coefficient.</param>
        /// <param name="hBottom">Specifies a handle to the bottom data in GPU memory.</param>
        /// <param name="hTop">Specifies a handle to the top data in GPU memory.</param>
        public void coeff_sub_fwd(int nCount, int nDim, int nNumOffset, double dfCoeff, long hCoeffData, long hBottom, long hTop)
        {
            if (m_dt == DataType.DOUBLE)
                m_cuda.RunDouble((int)m_hKernel, (int)CUDAFN.CUDA_COEFF_SUB_FWD, m_param.AsDouble( nCount, nDim, nNumOffset, dfCoeff, hCoeffData, hBottom, hTop));
            else
                m_cuda.RunFloat((int)m_hKernel, (int)CUDAFN.CUDA_COEFF_SUB_FWD, m_param.AsFloat( nCount, nDim, nNumOffset, (float)dfCoeff, hCoeffData, hBottom, hTop));
        }


        /// <summary>
        /// Performs a coefficient sub backward pass in Cuda.
        /// </summary>
        /// <param name="nCount">Specifies the number of items.</param>
        /// <param name="nDim"><b><i>Specifies the dimension of the data where the data is sized 'num' x 'dim'.</i></b></param>
        /// <param name="nNumOffset">Specifies the offset applied to the coefficent indexing.</param>
        /// <param name="dfCoeff">Specifies a primary coefficient value applied to each input before summing.</param>
        /// <param name="hCoeffData">Optionally specifies a handle to coefficient data that is applied to the primary coefficient.</param>
        /// <param name="hTopDiff">Specifies a handle to the top diff in GPU memory.</param>
        /// <param name="hBottomDiff">Specifies a handle to the bottom diff in GPU memory.</param>
        public void coeff_sub_bwd(int nCount, int nDim, int nNumOffset, double dfCoeff, long hCoeffData, long hTopDiff, long hBottomDiff)
        {
            if (m_dt == DataType.DOUBLE)
                m_cuda.RunDouble((int)m_hKernel, (int)CUDAFN.CUDA_COEFF_SUB_BWD, m_param.AsDouble( nCount, nDim, nNumOffset, dfCoeff, hCoeffData, hTopDiff, hBottomDiff));
            else
                m_cuda.RunFloat((int)m_hKernel, (int)CUDAFN.CUDA_COEFF_SUB_BWD, m_param.AsFloat( nCount, nDim, nNumOffset, (float)dfCoeff, hCoeffData, hTopDiff, hBottomDiff));
        }


        /// <summary>
        /// Performs a sigmoid cross entropy forward pass in Cuda.
        /// </summary>
        /// <param name="nCount">Specifies the number of items.</param>
        /// <param name="hInput">Specifies a handle to the input data in GPU memory.</param>
        /// <param name="hTarget">Specifies a handle to the target data in GPU memory.</param>
        /// <param name="hLoss">Specifies a handle to the loss data in GPU memory.</param>
        /// <param name="bHasIgnoreLabel">Specifies whether or not an ignore label is used.</param>
        /// <param name="nIgnoreLabel">Specifies the ignore label which is used when <i>bHasIgnoreLabel</i> is <code>true</code></param>
        /// <param name="hCountData">Specifies a handle to the count data in GPU memory.</param>
        public void cross_entropy_fwd(int nCount, long hInput, long hTarget, long hLoss, bool bHasIgnoreLabel, int nIgnoreLabel, long hCountData)
        {
            if (m_dt == DataType.DOUBLE)
                m_cuda.RunDouble((int)m_hKernel, (int)CUDAFN.CUDA_CROSS_ENTROPY_FWD, m_param.AsDouble( nCount, hInput, hTarget, hLoss, (bHasIgnoreLabel) ? 1 : 0, nIgnoreLabel, hCountData));
            else
                m_cuda.RunFloat((int)m_hKernel, (int)CUDAFN.CUDA_CROSS_ENTROPY_FWD, m_param.AsFloat( nCount, hInput, hTarget, hLoss, (bHasIgnoreLabel) ? 1 : 0, nIgnoreLabel, hCountData));
        }

        /// <summary>
        /// Performs a sigmoid cross entropy backward pass in Cuda when an ignore label is specified.
        /// </summary>
        /// <param name="nCount">Specifies the number of items.</param>
        /// <param name="nIgnoreLabel">Specifies the label to ignore.</param>
        /// <param name="hTarget">Specifies a handle to the target data in GPU memory.</param>
        /// <param name="hBottomDiff">Specifies a handle to the bottom diff in GPU memory.</param>
        public void cross_entropy_ignore(int nCount, int nIgnoreLabel, long hTarget, long hBottomDiff)
        {
            if (m_dt == DataType.DOUBLE)
                m_cuda.RunDouble((int)m_hKernel, (int)CUDAFN.CUDA_CROSS_ENTROPY_IGNORE, m_param.AsDouble( nCount, nIgnoreLabel, hTarget, hBottomDiff));
            else
                m_cuda.RunFloat((int)m_hKernel, (int)CUDAFN.CUDA_CROSS_ENTROPY_IGNORE, m_param.AsFloat( nCount, nIgnoreLabel, hTarget, hBottomDiff));
        }

#pragma warning disable 1591

        /// <summary>
        /// The debug function is uses only during debugging the debug version of the low-level DLL.
        /// </summary>
        public void debug()
        {
            if (m_dt == DataType.DOUBLE)
                m_cuda.RunDouble((int)m_hKernel, (int)CUDAFN.CUDA_DEBUG, null);
            else
                m_cuda.RunFloat((int)m_hKernel, (int)CUDAFN.CUDA_DEBUG, null);
        }

        public void matrix_set_diagonal(int nCount, int nRows, double dfVal, long hData) /** @private */
        {
            if (m_dt == DataType.DOUBLE)
                m_cuda.RunDouble((int)m_hKernel, (int)CUDAFN.CUDA_MTX_SET_DIAGONAL, m_param.AsDouble( nCount, nRows, dfVal, hData));
            else
                m_cuda.RunFloat((int)m_hKernel, (int)CUDAFN.CUDA_MTX_SET_DIAGONAL, m_param.AsFloat( nCount, nRows, (float)dfVal, hData));
        }

        public void matrix_set_diagonal(int nCount, int nRows, long hDiagonal, double dfScaleA, double dfScaleB, long hData) /** @private */
        {
            if (m_dt == DataType.DOUBLE)
                m_cuda.RunDouble((int)m_hKernel, (int)CUDAFN.CUDA_MTX_SET_DIAGONAL2, m_param.AsDouble( nCount, nRows, hDiagonal, dfScaleA, dfScaleB, hData));
            else
                m_cuda.RunFloat((int)m_hKernel, (int)CUDAFN.CUDA_MTX_SET_DIAGONAL2, m_param.AsFloat( nCount, nRows, hDiagonal, (float)dfScaleA, (float)dfScaleB, hData));
        }

        public void matrix_add_vector(ORIENTATION orientation, int nWidth, int nHeight, double dfScale, long hA, long hB, long hY) /** @private */
        {
            if (m_dt == DataType.DOUBLE)
                m_cuda.RunDouble((int)m_hKernel, (int)CUDAFN.CUDA_MTX_ADD_VECTOR, m_param.AsDouble( (int)orientation, nWidth, nHeight, dfScale, hA, hB, hY));
            else
                m_cuda.RunFloat((int)m_hKernel, (int)CUDAFN.CUDA_MTX_ADD_VECTOR, m_param.AsFloat( (int)orientation, nWidth, nHeight, (float)dfScale, hA, hB, hY));
        }

        public void matrix_transpose_operation(TRANSPOSE_OPERATION op, int nWidth, int nHeight, long hA, long hB, long hY, double dfScaleA = 1.0, double dfScaleB = 1.0) /** @private */
        {
            if (m_dt == DataType.DOUBLE)
                m_cuda.RunDouble((int)m_hKernel, (int)CUDAFN.CUDA_MTX_TRANSPOSE_OPERATION, m_param.AsDouble( (int)op, nWidth, nHeight, hA, hB, hY, dfScaleA, dfScaleB));
            else
                m_cuda.RunFloat((int)m_hKernel, (int)CUDAFN.CUDA_MTX_TRANSPOSE_OPERATION, m_param.AsFloat( (int)op, nWidth, nHeight, hA, hB, hY, (float)dfScaleA, (float)dfScaleB));
        }

        public void matrix_transpose_add(int nWidth, int nHeight, double dfScaleA, double dfScaleB, long hA, long hB, long hY) /** @private */
        {
            matrix_transpose_operation(TRANSPOSE_OPERATION.ADD, nWidth, nHeight, hA, hB, hY, dfScaleA, dfScaleB);
        }

        public void matrix_transpose_mul(int nWidth, int nHeight, long hA, long hB, long hY) /** @private */
        {
            matrix_transpose_operation(TRANSPOSE_OPERATION.MUL, nWidth, nHeight, hA, hB, hY);
        }

        public void matrix_transpose_div(int nWidth, int nHeight, long hA, long hB, long hY) /** @private */
        {
            matrix_transpose_operation(TRANSPOSE_OPERATION.DIV, nWidth, nHeight, hA, hB, hY);
        }

        public void matrix_aggregate_cols(AGGREGATIONS op, int nWidth, int nHeight, long hA, long hY) /** @private */
        {
            if (m_dt == DataType.DOUBLE)
                m_cuda.RunDouble((int)m_hKernel, (int)CUDAFN.CUDA_MTX_AGGREGATE_COLS, m_param.AsDouble( (int)op, nWidth, nHeight, hA, hY));
            else
                m_cuda.RunFloat((int)m_hKernel, (int)CUDAFN.CUDA_MTX_AGGREGATE_COLS, m_param.AsFloat( (int)op, nWidth, nHeight, hA, hY));
        }

        public void matrix_aggregate_rows(AGGREGATIONS op, int nWidth, int nHeight, long hA, long hOnes, long hY) /** @private */
        {
            if (m_dt == DataType.DOUBLE)
                m_cuda.RunDouble((int)m_hKernel, (int)CUDAFN.CUDA_MTX_AGGREGATE_ROWS, m_param.AsDouble( (int)op, nWidth, nHeight, hA, hOnes, hY));
            else
                m_cuda.RunFloat((int)m_hKernel, (int)CUDAFN.CUDA_MTX_AGGREGATE_ROWS, m_param.AsFloat( (int)op, nWidth, nHeight, hA, hOnes, hY));
        }

        public void matrix_transpose(int nWidth, int nHeight, long hA, long hY) /** @private */
        {
            if (m_dt == DataType.DOUBLE)
                m_cuda.RunDouble((int)m_hKernel, (int)CUDAFN.CUDA_MTX_TRANSPOSE, m_param.AsDouble( nWidth, nHeight, hA, hY));
            else
                m_cuda.RunFloat((int)m_hKernel, (int)CUDAFN.CUDA_MTX_TRANSPOSE, m_param.AsFloat( nWidth, nHeight, hA, hY));
        }

        /// <summary>
        /// Mean center the data by columns, where each column is summed and then subtracted from each column value.
        /// </summary>
        /// <param name="nWidth">Number of columns in the matrix (dimension D)</param>
        /// <param name="nHeight">Number of rows in the matrix (dimension N)</param>
        /// <param name="hA">Input data matrix - N x D matrix (N rows, D columns)</param>
        /// <param name="hB">Column sums vector - D x 1 vector containing the sum of each column.</param>
        /// <param name="hY">Output data matrix - N x D matrix (N rows, D columns) containing mean centering of the input data matrix.</param>
        /// <param name="bNormalize">When true, each data item is divided by N to normalize each row item by column.</param>
        public void matrix_meancenter_by_column(int nWidth, int nHeight, long hA, long hB, long hY, bool bNormalize = false)
        {
            if (m_dt == DataType.DOUBLE)
                m_cuda.RunDouble((int)m_hKernel, (int)CUDAFN.CUDA_MTX_MEANCENTER_BY_COL, m_param.AsDouble( nWidth, nHeight, hA, hB, hY, (bNormalize) ? 1 : 0));
            else
                m_cuda.RunFloat((int)m_hKernel, (int)CUDAFN.CUDA_MTX_MEANCENTER_BY_COL, m_param.AsFloat( nWidth, nHeight, hA, hB, hY, (bNormalize) ? 1 : 0));
        }

        public void matrix_euclidean_distance(long hX, long hY, long hOut, int n, int d, int nStart, int nEnd) /** @private */
        {
            if (m_dt == DataType.DOUBLE)
                m_cuda.RunDouble((int)m_hKernel, (int)CUDAFN.CUDA_MTX_EUCLIDEAN_DIST, m_param.AsDouble( hX, hY, hOut, n, d, nStart, nEnd));
            else
                m_cuda.RunFloat((int)m_hKernel, (int)CUDAFN.CUDA_MTX_EUCLIDEAN_DIST, m_param.AsFloat( hX, hY, hOut, n, d, nStart, nEnd));
        }

        public void matrix_dot(int m, int n, int k, long hA, long hB, long hC) /** @private */
        {
            if (m_dt == DataType.DOUBLE)
                m_cuda.RunDouble((int)m_hKernel, (int)CUDAFN.CUDA_MTX_DOT, m_param.AsDouble( m, n, k, hA, hB, hC));
            else
                m_cuda.RunFloat((int)m_hKernel, (int)CUDAFN.CUDA_MTX_DOT, m_param.AsFloat( m, n, k, hA, hB, hC));
        }

        public void matrix_mean_rows(int nWidth, int nHeight, long hA, long hOnes, double dfAlpha, long hY) /** @private */
        {
            if (m_dt == DataType.DOUBLE)
                m_cuda.RunDouble((int)m_hKernel, (int)CUDAFN.CUDA_MTX_MEAN, m_param.AsDouble( nWidth, nHeight, hA, hOnes, dfAlpha, hY));
            else
                m_cuda.RunFloat((int)m_hKernel, (int)CUDAFN.CUDA_MTX_MEAN, m_param.AsFloat( nWidth, nHeight, hA, hOnes, (float)dfAlpha, hY));
        }

        public void matrix_stdev_rows(int nWidth, int nHeight, long hA, long hOnes, long hMean, long hWork, long hY) /** @private */
        {
            if (m_dt == DataType.DOUBLE)
                m_cuda.RunDouble((int)m_hKernel, (int)CUDAFN.CUDA_MTX_STDEV, m_param.AsDouble( nWidth, nHeight, hA, hOnes, hMean, hWork, hY));
            else
                m_cuda.RunFloat((int)m_hKernel, (int)CUDAFN.CUDA_MTX_STDEV, m_param.AsFloat( nWidth, nHeight, hA, hOnes, hMean, hWork, hY));
        }

        public void matrix_correlations(int nWidth, int nHeight, long hA, long hOnes, long hMean, long hStdev, long hWork, long hY) /** @private */
        {
            if (m_dt == DataType.DOUBLE)
                m_cuda.RunDouble((int)m_hKernel, (int)CUDAFN.CUDA_MTX_CORRELATIONS, m_param.AsDouble( nWidth, nHeight, hA, hOnes, hMean, hStdev, hWork, hY));
            else
                m_cuda.RunFloat((int)m_hKernel, (int)CUDAFN.CUDA_MTX_CORRELATIONS, m_param.AsFloat( nWidth, nHeight, hA, hOnes, hMean, hStdev, hWork, hY));
        }

#pragma warning restore 1591

        #endregion

        #region T-SNE Methods

#pragma warning disable 1591

        public void tsne_update(int n, double dfMomentum, double dfLearningRate, long hdY, long huY, long hGains, long hY, double fGainFactor1 = 0.2, double fGainFactor2 = 0.8) /** @private */
        {
            if (m_dt == DataType.DOUBLE)
                m_cuda.RunDouble((int)m_hKernel, (int)CUDAFN.CUDA_TSNE_UPDATE, m_param.AsDouble( n, dfMomentum, dfLearningRate, hdY, huY, hGains, hY, fGainFactor1, fGainFactor2));
            else
                m_cuda.RunFloat((int)m_hKernel, (int)CUDAFN.CUDA_TSNE_UPDATE, m_param.AsFloat( n, (float)dfMomentum, (float)dfLearningRate, hdY, huY, hGains, hY, (float)fGainFactor1, (float)fGainFactor2));
        }

        public void tsne_update_grad(int n, long hPosF, long hNegF, double dfSumQ, long hdC) /** @private */
        {
            if (m_dt == DataType.DOUBLE)
                m_cuda.RunDouble((int)m_hKernel, (int)CUDAFN.CUDA_TSNE_UPDATE_GRAD, m_param.AsDouble( n, hPosF, hNegF, dfSumQ, hdC));
            else
                m_cuda.RunFloat((int)m_hKernel, (int)CUDAFN.CUDA_TSNE_UPDATE_GRAD, m_param.AsFloat( n, hPosF, hNegF, (float)dfSumQ, hdC));
        }

        public void tsne_compute_exact_error(int n, long hP, long hQ, long hY) /** @private */
        {
            if (m_dt == DataType.DOUBLE)
                m_cuda.RunDouble((int)m_hKernel, (int)CUDAFN.CUDA_TSNE_COMPUTE_EXACT_ERROR, m_param.AsDouble( n, hP, hQ, hY));
            else
                m_cuda.RunFloat((int)m_hKernel, (int)CUDAFN.CUDA_TSNE_COMPUTE_EXACT_ERROR, m_param.AsFloat( n, hP, hQ, hY));
        }

        public void tsne_compute_squared_euclidean_distance(int n, int d, long hWork, long hX, long hDD_on_host) /** @private */
        {
            if (m_dt == DataType.DOUBLE)
                m_cuda.RunDouble((int)m_hKernel, (int)CUDAFN.CUDA_TSNE_COMPUTE_SQUARED_EUCLIDEAN_DISTANCE, m_param.AsDouble( n, d, hWork, hX, hDD_on_host));
            else
                m_cuda.RunFloat((int)m_hKernel, (int)CUDAFN.CUDA_TSNE_COMPUTE_SQUARED_EUCLIDEAN_DISTANCE, m_param.AsFloat( n, d, hWork, hX, hDD_on_host));
        }

        public double tsne_compute_q_matrix(int n, long hDD_on_host, long hQ, bool bQisHostMem) /** @private */
        {
            if (m_dt == DataType.DOUBLE)
            {
                double[] rg = m_cuda.RunDouble((int)m_hKernel, (int)CUDAFN.CUDA_TSNE_COMPUTE_Q_MATRIX, m_param.AsDouble( n, hDD_on_host, hQ, (bQisHostMem) ? 1 : 0));
                return rg[0];
            }
            else
            {
                float[] rg = m_cuda.RunFloat((int)m_hKernel, (int)CUDAFN.CUDA_TSNE_COMPUTE_Q_MATRIX, m_param.AsFloat( n, hDD_on_host, hQ, (bQisHostMem) ? 1 : 0));
                return rg[0];
            }
        }

        public void tsne_compute_exact_gradient(int n, int d, long hY, long hP, long hQ, bool bQonHost, long hdC, double dfSumQ) /** @private */
        {
            if (m_dt == DataType.DOUBLE)
                m_cuda.RunDouble((int)m_hKernel, (int)CUDAFN.CUDA_TSNE_COMPUTE_EXACT_GRADIENT, m_param.AsDouble( n, d, hY, hP, hQ, (bQonHost) ? 1 : 0, hdC, dfSumQ));
            else
                m_cuda.RunFloat((int)m_hKernel, (int)CUDAFN.CUDA_TSNE_COMPUTE_EXACT_GRADIENT, m_param.AsFloat( n, d, hY, hP, hQ, (bQonHost) ? 1 : 0, hdC, (float)dfSumQ));
        }

        public long tsne_symmetrize_matrix(int n, long hRowP, long hColP, long hValP) /** @private */
        {
            if (m_dt == DataType.DOUBLE)
            {
                double[] rg = m_cuda.RunDouble((int)m_hKernel, (int)CUDAFN.CUDA_TSNE_SYMMETRIZE_MATRIX, m_param.AsDouble( n, hRowP, hColP, hValP));
                return (long)rg[0];
            }
            else
            {
                float[] rg = m_cuda.RunFloat((int)m_hKernel, (int)CUDAFN.CUDA_TSNE_SYMMETRIZE_MATRIX, m_param.AsFloat( n, hRowP, hColP, hValP));
                return (long)rg[0];
            }
        }

        public void tsne_compute_knn_bounds(int n, long hData, double dfCirclePct, out double dfMinX, out double dfMinY, out double dfMaxX, out double dfMaxY) /** @private */
        {
            if (m_dt == DataType.DOUBLE)
            {
                double[] rg = m_cuda.RunDouble((int)m_hKernel, (int)CUDAFN.CUDA_TSNE_COMPUTE_KNN_BOUNDS, m_param.AsDouble( n, hData, dfCirclePct));
                dfMinX = rg[0];
                dfMinY = rg[1];
                dfMaxX = rg[2];
                dfMaxY = rg[3];
            }
            else
            {
                float[] rg = m_cuda.RunFloat((int)m_hKernel, (int)CUDAFN.CUDA_TSNE_COMPUTE_KNN_BOUNDS, m_param.AsFloat( n, hData, (float)dfCirclePct));
                dfMinX = rg[0];
                dfMinY = rg[1];
                dfMaxX = rg[2];
                dfMaxY = rg[3];
            }
        }

        public long CreateTsneGaussianPerplexity(int n, int d, int k, long hX, long hCurP, long hValP, long hRowPonHost, long hColPonHost, double fPerplexity) /** @private */
        {
            if (m_dt == DataType.DOUBLE)
            {
                double[] rg = m_cuda.RunDouble((int)m_hKernel, (int)CUDAFN.CUDA_TSNE_CREATE_GAUSSIAN_PERPLEXITY, m_param.AsDouble( n, d, k, hX, hCurP, hValP, hRowPonHost, hColPonHost, fPerplexity));
                return (long)rg[0];
            }
            else
            {
                float[] rg = m_cuda.RunFloat((int)m_hKernel, (int)CUDAFN.CUDA_TSNE_CREATE_GAUSSIAN_PERPLEXITY, m_param.AsFloat( n, d, k, hX, hCurP, hValP, hRowPonHost, hColPonHost, (float)fPerplexity));
                return (long)rg[0];
            }
        }

        public bool FindTsneGaussianPerplexity(long hTsnePerplexity, out int nCurrentIteration, out int nMaxIteration) /** @private */
        {
            bool bDone = false;

            if (m_dt == DataType.DOUBLE)
            {
                double[] rg = m_cuda.RunDouble((int)m_hKernel, (int)CUDAFN.CUDA_TSNE_FIND_GAUSSIAN_PERPLEXITY, m_param.AsDouble(hTsnePerplexity));
                bDone = (rg[0] == 1.0) ? true : false;
                nCurrentIteration = (int)rg[1];
                nMaxIteration = (int)rg[2];
            }
            else
            {
                float[] rg = m_cuda.RunFloat((int)m_hKernel, (int)CUDAFN.CUDA_TSNE_FIND_GAUSSIAN_PERPLEXITY, m_param.AsFloat( hTsnePerplexity));
                bDone = (rg[0] == 1.0) ? true : false;
                nCurrentIteration = (int)rg[1];
                nMaxIteration = (int)rg[2];
            }

            return bDone;
        }

        public void FreeTsneGaussianPerplexity(long hTsnePerplexity) /** @private */
        {
            if (m_dt == DataType.DOUBLE)
                m_cuda.RunDouble((int)m_hKernel, (int)CUDAFN.CUDA_TSNE_FREE_GAUSSIAN_PERPLEXITY, m_param.AsDouble(hTsnePerplexity));
            else
                m_cuda.RunFloat((int)m_hKernel, (int)CUDAFN.CUDA_TSNE_FREE_GAUSSIAN_PERPLEXITY, m_param.AsFloat( hTsnePerplexity));
        }

        public long CreateTsne(int n, int d, long hY, long hValP, long hRowP, long hColP, long hdC, double fTheta) /** @private */
        {
            if (m_dt == DataType.DOUBLE)
            {
                double[] rg = m_cuda.RunDouble((int)m_hKernel, (int)CUDAFN.CUDA_TSNE_CREATE, m_param.AsDouble( n, d, hY, hValP, hRowP, hColP, hdC, fTheta));
                return (long)rg[0];
            }
            else
            {
                float[] rg = m_cuda.RunFloat((int)m_hKernel, (int)CUDAFN.CUDA_TSNE_CREATE, m_param.AsFloat( n, d, hY, hValP, hRowP, hColP, hdC, (float)fTheta));
                return (long)rg[0];
            }
        }

        public void ComputeTsneGradient(long hTsne, bool bValPUpdated) /** @private */
        {
            if (m_dt == DataType.DOUBLE)
                m_cuda.RunDouble((int)m_hKernel, (int)CUDAFN.CUDA_TSNE_COMPUTE_GRADIENT1, m_param.AsDouble( hTsne, (bValPUpdated) ? 1 : 0));
            else
                m_cuda.RunFloat((int)m_hKernel, (int)CUDAFN.CUDA_TSNE_COMPUTE_GRADIENT1, m_param.AsFloat( hTsne, (bValPUpdated) ? 1 : 0));
        }

        public double EvaluateTsneError(long hTsne) /** @private */
        {
            if (m_dt == DataType.DOUBLE)
            {
                double[] rg = m_cuda.RunDouble((int)m_hKernel, (int)CUDAFN.CUDA_TSNE_COMPUTE_ERROR1, m_param.AsDouble( hTsne));
                return rg[0];
            }
            else
            {
                float[] rg = m_cuda.RunFloat((int)m_hKernel, (int)CUDAFN.CUDA_TSNE_COMPUTE_ERROR1, m_param.AsFloat( hTsne));
                return rg[0];
            }
        }

        public void FreeTsne(long hTsne) /** @private */
        {
            if (m_dt == DataType.DOUBLE)
                m_cuda.RunDouble((int)m_hKernel, (int)CUDAFN.CUDA_TSNE_FREE, m_param.AsDouble( hTsne));
            else
                m_cuda.RunFloat((int)m_hKernel, (int)CUDAFN.CUDA_TSNE_FREE, m_param.AsFloat( hTsne));
        }

#pragma warning restore 1591

        #endregion

        #region Image Processing And Misc

        /// <summary>
        /// The gaussian_blur runs a Gaussian blurring operation over each channel of the data using the sigma.
        /// </summary>
        /// <remarks>
        /// The gaussian blur operation runs a 3x3 patch, initialized with the gaussian distribution using the formula
        /// @f$
        /// G(x, y) = \frac{1}{{2\pi\sigma^2 }}e^{{{ - \left( {x^2 - y^2 } \right) } \mathord{\left/ {\vphantom {{ - \left( {x^2 - y^2 } \right) } {2\sigma ^2 }}} \right. \kern-\nulldelimiterspace} {2\sigma ^2 }}}
        /// @f$
        /// @see [Gaussian Blur](https://en.wikipedia.org/wiki/Gaussian_blur) on Wikipedia for more information.
        /// </remarks>
        /// <param name="n">Specifies the number of items in the memory of 'X'.</param>
        /// <param name="nChannels">Specifies the number of channels (i.e. 3 for RGB, 1 for B/W).</param>
        /// <param name="nHeight">Specifies the height of each item.</param>
        /// <param name="nWidth">Specifies the width of each item.</param>
        /// <param name="dfSigma">Specifies the sigma used in the gaussian blur.</param>
        /// <param name="hX">Specifies a handle to GPU memory containing the source data to blur.</param>
        /// <param name="hY">Specifies a handle to GPU memory where the blurred information is placed.</param>
        public void gaussian_blur(int n, int nChannels, int nHeight, int nWidth, double dfSigma, long hX, long hY)
        {
            if (m_dt == DataType.DOUBLE)
                m_cuda.RunDouble((int)m_hKernel, (int)CUDAFN.CUDA_GUASSIAN_BLUR, m_param.AsDouble( n, nChannels, nHeight, nWidth, dfSigma, hX, hY));
            else
                m_cuda.RunFloat((int)m_hKernel, (int)CUDAFN.CUDA_GUASSIAN_BLUR, m_param.AsFloat( n, nChannels, nHeight, nWidth, (float)dfSigma, hX, hY));
        }

        /// <summary>
        /// The hamming_distance calculates the Hamming Distance between X and Y both of length <i>n</i>.
        /// </summary>
        /// <remarks>
        /// To calculate the hamming distance first, X and Y are bitified where each element is converted to 1 if > than the threshold, or 0 otherwise.
        /// Next, the bitified versions of X and Y are subtracted from one another, and the Asum of the result is returned, which is the
        /// number of bits that are different, thus the Hamming distance.
        /// </remarks>
        /// <param name="n">Specifies the number of elements to compare in both X and Y.</param>
        /// <param name="dfThreshold">Specifies the threshold used to 'bitify' both X and Y</param>
        /// <param name="hA">Specifies the handle to the GPU memory containing the first vector to compare.</param>
        /// <param name="hB">Specifies the handle to the GPU memory containing the second vector to compare.</param>
        /// <param name="hY">Specifies the handle to the GPU memory where the hamming difference (bitified A - bitified B) is placed.</param>
        /// <param name="nOffA">Optionally, specifies an offset into the GPU memory of A, the default is 0.</param>
        /// <param name="nOffB">Optionally, specifies an offset into the GPU memory of B, the default is 0.</param>
        /// <param name="nOffY">Optionally, specifies an offset into the GPU memory of Y, the default is 0.</param>
        /// <returns>The hamming distance is returned.</returns>
        public double hamming_distance(int n, double dfThreshold, long hA, long hB, long hY, int nOffA = 0, int nOffB = 0, int nOffY = 0)
        {
            if (m_dt == DataType.DOUBLE)
                m_cuda.RunDouble((int)m_hKernel, (int)CUDAFN.CUDA_HAMMING_DIFF, m_param.AsDouble( n, dfThreshold, hA, hB, hY, nOffA, nOffB, nOffY));
            else
                m_cuda.RunFloat((int)m_hKernel, (int)CUDAFN.CUDA_HAMMING_DIFF, m_param.AsFloat( n, (float)dfThreshold, hA, hB, hY, nOffA, nOffB, nOffY));

            return asum_double(n, hY);
        }

        /// <summary>
        /// The calculate_batch_distances method calculates a set of distances based on the DistanceMethod specified.
        /// </summary>
        /// <param name="distMethod">Specifies the DistanceMethod to use (i.e. HAMMING or EUCLIDEAN).</param>
        /// <param name="dfThreshold">Specifies the threshold used when binarifying the values for the HAMMING distance.  This parameter is ignored when calculating the EUCLIDEAN distance.</param>
        /// <param name="nItemDim">Specifies the dimension of a single item.</param>
        /// <param name="hSrc">Specifies the GPU memory containing the source items.</param>
        /// <param name="hTargets">Specifies the GPU memory containing the target items that are compared against the source items.</param>
        /// <param name="hWork">Specifies the GPU memory containing the work memory - this must be the same size as the maximum size of the src or targets.</param>
        /// <param name="rgOffsets">Specifies the array of offset pairs where the first offset is into the source and the second is into the target.</param>
        /// <returns>The array distances corresponding to each offset pair is returned.</returns>
        public double[] calculate_batch_distances(DistanceMethod distMethod, double dfThreshold, int nItemDim, long hSrc, long hTargets, long hWork, int[,] rgOffsets)
        {
            if (m_dt == DataType.DOUBLE)
            {
                List<double> rgArg = new List<double> { (int)distMethod, dfThreshold, nItemDim, hSrc, hTargets, hWork };
                int nDim0 = rgOffsets.GetLength(0);
                int nDim1 = rgOffsets.GetLength(1);

                rgArg.Add(nDim0);
                rgArg.Add(nDim1);

                for (int i = 0; i < nDim0; i++)
                {
                    for (int j = 0; j < nDim1; j++)
                    {
                        rgArg.Add(rgOffsets[i, j]);
                    }
                }

                return m_cuda.RunDouble((int)m_hKernel, (int)CUDAFN.CUDA_CALC_BATCH_DIST, rgArg.ToArray());
            }
            else
            {
                List<float> rgArg = new List<float> { (int)distMethod, (float)dfThreshold, nItemDim, hSrc, hTargets, hWork };
                int nDim0 = rgOffsets.GetLength(0);
                int nDim1 = rgOffsets.GetLength(1);

                rgArg.Add(nDim0);
                rgArg.Add(nDim1);

                for (int i = 0; i < nDim0; i++)
                {
                    for (int j = 0; j < nDim1; j++)
                    {
                        rgArg.Add(rgOffsets[i, j]);
                    }
                }

                float[] rg = m_cuda.RunFloat((int)m_hKernel, (int)CUDAFN.CUDA_CALC_BATCH_DIST, rgArg.ToArray());
                double[] rgD = new double[rg.Length];

                for (int i = 0; i < rg.Length; i++)
                {
                    rgD[i] = rg[i];
                }

                return rgD;
            }
        }

        #endregion

        //---------------------------------------------------------------------
        //  Conversion Methods
        //---------------------------------------------------------------------
        #region Convertion Methods

        private T[] convert(double[] rg)
        {
            if (rg == null)
                return null;

            if (typeof(T) == typeof(double))
                return (T[])Convert.ChangeType(rg, typeof(T[]));

            T[] rgt = new T[rg.Length];
            Array.Copy(Array.ConvertAll(rg, p => Convert.ToSingle(p)), rgt, rg.Length);

            return rgt;
        }

        private T[] convert(float[] rg)
        {
            if (rg == null)
                return null;

            if (typeof(T) == typeof(float))
                return (T[])Convert.ChangeType(rg, typeof(T[]));

            T[] rgt = new T[rg.Length];
            Array.Copy(rg, rgt, rg.Length);

            return rgt;
        }

        private float convertF1(T f)
        {
            return (float)Convert.ChangeType(f, typeof(float));
        }

        private T convertF1(float f)
        {
            return (T)Convert.ChangeType(f, typeof(T));
        }

        private float[] convertF(T[] rg, int nCount = -1)
        {
            if (rg == null)
                return null;

            if (nCount == -1)
                nCount = rg.Length;

            if (typeof(T) == typeof(float))
                return (float[])Convert.ChangeType(rg, typeof(float[]));

            float[] rgf = new float[rg.Length];
            Array.Copy(Array.ConvertAll(rg, p => Convert.ToSingle(p)), rgf, rg.Length);

            return rgf;
        }

        private float[] convertF(T[] rg, float[] rgDst, int nOffset = 0, int nCount = -1)
        {
            if (rg == null)
                return null;

            if (nCount == -1)
                nCount = rg.Length;

            if (typeof(T) == typeof(float))
            {
                float[] rgConv = (float[])Convert.ChangeType(rg, typeof(float[]));
                Array.Copy(rgConv, 0, rgDst, nOffset, nCount);
            }
            else
            {
                Array.Copy(rg, 0, rgDst, nOffset, nCount);
            }

            return rgDst;
        }

        private double convertD1(T df)
        {
            return (double)Convert.ChangeType(df, typeof(double));
        }

        private T convertD1(double df)
        {
            return (T)Convert.ChangeType(df, typeof(T));
        }

        private double[] convertD(T[] rg, int nCount = -1)
        {
            if (rg == null)
                return null;

            if (nCount == -1)
                nCount = rg.Length;

            if (typeof(T) == typeof(double))
                return (double[])Convert.ChangeType(rg, typeof(double[]));

            double[] rgdf = new double[rg.Length];
            Array.Copy(rg, rgdf, rg.Length);

            return rgdf;
        }

        private double[] convertD(T[] rg, double[] rgDst, int nOffset = 0, int nCount = -1)
        {
            if (rg == null)
                return null;

            if (nCount == -1)
                nCount = rg.Length;

            if (typeof(T) == typeof(double))
            {
                double[] rgConv = (double[])Convert.ChangeType(rg, typeof(double[]));
                Array.Copy(rgConv, 0, rgDst, nOffset, nCount);
            }
            else
            {
                Array.Copy(rg, 0, rgDst, nOffset, nCount);
            }

            return rgDst;
        }

        #endregion
    }

#pragma warning disable 1591

    class Params /** @private */
    {
        public Params()
        {
        }

        public double[] AsDouble(params double[] rg)
        {
            return rg;
        }

        public float[] AsFloat(params float[] rg)
        {
            return rg;
        }
    }

#pragma warning restore 1591
}
