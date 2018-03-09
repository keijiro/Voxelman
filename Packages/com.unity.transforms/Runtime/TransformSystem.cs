using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;

namespace Unity.Transforms
{
    public class TransformSystem : JobComponentSystem
    {
        [Inject] [ReadOnly] ComponentDataFromEntity<LocalPosition> m_LocalPositions;
        [Inject] [ReadOnly] ComponentDataFromEntity<LocalRotation> m_LocalRotations;
        [Inject] ComponentDataFromEntity<Position> m_Positions;
        [Inject] ComponentDataFromEntity<Rotation> m_Rotations;
        [Inject] ComponentDataFromEntity<TransformMatrix> m_TransformMatrices;

        struct RootTransGroup
        {
            [ReadOnly] public SubtractiveComponent<Rotation> rotations;
            [ReadOnly] public SubtractiveComponent<TransformParent> parents;
            [ReadOnly] public ComponentDataArray<Position> positions;
            [ReadOnly] public EntityArray entities;
            public int Length;
        }

        [Inject] RootTransGroup m_RootTransGroup;
        
        struct RootRotGroup
        {
            [ReadOnly] public ComponentDataArray<Rotation> rotations;
            [ReadOnly] public SubtractiveComponent<TransformParent> parents;
            [ReadOnly] public SubtractiveComponent<Position> positions;
            [ReadOnly] public EntityArray entities;
            public int Length;
        }

        [Inject] RootRotGroup m_RootRotGroup;
        
        struct RootRotTransNoTransformGroup
        {
            [ReadOnly] public ComponentDataArray<Rotation> rotations;
            [ReadOnly] public SubtractiveComponent<TransformParent> parents;
            [ReadOnly] public ComponentDataArray<Position> positions;
            [ReadOnly] public EntityArray entities;
            [ReadOnly] public SubtractiveComponent<TransformMatrix> transforms;
            public int Length;
        }

        [Inject] RootRotTransNoTransformGroup m_RootRotTransNoTransformGroup;
        
        struct RootRotTransTransformGroup
        {
            [ReadOnly] public ComponentDataArray<Rotation> rotations;
            [ReadOnly] public SubtractiveComponent<TransformParent> parents;
            [ReadOnly] public ComponentDataArray<Position> positions;
            [ReadOnly] public EntityArray entities;
            public ComponentDataArray<TransformMatrix> transforms;
            public int Length;
        }

        [Inject] RootRotTransTransformGroup m_RootRotTransTransformGroup;

        struct ParentGroup
        {
            [ReadOnly] public ComponentDataArray<TransformParent> transformParents;
            [ReadOnly] public EntityArray entities;
            public int Length;
        }

        [Inject] ParentGroup m_ParentGroup;

        [ComputeJobOptimization]
        struct BuildHierarchy : IJobParallelFor
        {
            public NativeMultiHashMap<Entity, Entity>.Concurrent hierarchy;
            [ReadOnly] public ComponentDataArray<TransformParent> transformParents;
            [ReadOnly] public EntityArray entities;

            public void Execute(int index)
            {
                hierarchy.Add(transformParents[index].Value,entities[index]);
            }
        }
        
        [ComputeJobOptimization]
        struct CopyEntities : IJobParallelFor
        {
            [ReadOnly] public EntityArray source;
            public NativeArray<Entity> result;

            public void Execute(int index)
            {
                result[index] = source[index];
            }
        }

        [ComputeJobOptimization]
        struct UpdateRotTransTransformRoots : IJobParallelFor
        {
            [ReadOnly] public ComponentDataArray<Rotation> rotations;
            [ReadOnly] public ComponentDataArray<Position> positions;
            public NativeArray<float4x4> matrices;
            public ComponentDataArray<TransformMatrix> transforms;

            public void Execute(int index)
            {
                float4x4 matrix = math.rottrans(rotations[index].Value, positions[index].Value);
                matrices[index] = matrix;
                transforms[index] = new TransformMatrix {Value = matrix};
            }
        }
        
        [ComputeJobOptimization]
        struct UpdateRotTransNoTransformRoots : IJobParallelFor
        {
            [ReadOnly] public ComponentDataArray<Rotation> rotations;
            [ReadOnly] public ComponentDataArray<Position> positions;
            public NativeArray<float4x4> matrices;

