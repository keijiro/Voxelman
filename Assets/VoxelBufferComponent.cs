using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;

struct VoxelBuffer : ISharedComponentData
{
    public NativeArray<float4> Buffer;
}

class VoxelBufferComponent : SharedComponentDataWrapper<VoxelBuffer> {}
