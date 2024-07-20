using Assets.Scripts.Player.Stats.UI;
using Assets.Scripts.Player.Stats;
using UnityEngine;
using Zenject;

public class GameplayInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.BindInterfacesAndSelfTo<PlayerStatsMaxModel>().FromNew().AsSingle().NonLazy();
        Container.BindInterfacesAndSelfTo<PlayerStatsModel>().FromNew().AsSingle().NonLazy();
        Container.BindInterfacesAndSelfTo<PlayerStatsRecoveryModel>().FromNew().AsSingle().NonLazy();
    }
}