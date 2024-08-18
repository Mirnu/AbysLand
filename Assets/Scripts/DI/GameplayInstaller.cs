using Assets.Scripts.Player.Stats.UI;
using Assets.Scripts.Player.Stats;
using UnityEngine;
using Zenject;
using Assets.Scripts.World;
using  Assets.Scripts.Entities;
using Assets.Scripts.Entities.Pathfinding;

public class GameplayInstaller : MonoInstaller
{
    public Entity ZombiePrefab;
    public override void InstallBindings()
    {
        bindModels();
    }

    private void bindModels()
    {
        Container.BindInterfacesAndSelfTo<PlayerStatsMaxModel>().AsSingle();
        Container.BindInterfacesAndSelfTo<PlayerStatsModel>().AsSingle();
        Container.BindInterfacesAndSelfTo<PlayerStatsRecoveryModel>().AsSingle();
        Container.BindInterfacesAndSelfTo<PlayerBoostModel>().AsSingle();

        Container.BindInterfacesAndSelfTo<EntitySpawner>().AsSingle();
        Container.BindFactory<IPathfindingStrategy, Entity, Entity.Factory>().FromComponentInNewPrefab(ZombiePrefab);
        Container.Bind<IPathfindingStrategy>().To<AStarPathfindingStrategy>().AsSingle();
    }
}