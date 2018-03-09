using System;
using System.Collections.Generic;

namespace UnityEditor.ECS
{
    [Serializable]
    public class SystemGraphState
    {
        public List<SystemViewData> systemViews = new List<SystemViewData>();
        public List<SystemGraphEdge> edges = new List<SystemGraphEdge>();
    }
}
