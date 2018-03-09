using Unity.Collections.LowLevel.Unsafe;
using System;

namespace Unity.Entities
{
    public struct SharedComponentDataArray<T> where T : struct, ISharedComponentData
    {
        ComponentChunkIterator               m_Iterator;
        ComponentChunkCache                  m_Cache;
        readonly SharedComponentDataManager  m_sharedComponentDataManager;
        readonly int                         m_sharedComponentIndex;

        readonly int                         m_Length;
#if ENABLE_UNITY_COLLECTIONS_CHECKS
        readonly AtomicSafetyHandle          m_Safety;
#endif

#if ENABLE_UNITY_COLLECTIONS_CHECKS
        internal SharedComponentDataArray(SharedComponentDataManager sharedComponentDataManager, int sharedComponentIndex, ComponentChunkIterator iterator, int length, AtomicSafetyHandle safety)
#else
        internal unsafe SharedComponentDataArray(SharedComponentDataManager sharedComponentDataManager, int sharedComponentIndex, ComponentChunkIterator iterator, int length)
#endif
        {
            m_sharedComponentDataManager = sharedComponentDataManager;
            m_sharedComponentIndex = sharedComponentIndex;
            m_Iterator = iterator;
	        m_Cache = default(ComponentChunkCache);

            m_Length = length;
#if ENABLE_UNITY_COLLECTIONS_CHECKS
            m_Safety = safety;
#endif
        }

        public T this[int index]
        {
            get
            {
#if ENABLE_UNITY_COLLECTIONS_CHECKS
                AtomicSafetyHandle.CheckReadAndThrow(m_Safety);
                if ((uint)index >= (uint)m_Length)
                    FailOutOfRangeError(index);
#endif

                if (index < m_Cache.CachedBeginIndex || index >= m_Cache.CachedEndIndex)
                    m_Iterator.UpdateCache(index, out m_Cache);

                var sharedComponent = m_Iterator.GetSharedComponentFromCurrentChunk(m_sharedComponentIndex);
                return m_sharedComponentDataManager.GetSharedComponentData<T>(sharedComponent);
            }
		}

#if ENABLE_UNITY_COLLECTIONS_CHECKS
        void FailOutOfRangeError(int index)
		{
			throw new IndexOutOfRangeException($"Index {index} is out of range of '{Length}' Length.");
		}
#endif

        public int Length => m_Length;
    }
}
