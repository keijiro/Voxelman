using Unity.Entities;
using UnityEngine;

public class BoxAuthoring : MonoBehaviour
{
    class Baker : Baker<BoxAuthoring>
    {
        public override void Bake(BoxAuthoring src)
          => AddComponent(GetEntity(TransformUsageFlags.Dynamic), new Box());
    }
}

public struct Box : IComponentData
{
    public float Time;
    public float Velocity;
}
