using System.Collections.Generic;
using Unity.Entities;
using Unity.Transforms;
using Unity.Collections;
using Unity.Rendering;

// Voxel buffer system
// Instantiates lots of voxels for instanced mesh renderers.

class VoxelBufferSystem : ComponentSystem
{
    // Used for enumerate buffer components
    List<VoxelBuffer> _uniques = new List<VoxelBuffer>();
    ComponentGroup _group;

    // Voxel archetype used for instantiation
    EntityArchetype _voxelArchetype;

    // Instance counter used for generating voxel IDs
    static uint _counter;

    protected override void OnCreateManager(int capacity)
    {
        _group = GetComponentGroup(typeof(VoxelBuffer));

        _voxelArchetype = EntityManager.CreateArchetype(
            typeof(Voxel), typeof(TransformMatrix), typeof(MeshInstanceRenderer)
        );
    }

    protected override void OnUpdate()
    {
        // Enumerate all the buffers.
        EntityManager.GetAllUniqueSharedComponentDatas(_uniques);
        for (var i = 0; i < _uniques.Count; i++)
        {
            _group.SetFilter(_uniques[i]);

            // Get a copy of the entity array.
            // Don't directly use the iterator -- we're going to remove
            // the buffer components, and it will invalidate the iterator.
            var iterator = _group.GetEntityArray();
            var entities = new NativeArray<Entity>(iterator.Length, Allocator.Temp);
            iterator.CopyTo(entities);

            // Instantiate voxels along with the buffer entities.
            for (var j = 0; j < entities.Length; j++)
            {
                // Create the first voxel.
                var voxel = EntityManager.CreateEntity(_voxelArchetype);
                EntityManager.SetComponentData(voxel, new Voxel { ID = _counter++ });
                EntityManager.SetSharedComponentData(voxel, _uniques[i].RendererSettings);

                // Make clones from the first voxel.
                var cloneCount = _uniques[i].MaxVoxelCount - 1;
                if (cloneCount > 0)
                {
                    var clones = new NativeArray<Entity>(cloneCount, Allocator.Temp);
                    EntityManager.Instantiate(voxel, clones);
                    for (var k = 0; k < cloneCount; k++)
                        EntityManager.SetComponentData(clones[k], new Voxel { ID = _counter++ });
                    clones.Dispose();
                }

                // Remove the buffer component from the entity.
                EntityManager.RemoveComponent(entities[j], typeof(VoxelBuffer));
            }

            entities.Dispose();
        }

        _uniques.Clear();
    }
}
