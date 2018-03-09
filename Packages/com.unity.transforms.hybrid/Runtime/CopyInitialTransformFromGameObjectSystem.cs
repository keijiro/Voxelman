using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using UnityEngine.Jobs;

namespace Unity.Transforms
{
    public class CopyInitialTransformFromGameObjectSystem : JobComponentSystem
    {
        [Inject] ComponentDataFromEntity<LocalPosition> m_LocalPositions;
        [Inject] ComponentDataFromEntity<LocalRotation> m_LocalRotations;
        [Inject] ComponentDataFromEntity<Position> m_Positions;
        [Inject] ComponentDataFromEntity<Rotation> m_Rotations;

        struct TransformStash
        {
            public float3 localPosition;
            public float3 position;
            public quaternion localRotation;
            public quaternion rotation;
        }

        [ComputeJobOptimization]
        struct StashTransforms : IJobParallelForTransform
        {
            public NativeArray<TransformStash> transformStashes;

            public void Execute(int index, TransformAccess transform)
            {
                transformStashes[index] = new TransformStash
                {
                    localPosition  = transform.localPosition,
                    rotation       = transform.rotation,
                    position       = transform.position,
                    localRotation  = transform.localRotation,
                };
            }
        }

        [ComputeJobOptimization]
        struct CopyTransforms : IJobParallelFor
        {
            [NativeDisableParallelForRestriction] public ComponentDataFromEntity<LocalPosition> localPositions;
            [NativeDisableParallelForRestriction] public ComponentDataFromEntity<LocalRotation> localRotations;
            [NativeDisableParallelForRestriction] public ComponentDataFromEntity<Position> positions;
            [NativeDisableParallelForRestriction] public ComponentDataFromEntity<Rotation> rotations;
            public EntityArray entities;
            [DeallocateOnJobCompletion] public NativeArray<TransformStash> transformStashes;

            public void Execute(int index)
            {
                var transformStash = transformStashes[index];
                var entity = entities[index];
                if (positions.Exists(entity))
                {
                    positions[entity] = new Position { Value = transformStash.position };
                }
                if (rotations.Exists(entity))
                {
                    rotations[entity] = new Rotation { Value = transformStash.rotation };
                }
                if (localPositions.Exists(entity))
                {
                    localPositions[entity] = new LocalPosition { Value = transformStash.localPosition };
                }
                if (localRotations.Exists(entity))
                {
                    localRotations[entity] = new LocalRotation { Value = transformStash.localRotation };
                }
            }
        }

        struct RemoveCopyInitialTransformFromGameObjectComponent : IJob
        {
            public EntityArray entities;
            public EntityCommandBuffer entityCommandBuffer;
            
            public void Execute()
            {
                for (int i = 0; i < entities.Length; i++)
                {
                    entityCommandBuffer.RemoveComponent<CopyInitialTransformFromGameObject>(entities[i]);
                }
                
            }
        }

        [Inject] private EndFrameBarrier m_EndFrameBarrier;

        ComponentGroup m_InitialTransformGroup;

        protected override void OnCreateManager(int capacity)
        {
            m_InitialTransformGroup = GetComponentGroup(ComponentType.ReadOnly(typeof(CopyInitialTransformFromGameObject)),typeof(UnityEngine.Transform));
        }

        protected override JobHandle OnUpdate(JobHandle inputDeps)
        {
            var transforms = m_InitialTransformGroup.GetTransformAccessArray();
            var entities = m_InitialTransformGroup.GetEntityArray();

            var transformStashes = new NativeArray<TransformStash>(transforms.Length, Allocator.TempJob);
            var stashTransformsJob = new StashTransforms
            {
                transformStashes = transformStashes
            };

            var stashTransformsJobHandle = stashTransformsJob.Schedule(transforms, inputDeps);

            var copyTransformsJob = new CopyTransforms
            {
                positions = m_Positions,
                rotations = m_Rotations,
                localPositions = m_LocalPositions,
                localRotations = m_LocalRotations,
                transformStashes = transformStashes,
                entities = entities
            };

            var copyTransformsJobHandle = copyTransformsJob.Schedule(transformStashes.Length,64,stashTransformsJobHandle);

            var removeComponentsJob = new RemoveCopyInitialTransformFromGameObjectComponent
            {
                entities = entities,
                entityCommandBuffer = m_EndFrameBarrier.CreateCommandBuffer()
            };
            var removeComponentsJobHandle = removeComponentsJob.Schedule(copyTransformsJobHandle);
            return removeComponentsJobHandle;
        }
    }
}
