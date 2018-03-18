using System.Collections.Generic;
using Unity.Entities;
using Unity.Transforms;
using Unity.Collections;
using Unity.Mathematics;
using Unity.Rendering;

class VoxelBufferSystem : ComponentSystem
{
    List<VoxelBuffer> _uniqueBuffers = new List<VoxelBuffer>();

    ComponentGroup _bufferGroup;

    // Voxel archetype used for instantiation
    EntityArchetype _voxelArchetype;

    protected override void OnCreateManager(int capacity)
    {
        _bufferGroup = GetComponentGroup(typeof(VoxelBuffer));

        // Voxel archetype: Voxel data, transform and instanced renderer.
        _voxelArchetype = EntityManager.CreateArchetype(
            typeof(Voxel), typeof(TransformMatrix), typeof(MeshInstanceRenderer)
        );
    }

    protected override void OnUpdate()
    {
        EntityManager.GetAllUniqueSharedComponentDatas(_uniqueBuffers);

        for (var i = 0; i < _uniqueBuffers.Count; i++)
        {
            _bufferGroup.SetFilter(_uniqueBuffers[i]);

            var entityArray = _bufferGroup.GetEntityArray();
            var entities = new NativeArray<Entity>(entityArray.Length, Allocator.Temp);
            entityArray.CopyTo(entities);

            for (var j = 0; j < entities.Length; j++)
            {
                var instance = EntityManager.CreateEntity(_voxelArchetype);
                EntityManager.SetSharedComponentData(instance, _uniqueBuffers[i].RendererSettings);

                var cloneCount = _uniqueBuffers[i].MaxVoxelCount - 1;
                if (cloneCount > 0)
                {
                    var clones = new NativeArray<Entity>(cloneCount, Allocator.Temp);
                    EntityManager.Instantiate(instance, clones);
                    clones.Dispose();
                }

                EntityManager.RemoveComponent(entities[j], typeof(VoxelBuffer));
            }

            entities.Dispose();
        }

        _uniqueBuffers.Clear();
    }
}
