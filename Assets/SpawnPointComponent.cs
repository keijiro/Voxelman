using Unity.Entities;
using Unity.Rendering;
using Unity.Mathematics;

// Hybrid style spawn point component

[System.Serializable]
struct SpawnPoint : ISharedComponentData
{
    public float3 Extent;
    public int3 Resolution;
    public MeshInstanceRenderer RendererSettings;
}

class SpawnPointComponent : SharedComponentDataWrapper<SpawnPoint> { }
