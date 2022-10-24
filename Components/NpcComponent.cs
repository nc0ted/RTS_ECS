using System;
using System.Collections.Generic;
using UnityEngine;

namespace Components
{
    [Serializable]
    public struct NpcComponent
    {
        public bool canMove;
        public string Name;
        public List<Vector3> pathVectorList;
        public bool hasTarget;
        public bool canGetResources;
        public int currentPathIndex;
        public bool canCutTree;
    }
}