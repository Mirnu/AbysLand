using Assets.Scripts.Player.Stats.UI;
using Assets.Scripts.Player.Stats;
using UnityEngine;
using Zenject;
using Assets.Scripts.World;

public class GameplayInstaller : MonoInstaller
{
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
    }
}