using Assets.Scripts.Game;
using Assets.Scripts.Game.States;
using Assets.Scripts.Game.Systems;
using Assets.Scripts.Misc.Utils;
using Assets.Scripts.Saving;
using Zenject;

public class GlobalInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        bindManagers();
        bindSystems();
        bindStates();
        bindMisc();
    }

    private void bindStates()
    {
        Container.BindInterfacesAndSelfTo<LoadingState>().AsSingle().NonLazy();
    }

    private void bindSystems()
    {
        Container.BindInterfacesAndSelfTo<GameStateObserver>().AsSingle();
    }

    private void bindManagers()
    {
        Container.BindInterfacesAndSelfTo<GameManager>().AsSingle().NonLazy();
        Container.BindInterfacesAndSelfTo<SavingManager>().AsSingle().NonLazy();
    }

    private void bindMisc()
    {
        Container.BindInterfacesAndSelfTo<Routine>().
            FromNewComponentOnNewGameObject().AsSingle();
        Container.BindInterfacesAndSelfTo<AngleUtils>().AsSingle();
    }
}