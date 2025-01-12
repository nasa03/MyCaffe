//=============================================================================
//	FILE:	memorycol.h
//
//	DESC:	This file implements the memory collection used to manage collections
//			of memory.
//=============================================================================
#ifndef __MEMORYCOL_CU__
#define __MEMORYCOL_CU__

#include "util.h"


//=============================================================================
//	Flags
//=============================================================================

const int MAX_ITEMS = 4096 * 512;

//-----------------------------------------------------------------------------
//	MemoryItem class
//-----------------------------------------------------------------------------
class MemoryItem
{
	protected:
		void* m_pData;
		size_t m_lSize;
		int m_nDeviceID;
		bool m_bOwner;
		bool m_bHalf;


	public:
		MemoryItem()
		{
			m_bOwner = true;
			m_pData = NULL;
			m_lSize = 0;
			m_nDeviceID = -1;
			m_bHalf = false;
		}

		~MemoryItem()
		{
			Free();
		}

		bool IsFree()
		{
			if (m_pData != NULL)
				return false;

			return true;
		}

		void* Data()
		{
			return m_pData;
		}

		size_t Size()
		{
			return m_lSize;
		}

		int DeviceID()
		{
			return m_nDeviceID;
		}

		bool IsHalf()
		{
			return m_bHalf;
		}

		long Allocate(int nDeviceID, bool bHalf, size_t lSize, void* pSrc = NULL, cudaStream_t pStream = NULL)
		{
			if (lSize == 0)
				return ERROR_PARAM_OUT_OF_RANGE;

			Free();

			LONG lErr = cudaMalloc(&m_pData, lSize);
			if (lErr != 0)
				return lErr;

			if (lErr = cudaMemset(m_pData, 0, lSize))
			{
				cudaFree(m_pData);
				m_pData = NULL;
				return lErr;
			}

			m_nDeviceID = nDeviceID;
			m_lSize = lSize;
			m_bOwner = true;
			m_bHalf = bHalf;

			if (pSrc != NULL)
			{
				if (pStream != NULL)
					return cudaMemcpyAsync(m_pData, pSrc, lSize, cudaMemcpyHostToDevice, pStream);
				else
					return cudaMemcpy(m_pData, pSrc, lSize, cudaMemcpyHostToDevice);
			}

			return 0;
		}

		long Allocate(int nDeviceID, bool bHalf, void* pData, size_t lSize)
		{
			if (lSize == 0)
				return ERROR_PARAM_OUT_OF_RANGE;

			m_pData = pData;
			m_nDeviceID = nDeviceID;
			m_lSize = lSize;
			m_bOwner = false;
			m_bHalf = bHalf;

			return 0;
		}

		long Free();

		long GetData(size_t lSize, void* pDst)
		{
			if (pDst == NULL)
				return ERROR_PARAM_NULL;

			if (m_pData == NULL)
				return ERROR_MEMORY_OUT;

			if (lSize <= 0 || lSize > m_lSize)
				return ERROR_PARAM_OUT_OF_RANGE;

			return cudaMemcpy(pDst, m_pData, lSize, cudaMemcpyDeviceToHost);
		}

		long GetDataAt(size_t lSize, void* pDst, size_t nOffsetInBytes)
		{
			if (pDst == NULL)
				return ERROR_PARAM_NULL;

			if (m_pData == NULL)
				return ERROR_MEMORY_OUT;

			if (lSize <= 0)
				return ERROR_PARAM_OUT_OF_RANGE;

			long long lSize1 = lSize + nOffsetInBytes;
			if (lSize1 > SIZE_MAX)
				return ERROR_MEMORY_RANGE_EXCEEDED;

			if ((size_t)lSize1 > m_lSize)
				return ERROR_PARAM_OUT_OF_RANGE;

			byte* pData = ((byte*)m_pData) + nOffsetInBytes;

			return cudaMemcpy(pDst, pData, lSize, cudaMemcpyDeviceToHost);
		}

		long SetData(size_t lSize, void* pSrc, cudaStream_t pStream = NULL)
		{
			if (pSrc == NULL)
				return ERROR_PARAM_NULL;

			if (m_pData == NULL)
				return ERROR_MEMORY_OUT;

			size_t lSizeMax = SIZE_MAX;
			if (lSize >= (lSizeMax - 10))
				lSize = m_lSize;

			if (lSize <= 0)
				return ERROR_PARAM_OUT_OF_RANGE;

			if (lSize > m_lSize)
				return ERROR_PARAM_OUT_OF_RANGE;

			if (lSize < m_lSize)
				cudaMemset(m_pData, 0, m_lSize);

			if (pStream != NULL)
				return cudaMemcpyAsync(m_pData, pSrc, lSize, cudaMemcpyHostToDevice, pStream);
			else
				return cudaMemcpy(m_pData, pSrc, lSize, cudaMemcpyHostToDevice);
		}

