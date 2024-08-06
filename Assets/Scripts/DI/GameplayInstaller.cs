using Assets.Scripts.Player.Stats;
using Zenject;
using Assets.Scripts.Game;
using Assets.Scripts.Player.Stats.LaU;

public class GameplayInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        bindEntryPoint();
        bindLaU();
        bindModels();
    }

    private void bindLaU()
    {
        Container.BindInterfacesAndSelfTo<PlayerStatsLaU>().AsSingle().NonLazy(); 
    }

    private void bindEntryPoint()
    {
        Container.BindInterfacesAndSelfTo<GameplayEntryPoint>().AsSingle().NonLazy();
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