            public void Execute(int index)
            {
                float4x4 matrix = math.rottrans(rotations[index].Value, positions[index].Value);
                matrices[index] = matrix;
            }
        }

        [ComputeJobOptimization]
        struct UpdateHierarchy : IJob
        {
            [ReadOnly] public NativeMultiHashMap<Entity, Entity> hierarchy;
            [DeallocateOnJobCompletion] [ReadOnly] public NativeArray<Entity> roots;
            [ReadOnly] public ComponentDataFromEntity<LocalPosition> localPositions;
            [ReadOnly] public ComponentDataFromEntity<LocalRotation> localRotations;

            public ComponentDataFromEntity<Position> positions;
            public ComponentDataFromEntity<Rotation> rotations;
            public ComponentDataFromEntity<TransformMatrix> transformMatrices;

            void TransformTree(Entity entity,float4x4 parentMatrix)
            {
                var position = new float3();
                var rotation = quaternion.identity;
                
                if (positions.Exists(entity))
                {
                    position = positions[entity].Value;
                }
                
                if (rotations.Exists(entity))
                {
                    rotation = rotations[entity].Value;
                }
                
                if (localPositions.Exists(entity))
                {
                    var worldPosition = math.mul(parentMatrix,new float4(localPositions[entity].Value,1.0f));
                    position = new float3(worldPosition.x,worldPosition.y,worldPosition.z);
                    if (positions.Exists(entity))
                    {
                        positions[entity] = new Position {Value = position};
                    }
                }
                
                if (localRotations.Exists(entity))
                {
                    var parentRotation = math.matrixToQuat(parentMatrix.m0.xyz, parentMatrix.m1.xyz, parentMatrix.m2.xyz);
                    var localRotation = localRotations[entity].Value;
                    rotation = math.mul(parentRotation, localRotation);
                    if (rotations.Exists(entity))
                    {
                        rotations[entity] = new Rotation { Value = rotation };
                    }
                }

                float4x4 matrix = math.rottrans(rotation, position);
                if (transformMatrices.Exists(entity))
                {
                    transformMatrices[entity] = new TransformMatrix {Value = matrix};
                }

                Entity child;
                NativeMultiHashMapIterator<Entity> iterator;
                bool found = hierarchy.TryGetFirstValue(entity, out child, out iterator);
                while (found)
                {
                    TransformTree(child,matrix);
                    found = hierarchy.TryGetNextValue(out child, ref iterator);
                }
            }

            public void Execute()
            {
                float4x4 identity = float4x4.identity;
                for (int i = 0; i < roots.Length; i++)
                {
                    TransformTree(roots[i],identity);
                }
            }
        }
        
        [ComputeJobOptimization]
        struct UpdateSubHierarchy : IJob
        {
            [ReadOnly] public NativeMultiHashMap<Entity, Entity> hierarchy;
            [DeallocateOnJobCompletion] [ReadOnly] public NativeArray<Entity> roots;
            [ReadOnly] public ComponentDataFromEntity<LocalPosition> localPositions;
            [ReadOnly] public ComponentDataFromEntity<LocalRotation> localRotations;
            [DeallocateOnJobCompletion] [ReadOnly] public NativeArray<float4x4> rootMatrices;

            public ComponentDataFromEntity<Position> positions;
            public ComponentDataFromEntity<Rotation> rotations;
            public ComponentDataFromEntity<TransformMatrix> transformMatrices;

