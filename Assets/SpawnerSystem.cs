using Unity.Entities;
using Unity.Transforms;
using Unity.Collections;
using Unity.Mathematics;
using Unity.Rendering;
using UnityEngine;

class SpawnerSystem : ComponentSystem
{
    struct Data
    {
        public Entity entity;
        public float3 position;
        public int count;
    }

    private ComponentGroup m_Group;
    
    protected override void OnCreateManager(int capacity)
    {
        m_Group = GetComponentGroup(typeof(Spawner), typeof(Position));
    }

    protected override void OnUpdate()
    {
        var entities = m_Group.GetEntityArray();
        var positions = m_Group.GetComponentDataArray<Position>();
        var spawners = m_Group.GetComponentDataArray<Spawner>();

        var data = new NativeArray<Data>(spawners.Length, Allocator.Temp);

        for (var i = 0; i != spawners.Length; i++)
        {
            data[i] = new Data
            {
                entity = entities[i],
                position = positions[i].Value,
                count = spawners[i].count
            };
        }

        var renderer = UnityEngine.GameObject.Find("Cube").GetComponent<MeshInstanceRendererComponent>().Value;

        for (var i = 0; i != data.Length; i++)
        {
            var instance = EntityManager.CreateEntity();
            EntityManager.AddComponent(instance, typeof(Position));
            EntityManager.AddComponent(instance, typeof(Spinner));
            EntityManager.SetComponentData(instance, new Position { Value = 0 });
            EntityManager.SetComponentData(instance, new Spinner { seed = 123 });
            EntityManager.AddSharedComponentData(instance, renderer);
            EntityManager.AddComponentData(instance, new TransformMatrix { Value = Matrix4x4.identity });
            
            var clones = new NativeArray<Entity>(data[i].count, Allocator.Temp);
            EntityManager.Instantiate(instance, clones);
            clones.Dispose();
            
            EntityManager.RemoveComponent(data[i].entity, typeof(Spawner));
        }

        data.Dispose();
    }
}
