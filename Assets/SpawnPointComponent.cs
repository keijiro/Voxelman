using Unity.Entities;
using Unity.Rendering;

// Hybrid style spawn point component

[System.Serializable]
struct SpawnPoint : ISharedComponentData
{
    public int SpawnCount;
    public float Radius;
    public MeshInstanceRenderer RendererSettings;
}

class SpawnPointComponent : SharedComponentDataWrapper<SpawnPoint> { }