            void TransformTree(Entity entity,float4x4 parentMatrix)
            {
                var position = new float3();
                var rotation = quaternion.identity;
                
                if (positions.Exists(entity))
                {
                    position = positions[entity].Value;
                }
                
                if (rotations.Exists(entity))
                {
                    rotation = rotations[entity].Value;
                }
                
                if (localPositions.Exists(entity))
                {
                    var worldPosition = math.mul(parentMatrix,new float4(localPositions[entity].Value,1.0f));
                    position = new float3(worldPosition.x,worldPosition.y,worldPosition.z);
                    if (positions.Exists(entity))
                    {
                        positions[entity] = new Position {Value = position};
                    }
                }
                
                if (localRotations.Exists(entity))
                {
                    var parentRotation = math.matrixToQuat(parentMatrix.m0.xyz, parentMatrix.m1.xyz, parentMatrix.m2.xyz);
                    var localRotation = localRotations[entity].Value;
                    rotation = math.mul(parentRotation, localRotation);
                    if (rotations.Exists(entity))
                    {
                        rotations[entity] = new Rotation { Value = rotation };
                    }
                }

                float4x4 matrix = math.rottrans(rotation, position);
                if (transformMatrices.Exists(entity))
                {
                    transformMatrices[entity] = new TransformMatrix {Value = matrix};
                }

                Entity child;
                NativeMultiHashMapIterator<Entity> iterator;
                bool found = hierarchy.TryGetFirstValue(entity, out child, out iterator);
                while (found)
                {
                    TransformTree(child,matrix);
                    found = hierarchy.TryGetNextValue(out child, ref iterator);
                }
            }

            public void Execute()
            {
                for (int i = 0; i < roots.Length; i++)
                {
                    Entity entity = roots[i];
                    float4x4 matrix = rootMatrices[i];
                    Entity child;
                    NativeMultiHashMapIterator<Entity> iterator;
                    bool found = hierarchy.TryGetFirstValue(entity, out child, out iterator);
                    while (found)
                    {
                        TransformTree(child,matrix);
                        found = hierarchy.TryGetNextValue(out child, ref iterator);
                    }
                }
            }
        }

        NativeMultiHashMap<Entity, Entity> m_Hierarchy;

