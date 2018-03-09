using System.Collections.Generic;
using UnityEngine;

namespace UnityEditor.ECS
{

    [System.Serializable]
    public class SystemViewData
    {
        [SerializeField]
        public string name;
        public string fullName;
        public Rect position;
        public List<int> updateAfter;
        public List<int> updateBefore;

        public SystemViewData(string name, string fullName, Rect position)
        {
            this.name = name;
            this.fullName = fullName;
            this.position = position;
        }

        public Vector3 Center
        {
            get
            {
                var center = (Vector3)position.center;
                center.z = -SystemGraphView.kArrowSize;
                return center;
            }
        }
    }
}