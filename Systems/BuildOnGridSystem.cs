using Components;
using Leopotam.Ecs;
using NTC.Global.Pool;
using UnityEngine;

namespace Systems
{
    public class BuildOnGridSystem:IEcsRunSystem,IEcsInitSystem
    {
        private readonly EcsWorld _world = null;
        private GameObject prefab;
        private bool canBuild;
        Vector3 originPos;
        private float cellSize;
        private GridInfoComponent[,] gridArray;
        private int width;
        private int height;
        private int value;
        private Vector3 mousePosition;
        private EcsFilter<MousePositionComponent,GridComponent,BuildOnGridComponent> mousePositionFilter = null;
       
        public void Init()
        {
            foreach (var mouseId in mousePositionFilter)
            {
                ref var gridComponent = ref mousePositionFilter.Get2(mouseId);
                originPos = gridComponent.originPosition;

                ref var buildOnGridComponent = ref mousePositionFilter.Get3(mouseId);
                prefab = buildOnGridComponent.prefab;
                
                cellSize = gridComponent.cellSize;
                width = gridComponent.width;
                height = gridComponent.height;
                gridArray = gridComponent.gridArray;
            }
        }
        
        public void Run()
        {
            foreach (var mouseId in mousePositionFilter)
            { 
                ref var buildOnGridComponent = ref mousePositionFilter.Get3(mouseId);
                canBuild = buildOnGridComponent.canBuild;
                ref var mousePositionComponent = ref mousePositionFilter.Get1(mouseId);
                ref Vector3 mousePos = ref mousePositionComponent.position;
                mousePosition = mousePos;
            }
            if (Input.GetMouseButtonDown(0)&&canBuild)
            {
                SpawnObject();
            }
        }
        private void GetXY(out int x,out int y)
        {
            x = Mathf.FloorToInt((mousePosition - originPos).x / cellSize);
            y = Mathf.FloorToInt((mousePosition - originPos).y / cellSize);
        }
        private void SpawnObject()
        {
            int x, y;
            GetXY(out x,out y);
            Spawn(x,y);
        }
        private void Spawn(int x,int y)
        {
            if (x >= 0 && y >= 0 && x < width && y < height&&!gridArray[x,y].hasBuilding)
            {
                NightPool.Spawn(prefab,GetWorldPosition(x,y) + new Vector3(cellSize, cellSize) * .5f);
                gridArray[x, y].hasBuilding = true;
            }
        }
        private Vector3 GetWorldPosition(int x,int y)
        {
            return new Vector3(x, y)*cellSize + originPos;                 
        }
    }
}