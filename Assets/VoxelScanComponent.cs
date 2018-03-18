using Unity.Entities;
using Unity.Mathematics;

[System.Serializable]
struct VoxelScan : ISharedComponentData
{
    public float3 Extent;
    public int3 Resolution;
    public UnityEngine.Mesh Mesh;
    public UnityEngine.Material Material;
}

class VoxelScanComponent : SharedComponentDataWrapper<VoxelScan> { }
