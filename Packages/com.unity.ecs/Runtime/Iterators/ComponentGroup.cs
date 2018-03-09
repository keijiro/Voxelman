using System;
using System.Collections.Generic;
using Unity.Jobs;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;

namespace Unity.Entities
{
    unsafe struct ComponentGroupData
    {
        readonly EntityGroupData*             m_GroupData;
        readonly EntityDataManager*           m_EntityDataManager;
        readonly int*                         m_FilteredSharedComponents;

        internal MatchingArchetypes* FirstMatchingArchetype => m_GroupData->FirstMatchingArchetype;
        internal MatchingArchetypes* LastMatchingArchetype => m_GroupData->LastMatchingArchetype;

        internal ComponentGroupData(EntityGroupData* groupData, EntityDataManager* entityDataManager)
        {
            m_GroupData = groupData;
            m_EntityDataManager = entityDataManager;
            m_FilteredSharedComponents = null;
        }

        internal ComponentGroupData(ComponentGroupData parentGroupData, int* filteredSharedComponents )
        {
            m_GroupData = parentGroupData.m_GroupData;
            m_EntityDataManager = parentGroupData.m_EntityDataManager;
            m_FilteredSharedComponents = filteredSharedComponents;
        }

        internal ComponentGroupData GetVariation<SharedComponent1>(ArchetypeManager typeManager,SharedComponent1 sharedComponent1)
            where SharedComponent1 : struct, ISharedComponentData
        {
            var componentIndex1 = GetIndexInComponentGroup(TypeManager.GetTypeIndex<SharedComponent1>());
            const int filteredCount = 1;

            var filtered = (int*)UnsafeUtility.Malloc((filteredCount * 2 + 1) * sizeof(int), sizeof(int), Allocator.Temp);

            filtered[0] = filteredCount;
            filtered[1] = componentIndex1;
            filtered[2] = typeManager.GetSharedComponentDataManager().InsertSharedComponent(sharedComponent1);

            return new ComponentGroupData(this, filtered);
        }

        internal ComponentGroupData GetVariation<SharedComponent1,SharedComponent2>(ArchetypeManager typeManager,SharedComponent1 sharedComponent1, SharedComponent2 sharedComponent2)
            where SharedComponent1 : struct, ISharedComponentData
            where SharedComponent2 : struct, ISharedComponentData
        {
            var componentIndex1 = GetIndexInComponentGroup(TypeManager.GetTypeIndex<SharedComponent1>());
            var componentIndex2 = GetIndexInComponentGroup(TypeManager.GetTypeIndex<SharedComponent2>());
            const int filteredCount = 2;

            var filtered = (int*)UnsafeUtility.Malloc((filteredCount * 2 + 1) * sizeof(int), sizeof(int), Allocator.Temp);

            filtered[0] = filteredCount;
            filtered[1] = componentIndex1;
            filtered[2] = typeManager.GetSharedComponentDataManager().InsertSharedComponent(sharedComponent1);
            filtered[3] = componentIndex2;
            filtered[4] = typeManager.GetSharedComponentDataManager().InsertSharedComponent(sharedComponent2);

            return new ComponentGroupData(this, filtered);
        }

        internal void RemoveFiterReferences(ArchetypeManager typeManager)
        {
            if (m_FilteredSharedComponents == null)
                return;

            var filteredCount = m_FilteredSharedComponents[0];
            var filtered = m_FilteredSharedComponents + 1;

            for(var i=0; i<filteredCount; ++i)
            {
                var sharedComponentIndex = filtered[i * 2 + 1];
                typeManager.GetSharedComponentDataManager().RemoveReference(sharedComponentIndex);
            }

            UnsafeUtility.Free(m_FilteredSharedComponents, Allocator.Temp);
        }

#if ENABLE_UNITY_COLLECTIONS_CHECKS
        internal AtomicSafetyHandle GetSafetyHandle(ComponentJobSafetyManager safetyManager, int indexInComponentGroup)
        {
            var type = m_GroupData->RequiredComponents + indexInComponentGroup;
            var isReadOnly = type->AccessModeType == ComponentType.AccessMode.ReadOnly;
            return safetyManager.GetSafetyHandle(type->TypeIndex, isReadOnly);
        }
#endif

