using System;
using Components;
using Leopotam.Ecs;
using NTC.Global.Pool;
using Unity.Mathematics;
using UnityEngine;

namespace Systems
{
    public class GridSystem:IEcsInitSystem
    {
        private readonly EcsWorld _world = null;
        private readonly EcsFilter<GridComponent,BuildOnGridComponent> gridFilter = null;
        public void Init()
        {
            foreach (var grid in gridFilter)
            {
                ref var gridComponent = ref gridFilter.Get1(grid);
                ref var buildOnGridComponent = ref gridFilter.Get2(grid);
                ref var prefab = ref buildOnGridComponent.prefab;
                
                ref var width = ref gridComponent.width;
                ref var height = ref gridComponent.height;
                ref var gridArray = ref gridComponent.gridArray;
                ref var cellSize = ref gridComponent.cellSize;
                ref var originPosition = ref gridComponent.originPosition;
                gridArray = new GridInfoComponent[width, height];

                for (int x = 0; x < width; x++)
                {
                    for (int y = 0; y < height; y++)
                    {
                        gridArray[x, y].x = x;
                        gridArray[x, y].y = y;
                    }
                }
            }
        }

        private Vector3 GetWorldPosition(int x, int y,float cellSize,Vector3 originPosition) {
            return new Vector3(x, y) * cellSize + originPosition;
        }
    }
}