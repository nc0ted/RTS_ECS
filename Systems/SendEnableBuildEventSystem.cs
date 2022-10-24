using Components;
using Leopotam.Ecs;
using UnityEngine;

namespace Systems
{
    public class SendEnableBuildEventSystem:IEcsRunSystem
    {
        private readonly EcsWorld _world = null;
        private EcsFilter<BuildOnGridComponent> filter = null;
        
        public void Run()
        {
            if (!Input.GetKeyDown(KeyCode.B)) return;
            foreach (var i in filter)
            {
                ref var entity = ref filter.GetEntity(i);
                entity.Get<EnableBuildEvent>();
            }
        }
    }
}