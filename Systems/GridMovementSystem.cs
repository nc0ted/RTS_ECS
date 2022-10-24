using System.Collections.Generic;
using System.Linq;
using Components;
using Leopotam.Ecs;
using Unity.Collections;
using Unity.Mathematics;
using UnityEngine;

namespace Systems
{
    public class GridMovementSystem:IEcsRunSystem
    {
        private readonly EcsWorld _world = null;
        private bool canMove;
        private int iteration;
        private List<Vector3> pathVectorList=new List<Vector3>();
        private int currentPathIndex;
        private EcsFilter<GridMoveComponent,UnitComponent,PlayerTag> filter = null;
        private EcsFilter<GridComponent,SetPathEvent> gridFilter=null;
        private float cellSize;
        private Vector3 originPos;

        public void Run()
        {
            foreach (var entity in filter)
            {
                ref var gridMovementComponent = ref filter.Get1(entity);
                ref var nodes = ref gridMovementComponent.nodes;
                foreach (var grid in gridFilter)
                {
                    canMove = true;
                    ref var gridComponent = ref gridFilter.Get1(grid);
                    cellSize = gridComponent.cellSize;
                    originPos = gridComponent.originPosition;
                    if(nodes.Length>0)
                    {
                        currentPathIndex = 0;
                    }
                }
                ref var unitComponent = ref filter.Get2(entity);
                var speedc = gridMovementComponent.speed;
                var transform = unitComponent.unitTransform;
                GetMovementPoint(transform,ref nodes);
            }
        }
        private void GetMovementPoint(Transform transform,ref int2[] nodes)
        {
            pathVectorList = new List<Vector3>();
            foreach (var node in nodes)
            {
                iteration++;
                if(iteration!=nodes.Length) //not including first path vector so movement becomes smoother
                    pathVectorList.Add(GetWorldVector(node.x, node.y));
                pathVectorList.Reverse();
            }
            iteration = 0;
            if (pathVectorList == null) return;
            if (pathVectorList==null||nodes.Length<=0) return;
            if (!canMove) return;
            Vector3 targetPosition = pathVectorList[currentPathIndex];
            if (Vector3.Distance(transform.position,  targetPosition) > 0.1) 
            {
                Vector3 moveDir = (targetPosition - transform.position).normalized;
                transform.position += moveDir * 3 * Time.deltaTime;
            } 
            else 
            {
                currentPathIndex++;
                if (currentPathIndex*2 >= pathVectorList.Count+1)
                {
                    currentPathIndex = 0;
                    canMove = false;
                    pathVectorList = null;
                }
            }
        }
        private Vector3 GetWorldPosition(int x,int y)
        {
            return new Vector3(x, y)*cellSize + originPos+ new Vector3(cellSize, cellSize) * .5f;                 
        }
        public Vector3 GetWorldVector(int x,int y) {
            return originPos + new Vector3(x * cellSize, y * cellSize)+ new Vector3(cellSize, cellSize) * .5f;
        }
    }
}