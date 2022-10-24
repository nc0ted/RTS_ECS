using Components;
using Leopotam.Ecs;
using UnityEngine;

sealed class MovementSystem : IEcsRunSystem
{
    private readonly EcsWorld _world = null;
    private readonly EcsFilter<UnitComponent,MovementComponent,DirectionComponent> ecsFilter = null;
    public void Run()
    {
        foreach (var i in ecsFilter)
        {
            var unitComponent = ecsFilter.Get1(i);
            var movementComponent = ecsFilter.Get2(i);
            var directionComponent = ecsFilter.Get3(i);

            var direction =  directionComponent.direction;
            var transform =  unitComponent.unitTransform;
            var speed =  movementComponent.speed;
            MovePlayer(transform,direction,speed);
        }
    }
    private void MovePlayer(Transform transformPos,Vector3 direction,float speed)
    {
        transformPos.position += new Vector3(direction.x*speed*Time.deltaTime,direction.y*speed*Time.deltaTime);
    }
}