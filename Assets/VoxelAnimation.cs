using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using Unity.Transforms;
using Unity.Mathematics;

class VoxelAnimationSystem : JobComponentSystem
{
    [ComputeJobOptimization]
    struct VoxelAnimation : IJobProcessComponentData<Voxel, TransformMatrix>
    {
        public float dt;

        public void Execute([ReadOnly] ref Voxel voxel, ref TransformMatrix matrix)
        {
            // Per-instance random number
            var rand1 = math.abs(new XXHash(voxel.ID).Value01(1));
            var rand2 = math.abs(new XXHash(voxel.ID).Value01(2));

            // Scale factor
            var scale = math.lerp(0.9f, 0.99f, rand1);

            // Current position
            var pos = math.mul(matrix.Value, new float4(0, 0, 0, 1)).xyz;

            // Transform matrices
            var m1 = math.translate(-pos);
            var m2 = math.scale(new float3(scale, scale, scale));
            var m3 = math.translate(pos + new float3(0.2f, -4, 0.6f) * rand2 * dt);

            // Apply these matrices.
            matrix = new TransformMatrix {
                Value = math.mul(m3, math.mul(m2, math.mul(m1, matrix.Value)))
            };
        }
    }

    protected override JobHandle OnUpdate(JobHandle inputDeps)
    {
        var job = new VoxelAnimation() { dt = UnityEngine.Time.deltaTime };
        return job.Schedule(this, 16, inputDeps);
    } 
}
