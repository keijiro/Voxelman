using System.Reflection;
using System.Collections.Generic;

using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;

namespace Unity.Entities
{
    struct InjectFromEntityData
	{
	    InjectionData[] m_InjectComponentDataFromEntity;
	    InjectionData[] m_InjectFixedArrayFromEntity;

	    public InjectFromEntityData(InjectionData[] componentDataFromEntity, InjectionData[] fixedArrayFromEntity)
	    {
	        m_InjectComponentDataFromEntity = componentDataFromEntity;
	        m_InjectFixedArrayFromEntity = fixedArrayFromEntity;
	    }

	    public static bool SupportsInjections(FieldInfo field)
	    {
	        if (field.FieldType.IsGenericType && field.FieldType.GetGenericTypeDefinition() == typeof(ComponentDataFromEntity<>))
	            return true;
	        if (field.FieldType.IsGenericType && field.FieldType.GetGenericTypeDefinition() == typeof(FixedArrayFromEntity<>))
	            return true;
	        return false;
	    }

	    public static void CreateInjection(FieldInfo field, EntityManager entityManager, List<InjectionData> componentDataFromEntity, List<InjectionData> fixedArrayFromEntity)
		{
			var isReadOnly = field.GetCustomAttributes(typeof(ReadOnlyAttribute), true).Length != 0;

			if (field.FieldType.IsGenericType && field.FieldType.GetGenericTypeDefinition() == typeof(ComponentDataFromEntity<>))
			{
				var injection = new InjectionData(field, field.FieldType.GetGenericArguments()[0], isReadOnly);
			    componentDataFromEntity.Add(injection);
			}
			else if (field.FieldType.IsGenericType && field.FieldType.GetGenericTypeDefinition() == typeof(FixedArrayFromEntity<>))
			{
				var injection = new InjectionData(field, field.FieldType.GetGenericArguments()[0], isReadOnly);
			    fixedArrayFromEntity.Add(injection);
			}
			else
			{
			    ComponentSystemInjection.ThrowUnsupportedInjectException(field);
			}
		}

		public unsafe void UpdateInjection(byte* pinnedSystemPtr, EntityManager entityManager)
		{
		    for (var i = 0; i != m_InjectComponentDataFromEntity.Length; i++)
		    {
		        var array = entityManager.GetComponentDataFromEntity<ProxyComponentData>(m_InjectComponentDataFromEntity[i].ComponentType.TypeIndex, m_InjectComponentDataFromEntity[i].IsReadOnly);
		        UnsafeUtility.CopyStructureToPtr(ref array, pinnedSystemPtr + m_InjectComponentDataFromEntity[i].FieldOffset);
		    }

		    for (var i = 0; i != m_InjectFixedArrayFromEntity.Length; i++)
		    {
		        var array = entityManager.GetFixedArrayFromEntity<int>(m_InjectFixedArrayFromEntity[i].ComponentType.TypeIndex, m_InjectFixedArrayFromEntity[i].IsReadOnly);
		        UnsafeUtility.CopyStructureToPtr(ref array, pinnedSystemPtr + m_InjectFixedArrayFromEntity[i].FieldOffset);
		    }
		}

	    public void ExtractJobDependencyTypes(ComponentSystemBase system)
	    {
	        if (m_InjectComponentDataFromEntity != null)
	        {
	            foreach (var injection in m_InjectComponentDataFromEntity)
	                system.AddReaderWriter(injection.ComponentType);
	        }

	        if (m_InjectFixedArrayFromEntity != null)
	        {
	            foreach (var injection in m_InjectFixedArrayFromEntity)
	                system.AddReaderWriter(injection.ComponentType);
	        }
	    }
	}
}
