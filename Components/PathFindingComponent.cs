using System;

namespace Components
{
    [Serializable]
    public struct PathFindingComponent
    {
        public int x;
        public int y;

        public int index;

        public int gCost;
        public int hCost;
        public int fCost;

        public bool isWalkable;

        public int cameFromNodeIndex;

        public void CalculateFCost() {
            fCost = gCost + hCost;
        }
    }
}