        public bool IsEmpty
        {
            get
            {
                if (m_FilteredSharedComponents == null)
                {
                    for (var match = m_GroupData->FirstMatchingArchetype; match != null; match = match->Next)
                    {
                        if (match->Archetype->EntityCount > 0)
                            return false;
                    }

                    return true;
                }
                else
                {
                    for (var match = m_GroupData->FirstMatchingArchetype; match != null; match = match->Next)
                    {
                        if (match->Archetype->EntityCount <= 0)
                            continue;

                        var archeType = match->Archetype;
                        for (var c = (Chunk*) archeType->ChunkList.Begin; c != archeType->ChunkList.End; c = (Chunk*) c->ChunkListNode.Next)
                        {
                            if (!c->MatchesFilter(match, m_FilteredSharedComponents))
                                continue;

                            if (c->Count > 0)
                                return false;
                        }
                    }

                    return true;
                }
            }
        }

        internal void GetComponentChunkIterator(out int outLength, out ComponentChunkIterator outIterator)
        {
            // Update the archetype segments
            var length = 0;
            MatchingArchetypes* first = null;
            Chunk* firstNonEmptyChunk = null;
            if (m_FilteredSharedComponents == null)
            {
                for (var match = m_GroupData->FirstMatchingArchetype; match != null; match = match->Next)
                {
                    if (match->Archetype->EntityCount > 0)
                    {
                        length += match->Archetype->EntityCount;
                        if (first == null)
                            first = match;
                    }
                }
                if (first != null)
                    firstNonEmptyChunk = (Chunk*)first->Archetype->ChunkList.Begin;
            }
            else
            {
                for (var match = m_GroupData->FirstMatchingArchetype; match != null; match = match->Next)
                {
                    if (match->Archetype->EntityCount <= 0)
                        continue;

                    var archeType = match->Archetype;
                    for (var c = (Chunk*)archeType->ChunkList.Begin; c != archeType->ChunkList.End; c = (Chunk*)c->ChunkListNode.Next)
                    {
                        if (!c->MatchesFilter(match, m_FilteredSharedComponents))
                            continue;

                        if (c->Count <= 0)
                            continue;

                        length += c->Count;
                        if (first != null)
                            continue;

                        first = match;
                        firstNonEmptyChunk = c;
                    }
                }
            }

            outLength = length;

            outIterator = first == null
                ? new ComponentChunkIterator(null, 0, null, null)
                : new ComponentChunkIterator(first, length, firstNonEmptyChunk, m_FilteredSharedComponents);
        }

        internal int GetIndexInComponentGroup(int componentType)
        {
            var componentIndex = 0;
            while (componentIndex < m_GroupData->RequiredComponentsCount && m_GroupData->RequiredComponents[componentIndex].TypeIndex != componentType)
                ++componentIndex;
#if ENABLE_UNITY_COLLECTIONS_CHECKS
            if (componentIndex >= m_GroupData->RequiredComponentsCount)
                throw new InvalidOperationException(
                    $"Trying to get iterator for {TypeManager.GetType(componentType)} but the required component type was not declared in the EntityGroup.");
#endif
            return componentIndex;
        }

        internal int ComponentTypeIndex(int indexInComponentGroup)
        {
            return m_GroupData->RequiredComponents[indexInComponentGroup].TypeIndex;
        }

        public bool CompareComponents(ComponentType[] componentTypes)
        {
            fixed (ComponentType* ptr = componentTypes)
            {
                return CompareComponents(ptr, componentTypes.Length);
            }
        }

        internal bool CompareComponents(ComponentType* componentTypes, int count)
        {
#if ENABLE_UNITY_COLLECTIONS_CHECKS
            for (var k = 0; k < count; ++k)
            {
                if (componentTypes[k].TypeIndex == TypeManager.GetTypeIndex<Entity>())
                    throw new System.ArgumentException("ComponentGroup.CompareComponents may not include typeof(Entity), it is implicit");
            }
#endif

            // ComponentGroups are constructed including the Entity ID
            int requiredCount = m_GroupData->RequiredComponentsCount;
            if (count != requiredCount - 1)
                return false;

            for (var k = 0; k < count; ++k)
            {
                int i;
                for (i = 1; i < requiredCount ; ++i)
                {
                    if (m_GroupData->RequiredComponents[i] == componentTypes[k])
                        break;
                }

                if (i == requiredCount)
                    return false;
            }

            return true;
        }

