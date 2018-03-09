using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.Jobs;
using Unity.Assertions;

[assembly:InternalsVisibleTo("Unity.Entities.Hybrid")]

namespace Unity.Entities
{
    //@TODO: There is nothing prevent non-main thread (non-job thread) access of EntityMnaager.
    //       Static Analysis or runtime checks?

    //@TODO: safety?
    public unsafe struct EntityArchetype
    {
        [NativeDisableUnsafePtrRestriction]
        internal Archetype* Archetype;

        public bool Valid => Archetype != null;

        public static bool operator ==(EntityArchetype lhs, EntityArchetype rhs) { return lhs.Archetype == rhs.Archetype; }
        public static bool operator !=(EntityArchetype lhs, EntityArchetype rhs) { return lhs.Archetype != rhs.Archetype; }
        public override bool Equals(object compare) { return this == (EntityArchetype)compare; }
        public override int GetHashCode() { return (int)Archetype; }
    }

    public struct Entity : IEquatable<Entity>
    {
        public int Index;
        public int Version;

        public static bool operator ==(Entity lhs, Entity rhs) { return lhs.Index == rhs.Index && lhs.Version == rhs.Version; }
        public static bool operator !=(Entity lhs, Entity rhs) { return lhs.Index != rhs.Index || lhs.Version != rhs.Version; }
        public override bool Equals(object compare) { return this == (Entity)compare; }
        public override int GetHashCode() { return Index; }

        public static Entity Null => new Entity();

        public bool Equals(Entity entity)
        {
            return entity.Index == Index && entity.Version == Version;
        }
    }

    public sealed unsafe class EntityManager : ScriptBehaviourManager
    {
        EntityDataManager*                m_Entities;

        ArchetypeManager                  m_ArchetypeManager;
        EntityGroupManager                m_GroupManager;

        SharedComponentDataManager        m_SharedComponentManager;

        ExclusiveEntityTransaction        m_ExclusiveEntityTransaction;

        ComponentType*                    m_CachedComponentTypeArray;
        ComponentTypeInArchetype*         m_CachedComponentTypeInArchetypeArray;

        internal EntityDataManager* Entities => m_Entities;
        internal ArchetypeManager ArchetypeManager => m_ArchetypeManager;

        internal List<ComponentDataWrapperBase>    m_CachedComponentList;
        
        protected override void OnBeforeCreateManagerInternal(World world, int capacity) { }
        protected override void OnBeforeDestroyManagerInternal() { }
        protected override void OnAfterDestroyManagerInternal() { }

        protected override void OnCreateManager(int capacity)
        {
            TypeManager.Initialize();

            m_CachedComponentList = new List<ComponentDataWrapperBase>();
            
            m_Entities = (EntityDataManager*)UnsafeUtility.Malloc(sizeof(EntityDataManager), 64, Allocator.Persistent);
            m_Entities->OnCreate(capacity);

            m_SharedComponentManager = new SharedComponentDataManager();

            m_ArchetypeManager = new ArchetypeManager(m_SharedComponentManager);
            ComponentJobSafetyManager = new ComponentJobSafetyManager();
            m_GroupManager = new EntityGroupManager(ComponentJobSafetyManager);

            m_ExclusiveEntityTransaction = new ExclusiveEntityTransaction(m_ArchetypeManager, m_GroupManager, m_SharedComponentManager, m_Entities);

            m_CachedComponentTypeArray = (ComponentType*)UnsafeUtility.Malloc(sizeof(ComponentType) * 32 * 1024, 16, Allocator.Persistent);
            m_CachedComponentTypeInArchetypeArray = (ComponentTypeInArchetype*)UnsafeUtility.Malloc(sizeof(ComponentTypeInArchetype) * 32 * 1024, 16, Allocator.Persistent);
        }