        protected override JobHandle OnUpdate(JobHandle inputDeps)
        {
            int rootCount = m_RootRotGroup.Length + m_RootTransGroup.Length + m_RootRotTransTransformGroup.Length + m_RootRotTransNoTransformGroup.Length;
            if (rootCount == 0)
            {
                return inputDeps;
            }
            
            var transRoots = new NativeArray<Entity>(m_RootTransGroup.Length, Allocator.TempJob);
            var rotTransTransformRoots = new NativeArray<Entity>(m_RootRotTransTransformGroup.Length, Allocator.TempJob);
            var rotTransNoTransformRoots = new NativeArray<Entity>(m_RootRotTransNoTransformGroup.Length, Allocator.TempJob);
            var rotTransTransformRootMatrices = new NativeArray<float4x4>(m_RootRotTransTransformGroup.Length, Allocator.TempJob);
            var rotTransNoTransformRootMatrices = new NativeArray<float4x4>(m_RootRotTransNoTransformGroup.Length, Allocator.TempJob);
            var rotRoots = new NativeArray<Entity>(m_RootRotGroup.Length, Allocator.TempJob);
            
            m_Hierarchy.Capacity = math.max(m_ParentGroup.Length + rootCount,m_Hierarchy.Capacity);
            m_Hierarchy.Clear();

            var copyTransRootsJob = new CopyEntities
            {
                source = m_RootTransGroup.entities,
                result = transRoots
            };
            var copyTransRootsJobHandle = copyTransRootsJob.Schedule(m_RootTransGroup.Length, 64, inputDeps);
            
            var copyRotTransTransformRootsJob = new CopyEntities
            {
                source = m_RootRotTransTransformGroup.entities,
                result = rotTransTransformRoots
            };
            var copyRotTransTransformRootsJobHandle = copyRotTransTransformRootsJob.Schedule(m_RootRotTransTransformGroup.Length, 64, inputDeps);
            
            var copyRotTransNoTransformRootsJob = new CopyEntities
            {
                source = m_RootRotTransNoTransformGroup.entities,
                result = rotTransNoTransformRoots
            };
            var copyRotTransNoTransformRootsJobHandle = copyRotTransNoTransformRootsJob.Schedule(m_RootRotTransNoTransformGroup.Length, 64, inputDeps);
            
            var copyRotRootsJob = new CopyEntities
            {
                source = m_RootRotGroup.entities,
                result = rotTransTransformRoots
            };
            var copyRotRootsJobHandle = copyRotRootsJob.Schedule(m_RootRotGroup.Length, 64, inputDeps);
        
            var buildHierarchyJob = new BuildHierarchy
            {
                hierarchy = m_Hierarchy,
                transformParents = m_ParentGroup.transformParents,
                entities = m_ParentGroup.entities
            };
            var buildHierarchyJobHandle = buildHierarchyJob.Schedule(m_ParentGroup.Length, 64, inputDeps);
                
            var updateRotTransTransformRootsJob = new UpdateRotTransTransformRoots
            {
                rotations = m_RootRotTransTransformGroup.rotations,
                positions = m_RootRotTransTransformGroup.positions,
                matrices = rotTransTransformRootMatrices,
                transforms = m_RootRotTransTransformGroup.transforms
            };
            var updateRotTransTransformRootsJobHandle = updateRotTransTransformRootsJob.Schedule(m_RootRotTransTransformGroup.Length, 64, inputDeps);
            
            var updateRotTransNoTransformRootsJob = new UpdateRotTransNoTransformRoots
            {
                rotations = m_RootRotTransNoTransformGroup.rotations,
                positions = m_RootRotTransNoTransformGroup.positions,
                matrices = rotTransNoTransformRootMatrices
            };
            var updateRotTransNoTransformRootsJobHandle = updateRotTransNoTransformRootsJob.Schedule(m_RootRotTransNoTransformGroup.Length, 64, inputDeps);

            var jh0 = JobHandle.CombineDependencies(copyRotTransTransformRootsJobHandle, copyRotTransNoTransformRootsJobHandle);
            var jh1 = JobHandle.CombineDependencies(copyTransRootsJobHandle, jh0);
            var jh2 = JobHandle.CombineDependencies(copyRotRootsJobHandle, buildHierarchyJobHandle);
            var jh3 = JobHandle.CombineDependencies(updateRotTransTransformRootsJobHandle, updateRotTransNoTransformRootsJobHandle);
            var jh4 = JobHandle.CombineDependencies(jh1, jh2);
            var jh5 = JobHandle.CombineDependencies(jh3, jh4);

            var updateTransHierarchyJob = new UpdateHierarchy
            {
                hierarchy = m_Hierarchy,
                roots = transRoots,
                localPositions = m_LocalPositions,
                localRotations = m_LocalRotations,
                positions = m_Positions,
                rotations = m_Rotations,
                transformMatrices = m_TransformMatrices
            };
            var updateTransHierarchyJobHandle = updateTransHierarchyJob.Schedule(jh5);
            
            var updateRotTransTransformHierarchyJob = new UpdateSubHierarchy
            {
                hierarchy = m_Hierarchy,
                roots = rotTransTransformRoots,
                rootMatrices = rotTransTransformRootMatrices,
                localPositions = m_LocalPositions,
                localRotations = m_LocalRotations,
                positions = m_Positions,
                rotations = m_Rotations,
                transformMatrices = m_TransformMatrices
            };
            var updateRotTransTransformHierarchyJobHandle = updateRotTransTransformHierarchyJob.Schedule(updateTransHierarchyJobHandle);
            
            var updateRotTransNoTransformHierarchyJob = new UpdateSubHierarchy
            {
                hierarchy = m_Hierarchy,
                roots = rotTransNoTransformRoots,
                rootMatrices = rotTransNoTransformRootMatrices,
                localPositions = m_LocalPositions,
                localRotations = m_LocalRotations,
                positions = m_Positions,
                rotations = m_Rotations,
                transformMatrices = m_TransformMatrices
            };
            var updateRotTransNoTransformHierarchyJobHandle = updateRotTransNoTransformHierarchyJob.Schedule(updateRotTransTransformHierarchyJobHandle);
            
            var updateRotHierarchyJob = new UpdateHierarchy
            {
                hierarchy = m_Hierarchy,
                roots = rotRoots,
                localPositions = m_LocalPositions,
                localRotations = m_LocalRotations,
                positions = m_Positions,
                rotations = m_Rotations,
                transformMatrices = m_TransformMatrices
            };
            var updateRotHierarchyJobHandle = updateRotHierarchyJob.Schedule(updateRotTransNoTransformHierarchyJobHandle);

            return updateRotHierarchyJobHandle;
        } 
        
        protected override void OnCreateManager(int capacity)
        {
            m_Hierarchy = new NativeMultiHashMap<Entity, Entity>(capacity, Allocator.Persistent);
        }

        protected override void OnDestroyManager()
        {
            m_Hierarchy.Dispose();
        }
        
    }
}
