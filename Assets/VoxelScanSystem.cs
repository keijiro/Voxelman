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

        var commands = new NativeArray<RaycastCommand>(total, Allocator.TempJob, NativeArrayOptions.UninitializedMemory);
        var hits = new NativeArray<RaycastHit>(total, Allocator.TempJob, NativeArrayOptions.UninitializedMemory);

        var scale = ext / reso;
        var vz = new float3(0, 0, 1);

        var i = 0;
        for (var ix = 0; ix < reso.x; ix++)
        {
            var x = math.lerp(-ext.x, ext.x, (float)ix / reso.x);
            for (var iy = 0; iy < reso.y; iy++)
            {
                var y = math.lerp(-ext.y, ext.y, (float)iy / reso.y);
                var p = new float3(x, y, -ext.z);
                commands[i++] = new RaycastCommand(p, vz, ext.z * 2);
            }
        }

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
