using System;
using Unity.Entities;
	
[Serializable]
struct Spinner : IComponentData
{
	public int seed;
}
