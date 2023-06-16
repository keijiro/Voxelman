using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;
using Random = Unity.Mathematics.Random;

public class SpawnerAuthoring : MonoBehaviour
{
    [SerializeField] GameObject _prefab = null;
    [SerializeField] float3 _extent = 1;
    [SerializeField] LayerMask _mask = 0;
    [SerializeField] float _frequency = 100;
    [SerializeField] uint _seed = 0xdeadbeef;

    class Baker : Baker<SpawnerAuthoring>
    {
        public override void Bake(SpawnerAuthoring src)
        {
            var data = new Spawner()
            {
                Prefab = GetEntity(src._prefab, TransformUsageFlags.Dynamic),
                Extent = src._extent,
                Mask = src._mask.value,
                Frequency = src._frequency,
                Random = new Random(src._seed)
            };
            AddComponent(GetEntity(TransformUsageFlags.Dynamic), data);
        }
    }
}

struct Spawner : IComponentData
{
    public Entity Prefab;
    public float3 Extent;
    public int Mask;
    public float Frequency;
    public Random Random;
    public float Timer;
}
