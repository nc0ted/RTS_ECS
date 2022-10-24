using Components;
using Leopotam.Ecs;
using UnityEngine;

namespace Systems
{
    sealed class InputSystem:IEcsRunSystem
    {
        private Vector2 moveDelta;
        private readonly EcsWorld _world = null;
        private readonly EcsFilter<PlayerTag,DirectionComponent> directionFilter=null;
        public void Run()
        {
            SetDirection();
            foreach (var i in directionFilter)
            {
                ref var directionComponent = ref directionFilter.Get2(i);
                ref var direction = ref directionComponent.direction;
                direction.x = moveDelta.x;
                direction.y = moveDelta.y;
            }
        }

        private void SetDirection()
        {
            float x = Input.GetAxis("Horizontal");
            float y = Input.GetAxis("Vertical");
            moveDelta = new Vector2(x, y);
        }
    }
}