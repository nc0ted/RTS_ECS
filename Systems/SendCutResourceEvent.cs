using Components;
using DefaultNamespace;
using Leopotam.Ecs;
using UnityEngine;

namespace Systems
{
    public class SendCutResourceEvent:IEcsRunSystem
    {
        private EcsFilter<PlayerResourcesComponent> filter = null;
        private readonly EcsWorld _world = null;

        public void Run()
        {
            foreach (var i in filter)
            {
                if (Input.GetKeyDown(KeyCode.F))
                {
                    ref var entity = ref filter.GetEntity(i);
                    entity.Get<CutWoodEvent>();
                }
            }
        }
    }
}