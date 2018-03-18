using Unity.Entities;
using Unity.Mathematics;

[System.Serializable]
struct VoxelScan : IComponentData
{
    public float3 Extent;
    public int3 Resolution;
}

class VoxelScanComponent : ComponentDataWrapper<VoxelScan> {}
