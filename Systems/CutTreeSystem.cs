using System.Threading.Tasks;
using Components;
using Leopotam.Ecs;
using UnityEngine;

namespace Systems
{
    public class CutTreeSystem:IEcsRunSystem
    {
        private readonly EcsWorld _world = null;
        private EcsFilter<PlayerResourcesComponent,UnitComponent,CutWoodEvent> filter = null;
        
        public void Run()
        {
            if (filter.IsEmpty()) return;
            ref var palyerTransform = ref filter.Get2(0);
            ref var playerResources = ref filter.Get1(0);
            var tree = Physics2D.OverlapCircle(palyerTransform.unitTransform.position, 0.1f);
            if (tree == null) return;
            if (tree.GetComponent<ResourceMonobeh>())
            {
                Cut(playerResources,tree);
            }
        }
        private async Task Cut(PlayerResourcesComponent playerResources,Collider2D tree)
        {
            await Task.Delay(1000);
            tree.gameObject.SetActive(false);
        }
    }
}