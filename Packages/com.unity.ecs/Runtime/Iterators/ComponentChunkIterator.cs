using Unity.Assertions;
using Unity.Collections.LowLevel.Unsafe;

namespace Unity.Entities
{
    unsafe struct ComponentChunkCache
    {
        [NativeDisableUnsafePtrRestriction]
        public void*                           CachedPtr;
        public int                             CachedBeginIndex;
        public int                             CachedEndIndex;
        public int                             CachedSizeOf;
    }

    unsafe struct ComponentChunkIterator
    {
        [NativeDisableUnsafePtrRestriction]
        readonly MatchingArchetypes*            m_FirstMatchingArchetype;
        [NativeDisableUnsafePtrRestriction]
        MatchingArchetypes*                     m_CurrentMatchingArchetype;
        public int                              IndexInComponentGroup;
        int                                     m_CurrentArchetypeIndex;
        [NativeDisableUnsafePtrRestriction]
        Chunk*                                  m_CurrentChunk;
        int                                     m_CurrentChunkIndex;


        [NativeDisableUnsafePtrRestriction]
        // The first element is the amount of filtered components
        readonly int*                                    m_FilteredSharedComponents;

        internal int GetSharedComponentFromCurrentChunk(int sharedComponentIndex)
        {
            var archetype = m_CurrentMatchingArchetype->Archetype;
            var indexInArchetype = m_CurrentMatchingArchetype->TypeIndexInArchetypeArray[sharedComponentIndex];
            var sharedComponentOffset = archetype->SharedComponentOffset[indexInArchetype];
            return m_CurrentChunk->SharedComponentValueArray[sharedComponentOffset];
        }

        void MoveToNextMatchingChunk()
        {
            var m = m_CurrentMatchingArchetype;
            var c = m_CurrentChunk;
            var e = (Chunk*)m->Archetype->ChunkList.End;

            do
            {
                c = (Chunk*)c->ChunkListNode.Next;
                while (c == e)
                {
                    m_CurrentArchetypeIndex += m_CurrentChunkIndex;
                    m_CurrentChunkIndex = 0;
                    m = m->Next;
                    if (m == null)
                    {
                        m_CurrentMatchingArchetype = null;
                        m_CurrentChunk = null;
                        return;
                    }

                    c = (Chunk*)m->Archetype->ChunkList.Begin;
                    e = (Chunk*)m->Archetype->ChunkList.End;
                }
            } while (!(c->MatchesFilter(m, m_FilteredSharedComponents) && (c->Capacity > 0)));
            m_CurrentMatchingArchetype = m;
            m_CurrentChunk = c;
        }

        public ComponentChunkIterator(MatchingArchetypes* match, int length, Chunk* firstChunk, int* filteredSharedComponents)
        {
            m_FirstMatchingArchetype = match;
            m_CurrentMatchingArchetype = match;
            IndexInComponentGroup = -1;
            m_CurrentArchetypeIndex = 0;
            m_CurrentChunk = firstChunk;
            m_CurrentChunkIndex = 0;
            m_FilteredSharedComponents = filteredSharedComponents;
        }

        public object GetManagedObject(ArchetypeManager typeMan, int typeIndexInArchetype, int cachedBeginIndex, int index)
        {
            return typeMan.GetManagedObject(m_CurrentChunk, typeIndexInArchetype, index - cachedBeginIndex);
        }

        public object GetManagedObject(ArchetypeManager typeMan, int cachedBeginIndex, int index)
        {
            return typeMan.GetManagedObject(m_CurrentChunk, m_CurrentMatchingArchetype->TypeIndexInArchetypeArray[IndexInComponentGroup], index - cachedBeginIndex);
        }

        public object[] GetManagedObjectRange(ArchetypeManager typeMan, int cachedBeginIndex, int index, out int rangeStart, out int rangeLength)
        {
            var objs = typeMan.GetManagedObjectRange(m_CurrentChunk, m_CurrentMatchingArchetype->TypeIndexInArchetypeArray[IndexInComponentGroup], out rangeStart, out rangeLength);
            rangeStart += index - cachedBeginIndex;
            rangeLength -= index - cachedBeginIndex;
            return objs;
        }

