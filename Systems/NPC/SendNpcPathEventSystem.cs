using Components;
using Leopotam.Ecs;
using UnityEngine;

namespace Systems
{
    public class SendNpcPathEventSystem : IEcsRunSystem
    {
        private readonly EcsWorld _world = null;
        private EcsFilter<NpcComponent> filter = null;
        
        public void Run()
        {
            foreach (var i in filter)
            {
                if (filter.Get1(i).hasTarget) continue;
                filter.Get1(i).hasTarget = true;
                ref var entity = ref filter.GetEntity(i);
                entity.Get<SetNpcPathEvent>();
            }
        }
    }
}