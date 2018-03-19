using System.Collections.Generic;
using Unity.Entities;
using Unity.Collections;
using Unity.Jobs;
using Unity.Transforms;
using Unity.Mathematics;
using UnityEngine;

class VoxelScanSystem : JobComponentSystem
{
    ComponentGroup _scanGroup;
    ComponentGroup _voxelGroup;

    [ComputeJobOptimization]
    struct SetupJob : IJobParallelFor
    {
        public NativeArray<RaycastCommand> Commands;

        public float3 Extent;
        public int3 Resolution;

        public void Execute(int i)
        {
            var ix = i % Resolution.x;
            var iy = i / Resolution.x;

            var x = math.lerp(-Extent.x, Extent.x, (float)ix / Resolution.x);
            var y = math.lerp(-Extent.y, Extent.y, (float)iy / Resolution.y);

            var p = new float3(x, y, -Extent.z);
            Commands[i] = new RaycastCommand(p, new float3(0, 0, 1), Extent.z * 2);
        }
    }

    // A job that transfers raycast results to a voxel.
    struct TransferJob : IJob
    {
        [DeallocateOnJobCompletion] [ReadOnly] public NativeArray<RaycastCommand> RaycastCommands;
        [DeallocateOnJobCompletion] [ReadOnly] public NativeArray<RaycastHit> RaycastHits;

        public ComponentDataArray<TransformMatrix> Transforms;
        public float Scale;

        public void Execute()
        {
            var count = 0;

            for (var i = 0; i < RaycastHits.Length && count < Transforms.Length; i++)
            {
                if (RaycastHits[i].distance > 0)
                {
                    var from = (float3)RaycastCommands[i].from;
                    var dist = RaycastHits[i].distance;
                    var p = from + new float3(0, 0, dist);
                    Transforms[count++] = new TransformMatrix { Value = new float4x4(
                        new float4(Scale, 0, 0, 0),
                        new float4(0, Scale, 0, 0),
                        new float4(0, 0, Scale, 0),
                        new float4(p.x, p.y, p.z, 1)) };
                }
            }

            var nullMatrix = math.translate(new float3(1000, 1000, 1000));

            while (count < Transforms.Length)
            {
                Transforms[count++] = new TransformMatrix { Value = nullMatrix };
            }
        }
    }

    JobHandle BuildJob(float3 origin, float3 ext, int3 reso, JobHandle deps)
    {
        var total = reso.x * reso.y;

        var commands = new NativeArray<RaycastCommand>(total, Allocator.TempJob);
        var hits = new NativeArray<RaycastHit>(total, Allocator.TempJob);

        var setupJob = new SetupJob {
            Commands = commands,
            Extent = ext,
            Resolution = reso
        };

        deps = setupJob.Schedule(total, 1, deps);

        // Raycast job
        deps = RaycastCommand.ScheduleBatch(commands, hits, 1, deps);

        // Transfer results to voxels.
        var dest = _voxelGroup.GetComponentDataArray<TransformMatrix>();

        var transferJob = new TransferJob {
            RaycastCommands = commands,
            RaycastHits = hits,
            Scale = ext.x * 2 / reso.x,
            Transforms = dest
        };

        return transferJob.Schedule(deps);
    }

    protected override void OnCreateManager(int capacity)
    {
        _scanGroup = GetComponentGroup(typeof(VoxelScan), typeof(Position));
        _voxelGroup = GetComponentGroup(typeof(Voxel), typeof(TransformMatrix));
    }

    protected override JobHandle OnUpdate(JobHandle inputDeps)
    {
        var scans = _scanGroup.GetComponentDataArray<VoxelScan>();
        var origins = _scanGroup.GetComponentDataArray<Position>();

        for (var i = 0; i < scans.Length; i++)
            inputDeps = BuildJob(origins[i].Value, scans[i].Extent, scans[i].Resolution, inputDeps);

        return inputDeps;
    }
}
