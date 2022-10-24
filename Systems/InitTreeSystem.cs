using Components;
using Leopotam.Ecs;
using UnityEngine;

namespace Systems
{
    public class InitTreeSystem : IEcsInitSystem
    {
        private readonly EcsWorld _world = null;
        private Vector3 originPos;
        private float cellSize;
        private GridInfoComponent[,] gridArray;
        private Vector3 treePosition; 
        
        private EcsFilter<TreeComponent> treeFilter = null;
        private EcsFilter<GridComponent> filter = null;

        public void Init()
        {
            if (!filter.IsEmpty())
            {
                ref var gridComponent = ref filter.Get1(0);
                gridArray = gridComponent.gridArray;
                originPos = gridComponent.originPosition;
                cellSize = gridComponent.cellSize;
            }
            foreach (var tree in treeFilter)
            {
                ref var treeComponent = ref treeFilter.Get1(tree);
                treePosition = treeComponent.treeTransform.position;
                SetupTree();
            }
        }
        private void SetupTree()
        {
            int x, y;
            GetXY(out x, out y);
            gridArray[x, y].hasTree = true;
        }
        private void GetXY(out int x,out int y)
        {
            x = Mathf.FloorToInt((treePosition - originPos).x / cellSize);
            y = Mathf.FloorToInt((treePosition - originPos).y / cellSize);
        }
    }
}