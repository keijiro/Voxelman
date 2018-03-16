using Unity.Entities;
using Unity.Mathematics;
	
// Spinner instance data
struct Spinner : IComponentData
{
	public int Seed;
    public float3 Origin;
    public float Radius;
}