        public void UpdateCache(int index, out ComponentChunkCache cache)
        {
            Assert.IsTrue(-1 != IndexInComponentGroup);

            if (m_FilteredSharedComponents == null)
            {
                if (index < m_CurrentArchetypeIndex)
                {
                    m_CurrentMatchingArchetype = m_FirstMatchingArchetype;
                    m_CurrentArchetypeIndex = 0;
                    m_CurrentChunk = (Chunk*) m_CurrentMatchingArchetype->Archetype->ChunkList.Begin;
                    m_CurrentChunkIndex = 0;
                }

                while (index >= m_CurrentArchetypeIndex + m_CurrentMatchingArchetype->Archetype->EntityCount)
                {
                    m_CurrentArchetypeIndex += m_CurrentMatchingArchetype->Archetype->EntityCount;
                    m_CurrentMatchingArchetype = m_CurrentMatchingArchetype->Next;
                    m_CurrentChunk = (Chunk*) m_CurrentMatchingArchetype->Archetype->ChunkList.Begin;
                    m_CurrentChunkIndex = 0;
                }

                index -= m_CurrentArchetypeIndex;
                if (index < m_CurrentChunkIndex)
                {
                    m_CurrentChunk = (Chunk*) m_CurrentMatchingArchetype->Archetype->ChunkList.Begin;
                    m_CurrentChunkIndex = 0;
                }

                while (index >= m_CurrentChunkIndex + m_CurrentChunk->Count)
                {
                    m_CurrentChunkIndex += m_CurrentChunk->Count;
                    m_CurrentChunk = (Chunk*) m_CurrentChunk->ChunkListNode.Next;
                }
            }
            else
            {
                if (index < m_CurrentArchetypeIndex + m_CurrentChunkIndex)
                {
                    if (index < m_CurrentArchetypeIndex)
                    {
                        m_CurrentMatchingArchetype = m_FirstMatchingArchetype;
                        m_CurrentArchetypeIndex = 0;
                    }

                    m_CurrentChunk = (Chunk*) m_CurrentMatchingArchetype->Archetype->ChunkList.Begin;
                    m_CurrentChunkIndex = 0;
                    if (!(m_CurrentChunk->MatchesFilter(m_CurrentMatchingArchetype, m_FilteredSharedComponents) &&
                          (m_CurrentChunk->Count > 0)))
                    {
                        MoveToNextMatchingChunk();
                    }
                }

                while (index >= m_CurrentArchetypeIndex + m_CurrentChunkIndex + m_CurrentChunk->Count)
                {
                    m_CurrentChunkIndex += m_CurrentChunk->Count;
                    MoveToNextMatchingChunk();
                }
            }

            var archetype = m_CurrentMatchingArchetype->Archetype;
            var typeIndexInArchetype = m_CurrentMatchingArchetype->TypeIndexInArchetypeArray[IndexInComponentGroup];

            cache.CachedBeginIndex = m_CurrentChunkIndex + m_CurrentArchetypeIndex;
            cache.CachedEndIndex = cache.CachedBeginIndex + m_CurrentChunk->Count;
            cache.CachedSizeOf = archetype->SizeOfs[typeIndexInArchetype];
            cache.CachedPtr = m_CurrentChunk->Buffer + archetype->Offsets[typeIndexInArchetype] - cache.CachedBeginIndex * cache.CachedSizeOf;
        }

        public void GetCacheForType(int componentType, out ComponentChunkCache cache, out int typeIndexInArchetype)
        {
            var archetype = m_CurrentMatchingArchetype->Archetype;

            typeIndexInArchetype = ChunkDataUtility.GetIndexInTypeArray(archetype, componentType);
#if ENABLE_UNITY_COLLECTIONS_CHECKS
            if (typeIndexInArchetype == -1)
                throw new System.ArgumentException("componentType does not exist in the iterated archetype");
#endif

            cache.CachedBeginIndex = m_CurrentChunkIndex + m_CurrentArchetypeIndex;
            cache.CachedEndIndex = cache.CachedBeginIndex + m_CurrentChunk->Count;
            cache.CachedSizeOf = archetype->SizeOfs[typeIndexInArchetype];
            cache.CachedPtr = m_CurrentChunk->Buffer + archetype->Offsets[typeIndexInArchetype] - cache.CachedBeginIndex * cache.CachedSizeOf;
        }
    }
}