		long SetDataAt(size_t lSize, void* pSrc, size_t nOffsetInBytes)
		{
			if (pSrc == NULL)
				return ERROR_PARAM_NULL;

			if (m_pData == NULL)
				return ERROR_MEMORY_OUT;

			if (lSize <= 0)
				return ERROR_PARAM_OUT_OF_RANGE;

			long long lSize1 = lSize + nOffsetInBytes;
			if (lSize1 > SIZE_MAX)
				return ERROR_MEMORY_RANGE_EXCEEDED;

			if ((size_t)lSize1 > m_lSize)
				return ERROR_PARAM_OUT_OF_RANGE;

			byte* pData = ((byte*)m_pData) + nOffsetInBytes;

			return cudaMemcpy(pData, pSrc, lSize, cudaMemcpyHostToDevice);
		}

		long SetData(int nVal)
		{
			LONG lErr;

			if (m_pData == NULL)
				return ERROR_MEMORY_OUT;

			if (m_lSize == 0)
				return ERROR_MEMORY_OUT;

			if (lErr = cudaMemset(m_pData, nVal, m_lSize))
				return lErr;

			return 0;
		}

		long Copy(size_t lSize, MemoryItem* pSrc)
		{
			if (lSize > pSrc->Size())
				return ERROR_PARAM_OUT_OF_RANGE;

			return SetData(lSize, pSrc);
		}

		float* GetHostDataAsFloat()
		{
			float* fp = (float*)malloc(Size());
			if (cudaMemcpy(fp, Data(), Size(), cudaMemcpyDeviceToHost))
			{
				free(fp);
				return NULL;
			}
			return fp;
		}

		double* GetHostDataAsDouble()
		{
			double* fp = (double*)malloc(Size());
			if (cudaMemcpy(fp, Data(), Size(), cudaMemcpyDeviceToHost))
			{
				free(fp);
				return NULL;
			}
			return fp;
		}
};


//-----------------------------------------------------------------------------
//	HandleCollection Class
//
//	The HandleCollection class manages a set of generic handles.
//-----------------------------------------------------------------------------
class MemoryCollection
{
	protected:
		MemoryCollection* m_pMemPtrs;
		MemoryItem m_rgHandles[MAX_ITEMS];
		long m_nLastIdx;
		unsigned long m_lTotalMem;

	public:
		MemoryCollection();
		~MemoryCollection();

		size_t GetCount(size_t lCount)
		{
			if (lCount % 2 != 0)
				lCount++;

			return lCount;
		}

		long long GetSize(size_t lCount, int nBaseSize)
		{
			long long llCount = (long long)GetCount(lCount);

			llCount *= (long long)nBaseSize;
			
			return llCount;
		}

		void SetMemoryPointers(MemoryCollection* pMemPtrs)
		{
			m_pMemPtrs = pMemPtrs;
		}

		long Allocate(int nDeviceID, bool bHalf, size_t lSize, void* pSrc, cudaStream_t pStream, long* phHandle);
		long Allocate(int nDeviceID, bool bHalf, void* pData, size_t lSize, long* phHandle);
		long Free(long hHandle);
		long GetData(long hHandle, MemoryItem** ppItem);
		long GetDataAt(long hHandle, bool bHalf, size_t lSize, void* pDst, size_t nOffsetInBytes);
		long SetData(long hHandle, bool bHalf, size_t lSize, void* pSrc, cudaStream_t pStream);
		long SetDataAt(long hHandle, bool bHalf, size_t lSize, void* pSrc, size_t nOffsetInBytes);
		long SetData(MemoryItem* pItem, bool bHalf, size_t lSize, void* pSrc, cudaStream_t pStream);
		long SetDataAt(MemoryItem* pItem, bool bHalf, size_t lSize, void* pSrc, size_t nOffsetInBytes);
		long GetCount();
		unsigned long GetTotalUsed();
};


//=============================================================================
//	Inline Methods
//=============================================================================

inline MemoryCollection::MemoryCollection()
{
	m_pMemPtrs = NULL;
	memset(m_rgHandles, 0, sizeof(MemoryItem) * MAX_ITEMS);
	// Skip 0 index, so that this handle can be treated as NULL.
	m_nLastIdx = 1;
	m_lTotalMem = 0;
}

inline long MemoryCollection::GetCount()
{
	return MAX_ITEMS;
}

