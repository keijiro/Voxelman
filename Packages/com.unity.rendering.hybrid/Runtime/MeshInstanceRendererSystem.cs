using System.Collections.Generic;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Assertions;
using Unity.Transforms;
using UnityEngine.Experimental.PlayerLoop;

namespace Unity.Rendering
{
    /// <summary>
    /// Renders all Entities containing both MeshInstanceRenderer & TransformMatrix components.
    /// </summary>
	[UpdateAfter(typeof(PreLateUpdate.ParticleSystemBeginUpdateAll))]
	public class MeshInstanceRendererSystem : ComponentSystem
	{
        // Instance renderer takes only batches of 1023
        Matrix4x4[]             m_MatricesArray = new Matrix4x4[1023];
	    List<MeshInstanceRenderer> m_CacheduniqueRendererTypes = new List<MeshInstanceRenderer>(10);

	    // This is the ugly bit, necessary until Graphics.DrawMeshInstanced supports NativeArrays pulling the data in from a job.
        public unsafe static void CopyMatrices(ComponentDataArray<TransformMatrix> transforms, int beginIndex, int length, Matrix4x4[] outMatrices)
        {
	        // @TODO: This is using unsafe code because the Unity DrawInstances API takes a Matrix4x4[] instead of NativeArray.
	        // We want to use the ComponentDataArray.CopyTo method
	        // because internally it uses memcpy to copy the data,
	        // if the nativeslice layout matches the layout of the component data. It's very fast...
            fixed (Matrix4x4* matricesPtr = outMatrices)
            {
                Assert.AreEqual(sizeof(Matrix4x4), sizeof(TransformMatrix));
	            var matricesSlice = NativeSliceUnsafeUtility.ConvertExistingDataToNativeSlice<TransformMatrix>(matricesPtr, sizeof(Matrix4x4), length);
	            #if ENABLE_UNITY_COLLECTIONS_CHECKS
	            NativeSliceUnsafeUtility.SetAtomicSafetyHandle(ref matricesSlice, AtomicSafetyHandle.GetTempUnsafePtrSliceHandle());
	            #endif
                transforms.CopyTo(matricesSlice, beginIndex);
            }
        }

        protected override void OnUpdate()
		{
            // We want to find all MeshInstanceRenderer & TransformMatrix combinations and render them
		    var maingroup = GetComponentGroup(typeof(MeshInstanceRenderer), typeof(TransformMatrix));

		    // We want to iterate over all unique MeshInstanceRenderer shared component data,
		    // that are attached to any entities in the world
            EntityManager.GetAllUniqueSharedComponentDatas(m_CacheduniqueRendererTypes);

            for (int i = 0;i != m_CacheduniqueRendererTypes.Count;i++)
            {
                // For each unique MeshInstanceRenderer data, we want to get all entities with a TransformMatrix
                // SharedComponentData gurantees that all those entities are packed togehter in a chunk with linear memory layout.
                // As a result the copy of the matrices out is internally done via memcpy.
                var renderer = m_CacheduniqueRendererTypes[i];
                var group = maingroup.GetVariation(renderer);
                var transforms = group.GetComponentDataArray<TransformMatrix>();

                // Graphics.DrawMeshInstanced has a set of limitations that are not optimal for working with ECS.
                // Specifically:
                // * No way to push the matrices from a job
                // * no NativeArray API, currently uses Matrix4x4[]
                // As a result this code is not yet jobified.
                // We are planning to adjust this API to make it more efficient for this use case.

                // For now, we have to copy our data into Matrix4x4[] with a specific upper limit of how many instances we can render in one batch.
                // So we just have a for loop here, representing each Graphics.DrawMeshInstanced batch
                int beginIndex = 0;
                while (beginIndex < transforms.Length)
                {
                    int length = math.min(m_MatricesArray.Length, transforms.Length - beginIndex);
                    CopyMatrices(transforms, beginIndex, length, m_MatricesArray);
                    Graphics.DrawMeshInstanced(renderer.mesh, 0, renderer.material, m_MatricesArray, length, null, renderer.castShadows, renderer.receiveShadows);

                    beginIndex += length;
                }

                group.Dispose();
            }
		    
		    m_CacheduniqueRendererTypes.Clear();
		}
	}
}
