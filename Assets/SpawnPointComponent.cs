using System;
using Unity.Entities;
using Unity.Rendering;

[Serializable]
struct SpawnPoint : ISharedComponentData
{
    public int spawnCount;
    public MeshInstanceRenderer rendererSettings;
}

class SpawnPointComponent : SharedComponentDataWrapper<SpawnPoint> { }
