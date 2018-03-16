#define USE_JOB_SYSTEM

using Unity.Entities;
using Unity.Collections;
using Unity.Jobs;
using Unity.Transforms;
using Unity.Mathematics;

#if USE_JOB_SYSTEM

class SpinnerSystem : JobComponentSystem
{
    [ComputeJobOptimization]
    struct JobSpinner : IJobProcessComponentData<Spinner, Position, Rotation>
    {
        public float Time;

        public void Execute([ReadOnly] ref Spinner spinner, ref Position position, ref Rotation rotation)
        {
            var hash = new XXHash(123);

            var noise = new float3(
                Perlin.Noise(spinner.Seed * 0.153f, Time),
                Perlin.Noise(spinner.Seed * 1.374f, Time),
                Perlin.Noise(spinner.Seed * 0.874f, Time)
            );

            position = new Position {
                Value = spinner.Origin + noise * spinner.Radius
            };

            rotation = new Rotation {
                Value = math.axisAngle(
                    GetRandomVector(hash, spinner.Seed + 10000),
                    hash.Range(-10.0f, 10.0f, spinner.Seed + 20000) * Time
                )
            };
        }
    }

    protected override JobHandle OnUpdate(JobHandle inputDeps)
    {
        var job = new JobSpinner { Time = UnityEngine.Time.time };
        return job.Schedule(this, 64, inputDeps);
    }

#else

class SpinnerSystem : ComponentSystem
{
    // Data entry for injection
    struct Data
    {
        public int Length;
        [ReadOnly] public ComponentDataArray<Spinner> Spinners;
        public ComponentDataArray<Position> Positions;
        public ComponentDataArray<Rotation> Rotations;
    }

    [Inject] private Data m_Data;

    protected override void OnUpdate()
    {
        var hash = new XXHash(123);
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
                Value = spinner.Origin + noise * spinner.Radius
            };

            m_Data.Rotations[i] = new Rotation {
                Value = math.axisAngle(
                    GetRandomVector(hash, spinner.Seed + 10000),
                    hash.Range(-10.0f, 10.0f, spinner.Seed + 20000) * time
                )
            };
        }
    }

#endif

    static float3 GetRandomVector(XXHash hash, int seed)
    {
        return new float3(
            hash.Range(-1.0f, 1.0f, seed),
            hash.Range(-1.0f, 1.0f, seed + 1000000),
            hash.Range(-1.0f, 1.0f, seed + 2000000)
        );
    }
}
