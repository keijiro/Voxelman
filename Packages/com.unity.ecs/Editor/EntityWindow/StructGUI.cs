using UnityEngine;
using System;
using System.Reflection;

namespace UnityEditor.ECS
{
	public static class StructGUI {

		public const float pointsPerLine = 14f;

		static GUIStyle labelStyle {
			get {
				if (m_LabelStyle == null)
				{
					m_LabelStyle = new GUIStyle(EditorStyles.miniLabel);
					m_LabelStyle.alignment = TextAnchor.LowerRight;
					var color = m_LabelStyle.normal.textColor;
					color.a *= 0.5f;
					m_LabelStyle.normal.textColor = color;
				}
				return m_LabelStyle;
			}
		}
		static GUIStyle m_LabelStyle;

		public static void CellGUI(Rect rect, object data, int rows)
		{
			var fields = data.GetType().GetFields(BindingFlags.Instance | BindingFlags.Public);
			var columns = ColumnsForType(data.GetType());
			var labelWidth = MaxLabelWidthForType(data.GetType());
			var fieldWidth = (rect.width - labelWidth)/columns;
			var fieldHeight = rect.height/rows;
			var currentCell = new Rect(rect.x + labelWidth, rect.y, fieldWidth, fieldHeight);
			foreach (var field in fields)
			{
				var labelCell = new Rect(rect.x, currentCell.y, labelWidth, fieldHeight);
				GUI.Label(labelCell, field.Name, labelStyle);

				var value = field.GetValue(data);
				MathGUI.FieldGUI(currentCell, value);
				currentCell.y += fieldHeight * MathGUI.RowsForType(field.FieldType);
			}
		}

		public static int RowsForType(Type type)
		{
			var fields = type.GetFields(BindingFlags.Instance | BindingFlags.Public);
			var rows = 0;
			foreach (var field in fields)
				rows += MathGUI.RowsForType(field.FieldType);
			return rows;
		}

		public static int ColumnsForType(Type type)
		{
			var fields = type.GetFields(BindingFlags.Instance | BindingFlags.Public);
			var columns = 0;
			foreach (var field in fields)
				columns = Mathf.Max(MathGUI.ColumnsForType(field.FieldType), columns);
			return columns;
		}

		public static float MaxLabelWidthForType(Type type)
		{
			var fields = type.GetFields(BindingFlags.Instance | BindingFlags.Public);
			var maxLabeLWidth = 0f;
			foreach (var field in fields)
				maxLabeLWidth = Mathf.Max(labelStyle.CalcSize(new GUIContent(field.Name)).x, maxLabeLWidth);
			return maxLabeLWidth;
		}
	}
}