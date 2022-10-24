using System;
using UnityEngine;

namespace Components
{
    [Serializable]
    public struct MousePositionComponent
    {
        public Camera camera;
        [HideInInspector]public Vector3 position;
    }
}