        public Type[] Types
        {
            get
            {
                var types = new List<Type> ();
                for (var i = 0; i < m_GroupData->RequiredComponentsCount; ++i)
                {
                    if (m_GroupData->RequiredComponents[i].AccessModeType != ComponentType.AccessMode.Subtractive)
                        types.Add(TypeManager.GetType(m_GroupData->RequiredComponents[i].TypeIndex));
                }

                return types.ToArray();
            }
        }

        internal void CompleteDependency(ComponentJobSafetyManager safetyManager)
        {
            safetyManager.CompleteDependencies(m_GroupData->ReaderTypes, m_GroupData->ReaderTypesCount, m_GroupData->WriterTypes, m_GroupData->WriterTypesCount);
        }

        internal JobHandle GetDependency(ComponentJobSafetyManager safetyManager)
        {
            return safetyManager.GetDependency(m_GroupData->ReaderTypes, m_GroupData->ReaderTypesCount, m_GroupData->WriterTypes, m_GroupData->WriterTypesCount);
        }

        internal void AddDependency(ComponentJobSafetyManager safetyManager, JobHandle job)
        {
            safetyManager.AddDependency(m_GroupData->ReaderTypes, m_GroupData->ReaderTypesCount, m_GroupData->WriterTypes, m_GroupData->WriterTypesCount, job);
        }

        internal int EntityIndex(Entity entity)
        {
            Chunk* entityChunk;
            int entityChunkIndex;

            m_EntityDataManager->GetComponentChunk(entity, out entityChunk, out entityChunkIndex);
            var entityArchetype = m_EntityDataManager->GetArchetype(entity);

            int entityStartIndex = 0;
            var matchingArchetype = m_GroupData->FirstMatchingArchetype;
            while (true)
            {
                var archetype = matchingArchetype->Archetype;
                if ((m_FilteredSharedComponents == null) && (archetype != entityArchetype))
                {
                    entityStartIndex += archetype->EntityCount;
                }
                else
                {
                    for (var c = (Chunk*)archetype->ChunkList.Begin; c != archetype->ChunkList.End; c = (Chunk*)c->ChunkListNode.Next)
                    {
                        if (c->Count <= 0)
                            continue;

                        if ((m_FilteredSharedComponents != null) && (!c->MatchesFilter(matchingArchetype, m_FilteredSharedComponents)))
                            continue;

                        if (c == entityChunk)
                        {
                            return entityStartIndex + entityChunkIndex;
                        }

                        entityStartIndex += c->Count;
                    }
                }

                if (matchingArchetype == m_GroupData->LastMatchingArchetype)
                    break;

                matchingArchetype = matchingArchetype->Next;
                if (matchingArchetype == null)
                    break;
            }

#if ENABLE_UNITY_COLLECTIONS_CHECKS
            throw new IndexOutOfRangeException($"Entity {entity.Index} is out of range of ComponentGroup.");
#else
            return -1;
#endif
        }
    }

    public unsafe class ComponentGroup : IDisposable
    {
        readonly ComponentJobSafetyManager    m_SafetyManager;
        readonly ArchetypeManager             m_TypeManager;
        readonly ComponentGroupData           m_ComponentGroupData;
        readonly EntityDataManager*           m_EntityDataManager;

        // TODO: this is temporary, used to cache some state to avoid recomputing the TransformAccessArray. We need to improve this.
        internal IDisposable m_CachedState;

#if ENABLE_UNITY_COLLECTIONS_CHECKS
        internal string                       DisallowDisposing = null;
#endif
        internal EntityDataManager* EntityDataManager => m_EntityDataManager;

