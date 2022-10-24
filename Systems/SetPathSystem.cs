using Components;
using Leopotam.Ecs;
using Unity.Burst;
using Unity.Mathematics;
using UnityEngine;

namespace Systems
{
    public class SetPathSystem : IEcsRunSystem
    {
        private readonly EcsWorld _world = null;
        private EcsFilter<MousePositionComponent,GridComponent,SetPathComponent,SetPathEvent> filter = null;
        private float cellSize;
        private Vector3 originPos;
        private Vector3 mousePosition;

        public void Run()
        {
            foreach (var entity in filter)
            {
                ref var mousePositionComponent = ref filter.Get1(entity);
                ref var setPathComponent = ref filter.Get3(entity);
                ref var gridComponent = ref filter.Get2(entity);
                cellSize = gridComponent.cellSize;
                originPos = gridComponent.originPosition;
                ref Vector3 mousePos = ref mousePositionComponent.position;
                mousePosition = mousePos;
                SetPath(out setPathComponent.endNode);
            }
        }
        private void SetPath(out int2 endNode)
        {
            int x, y;
            GetXY(out x,out y);
            endNode = new int2(x, y);
        }
        private void GetXY(out int x,out int y)
        {
            x = Mathf.FloorToInt((mousePosition - originPos).x / cellSize);
            y = Mathf.FloorToInt((mousePosition - originPos).y / cellSize);
        }
    }
}