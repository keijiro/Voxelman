using System.Collections.Generic;
using Unity.Entities;
using Unity.Transforms;
using Unity.Collections;
using Unity.Rendering;

// Voxel renderer system
// Instantiates lots of voxels for instanced mesh renderers.

class VoxelRendererSystem : ComponentSystem
{
    // Used for enumerate renderers
    List<VoxelRenderer> _uniques = new List<VoxelRenderer>();
    ComponentGroup _group;

    // Voxel archetype used for instantiation
    EntityArchetype _voxelArchetype;

    protected override void OnCreateManager(int capacity)
    {
        _group = GetComponentGroup(typeof(VoxelRenderer));

        _voxelArchetype = EntityManager.CreateArchetype(
            typeof(Voxel), typeof(TransformMatrix), typeof(MeshInstanceRenderer)
        );
    }

    protected override void OnUpdate()
    {
        // Enumerate all the renderers.
        EntityManager.GetAllUniqueSharedComponentDatas(_uniques);
        for (var i = 0; i < _uniques.Count; i++)
        {
            _group.SetFilter(_uniques[i]);

            // Get a copy of the entity array.
            // Don't use the iterator directly because we're going to remove
            // the renderer components; the iterator will be invalidated.
            var iterator = _group.GetEntityArray();
            var entities = new NativeArray<Entity>(iterator.Length, Allocator.Temp);
            iterator.CopyTo(entities);

            // Instantiate voxels within the entities.
            for (var j = 0; j < entities.Length; j++)
            {
                // Create the first voxel.
                var voxel = EntityManager.CreateEntity(_voxelArchetype);
                EntityManager.SetSharedComponentData(voxel, _uniques[i].RendererSettings);

                // Clone the first voxel.
                var cloneCount = _uniques[i].MaxVoxelCount - 1;
                if (cloneCount > 0)
                {
                    var clones = new NativeArray<Entity>(cloneCount, Allocator.Temp);
                    EntityManager.Instantiate(voxel, clones);
                    clones.Dispose();
                }

                // Remove the renderer component from the entity.
                EntityManager.RemoveComponent(entities[j], typeof(VoxelRenderer));
            }

            entities.Dispose();
        }

        _uniques.Clear();
    }
}
