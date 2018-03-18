using Unity.Entities;
using Unity.Rendering;

[System.Serializable]
struct VoxelBuffer : ISharedComponentData
{
    public int MaxVoxelCount;
    public MeshInstanceRenderer RendererSettings;
}

class VoxelBufferComponent : SharedComponentDataWrapper<VoxelBuffer> {}
