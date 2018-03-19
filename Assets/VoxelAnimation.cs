using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using Unity.Transforms;
using Unity.Mathematics;

class AnimationSystem : JobComponentSystem
{
    [ComputeJobOptimization]
    struct Animation : IJobProcessComponentData<Voxel, TransformMatrix>
    {
        public float dt;

        public void Execute([ReadOnly] ref Voxel voxel, ref TransformMatrix matrix)
        {
            var p = math.mul(matrix.Value, new float4(0, 0, 0, 1)).xyz;

            var m1 = math.translate(-p);
            var m2 = math.scale(new float3(0.95f, 0.95f, 0.95f));
            var m3 = math.translate(p + new float3(0, -0.4f * dt, 0));

            matrix = new TransformMatrix {
                Value = math.mul(m3, math.mul(m2, math.mul(m1, matrix.Value)))
            };
        }
    }

    protected override JobHandle OnUpdate(JobHandle inputDeps)
    {
        var job = new Animation() { dt = UnityEngine.Time.deltaTime };
        return job.Schedule(this, 16, inputDeps);
    } 
}
