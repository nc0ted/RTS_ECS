using Components;
using Leopotam.Ecs;
using UnityEngine;

namespace Systems.NPC
{
    public class SendCutWoodNpcEvent: IEcsRunSystem
    {
        private readonly EcsWorld _world = null;
        private EcsFilter<NpcComponent> npcFilter = null;
        public void Run()
        {
            foreach (var npc in npcFilter)
            {
                ref var npcComponent = ref npcFilter.Get1(npc);
                if (npcComponent.canGetResources)
                {
                    ref var npcEntity = ref npcFilter.GetEntity(npc);
                    npcEntity.Get<CutWoodNpcEvent>();
                    npcComponent.canGetResources = false;
                }
            }
        }
    }
}