        protected override void OnDestroyManager()
        {
            EndExclusiveEntityTransaction();

            ComponentJobSafetyManager.Dispose(); ComponentJobSafetyManager = null;

            m_Entities->OnDestroy();
            UnsafeUtility.Free(m_Entities, Allocator.Persistent);
            m_Entities = null;
            m_ArchetypeManager.Dispose(); m_ArchetypeManager = null;
            m_GroupManager.Dispose(); m_GroupManager = null;
            m_ExclusiveEntityTransaction.OnDestroyManager();

            m_SharedComponentManager.Dispose();

            UnsafeUtility.Free(m_CachedComponentTypeArray, Allocator.Persistent);
            m_CachedComponentTypeArray = null;

            UnsafeUtility.Free(m_CachedComponentTypeInArchetypeArray, Allocator.Persistent);
            m_CachedComponentTypeInArchetypeArray = null;
        }

        internal override void InternalUpdate()
        {
        }

        public bool IsCreated => m_CachedComponentTypeArray != null;

        public int EntityCapacity
        {
            get { return m_Entities->Capacity; }
            set
            {
                BeforeStructuralChange();
                m_Entities->Capacity = value;
            }
        }

        int PopulatedCachedTypeArray(ComponentType* requiredComponents, int count)
        {
            m_CachedComponentTypeArray[0] = ComponentType.Create<Entity>();
            for (var i = 0; i < count; ++i)
                SortingUtilities.InsertSorted(m_CachedComponentTypeArray, i + 1, requiredComponents[i]);
            return count + 1;
        }

        int PopulatedCachedTypeInArchetypeArray(ComponentType* requiredComponents, int count)
        {
            m_CachedComponentTypeInArchetypeArray[0] = new ComponentTypeInArchetype(ComponentType.Create<Entity>());
            for (var i = 0; i < count; ++i)
                SortingUtilities.InsertSorted(m_CachedComponentTypeInArchetypeArray, i + 1, requiredComponents[i]);
            return count + 1;
        }

        public ComponentGroup CreateComponentGroup(ComponentType* requiredComponents, int count)
        {
            var typeArrayCount = PopulatedCachedTypeArray(requiredComponents, count);
            var grp = m_GroupManager.CreateEntityGroupIfCached(m_ArchetypeManager, m_Entities, m_CachedComponentTypeArray, typeArrayCount);
            if (grp != null)
                return grp;

            BeforeStructuralChange();

            return m_GroupManager.CreateEntityGroup(m_ArchetypeManager, m_Entities, m_CachedComponentTypeArray, typeArrayCount);
        }

        public ComponentGroup CreateComponentGroup(params ComponentType[] requiredComponents)
        {
            fixed (ComponentType* requiredComponentsPtr = requiredComponents)
            {
                return CreateComponentGroup(requiredComponentsPtr, requiredComponents.Length);
            }
        }

        internal EntityArchetype CreateArchetype(ComponentType* types, int count)
        {
            var cachedComponentCount = PopulatedCachedTypeInArchetypeArray(types, count);

            // Lookup existing archetype (cheap)
            EntityArchetype entityArchetype;
            entityArchetype.Archetype = m_ArchetypeManager.GetExistingArchetype(m_CachedComponentTypeInArchetypeArray, cachedComponentCount);
            if (entityArchetype.Archetype != null)
                return entityArchetype;

            // Creating an archetype invalidates all iterators / jobs etc
            // because it affects the live iteration linked lists...
            BeforeStructuralChange();

            entityArchetype.Archetype = m_ArchetypeManager.GetOrCreateArchetype(m_CachedComponentTypeInArchetypeArray, cachedComponentCount, m_GroupManager);
            return entityArchetype;
        }

        public EntityArchetype CreateArchetype(params ComponentType[] types)
        {
            fixed (ComponentType* typesPtr = types)
            {
                return CreateArchetype(typesPtr, types.Length);
            }
        }

        public void CreateEntity(EntityArchetype archetype, NativeArray<Entity> entities)
        {
            CreateEntityInternal(archetype, (Entity*)entities.GetUnsafePtr(), entities.Length);
        }

        public Entity CreateEntity(EntityArchetype archetype)
        {
            Entity entity;
            CreateEntityInternal(archetype, &entity, 1);
            return entity;
        }

        public Entity CreateEntity(params ComponentType[] types)
        {
            return CreateEntity(CreateArchetype(types));
        }

