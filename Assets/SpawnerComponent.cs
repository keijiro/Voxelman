using System;
using Unity.Entities;
	
[Serializable]
struct Spawner : IComponentData
{
	public int count;
}

class SpawnerComponent : ComponentDataWrapper<Spawner> { }
