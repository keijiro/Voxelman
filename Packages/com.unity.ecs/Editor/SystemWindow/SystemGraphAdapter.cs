using System.Collections.Generic;

using Microsoft.Msagl.Core.Geometry;
using Microsoft.Msagl.Core.Geometry.Curves;
using Microsoft.Msagl.Core.Layout;
using MsaglEdge = Microsoft.Msagl.Core.Layout.Edge;
using MsaglNode = Microsoft.Msagl.Core.Layout.Node;

namespace UnityEditor.ECS
{
    public class MsaglGraphAdapter
    {
        public GeometryGraph resultGraph { get; } = new GeometryGraph();

        public MsaglGraphAdapter(SystemGraphState state)
        {
            var nodes = new List<MsaglNode>();

            state.systemViews.ForEach(node =>
            {
                nodes.Add(AddNode(node, state));
            });

            for (var i = 0; i < state.systemViews.Count; ++i)
            {
                var view = state.systemViews[i];
                var node = nodes[i];
                view.updateAfter.ForEach(otherId =>
                {
                    AddEdge(node, nodes[otherId]);
                });
                view.updateBefore.ForEach(otherId =>
                {
                    AddEdge(nodes[otherId], node);
                });
            }
        }

        private MsaglNode AddNode(SystemViewData view, SystemGraphState state)
        {
            ICurve curve = CurveFactory.CreateRectangle(view.position.width, view.position.height, new Point());
            MsaglNode newNode = new MsaglNode(curve, state.systemViews.IndexOf(view).ToString())
            {
                UserData = view
            };
            resultGraph.Nodes.Add(newNode);
            return newNode;
        }

        private void AddEdge(MsaglNode outputNode, MsaglNode inputNode)
        {
            resultGraph.Edges.Add(new MsaglEdge(outputNode, inputNode));
        }
    }

}
