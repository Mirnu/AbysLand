using Assets.Scripts.Player.Stats.UI;
using Assets.Scripts.Player.Stats;
using Zenject;
using Assets.Scripts.Game;

public class GameplayInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        bindEntryPoint();
        bindModels();
    }

    private void bindEntryPoint()
    {
        Container.BindInterfacesAndSelfTo<GameEntryPoint>().AsSingle().NonLazy();
        Container.BindInterfacesAndSelfTo<WorldInitializer>().AsSingle();
    }

    private void bindModels()
    {
        Container.BindInterfacesAndSelfTo<PlayerStatsMaxModel>().AsSingle();
        Container.BindInterfacesAndSelfTo<PlayerStatsModel>().AsSingle();
        Container.BindInterfacesAndSelfTo<PlayerStatsRecoveryModel>().AsSingle();
        Container.BindInterfacesAndSelfTo<PlayerBoostModel>().AsSingle();
    }
}