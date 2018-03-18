using Unity.Entities;
using Unity.Rendering;

// Voxel renderer component
// Instantiates lots of voxels for instanced mesh renderers.

[System.Serializable]
struct VoxelRenderer : ISharedComponentData
{
    public int MaxVoxelCount;
    public MeshInstanceRenderer RendererSettings;
}

class VoxelRendererComponent : SharedComponentDataWrapper<VoxelRenderer> {}
