using System;
using System.Runtime.InteropServices;
using Unity.Collections;

namespace Unity.Entities
{
    unsafe class EntityGroupManager : IDisposable
    {
        NativeMultiHashMap<uint, IntPtr>    m_GroupLookup;
        ChunkAllocator                      m_GroupDataChunkAllocator;
        EntityGroupData*                    m_LastGroupData;
        readonly ComponentJobSafetyManager  m_JobSafetyManager;

        public EntityGroupManager(ComponentJobSafetyManager safetyManager)
        {
            m_GroupLookup = new NativeMultiHashMap<uint, IntPtr>(256, Allocator.Persistent);
            m_JobSafetyManager = safetyManager;
        }

        private EntityGroupData* GetCachedGroupData(uint hash, ComponentType* requiredTypes,
            int requiredCount)
        {
            NativeMultiHashMapIterator<uint> it;
            IntPtr grpPtr;
            if (!m_GroupLookup.TryGetFirstValue(hash, out grpPtr, out it))
                return null;
            do
            {
                var grp = (EntityGroupData*) grpPtr;
                if (ComponentType.CompareArray(grp->RequiredComponents, grp->RequiredComponentsCount, requiredTypes,
                    requiredCount))
                    return grp;
            } while (m_GroupLookup.TryGetNextValue(out grpPtr, ref it));

            return null;
        }
        public ComponentGroup CreateEntityGroupIfCached(ArchetypeManager typeMan, EntityDataManager* entityDataManager, ComponentType* requiredTypes,
            int requiredCount)
        {
            var hash = HashUtility.Fletcher32((ushort*) requiredTypes,
                requiredCount * sizeof(ComponentType) / sizeof(short));
            EntityGroupData* grp = GetCachedGroupData(hash, requiredTypes, requiredCount);
            if (grp != null)
                return new ComponentGroup(grp, m_JobSafetyManager, typeMan, entityDataManager);
            return null;
        }

        public ComponentGroup CreateEntityGroup(ArchetypeManager typeMan, EntityDataManager* entityDataManager, ComponentType* requiredTypes, int requiredCount)
        {
            var hash = HashUtility.Fletcher32((ushort*)requiredTypes, requiredCount * sizeof(ComponentType) / sizeof(short));
            EntityGroupData* grp = GetCachedGroupData(hash, requiredTypes, requiredCount);
            if (grp != null)
                return new ComponentGroup(grp, m_JobSafetyManager, typeMan, entityDataManager);

            m_JobSafetyManager.CompleteAllJobsAndInvalidateArrays();

            grp = (EntityGroupData*)m_GroupDataChunkAllocator.Allocate(sizeof(EntityGroupData), 8);
            grp->PrevGroup = m_LastGroupData;
            m_LastGroupData = grp;
            grp->RequiredComponentsCount = requiredCount;
            grp->RequiredComponents = (ComponentType*)m_GroupDataChunkAllocator.Construct(sizeof(ComponentType) * requiredCount, 4, requiredTypes);

            grp->ReaderTypesCount = 0;
            grp->WriterTypesCount = 0;

            grp->SubtractiveComponentsCount = 0;

            for (var i = 0; i != requiredCount;i++)
            {
                if (requiredTypes[i].AccessModeType == ComponentType.AccessMode.Subtractive)
                    grp->SubtractiveComponentsCount++;
                if (!requiredTypes[i].RequiresJobDependency)
                    continue;
                switch (requiredTypes[i].AccessModeType)
                {
                    case ComponentType.AccessMode.ReadOnly:
                        grp->ReaderTypesCount++;
                        break;
                    default:
                        grp->WriterTypesCount++;
                        break;
                }
            }
            grp->ReaderTypes = (int*)m_GroupDataChunkAllocator.Allocate(sizeof(int) * grp->ReaderTypesCount, 4);
            grp->WriterTypes = (int*)m_GroupDataChunkAllocator.Allocate(sizeof(int) * grp->WriterTypesCount, 4);

            var curReader = 0;
            var curWriter = 0;
            for (var i = 0; i != requiredCount;i++)
            {
                if (!requiredTypes[i].RequiresJobDependency)
                    continue;
                switch (requiredTypes[i].AccessModeType)
                {
                    case ComponentType.AccessMode.ReadOnly:
                        grp->ReaderTypes[curReader++] = requiredTypes[i].TypeIndex;
                        break;
                    default:
                        grp->WriterTypes[curWriter++] = requiredTypes[i].TypeIndex;
                        break;
                }
            }

            grp->RequiredComponents = (ComponentType*)m_GroupDataChunkAllocator.Construct(sizeof(ComponentType) * requiredCount, 4, requiredTypes);

            grp->FirstMatchingArchetype = null;
            grp->LastMatchingArchetype = null;
            for (var type = typeMan.m_LastArchetype; type != null; type = type->PrevArchetype)
                AddArchetypeIfMatching(type, grp);
            m_GroupLookup.Add(hash, (IntPtr)grp);
            return new ComponentGroup(grp, m_JobSafetyManager, typeMan, entityDataManager);
        }

