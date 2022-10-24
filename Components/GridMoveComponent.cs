using System;
using System.Collections.Generic;
using Unity.Collections;
using Unity.Mathematics;

namespace Components
{
    [Serializable]
    public struct GridMoveComponent
    {
        public float speed;
        public int2[] nodes;
    }
}