using System;
using System.Collections.Generic;
using Microsoft.Msagl.Core.Geometry.Curves;
using Microsoft.Msagl.Layout.Layered;
using Unity.Entities;
using UnityEngine;

namespace UnityEditor.ECS
{
	public class SystemGraphView
	{
		private readonly Vector2 kWindowOffset;
		public const float kArrowSize = 11f;
		private const float kLineWidth = 2f;
		private const float kLayerHeight = 50f;
		private const float kHorizontalSpacing = 200f;

		private SystemGraphState state;
	    private readonly Texture2D lineTexture;

	    const float kAlpha = 0.25f;

	    public SystemGraphView(Vector2 windowOffset)
	    {
	        kWindowOffset = windowOffset;
	        lineTexture = AssetDatabase.LoadAssetAtPath<Texture2D>("Packages/com.unity.ecs_bundle/Unity.Entities.Editor/Resources/AALineRetina.png");
	    }

	    public static SystemGraphState GetStateForWorld(World world, ref List<SystemGraphState> stateForWorlds,
	        ref List<string> worldNames)
	    {
	        if (world == null)
	            return new SystemGraphState();

	        if (stateForWorlds == null)
	        {
	            stateForWorlds = new List<SystemGraphState>();
	            worldNames = new List<string>();
	        }
	        var currentWorldName = world.Name;

	        SystemGraphState stateForCurrentWorld = null;
	        for (var i = 0; i < stateForWorlds.Count; ++i)
	        {
	            if (worldNames[i] == currentWorldName)
	            {
	                stateForCurrentWorld = stateForWorlds[i];
	                break;
	            }
	        }
	        if (stateForCurrentWorld == null)
	        {
	            stateForCurrentWorld = new SystemGraphState();
	            stateForWorlds.Add(stateForCurrentWorld);
	            worldNames.Add(currentWorldName);
	        }
	        return stateForCurrentWorld;
	    }

		public void SetSystemsAndState(Type[] systemTypes, SystemGraphState savedState)
		{
		    state = savedState;

			var systemViewIndicesByType = new Dictionary<Type, int>();

			if (systemTypes != null)
			{
				foreach (var type in systemTypes)
				{
					var systemViewIndex = state.systemViews.FindIndex(x => x.fullName == type.FullName);
					if (systemViewIndex < 0)
					{
					    var size = GUI.skin.label.CalcSize(new GUIContent(type.Name));
					    state.systemViews.Add(new SystemViewData(type.Name, type.FullName, new Rect(Vector2.zero, size)));
						systemViewIndex = state.systemViews.Count - 1;
					}
				    state.systemViews[systemViewIndex].updateAfter = new List<int>();
				    state.systemViews[systemViewIndex].updateBefore = new List<int>();
					systemViewIndicesByType.Add(type, systemViewIndex);
				}
				foreach (var systemType in systemTypes)
				{
					var systemView = state.systemViews[systemViewIndicesByType[systemType]];
					foreach (var attribute in systemType.GetCustomAttributesData())
					{
						if (attribute.AttributeType == typeof(UpdateAfterAttribute))
						{
							var type = (Type)attribute.ConstructorArguments[0].Value;
							if (systemViewIndicesByType.ContainsKey(type))
								systemView.updateAfter.Add(systemViewIndicesByType[type]);
						}
						if (attribute.AttributeType == typeof(UpdateBeforeAttribute))
						{
							var type = (Type)attribute.ConstructorArguments[0].Value;
							if (systemViewIndicesByType.ContainsKey(type))
								systemView.updateBefore.Add(systemViewIndicesByType[type]);
						}
					}
				}
			}
		    GraphLayout();
		}

		public void GraphLayout()
		{
		    var graphAdapter = new MsaglGraphAdapter(state);
		    var settings = new SugiyamaLayoutSettings()
		    {
		        Transformation = PlaneTransformation.Rotation(3.0*Math.PI/2.0),
		        NodeSeparation = 10.0
		    };
		    var layout = new LayeredLayout(graphAdapter.resultGraph, settings);
		    layout.Run();

		    var minPosition = new Vector2(float.MaxValue, float.MaxValue);
		    foreach (var node in graphAdapter.resultGraph.Nodes)
		    {
		        var position = node.BoundingBox.LeftBottom;
		        if (position.X < minPosition.x)
		            minPosition.x = (float) position.X;
		        if (position.Y < minPosition.y)
		            minPosition.y = (float) position.Y;
		    }
		    foreach (var node in graphAdapter.resultGraph.Nodes)
		    {
		        var view = (SystemViewData) node.UserData;
		        var vector = new Vector2(Mathf.Round((float) node.BoundingBox.Left), Mathf.Round((float) node.BoundingBox.Bottom));
		        view.position.position = vector - minPosition + kWindowOffset;
		    }

		    state.edges.Clear();
		    foreach (var edge in graphAdapter.resultGraph.Edges)
		    {
		        var systemEdge = new SystemGraphEdge();
		        foreach (var point in edge.EdgeGeometry.SmoothedPolyline)
		        {
		            var vector = new Vector2((float) point.X, (float) point.Y);
		            systemEdge.points.Add(vector - minPosition + kWindowOffset);
		        }

		        systemEdge.target = state.systemViews.IndexOf((SystemViewData) edge.Target.UserData);
		        systemEdge.points[systemEdge.points.Count - 1] =
		            ExteriorPointFromOtherPoint(state.systemViews[systemEdge.target].position,
		                systemEdge.points[systemEdge.points.Count - 2]);
		        state.edges.Add(systemEdge);
		    }
		}