        public void Dispose()
        {
            //@TODO: Need to wait for all job handles to be completed..

            m_GroupLookup.Dispose();
            m_GroupDataChunkAllocator.Dispose();
        }

        internal void OnArchetypeAdded(Archetype* type)
        {
            for (var grp = m_LastGroupData; grp != null; grp = grp->PrevGroup)
                AddArchetypeIfMatching(type, grp);
        }

        void AddArchetypeIfMatching(Archetype* archetype, EntityGroupData* group)
        {
            // If the group has more actually required types than the archetype it can never match, so early out as an optimization
            if (group->RequiredComponentsCount - group->SubtractiveComponentsCount > archetype->TypesCount)
                return;
            var typeI = 0;
            var prevTypeI = 0;
            for (var i = 0; i < group->RequiredComponentsCount; ++i, ++typeI)
            {
                while (archetype->Types[typeI].TypeIndex < group->RequiredComponents[i].TypeIndex && typeI < archetype->TypesCount)
                    ++typeI;

                var hasComponent = !(typeI >= archetype->TypesCount);

                // Type mismatch
                if (hasComponent && archetype->Types[typeI].TypeIndex != group->RequiredComponents[i].TypeIndex)
                    hasComponent = false;

                if (hasComponent && group->RequiredComponents[i].AccessModeType == ComponentType.AccessMode.Subtractive)
                    return;
                if (!hasComponent && group->RequiredComponents[i].AccessModeType != ComponentType.AccessMode.Subtractive)
                    return;
                if (hasComponent)
                    prevTypeI = typeI;
                else
                    typeI = prevTypeI;
            }
            var match = (MatchingArchetypes*)m_GroupDataChunkAllocator.Allocate(MatchingArchetypes.GetAllocationSize(group->RequiredComponentsCount), 8);
            match->Archetype = archetype;
            var typeIndexInArchetypeArray = match->TypeIndexInArchetypeArray;

            if (group->LastMatchingArchetype == null)
                group->LastMatchingArchetype = match;

            match->Next = group->FirstMatchingArchetype;
            group->FirstMatchingArchetype = match;

            for (var component = 0; component < group->RequiredComponentsCount; ++component)
            {
                var typeComponentIndex = -1;
                if (group->RequiredComponents[component].AccessModeType != ComponentType.AccessMode.Subtractive)
                {
                    typeComponentIndex = ChunkDataUtility.GetIndexInTypeArray(archetype, group->RequiredComponents[component].TypeIndex);
                    UnityEngine.Assertions.Assert.AreNotEqual(-1, typeComponentIndex);
                }

                typeIndexInArchetypeArray[component] = typeComponentIndex;
            }
        }
    }

    [StructLayout(LayoutKind.Sequential)]
    unsafe struct MatchingArchetypes
    {
        public Archetype*                       Archetype;
        public MatchingArchetypes*              Next;
        public fixed int                        TypeIndexInArchetypeArray[1];

        public static int GetAllocationSize(int requiredComponentsCount)
        {
            return sizeof(MatchingArchetypes) + sizeof(int) * (requiredComponentsCount - 1);
        }
    }

    unsafe struct EntityGroupData
    {
        public int*                 ReaderTypes;
        public int                  ReaderTypesCount;

        public int*                 WriterTypes;
        public int                  WriterTypesCount;

        public ComponentType*       RequiredComponents;
        public int                  RequiredComponentsCount;
        public int                  SubtractiveComponentsCount;
        public MatchingArchetypes*  FirstMatchingArchetype;
        public MatchingArchetypes*  LastMatchingArchetype;
        public EntityGroupData*     PrevGroup;
    }
}
