using Unity.Entities;
using Unity.Mathematics;

// Scanner system
// Moves old voxels to points where rays hit colliders.

[System.Serializable]
struct Scanner : IComponentData
{
    public float3 Extent;
    public int2 Resolution;
}

class ScannerComponent : ComponentDataWrapper<Scanner> {}
