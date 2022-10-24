using System.Collections.Generic;
using System.Linq;
using Components;
using DefaultNamespace;
using Leopotam.Ecs;
using Unity.Mathematics;
using UnityEngine;

namespace Systems
{
    public class NpcMovementSystem :IEcsRunSystem,IEcsInitSystem
    {
        private readonly EcsWorld _world = null;
        private float cellSize;
        private Vector3 originPos;
        private int iteration;
        private EcsFilter<UnitComponent,NpcTag,GridMoveComponent,NpcComponent> ecsFilter = null;
        private EcsFilter<SetNpcPathEvent> setPathEvent = null;
        private EcsFilter<GridComponent> gridFilter = null;

        public void Init()
        {
            if (!gridFilter.IsEmpty())
            {
                var gridComponent =  gridFilter.Get1(0);
                cellSize = gridComponent.cellSize;
                originPos = gridComponent.originPosition;
            }
        }
        public void Run()
        {
            foreach (var enemy in ecsFilter)
            {
                var gridMoveComponent =  ecsFilter.Get3(enemy);
                var nodes = gridMoveComponent.nodes;
                var unitComponent =  ecsFilter.Get1(enemy);
                var transform =  unitComponent.unitTransform;
                var npcComponent =  ecsFilter.Get4(enemy);
                foreach (var pathEvent in setPathEvent)
                {
                    npcComponent.canMove = true;
                    npcComponent.currentPathIndex = 0;
                }
                GetMovementPoint(transform, nodes, npcComponent);
            }
        }
        private void GetMovementPoint(Transform transform, int2[] nodes, NpcComponent npcComponent)
        {
            npcComponent.pathVectorList = new List<Vector3>();
            foreach (var node in nodes)
            {
                iteration++;
                if(iteration!=nodes.Length) //not including first path vector so movement becomes smoother
                    npcComponent.pathVectorList.Add(GetWorldVector(node.x, node.y));
             //   npcComponent.pathVectorList.Reverse();
            }
            iteration = 0;
            if (!npcComponent.canMove)return;

            Vector3 targetPosition =  npcComponent.pathVectorList[npcComponent.currentPathIndex];
            if (Vector3.Distance(transform.position,  targetPosition) > 0.1) 
            {
                var position = transform.position;
                Vector3 moveDir = (targetPosition - position).normalized;
                position += moveDir * 3 * Time.deltaTime;
                transform.position = position;
            } 
            else 
            {
                npcComponent.currentPathIndex++;
                if (npcComponent.currentPathIndex*2 >=  npcComponent.pathVectorList.Count+1)
                { 
                    npcComponent.currentPathIndex = 0;
                    npcComponent.canMove = false;
                    npcComponent.pathVectorList = null;
                    npcComponent.canGetResources =true;
                }
            }
        }

        private Vector3 GetWorldVector(int x,int y) {
            return originPos + new Vector3(x * cellSize, y * cellSize)+ new Vector3(cellSize, cellSize) * .5f;
        }
        private Vector3 GetWorldPosition(int x,int y)
        {
            return new Vector3(x, y)*1 + new Vector3(-15,-2.5f)+ new Vector3(1, 1) * .5f;                 
        }
    }
}