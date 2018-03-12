using Boo.Lang;
using Unity.Entities;
using Unity.Transforms;
using Unity.Collections;
using Unity.Mathematics;
using Unity.Rendering;

class SpawnerSystem : ComponentSystem
{
    struct TempData
    {
        public int sharedDataIndex;
        public Entity sourceEntity;
        public float3 position;
        public quaternion rotation;
    }

    private XXHash m_Hash;
    private ComponentGroup m_MainGroup;
    private EntityArchetype m_SpinnerArchetype;

    protected override void OnCreateManager(int capacity)
    {
        m_Hash = new XXHash(100);
        m_MainGroup = GetComponentGroup(typeof(SpawnPoint), typeof(Position), typeof(Rotation));
        m_SpinnerArchetype = EntityManager.CreateArchetype(
            typeof(Position), typeof(Rotation), typeof(TransformMatrix),
            typeof(Spinner), typeof(MeshInstanceRendererComponent));
    }

    protected override void OnUpdate()
    {
        var sharedDatas = new System.Collections.Generic.List<SpawnPoint>(10);
        EntityManager.GetAllUniqueSharedComponentDatas(sharedDatas);
        
        var spawnCount = 0;
        for (var sharedIndex = 0; sharedIndex < sharedDatas.Count; sharedIndex++)
        {
            m_MainGroup.SetFilter(sharedDatas[sharedIndex]);
            spawnCount += m_MainGroup.CalculateLength();
        }
        
        if (spawnCount == 0) return;
        
        var tempDatas = new NativeArray<TempData>(spawnCount, Allocator.Temp);
        
        spawnCount = 0;
        for (var sharedIndex = 0; sharedIndex < sharedDatas.Count; sharedIndex++)
        {
            m_MainGroup.SetFilter(sharedDatas[sharedIndex]);
            
            var entities = m_MainGroup.GetEntityArray();
            var positions = m_MainGroup.GetComponentDataArray<Position>();
            var rotations = m_MainGroup.GetComponentDataArray<Rotation>();

            for (var i = 0; i < entities.Length; i++)
            {
                tempDatas[spawnCount++] = new TempData
                {
                    sharedDataIndex = sharedIndex,
                    sourceEntity = entities[i],
                    position = positions[i].Value,
                    rotation = rotations[i].Value
                };
            }
        }
        
        var seed = 1;

        for (var i = 0; i < spawnCount; i++)
        {
            var sharedIndex = tempDatas[i].sharedDataIndex;
            
            var instance = EntityManager.CreateEntity(m_SpinnerArchetype);
            EntityManager.SetComponentData(instance, new Position {Value = tempDatas[i].position});
            EntityManager.SetComponentData(instance, new Rotation {Value = tempDatas[i].rotation});
            EntityManager.SetComponentData(instance, new Spinner {seed = seed++});
            EntityManager.AddSharedComponentData(instance, sharedDatas[sharedIndex].rendererSettings);

            var cloneCount = sharedDatas[sharedIndex].spawnCount - 1;
            if (cloneCount > 0)
            {
                var clones = new NativeArray<Entity>(cloneCount, Allocator.Temp);
                EntityManager.Instantiate(instance, clones);

                for (var offs = 0; offs < cloneCount; offs++)
                {
                    var pos = tempDatas[i].position + new float3(
                        m_Hash.Range(-5.0f, 5.0f, seed++),
                        m_Hash.Range(-5.0f, 5.0f, seed++),
                        m_Hash.Range(-5.0f, 5.0f, seed++));
                    EntityManager.SetComponentData(clones[offs], new Position {Value = pos});
                    EntityManager.SetComponentData(clones[offs], new Spinner {seed = seed++});
                }

                clones.Dispose();
            }

            EntityManager.RemoveComponent(tempDatas[i].sourceEntity, typeof(SpawnPoint));
        }
        
        tempDatas.Dispose();
    }
}