        void CreateEntityInternal(EntityArchetype archetype, Entity* entities, int count)
        {
            BeforeStructuralChange();
            m_Entities->CreateEntities(m_ArchetypeManager, archetype.Archetype, entities, count);
        }

        public void DestroyEntity(NativeArray<Entity> entities)
        {
            DestroyEntityInternal((Entity*)entities.GetUnsafeReadOnlyPtr(), entities.Length);
        }

        public void DestroyEntity(NativeSlice<Entity> entities)
        {
            DestroyEntityInternal((Entity*)entities.GetUnsafeReadOnlyPtr(), entities.Length);
        }

        public void DestroyEntity(Entity entity)
        {
            DestroyEntityInternal(&entity, 1);
        }

        void DestroyEntityInternal(Entity* entities, int count)
        {
            BeforeStructuralChange();
            m_Entities->AssertEntitiesExist(entities, count);
            m_Entities->DeallocateEnties(m_ArchetypeManager, m_SharedComponentManager, entities, count);
        }

        public bool Exists(Entity entity)
        {
            return m_Entities->Exists(entity);
        }

        public bool HasComponent<T>(Entity entity)
        {
            return m_Entities->HasComponent(entity, ComponentType.Create<T>());
        }

        public bool HasComponent(Entity entity, ComponentType type)
        {
            return m_Entities->HasComponent(entity, type);
        }

        public Entity Instantiate(Entity srcEntity)
        {
            Entity entity;
            InstantiateInternal(srcEntity, &entity, 1);
            return entity;
        }

        public void Instantiate(Entity srcEntity, NativeArray<Entity> outputEntities)
        {
            InstantiateInternal(srcEntity, (Entity*)outputEntities.GetUnsafePtr(), outputEntities.Length);
        }

        internal void InstantiateInternal(Entity srcEntity, Entity* outputEntities, int count)
        {
            BeforeStructuralChange();
            if (!m_Entities->Exists(srcEntity))
                throw new ArgumentException("srcEntity is not a valid entity");

            m_Entities->InstantiateEntities(m_ArchetypeManager, m_SharedComponentManager, srcEntity, outputEntities, count);
        }

        public void AddComponent(Entity entity, ComponentType type)
        {
            BeforeStructuralChange();
            m_Entities->AssertEntitiesExist(&entity, 1);
            m_Entities->AddComponent(entity, type, m_ArchetypeManager, m_SharedComponentManager, m_GroupManager, m_CachedComponentTypeInArchetypeArray);
        }

        public void RemoveComponent(Entity entity, ComponentType type)
        {
            BeforeStructuralChange();
            m_Entities->AssertEntityHasComponent(entity, type);
            m_Entities->RemoveComponent(entity, type, m_ArchetypeManager, m_SharedComponentManager, m_GroupManager, m_CachedComponentTypeInArchetypeArray);
        }

        public void RemoveComponent<T>(Entity entity)
        {
            RemoveComponent(entity, ComponentType.Create<T>());
        }

        public void AddComponentData<T>(Entity entity, T componentData) where T : struct, IComponentData
        {
            AddComponent(entity, ComponentType.Create<T>());
            SetComponentData(entity, componentData);
        }

        internal ComponentDataFromEntity<T> GetComponentDataFromEntity<T>(int typeIndex, bool isReadOnly) where T : struct, IComponentData
        {
#if ENABLE_UNITY_COLLECTIONS_CHECKS
            return new ComponentDataFromEntity<T>(typeIndex, m_Entities, ComponentJobSafetyManager.GetSafetyHandle(typeIndex, isReadOnly));
#else
            return new ComponentDataFromEntity<T>(typeIndex, m_Entities);
#endif
        }

        public FixedArrayFromEntity<T> GetFixedArrayFromEntity<T>(int typeIndex, bool isReadOnly = false) where T : struct
        {
#if ENABLE_UNITY_COLLECTIONS_CHECKS
            return new FixedArrayFromEntity<T>(typeIndex, m_Entities, ComponentJobSafetyManager.GetSafetyHandle(typeIndex, isReadOnly));
#else
            return new FixedArrayFromEntity<T>(typeIndex, m_Entities);
#endif
        }

