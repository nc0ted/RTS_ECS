using System;
using Unity.Mathematics;

namespace Components
{
    [Serializable]
    public struct SetPathComponent
    {
        public int2 startNode;
        public int2 endNode;
    }
}