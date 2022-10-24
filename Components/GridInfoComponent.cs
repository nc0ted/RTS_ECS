using UnityEngine;

namespace Components
{
    public struct GridInfoComponent
    {
        public int x;
        public int y;
        public bool isEmpty;
        public bool isTargeted;
        public bool hasBuilding;
        public bool hasTree;
        public GameObject gameObject;
        public bool IsEmpty()
        {
            if (hasBuilding || hasTree)
                isEmpty = false;
            else
                isEmpty = true;
            return isEmpty;
        }
    }
}