        public T GetComponentData<T>(Entity entity) where T : struct, IComponentData
        {
            var typeIndex = TypeManager.GetTypeIndex<T>();
            m_Entities->AssertEntityHasComponent(entity, typeIndex);
            ComponentJobSafetyManager.CompleteWriteDependency(typeIndex);

            var ptr = m_Entities->GetComponentDataWithType (entity, typeIndex);

            T value;
            UnsafeUtility.CopyPtrToStructure (ptr, out value);
            return value;
        }

        public void SetComponentData<T>(Entity entity, T componentData) where T: struct, IComponentData
        {
            var typeIndex = TypeManager.GetTypeIndex<T>();
            m_Entities->AssertEntityHasComponent(entity, typeIndex);

            ComponentJobSafetyManager.CompleteReadAndWriteDependency(typeIndex);

            var ptr = m_Entities->GetComponentDataWithType (entity, typeIndex);
            UnsafeUtility.CopyStructureToPtr (ref componentData, ptr);
        }

        internal void SetComponentRaw(Entity entity, int typeIndex, void* data, int size)
        {
            m_Entities->AssertEntityHasComponent(entity, typeIndex);

            ComponentJobSafetyManager.CompleteReadAndWriteDependency(typeIndex);

            var ptr = m_Entities->GetComponentDataWithType (entity, typeIndex);
            UnsafeUtility.MemCpy(ptr, data, size);
        }

        internal void SetComponentObject(Entity entity, ComponentType componentType, object componentObject)
        {
            m_Entities->AssertEntityHasComponent(entity, componentType.TypeIndex);

            Chunk* chunk;
            int chunkIndex;
            m_Entities->GetComponentChunk(entity, out chunk, out chunkIndex);
            m_ArchetypeManager.SetManagedObject(chunk, componentType, chunkIndex, componentObject);
        }

        public void GetAllUniqueSharedComponentDatas<T>(List<T> sharedComponentValues)
            where T : struct, ISharedComponentData
        {
            m_SharedComponentManager.GetAllUniqueSharedComponents(sharedComponentValues);
        }

        public T GetSharedComponentData<T>(Entity entity) where T : struct, ISharedComponentData
        {
            var typeIndex = TypeManager.GetTypeIndex<T>();
            m_Entities->AssertEntityHasComponent(entity, typeIndex);

            var sharedComponentIndex = m_Entities->GetSharedComponentDataIndex(entity, typeIndex);
            return m_SharedComponentManager.GetSharedComponentData<T>(sharedComponentIndex);
        }

        public void AddSharedComponentData<T>(Entity entity, T componentData) where T : struct, ISharedComponentData
        {
            //TODO: optimize this (no need to move the entity to a new chunk twice)
            AddComponent(entity, ComponentType.Create<T>());
            SetSharedComponentData(entity, componentData);
        }

        public void SetSharedComponentData<T>(Entity entity, T componentData) where T: struct, ISharedComponentData
        {
            BeforeStructuralChange();

            var typeIndex = TypeManager.GetTypeIndex<T>();
            m_Entities->AssertEntityHasComponent(entity, typeIndex);

            var newSharedComponentDataIndex = m_SharedComponentManager.InsertSharedComponent(componentData);
            m_Entities->SetSharedComponentDataIndex(m_ArchetypeManager, m_SharedComponentManager, entity, typeIndex, newSharedComponentDataIndex);
            m_SharedComponentManager.RemoveReference(newSharedComponentDataIndex);
        }

        public NativeArray<T> GetFixedArray<T>(Entity entity) where T : struct
        {
            var typeIndex = TypeManager.GetTypeIndex<T>();
#if ENABLE_UNITY_COLLECTIONS_CHECKS
            m_Entities->AssertEntityHasComponent(entity, typeIndex);
            if (TypeManager.GetComponentType<T>().Category != TypeManager.TypeCategory.OtherValueType)
                throw new ArgumentException($"GetComponentFixedArray<{typeof(T)}> may not be IComponentData or ISharedComponentData");
#endif

            ComponentJobSafetyManager.CompleteWriteDependency(typeIndex);

            byte* ptr;
            int length;
            m_Entities->GetComponentDataWithTypeAndFixedArrayLength (entity, typeIndex, out ptr, out length);

            var array = NativeArrayUnsafeUtility.ConvertExistingDataToNativeArray<T>(ptr, length, Allocator.Invalid);

#if ENABLE_UNITY_COLLECTIONS_CHECKS
            NativeArrayUnsafeUtility.SetAtomicSafetyHandle(ref array, ComponentJobSafetyManager.GetSafetyHandle(typeIndex, false));
#endif

            return array;
        }

