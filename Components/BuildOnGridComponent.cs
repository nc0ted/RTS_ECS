using System;
using UnityEngine;

namespace Components
{
    [Serializable]
    public struct BuildOnGridComponent
    {
        public bool canBuild;
        public GameObject prefab;
    }
}