inline long MemoryCollection::GetData(long hHandle, MemoryItem** ppItem)
{
	if (hHandle < 1 || hHandle >= MAX_ITEMS * 2)
		return ERROR_PARAM_OUT_OF_RANGE;

	if (ppItem == NULL)
		return ERROR_PARAM_NULL;


	//--------------------------------------------------------
	//	If the handle is in the range [MAX_ITEM, MAX_ITEM*2]
	//	then it is a ponter into already existing memory.
	//--------------------------------------------------------
	if (hHandle > MAX_ITEMS)
	{
		if (m_pMemPtrs == NULL)
			return ERROR_PARAM_OUT_OF_RANGE;

		LONG lErr;

		if (lErr = m_pMemPtrs->GetData(hHandle - MAX_ITEMS, ppItem))
			return lErr;

		return 0;
	}

	*ppItem = &m_rgHandles[hHandle];
	return 0;
}

inline long MemoryCollection::GetDataAt(long hHandle, bool bHalf, size_t lSize, void* pDst, size_t nOffsetInBytes)
{
	MemoryItem* pItem;

	if (hHandle < 1 || hHandle >= MAX_ITEMS * 2)
		return ERROR_PARAM_OUT_OF_RANGE;

	//--------------------------------------------------------
	//	If the handle is in the range [MAX_ITEM, MAX_ITEM*2]
	//	then it is a ponter into already existing memory.
	//--------------------------------------------------------
	if (hHandle > MAX_ITEMS)
	{
		if (m_pMemPtrs == NULL)
			return ERROR_PARAM_OUT_OF_RANGE;

		LONG lErr;

		MemoryItem* pItem;
		if (lErr = m_pMemPtrs->GetData(hHandle - MAX_ITEMS, &pItem))
			return lErr;
	}
	else
	{
		pItem = &m_rgHandles[hHandle];
	}

	return pItem->GetDataAt(lSize, pDst, nOffsetInBytes);
}

inline long MemoryCollection::SetData(long hHandle, bool bHalf, size_t lSize, void* pSrc, cudaStream_t pStream)
{
	MemoryItem* pItem;

	if (hHandle < 1 || hHandle >= MAX_ITEMS * 2)
		return ERROR_PARAM_OUT_OF_RANGE;

	//--------------------------------------------------------
	//	If the handle is in the range [MAX_ITEM, MAX_ITEM*2]
	//	then it is a ponter into already existing memory.
	//--------------------------------------------------------
	if (hHandle > MAX_ITEMS)
	{
		if (m_pMemPtrs == NULL)
			return ERROR_PARAM_OUT_OF_RANGE;

		LONG lErr;

		MemoryItem* pItem;
		if (lErr = m_pMemPtrs->GetData(hHandle - MAX_ITEMS, &pItem))
			return lErr;
	}
	else
	{
		pItem = &m_rgHandles[hHandle];
	}

	return pItem->SetData(lSize, pSrc, pStream);

}

inline long MemoryCollection::SetDataAt(long hHandle, bool bHalf, size_t lSize, void* pSrc, size_t nOffsetInBytes)
{
	MemoryItem* pItem;

	if (hHandle < 1 || hHandle >= MAX_ITEMS * 2)
		return ERROR_PARAM_OUT_OF_RANGE;

	//--------------------------------------------------------
	//	If the handle is in the range [MAX_ITEM, MAX_ITEM*2]
	//	then it is a ponter into already existing memory.
	//--------------------------------------------------------
	if (hHandle > MAX_ITEMS)
	{
		if (m_pMemPtrs == NULL)
			return ERROR_PARAM_OUT_OF_RANGE;

		LONG lErr;

		MemoryItem* pItem;
		if (lErr = m_pMemPtrs->GetData(hHandle - MAX_ITEMS, &pItem))
			return lErr;
	}
	else
	{
		pItem = &m_rgHandles[hHandle];
	}

	return pItem->SetDataAt(lSize, pSrc, nOffsetInBytes);
}

inline long MemoryCollection::SetData(MemoryItem* pItem, bool bHalf, size_t lSize, void* pSrc, cudaStream_t pStream)
{
	if (pItem->IsHalf() != bHalf)
		return ERROR_PARAM_OUT_OF_RANGE;

	return pItem->SetData(lSize, pSrc, pStream);
}

inline long MemoryCollection::SetDataAt(MemoryItem* pItem, bool bHalf, size_t lSize, void* pSrc, size_t nOffsetInBytes)
{
	if (pItem->IsHalf() != bHalf)
		return ERROR_PARAM_OUT_OF_RANGE;

	return pItem->SetDataAt(lSize, pSrc, nOffsetInBytes);
}

inline unsigned long MemoryCollection::GetTotalUsed()
{
	return m_lTotalMem;
}

#endif // __HANDLECOL_CU__