        public int GetComponentOrderVersion<T>()
        {
            return m_Entities->GetComponentTypeOrderVersion(TypeManager.GetTypeIndex<T>());
        }

        public int GetSharedComponentOrderVersion<T>(T sharedComponent) where T : struct, ISharedComponentData
        {
            return m_SharedComponentManager.GetSharedComponentVersion(sharedComponent);
        }

        internal ComponentJobSafetyManager ComponentJobSafetyManager { get; private set; }

        public ExclusiveEntityTransaction BeginExclusiveEntityTransaction()
        {
            ComponentJobSafetyManager.BeginExclusiveTransaction();
#if ENABLE_UNITY_COLLECTIONS_CHECKS
            m_ExclusiveEntityTransaction.SetAtomicSafetyHandle(ComponentJobSafetyManager.ExclusiveTransactionSafety);
#endif
            return m_ExclusiveEntityTransaction;
        }

        public JobHandle ExclusiveEntityTransactionDependency
        {
            get { return ComponentJobSafetyManager.ExclusiveTransactionDependency; }
            set { ComponentJobSafetyManager.ExclusiveTransactionDependency = value; }
        }

        public void EndExclusiveEntityTransaction()
        {
            ComponentJobSafetyManager.EndExclusiveTransaction();
        }

        void BeforeStructuralChange()
        {
#if ENABLE_UNITY_COLLECTIONS_CHECKS
            if (ComponentJobSafetyManager.IsInTransaction)
                throw new InvalidOperationException("Access to EntityManager is not allowed after EntityManager.BeginExclusiveEntityTransaction(); has been called.");
#endif
            ComponentJobSafetyManager.CompleteAllJobsAndInvalidateArrays();
        }

        //@TODO: Not clear to me what this method is really for...
        public void CompleteAllJobs()
        {
            ComponentJobSafetyManager.CompleteAllJobsAndInvalidateArrays();
        }

        public void MoveEntitiesFrom(EntityManager srcEntities)
        {
#if ENABLE_UNITY_COLLECTIONS_CHECKS
            if (srcEntities == this)
                throw new ArgumentException("srcEntities must not be the same as this EntityManager.");
#endif

            BeforeStructuralChange();
            srcEntities.BeforeStructuralChange();

            ArchetypeManager.MoveChunks(srcEntities.m_ArchetypeManager, srcEntities.m_Entities, srcEntities.m_SharedComponentManager, m_ArchetypeManager, m_GroupManager, m_SharedComponentManager, m_Entities, m_SharedComponentManager);

            //@TODO: Need to incrmeent the component versions based the moved chunks...
        }

        public void CheckInternalConsistency()
        {
#if ENABLE_UNITY_COLLECTIONS_CHECKS

            //@TODO: Validate from perspective of componentgroup...
            //@TODO: Validate shared component data refcounts...
            var entityCountEntityData = m_Entities->CheckInternalConsistency();
            var entityCountArchetypeManager = m_ArchetypeManager.CheckInternalConsistency();

            Assert.AreEqual(entityCountEntityData, entityCountArchetypeManager);
#endif
        }

        public List<Type> GetAssignableComponentTypes(Type interfaceType)
        {
            // #todo Cache this. It only can change when TypeManager.GetTypeCount() changes
            var componentTypeCount = TypeManager.GetTypeCount();
            var assignableTypes = new List<Type>();
            for (var i = 0; i < componentTypeCount; i++)
            {
                var type = TypeManager.GetType(i);
                if (interfaceType.IsAssignableFrom(type))
                {
                    assignableTypes.Add(type);
                }
            }
            return assignableTypes;
        }
    }
}
