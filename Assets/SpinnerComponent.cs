using Unity.Entities;
using Unity.Mathematics;
	
// Spinner instance data
struct Spinner : IComponentData
{
	public int Seed;
}

// Spinner shared data
struct SpinnerOrigin : ISharedComponentData
{
    public float Radius;
    public float3 Origin;
}
