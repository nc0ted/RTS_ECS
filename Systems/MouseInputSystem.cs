using Components;
using Leopotam.Ecs;
using UnityEngine;

namespace Systems
{
    public class MouseInputSystem: IEcsRunSystem
    {
        private readonly EcsWorld _world = null;
        private EcsFilter<MousePositionComponent> mousePositionFilter = null;

        public void Run()
        {
            foreach (var mousePos in mousePositionFilter)
            {
                ref var mousePositionComponent = ref mousePositionFilter.Get1(mousePos);
                ref var camera = ref mousePositionComponent.camera;
                mousePositionComponent.position = GetAxis(camera);;
            }
        }
        private Vector3 GetAxis(Camera camera)
        { 
            var mouse= camera.ScreenToWorldPoint(Input.mousePosition);
            return new Vector3(mouse.x, mouse.y, 0);
        }
    }
}