        internal ComponentGroup(EntityGroupData* groupData, ComponentJobSafetyManager safetyManager, ArchetypeManager typeManager, EntityDataManager* entityDataManager )
        {
            m_ComponentGroupData = new ComponentGroupData(groupData,entityDataManager);
            m_SafetyManager = safetyManager;
            m_TypeManager = typeManager;
            m_EntityDataManager = entityDataManager;
        }

        internal ComponentGroup(ComponentGroup parentComponentGroup, ComponentGroupData componentGroupData)
        {
            m_ComponentGroupData = componentGroupData;
            m_SafetyManager = parentComponentGroup.m_SafetyManager;
            m_TypeManager = parentComponentGroup.m_TypeManager;
            m_EntityDataManager = parentComponentGroup.m_EntityDataManager;
        }

        public void Dispose()
        {
#if ENABLE_UNITY_COLLECTIONS_CHECKS
            if (DisallowDisposing  != null)
                throw new System.ArgumentException(DisallowDisposing);
#endif

            if(m_CachedState != null)
                m_CachedState.Dispose();

            m_ComponentGroupData.RemoveFiterReferences(m_TypeManager);
        }

#if ENABLE_UNITY_COLLECTIONS_CHECKS
        internal AtomicSafetyHandle GetSafetyHandle(int indexInComponentGroup) => m_ComponentGroupData.GetSafetyHandle(m_SafetyManager, indexInComponentGroup);
#endif

        public bool IsEmpty => m_ComponentGroupData.IsEmpty;

        internal void GetComponentChunkIterator(out int length, out ComponentChunkIterator iterator) =>
            m_ComponentGroupData.GetComponentChunkIterator(out length, out iterator);

        internal int GetIndexInComponentGroup(int componentType) =>
            m_ComponentGroupData.GetIndexInComponentGroup(componentType);

        internal void GetIndexFromEntity(out IndexFromEntity output)
        {
#if ENABLE_UNITY_COLLECTIONS_CHECKS
            output = new IndexFromEntity(m_ComponentGroupData, m_SafetyManager.GetSafetyHandle(TypeManager.GetTypeIndex<Entity>(), true));
#else
            output = new IndexFromEntity(m_ComponentGroupData);
#endif
        }

        internal IndexFromEntity GetIndexFromEntity()
        {
            IndexFromEntity res;
            GetIndexFromEntity(out res);
            return res;
        }

        internal void GetComponentDataArray<T>(ref ComponentChunkIterator iterator, int indexInComponentGroup, int length, out ComponentDataArray<T> output) where T : struct, IComponentData
        {
            iterator.IndexInComponentGroup = indexInComponentGroup;
#if ENABLE_UNITY_COLLECTIONS_CHECKS
            output = new ComponentDataArray<T>(iterator, length, GetSafetyHandle(indexInComponentGroup));
#else
			output = new ComponentDataArray<T>(iterator, length);
#endif
        }

        public ComponentDataArray<T> GetComponentDataArray<T>() where T : struct, IComponentData
        {
            int length;
            ComponentChunkIterator iterator;
            GetComponentChunkIterator(out length, out iterator);
            var indexInComponentGroup = GetIndexInComponentGroup(TypeManager.GetTypeIndex<T>());

            ComponentDataArray<T> res;
            GetComponentDataArray<T>(ref iterator, indexInComponentGroup, length, out res);
            return res;
        }

        internal void GetSharedComponentDataArray<T>(ref ComponentChunkIterator iterator, int indexInComponentGroup, int length, out SharedComponentDataArray<T> output) where T : struct, ISharedComponentData
        {
            iterator.IndexInComponentGroup = indexInComponentGroup;
#if ENABLE_UNITY_COLLECTIONS_CHECKS
            var typeIndex = m_ComponentGroupData.ComponentTypeIndex(indexInComponentGroup);
            output = new SharedComponentDataArray<T>(m_TypeManager.GetSharedComponentDataManager(), indexInComponentGroup, iterator, length, m_SafetyManager.GetSafetyHandle(typeIndex, true));
#else
			output = new SharedComponentDataArray<T>(m_TypeManager.GetSharedComponentDataManager(), indexInComponentGroup, iterator, length);
#endif
        }

