using Components;
using Leopotam.Ecs;
using Unity.Mathematics;
using UnityEngine;

namespace Systems
{
    public class SetStartNodeSystem : IEcsRunSystem
    {
        private readonly EcsWorld _world = null;
        public EcsEntity entity;
        private float cellSize;
        private Vector3 originPos;
        private Transform transform;
        private EcsFilter<UnitComponent,PlayerTag> unitFilter=null;
        private EcsFilter<GridComponent,SetPathComponent,SetPathEvent> filter=null;

        public void Run()
        {
            foreach (var entity in filter)
            {
                ref var setPathComponent = ref filter.Get2(entity);
                ref var gridComponent = ref filter.Get1(entity);
                originPos = gridComponent.originPosition;
                cellSize = gridComponent.cellSize;
                foreach (var unit in unitFilter)
                {
                    ref var unitComponent = ref unitFilter.Get1(unit);
                    transform = unitComponent.unitTransform;
                }
                SetStartNodeValue(out setPathComponent.startNode);
            }
        }
        private void SetStartNodeValue(out int2 startNode)
        {
            int x, y;
            GetXY(out x, out y);
            startNode = new int2(x, y);
        }
        private void GetXY(out int x,out int y)
        {
            x = Mathf.FloorToInt((transform.position - originPos).x / cellSize);
            y = Mathf.FloorToInt((transform.position - originPos).y / cellSize);
        }
    }
    
}