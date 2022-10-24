using System;
using UnityEngine;

namespace Components
{
    [Serializable]
    public struct GridComponent
    {
        public int width;
        public int height;
        public float cellSize;
        public Vector3 originPosition;
        public GridInfoComponent[,] gridArray;
    }
}