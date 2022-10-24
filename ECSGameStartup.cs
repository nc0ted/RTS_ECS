using Components;
using Leopotam.Ecs;
using Systems;
using Systems.NPC;
using UnityEngine;
using Voody.UniLeo;

public class ECSGameStartup : MonoBehaviour
{
    private bool start;
    private EcsWorld world;
    private EcsSystems systems;

    private void Start()
    {
        world = new EcsWorld();
        systems = new EcsSystems(world);
            
        systems.ConvertScene();
            
        AddInjections();
        AddOneFrames();
        AddSystems();
            
        systems.Init();
    }

    private void AddInjections()
    {
        
    }
    private void Update()
    {
        if(start)
            systems.Run();
    }

    private void AddSystems()
    {
        systems.Add(new MouseInputSystem()).Add(new GridSystem())
            .Add(new SendEnableBuildEventSystem()).Add(new EnableBuildSystem())
            .Add(new BuildOnGridSystem()).Add(new SendSetPathEventSystem())
            .Add(new SetPathSystem()).Add(new SetStartNodeSystem())
            .Add(new SendCutResourceEvent()).Add(new InitTreeSystem())
            .Add(new SendNpcPathEventSystem()).Add(new SetNpcPathSystem())
            .Add(new FindPathSystem()).Add(new GridMovementSystem())
            .Add(new NpcMovementSystem()).Add(new SendCutWoodNpcEvent())
            .Add(new NpcCutTreeSystem());
    }
    private void AddOneFrames()
    {
        systems.OneFrame<SetPathEvent>()
            .OneFrame<EnableBuildEvent>().OneFrame<CutWoodEvent>()
            .OneFrame<SetNpcPathEvent>().OneFrame<CutWoodNpcEvent>();
    }
    private void OnDestroy()
    {
        if (systems == null) return;
        
        systems.Destroy();
        systems = null;
        world.Destroy();
        world = null;
    }

    public void StartGame()
    {
        start = true;
    }
}