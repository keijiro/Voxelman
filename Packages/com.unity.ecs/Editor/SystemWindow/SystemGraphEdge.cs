using System;
using System.Collections.Generic;
using UnityEngine;

namespace UnityEditor.ECS
{

    [Serializable]
    public class SystemGraphEdge
    {
        public List<Vector2> points = new List<Vector2>();
        public int target;
    }
    
}