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

        public ComponentDataArray<Position> Positions;

        public void Execute()
        {
            var i1 = 0;
            var i2 = 0;

            while (i1 < RaycastHits.Length && i2 < Positions.Length)
            {
                if (RaycastHits[i1].distance > 0)
                {
                    var p = RaycastCommands[i1].from + RaycastCommands[i1].direction * RaycastCommands[i1].distance / 2;
                    Positions[i2++] = new Position { Value = new float3(p.x, p.y, p.z) };
                    i1 = (i1 / 6 + 1) * 6;
                }
                else
                {
                    i1++;
                }
            }

            while (i2 < Positions.Length)
            {
                Positions[i2++] = new Position { Value = new float3(1000, 1000, 1000) };
            }
        }
    }

    JobHandle BuildJob(float3 origin, float3 ext, int3 reso, JobHandle deps)
    {
        var total = reso.x * reso.y * reso.z * 6;

        var commands = new NativeArray<RaycastCommand>(total, Allocator.TempJob, NativeArrayOptions.UninitializedMemory);
        var hits = new NativeArray<RaycastHit>(total, Allocator.TempJob, NativeArrayOptions.UninitializedMemory);

        var scale = ext / reso;
        var dist = math.max(math.max(scale.x, scale.y), scale.z);

        var i = 0;
        for (var ix = 0; ix < reso.x; ix++)
        {
            var x = math.lerp(-ext.x, ext.x, (float)ix / reso.x);
            for (var iy = 0; iy < reso.y; iy++)
            {
                var y = math.lerp(-ext.y, ext.y, (float)iy / reso.y);
                for (var iz = 0; iz < reso.z; iz++)
                {
                    var z = math.lerp(-ext.z, ext.z, (float)iz / reso.z);
                    var p = new float3(x, y, z);

                    var d = new float3(1, 0, 0);
                    commands[i++] = new RaycastCommand(p - d * dist, +d, dist * 2);
                    commands[i++] = new RaycastCommand(p + d * dist, -d, dist * 2);

                    d = new float3(0, 1, 0);
                    commands[i++] = new RaycastCommand(p - d * dist, +d, dist * 2);
                    commands[i++] = new RaycastCommand(p + d * dist, -d, dist * 2);

                    d = new float3(0, 0, 1);
                    commands[i++] = new RaycastCommand(p - d * dist, +d, dist * 2);
                    commands[i++] = new RaycastCommand(p + d * dist, -d, dist * 2);
                }
            }
        }

        // Raycast job
        deps = RaycastCommand.ScheduleBatch(commands, hits, 64, deps);

        // Transfer results to voxels.
        var dest = _voxelGroup.GetComponentDataArray<Position>();

        var transferJob = new TransferJob {
            RaycastCommands = commands,
            RaycastHits = hits,
            Positions = dest
        };

        return transferJob.Schedule(deps);
    }

    protected override void OnCreateManager(int capacity)
    {
        _scanGroup = GetComponentGroup(typeof(VoxelScan), typeof(Position));
        _voxelGroup = GetComponentGroup(typeof(Voxel), typeof(Position));
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
