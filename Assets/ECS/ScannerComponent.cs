using Unity.Entities;
using Unity.Mathematics;

[System.Serializable]
struct Scanner : IComponentData
{
    public float3 Extent;
    public int3 Resolution;
}

class ScannerComponent : ComponentDataWrapper<Scanner> {}
