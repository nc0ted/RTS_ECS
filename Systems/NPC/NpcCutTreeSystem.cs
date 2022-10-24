using Components;
using Leopotam.Ecs;
using TMPro;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Systems.NPC
{
    public class NpcCutTreeSystem:IEcsRunSystem,IEcsInitSystem
    {
        private Transform unitTransform;
        private readonly EcsWorld _world = null;
        private float cellSize;
        private Vector3 originPos;
        private GridInfoComponent[,] gridArray;

        private EcsFilter<PlayerResourcesComponent> playerResourcesFilter;
        private EcsFilter<GridComponent> gridFilter; 
        private EcsFilter<UnitComponent,NpcComponent,SetPathComponent> filter = null;
        private TMP_Text wood;

        public void Init()
        {
            if (!gridFilter.IsEmpty())
            {
                ref var gridComp= ref gridFilter.Get1(0);
                originPos = gridComp.originPosition;
                cellSize = gridComp.cellSize;
                gridArray = gridComp.gridArray;
            }
            if (!playerResourcesFilter.IsEmpty())
            {
               wood = playerResourcesFilter.Get1(0).woodText;
            }
        }
        
        public void Run()
        {
            foreach (var i in filter)
            {
                ref var unitComponent = ref filter.Get1(i);
                unitTransform = unitComponent.unitTransform;
                if (unitTransform == null) continue;
                var tree = Physics2D.OverlapCircle(unitTransform.position, 1.3f);
                if (tree == null) continue;
                filter.Get2(i).canCutTree = true;
                if (tree.GetComponent<ResourceMonobeh>()&&tree.gameObject.activeInHierarchy && filter.Get2(i).canCutTree)
                {
                    int2 int2=new int2();
                    GetXY(out int2.x,out int2.y,tree.transform.position);
                    if (filter.Get3(i).endNode.x == int2.x && filter.Get3(i).endNode.y == int2.y)
                    {
                        Cut(tree, ref filter.Get2(i));
                    }
                }
            }

        }
        private void Cut(Collider2D tree,ref NpcComponent npcComponent)
        {
            npcComponent.canCutTree = false; 
            tree.gameObject.SetActive(false);
            npcComponent.hasTarget = false;
            npcComponent.canGetResources = true;
            var woodInt= int.Parse(wood.text);
            woodInt+=Random.Range(1,4);
            wood.text = woodInt.ToString();
        }
        private void GetXY(out int x,out int y,Vector3 treePosition)
        {
            x = Mathf.FloorToInt((treePosition - originPos).x / cellSize);
            y = Mathf.FloorToInt((treePosition - originPos).y / cellSize);
        }
    }
}