		public void OnGUIArrows()
		{
		    Handles.matrix = Matrix4x4.Translate(Vector3.forward * -5f);
		    foreach (var edge in state.edges)
		    {
		        Handles.color = EditorStyles.label.normal.textColor;
		        if (edge.points.Count > 2)
		        {
		            DrawBezierSegment(edge.points[0], edge.points[0], edge.points[1], edge.points[2]);
		        }

		        for (var i = 1; i < edge.points.Count - 2; ++i)
		        {
		            DrawBezierSegment(edge.points[i - 1], edge.points[i], edge.points[i + 1], edge.points[i + 2]);
		        }

		        var secondLast = edge.points[edge.points.Count - 2];
		        var thirdLast = edge.points.Count > 2 ? edge.points[edge.points.Count - 3] : secondLast;
		        var last = edge.points[edge.points.Count - 1];

		        DrawBezierSegment(thirdLast, secondLast, last, last);

		        var arrowDirection = last - secondLast;
		        if (arrowDirection == Vector2.zero)
		            return;

		        var endPos = ExteriorPointFromOtherPoint(state.systemViews[edge.target].position, secondLast);
		        endPos -= (endPos - secondLast).normalized * 0.6f * kArrowSize;
		        var rotation = Quaternion.LookRotation(arrowDirection, Vector3.forward);
		        Handles.ConeHandleCap(0, endPos, rotation, kArrowSize, Event.current.type);
		    }
		    Handles.matrix = Matrix4x4.identity;
		}

	    void DrawBezierSegment(Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3)
	    {
	        var d = (p2 - p1).magnitude * kAlpha;
	        var t1 = (p2 - p0).normalized * d + p1;
	        var t2 = (p1 - p3).normalized * d + p2;
	        Handles.DrawBezier(p1, p2, t1, t2, Handles.color, lineTexture, EditorGUIUtility.pixelsPerPoint * kLineWidth);
	    }

	    public void OnGUIWindows()
	    {
	        for (var i = 0; i < state.systemViews.Count; ++i)
	        {
	            var view = state.systemViews[i];
	            view.position = GUI.Window(i, view.position, WindowFunction, "", GUI.skin.box);
	        }
	    }

		private void DrawArrowBetweenBoxes(SystemViewData fromView, SystemViewData toView)
		{
			var arrowDirection = toView.Center - fromView.Center;
			if (arrowDirection == Vector3.zero)
				return;
			Handles.color = EditorStyles.label.normal.textColor;
			var startPos = ExteriorPointFromOtherPoint(fromView.position, toView.Center);
			var endPos = ExteriorPointFromOtherPoint(toView.position, fromView.Center);
			endPos -= (endPos - startPos).normalized * 0.6f * kArrowSize;
			Handles.DrawAAPolyLine(lineTexture, EditorGUIUtility.pixelsPerPoint * kLineWidth, startPos, endPos);
			var rotation = Quaternion.LookRotation(arrowDirection, Vector3.forward);
			Handles.ConeHandleCap(0, endPos, rotation, kArrowSize, Event.current.type);
		}

		static Vector2 ExteriorPointFromOtherPoint(Rect rect, Vector2 other)
		{
			if (rect.width == 0f || rect.height == 0f)
				return rect.center;
			var localOther = other - rect.center;
			var ext = localOther;
			ext.x = Mathf.Abs(ext.x) / (rect.width*0.5f);
			ext.y = Mathf.Abs(ext.y) / (rect.height*0.5f);

			if (ext.x > ext.y)
			{
				ext.y /= ext.x;
				ext.x = 1f;
			}
			else if (ext.y > ext.x)
			{
				ext.x /= ext.y;
				ext.y = 1f;
			}
			ext.x *= Mathf.Sign(localOther.x)*(rect.width*0.5f);
			ext.y *= Mathf.Sign(localOther.y)*(rect.height*0.5f);
			ext += rect.center;

			return new Vector2(ext.x, ext.y);
		}

		void WindowFunction(int id)
		{
			GUI.Label(new Rect(Vector2.zero, state.systemViews[id].position.size), new GUIContent(state.systemViews[id].name, state.systemViews[id].fullName));
		}
	}

}
