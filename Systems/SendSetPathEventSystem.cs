using Components;
using Leopotam.Ecs;
using UnityEngine;

namespace Systems
{
    public class SendSetPathEventSystem:IEcsRunSystem
    {
        private readonly EcsWorld _world = null;
        private EcsFilter<SetPathComponent> filter=null;
        public void Run()
        {
            if (!Input.GetMouseButtonDown(0)) return;
            foreach (var i in filter)
            {
                ref var entity = ref filter.GetEntity(i);
                entity.Get<SetPathEvent>();
            }
        }
    }
}