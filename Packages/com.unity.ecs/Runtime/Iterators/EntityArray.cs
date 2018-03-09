using System;

using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;

namespace Unity.Entities
{
	[NativeContainer]
	[NativeContainerSupportsMinMaxWriteRestriction]
	public unsafe struct EntityArray
	{
	    ComponentChunkIterator 		m_Iterator;
	    ComponentChunkCache 		m_Cache;
	    readonly int                     	m_Length;

#if ENABLE_UNITY_COLLECTIONS_CHECKS
	    readonly int                      	m_MinIndex;
	    readonly int                      	m_MaxIndex;
	    readonly AtomicSafetyHandle       	m_Safety;
#endif

#if ENABLE_UNITY_COLLECTIONS_CHECKS
        internal EntityArray(ComponentChunkIterator iterator, int length, AtomicSafetyHandle safety)
#else
        internal unsafe EntityArray(ComponentChunkIterator iterator, int length)
#endif
		{
            m_Length = length;
            m_Iterator = iterator;
			m_Cache = default(ComponentChunkCache);

			#if ENABLE_UNITY_COLLECTIONS_CHECKS
			m_MinIndex = 0;
			m_MaxIndex = length - 1;
			m_Safety = safety;
			#endif

		}

		public Entity this[int index]
		{
			get
			{
#if ENABLE_UNITY_COLLECTIONS_CHECKS
				AtomicSafetyHandle.CheckReadAndThrow(m_Safety);
				if (index < m_MinIndex || index > m_MaxIndex)
					FailOutOfRangeError(index);
#endif

                if (index < m_Cache.CachedBeginIndex || index >= m_Cache.CachedEndIndex)
                    m_Iterator.UpdateCache(index, out m_Cache);

                return UnsafeUtility.ReadArrayElement<Entity>(m_Cache.CachedPtr, index);
			}
		}

#if ENABLE_UNITY_COLLECTIONS_CHECKS
	    void FailOutOfRangeError(int index)
		{
			//@TODO: Make error message utility and share with NativeArray...
			if (index < Length && (m_MinIndex != 0 || m_MaxIndex != Length - 1))
				throw new IndexOutOfRangeException(
				        $"Index {index} is out of restricted IJobParallelFor range [{m_MinIndex}...{m_MaxIndex}] in ReadWriteBuffer.\nReadWriteBuffers are restricted to only read & write the element at the job index. You can use double buffering strategies to avoid race conditions due to reading & writing in parallel to the same elements from a job.");

			throw new IndexOutOfRangeException($"Index {index} is out of range of '{Length}' Length.");
		}
#endif

	    public NativeArray<Entity> GetChunkArray(int startIndex, int maxCount)
	    {
#if ENABLE_UNITY_COLLECTIONS_CHECKS
            AtomicSafetyHandle.CheckReadAndThrow(m_Safety);

	        if (startIndex < m_MinIndex)
		        FailOutOfRangeError(startIndex);
	        else if (startIndex + maxCount > m_MaxIndex + 1)
		        FailOutOfRangeError(startIndex + maxCount);
#endif

	        m_Iterator.UpdateCache(startIndex, out m_Cache);

	        void* ptr = (byte*)m_Cache.CachedPtr + startIndex * m_Cache.CachedSizeOf;
	        var count = Math.Min(maxCount, m_Cache.CachedEndIndex - startIndex);

	        var arr = NativeArrayUnsafeUtility.ConvertExistingDataToNativeArray<Entity>(ptr, count, Allocator.Invalid);

#if ENABLE_UNITY_COLLECTIONS_CHECKS
            NativeArrayUnsafeUtility.SetAtomicSafetyHandle(ref arr, m_Safety);
#endif

	        return arr;
	    }

	    public void CopyTo(NativeSlice<Entity> dst, int startIndex = 0)
	    {
	        var copiedCount = 0;
	        while (copiedCount < dst.Length)
	        {
	            var chunkArray = GetChunkArray(startIndex + copiedCount, dst.Length - copiedCount);
	            dst.Slice(copiedCount, chunkArray.Length).CopyFrom(chunkArray);

	            copiedCount += chunkArray.Length;
	        }
	    }

        public int Length => m_Length;
	}
}
