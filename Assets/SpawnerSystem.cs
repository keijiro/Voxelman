using Unity.Entities;
using Unity.Transforms;
using Unity.Collections;
using Unity.Mathematics;
using Unity.Rendering;

class SpawnerSystem : ComponentSystem
{
    struct SpawnerData
    {
        public Entity entity;
        public float3 position;
        public quaternion rotation;
        public int count;
    }

    private ComponentGroup m_SpawnerGroup;
    private EntityArchetype m_SpinnerArchetype;

    protected override void OnCreateManager(int capacity)
    {
        m_SpawnerGroup = GetComponentGroup(typeof(Spawner), typeof(Position), typeof(Rotation));
        m_SpinnerArchetype = EntityManager.CreateArchetype(
            typeof(Position), typeof(Rotation), typeof(TransformMatrix),
            typeof(Spinner), typeof(MeshInstanceRendererComponent));
    }

    protected override void OnUpdate()
    {
        var spawnerCount = m_SpawnerGroup.CalculateLength();
        var data = new NativeArray<SpawnerData>(spawnerCount, Allocator.Temp);

        {
            var entities = m_SpawnerGroup.GetEntityArray();
            var positions = m_SpawnerGroup.GetComponentDataArray<Position>();
            var rotations = m_SpawnerGroup.GetComponentDataArray<Rotation>();
            var spawners = m_SpawnerGroup.GetComponentDataArray<Spawner>();

            for (var i = 0; i != spawnerCount; i++)
            {
                data[i] = new SpawnerData
                {
                    entity = entities[i],
                    position = positions[i].Value,
                    rotation = rotations[i].Value,
                    count = spawners[i].count
                };
            }
        }

        var renderer = UnityEngine.GameObject.Find("Cube").GetComponent<MeshInstanceRendererComponent>().Value;
        var seed = 0;

        for (var i = 0; i != spawnerCount; i++)
        {
            var instance = EntityManager.CreateEntity(m_SpinnerArchetype);
            EntityManager.SetComponentData(instance, new Position {Value = data[i].position});
            EntityManager.SetComponentData(instance, new Rotation {Value = data[i].rotation});
            EntityManager.SetComponentData(instance, new Spinner {seed = seed++});
            EntityManager.AddSharedComponentData(instance, renderer);

            var cloneCount = data[i].count - 1;
            if (cloneCount > 0)
            {
                var clones = new NativeArray<Entity>(cloneCount, Allocator.Temp);
                EntityManager.Instantiate(instance, clones);

                for (var offs = 0; offs < cloneCount; offs++)
                    EntityManager.SetComponentData(clones[offs], new Spinner {seed = seed++});

                clones.Dispose();
            }

            EntityManager.RemoveComponent(data[i].entity, typeof(Spawner));
        }

        data.Dispose();
    }
}
