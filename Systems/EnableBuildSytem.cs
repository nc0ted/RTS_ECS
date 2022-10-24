using Components;
using Leopotam.Ecs;
using UnityEngine;

namespace Systems
{
    public class EnableBuildSystem:IEcsRunSystem
    {
        private readonly EcsWorld _world = null;
        private EcsFilter<BuildOnGridComponent,EnableBuildEvent> filter = null;
        public void Run()
        {
            if (!filter.IsEmpty())
            {
                ref var buildOnGridComponent = ref filter.Get1(0);
                buildOnGridComponent.canBuild = !buildOnGridComponent.canBuild;
                Debug.Log(buildOnGridComponent.canBuild);
            }
        }
    }
}