        public SharedComponentDataArray<T> GetSharedComponentDataArray<T>() where T : struct, ISharedComponentData
        {
            int length;
            ComponentChunkIterator iterator;
            GetComponentChunkIterator(out length, out iterator);
            var indexInComponentGroup = GetIndexInComponentGroup(TypeManager.GetTypeIndex<T>());

            SharedComponentDataArray<T> res;
            GetSharedComponentDataArray<T>(ref iterator, indexInComponentGroup, length, out res);
            return res;
        }

        internal void GetFixedArrayArray<T>(ref ComponentChunkIterator iterator, int indexInComponentGroup, int length, out FixedArrayArray<T> output) where T : struct
        {
            iterator.IndexInComponentGroup = indexInComponentGroup;

#if ENABLE_UNITY_COLLECTIONS_CHECKS
            output = new FixedArrayArray<T>(iterator, length, GetSafetyHandle(indexInComponentGroup));
#else
			output = new FixedArrayArray<T>(iterator, length);
#endif
        }

        public FixedArrayArray<T> GetFixedArrayArray<T>() where T : struct
        {
            int length;
            ComponentChunkIterator iterator;
            GetComponentChunkIterator(out length, out iterator);
            var indexInComponentGroup = GetIndexInComponentGroup(TypeManager.GetTypeIndex<T>());

            FixedArrayArray<T> res;
            GetFixedArrayArray<T>(ref iterator, indexInComponentGroup, length, out res);
            return res;
        }

        internal void GetEntityArray(ref ComponentChunkIterator iterator, int length, out EntityArray output)
        {
            iterator.IndexInComponentGroup = 0;
#if ENABLE_UNITY_COLLECTIONS_CHECKS
            //@TODO: Comment on why this has to be false...
            output = new EntityArray(iterator, length, m_SafetyManager.GetSafetyHandle(TypeManager.GetTypeIndex<Entity>(), false));
#else
			output = new EntityArray(iterator, length);
#endif
        }

        public EntityArray GetEntityArray()
        {
            int length;
            ComponentChunkIterator iterator;
            GetComponentChunkIterator(out length, out iterator);

            EntityArray res;
            GetEntityArray(ref iterator, length, out res);
            return res;
        }

        public int CalculateLength()
        {
            int length;
            ComponentChunkIterator iterator;
            GetComponentChunkIterator(out length, out iterator);
            return length;
        }

        public bool CompareComponents(ComponentType[] componentTypes) =>
            m_ComponentGroupData.CompareComponents(componentTypes);

        internal bool CompareComponents(ComponentType* componentTypes, int count) =>
            m_ComponentGroupData.CompareComponents(componentTypes,count);

        public Type[] Types => m_ComponentGroupData.Types;

        internal ArchetypeManager ArchetypeManager => m_TypeManager;

        public ComponentGroup GetVariation<SharedComponent1>(SharedComponent1 sharedComponent1)
            where SharedComponent1 : struct, ISharedComponentData
        {
            var componentGroupData =
                m_ComponentGroupData.GetVariation(m_TypeManager, sharedComponent1);
            return new ComponentGroup(this,componentGroupData);
        }

        public ComponentGroup GetVariation<SharedComponent1, SharedComponent2>(SharedComponent1 sharedComponent1, SharedComponent2 sharedComponent2)
            where SharedComponent1 : struct, ISharedComponentData
            where SharedComponent2 : struct, ISharedComponentData
        {
            var componentGroupData =
                m_ComponentGroupData.GetVariation(m_TypeManager, sharedComponent1,sharedComponent2);
            return new ComponentGroup(this,componentGroupData);
        }

        public void CompleteDependency() => m_ComponentGroupData.CompleteDependency(m_SafetyManager);
        public JobHandle GetDependency() => m_ComponentGroupData.GetDependency(m_SafetyManager);
        public void AddDependency(JobHandle job) => m_ComponentGroupData.AddDependency(m_SafetyManager, job);

        internal ArchetypeManager GetArchetypeManager()
        {
            return m_TypeManager;
        }
    }
}
