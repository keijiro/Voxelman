using Unity.Entities;
using Unity.Collections;
using Unity.Transforms;
using Unity.Mathematics;

class SpinnerSystem : ComponentSystem
{
    // Data entry for injection
    struct Data
    {
        public int Length;
        [ReadOnly] public ComponentDataArray<Spinner> Spinners;
        [ReadOnly] public SharedComponentDataArray<SpinnerOrigin> Origins;
        public ComponentDataArray<Position> Positions;
        public ComponentDataArray<Rotation> Rotations;
    }

    [Inject] private Data m_Data;

    // Random number generator
    static private XXHash m_Hash;

    protected override void OnCreateManager(int capacity)
    {
        m_Hash = new XXHash(123);
    }

    protected override void OnUpdate()
    {
        var time = UnityEngine.Time.time;

        for (var i = 0; i < m_Data.Length; i++)
        {
            var spinner = m_Data.Spinners[i];

            var noise = new float3(
                Perlin.Noise(spinner.Seed * 0.153f, time),
                Perlin.Noise(spinner.Seed * 1.374f, time),
                Perlin.Noise(spinner.Seed * 0.874f, time)
            );

            m_Data.Positions[i] = new Position {
                Value = m_Data.Origins[i].Origin + noise * m_Data.Origins[i].Radius
            };

            m_Data.Rotations[i] = new Rotation {
                Value = math.axisAngle(
                    GetRandomVector(spinner.Seed + 10000),
                    m_Hash.Range(-10.0f, 10.0f, spinner.Seed + 20000) * time
                )
            };
        }
    }

    static float3 GetRandomVector(int seed)
    {
        return new float3(
            m_Hash.Range(-1.0f, 1.0f, seed),
            m_Hash.Range(-1.0f, 1.0f, seed + 1000000),
            m_Hash.Range(-1.0f, 1.0f, seed + 2000000)
        );
    }
}
