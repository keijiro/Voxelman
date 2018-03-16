using Unity.Entities;
using Unity.Collections;
using Unity.Jobs;
using Unity.Transforms;
using Unity.Mathematics;

class SpinnerSystem : JobComponentSystem
{
    // Data entry for injection
    struct Data
    {
        public int Length;
        [ReadOnly] public ComponentDataArray<Spinner> Spinners;
        public ComponentDataArray<Position> Positions;
        public ComponentDataArray<Rotation> Rotations;
    }

    [ComputeJobOptimization]
    struct JobData : IJobParallelFor
    {
        [ReadOnly] public ComponentDataArray<Spinner> Spinners;
        public ComponentDataArray<Position> Positions;
        public ComponentDataArray<Rotation> Rotations;

        public float Time;

        public void Execute(int index)
        {
            var time = Time;
            var hash = new XXHash(123);

            var spinner = Spinners[index];

            var noise = new float3(
                Perlin.Noise(spinner.Seed * 0.153f, time),
                Perlin.Noise(spinner.Seed * 1.374f, time),
                Perlin.Noise(spinner.Seed * 0.874f, time)
            );

            Positions[index] = new Position {
                Value = spinner.Origin + noise * spinner.Radius
            };

            Rotations[index] = new Rotation {
                Value = math.axisAngle(
                    GetRandomVector(hash, spinner.Seed + 10000),
                    hash.Range(-10.0f, 10.0f, spinner.Seed + 20000) * time
                )
            };
        }
    }

    [Inject] private Data m_Data;

    protected override void OnCreateManager(int capacity)
    {
    }

    protected override JobHandle OnUpdate(JobHandle inputDeps)
    {
        var jobs = new JobData {
            Spinners = m_Data.Spinners,
            Positions = m_Data.Positions,
            Rotations = m_Data.Rotations,
            Time = UnityEngine.Time.time
        };

        return jobs.Schedule(m_Data.Length, 64, inputDeps);
    }

    static float3 GetRandomVector(XXHash hash, int seed)
    {
        return new float3(
            hash.Range(-1.0f, 1.0f, seed),
            hash.Range(-1.0f, 1.0f, seed + 1000000),
            hash.Range(-1.0f, 1.0f, seed + 2000000)
        );
    }
}
