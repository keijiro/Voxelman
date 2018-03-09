using UnityEngine;
using Unity.Mathematics;
using System;
using System.Reflection;

namespace UnityEditor.ECS
{
	public static class MathGUI {

		static readonly string[] vectorFieldNames = new string[]{"x", "y", "z", "w"};
		static readonly string[] matrixRowNames = new string[] {"m0", "m1", "m2", "m3"};

		public static void FieldGUI(Rect firstCell, object data)
		{
			var type = data.GetType();
			var rows = RowsForType(type);
			var columns = ColumnsForType(type);
			if (rows == 1)
			{
				if (columns == 1)
				{
					ScalarGUI(firstCell, data);
				}
				else
				{
					VectorGUI(firstCell, data);
				}
			}
			else
			{
				MatrixGUI(firstCell, data);
			}
		}

		public static void MatrixGUI(Rect firstCell, object data)
		{
			var type = data.GetType();
			var rows = RowsForType(type);
			var currentCell = firstCell;
			for (var i = 0; i < rows; ++i)
			{
				var field = type.GetField(matrixRowNames[i], BindingFlags.Instance | BindingFlags.Public);
				var value = field.GetValue(data);
				VectorGUI(currentCell, value);
				currentCell.y += firstCell.height;
			}
		}

		public static void VectorGUI(Rect firstCell, object data)
		{
			var type = data.GetType();
			var columns = ColumnsForType(type);
			var currentCell = firstCell;
			for (var i = 0; i < columns; ++i)
			{
				var field = type.GetField(vectorFieldNames[i], BindingFlags.Instance | BindingFlags.Public);
				var value = field.GetValue(data);
				ScalarGUI(currentCell, value);
				currentCell.x += firstCell.width;
			}
		}

		public static void ScalarGUI(Rect cell, object value)
		{
			GUI.Label(cell, value.ToString());
		}

		public static int ColumnsForType(Type type)
		{
			if (type == typeof(float4) || type == typeof(float4x4))
				return 4;
			else if (type == typeof(float3) || type == typeof(float3x3))
				return 3;
			else if (type == typeof(float2) || type == typeof(float2x2))
				return 2;
			else if (type == typeof(float))
				return 1;
			else
				return 1;
		}

		public static int RowsForType(Type type)
		{
			if (type == typeof(float4x4))
				return 4;
			else if (type == typeof(float3x3))
				return 3;
			else if (type == typeof(float2x2))
				return 2;
			else if (type == typeof(float) || type == typeof(float2) || type == typeof(float3) || type == typeof(float4))
				return 1;
			else
				return 1;
		}
	}
}