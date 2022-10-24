using System;
using Components;
using DefaultNamespace;
using Leopotam.Ecs;
using Unity.Mathematics;
using UnityEngine;

namespace Systems
{
    public class SetNpcPathSystem:IEcsRunSystem
    {
        private Vector3 originPos;
        private float cellSize;
        private GridInfoComponent[,] gridArray;
        private Transform transform;
        
        private readonly EcsWorld _world = null;
        private EcsFilter<NpcTag,SetPathComponent,SetNpcPathEvent,UnitComponent> filter = null;
        private EcsFilter<GridComponent> gridFilter = null;
        
        public void Run()
        {
            if (!gridFilter.IsEmpty())
            {
                ref var gridComponent = ref gridFilter.Get1(0);
                gridArray = gridComponent.gridArray;
                originPos = gridComponent.originPosition;
                cellSize = gridComponent.cellSize;
            }
            foreach (var entity in filter)
            {
                ref var setPathComponent = ref filter.Get2(entity);
                ref var unitComponent = ref filter.Get4(entity);
                transform = unitComponent.unitTransform;
                SetStartNode(out setPathComponent.startNode);
                SetPath(out setPathComponent.endNode);
            }
        }
        private void SetStartNode(out int2 startNode)
        {
            int x, y;
            GetXY(out x,out y);
            startNode = new int2(x, y);
        }
        private void SetPath(out int2 endNode)
        {
            Debug.Log("set path");
            int2 newEndNode = default;
            foreach (var gridCell in gridArray)
            {
                if (gridCell.hasTree&&!gridCell.isTargeted)
                {
                    newEndNode = new int2(gridCell.x, gridCell.y);
                    gridArray[gridCell.x,gridCell.y].isTargeted = true;
                    gridArray[gridCell.x,gridCell.y].hasTree = false;
                    Debug.Log(GetWorldVector(gridCell.x, gridCell.y));
                    break;
                }
            }
            endNode = newEndNode;
        }
        private void GetXY(out int x,out int y)
        {
            var position = transform.position;
            x = Mathf.FloorToInt((position - originPos).x / cellSize);
            y = Mathf.FloorToInt((position - originPos).y / cellSize);
        }
        private Vector3 GetWorldVector(int x,int y) {
            return originPos + new Vector3(x * cellSize, y * cellSize)+ new Vector3(cellSize, cellSize) * .5f;
        }
    }
}