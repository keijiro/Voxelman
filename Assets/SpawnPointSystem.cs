using Unity.Entities;
using Unity.Transforms;
using Unity.Collections;
using Unity.Mathematics;
using Unity.Rendering;

// System implementation for processing SpawnPoints

class SpawnSystem : ComponentSystem
{
    // Spawn point data entry for temporary use
    struct TempData
    {
        public int SharedDataIndex;
        public Entity SourceEntity;
        public float3 Position;
    }

    // Spawn point data group used for querying
    private ComponentGroup m_MainGroup;

    // Spinner archetype used for instantiation
    private EntityArchetype m_SpinnerArchetype;

    protected override void OnCreateManager(int capacity)
    {
        // Spawn point data group: Only requires SpawnPoint and Position.
        m_MainGroup = GetComponentGroup(typeof(SpawnPoint), typeof(Position));

        // Spinner archetype: Spinner data, transform and instanced renderer.
        m_SpinnerArchetype = EntityManager.CreateArchetype(
            typeof(Spinner), typeof(SpinnerOrigin),
            typeof(Position), typeof(Rotation), typeof(TransformMatrix),
            typeof(MeshInstanceRenderer)
        );
    }

    protected override void OnUpdate()
    {
        // Spawn point data can be shared between spawn point instances.
        // Enumerate all the unique data first.
        var sharedDatas = new System.Collections.Generic.List<SpawnPoint>(10);
        EntityManager.GetAllUniqueSharedComponentDatas(sharedDatas);
        
        // Calculate the total count of the spawn points.
        var spawnCount = 0;
        for (var sharedIndex = 0; sharedIndex < sharedDatas.Count; sharedIndex++)
        {
            m_MainGroup.SetFilter(sharedDatas[sharedIndex]);
            spawnCount += m_MainGroup.CalculateLength();
        }
        
        // Do nothing if no one is there.
        if (spawnCount == 0) return;

        // Build a simple plain array of spawn points.
        var tempDatas = new NativeArray<TempData>(spawnCount, Allocator.Temp);

        spawnCount = 0;

        for (var sharedIndex = 0; sharedIndex < sharedDatas.Count; sharedIndex++)
        {
            m_MainGroup.SetFilter(sharedDatas[sharedIndex]);
            
            var entities = m_MainGroup.GetEntityArray();
            var positions = m_MainGroup.GetComponentDataArray<Position>();

            for (var i = 0; i < entities.Length; i++)
            {
                tempDatas[spawnCount++] = new TempData
                {
                    SharedDataIndex = sharedIndex,
                    SourceEntity = entities[i],
                    Position = positions[i].Value
                };
            }
        }

        // Spawn spinners at the each spawn point.
        var seed = 1;

        for (var i = 0; i < spawnCount; i++)
        {
            var sharedIndex = tempDatas[i].SharedDataIndex;

            // Create the first entity.
            var instance = EntityManager.CreateEntity(m_SpinnerArchetype);

            EntityManager.SetComponentData(instance, new Position { Value = tempDatas[i].Position });
            EntityManager.SetComponentData(instance, new Spinner { Seed = seed++ });

            EntityManager.SetSharedComponentData(instance, sharedDatas[sharedIndex].RendererSettings);

            EntityManager.SetSharedComponentData(instance, new SpinnerOrigin {
                Origin = tempDatas[i].Position,
                Radius = sharedDatas[sharedIndex].Radius
            });

            // Clone the first entity.
            var cloneCount = sharedDatas[sharedIndex].SpawnCount - 1;
            if (cloneCount > 0)
            {
                var clones = new NativeArray<Entity>(cloneCount, Allocator.Temp);
                EntityManager.Instantiate(instance, clones);

                // Set unique data.
                for (var offs = 0; offs < cloneCount; offs++)
                    EntityManager.SetComponentData(clones[offs], new Spinner { Seed = seed++ });

                clones.Dispose();
            }

            // Remove the spawn point data from the source spawner entity.
            EntityManager.RemoveComponent(tempDatas[i].SourceEntity, typeof(SpawnPoint));
        }
        
        tempDatas.Dispose();
    }
}
