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
        bindGen();
    }

    private void bindModels()
    {
        Container.BindInterfacesAndSelfTo<PlayerStatsMaxModel>().AsSingle();
        Container.BindInterfacesAndSelfTo<PlayerStatsModel>().AsSingle();
        Container.BindInterfacesAndSelfTo<PlayerStatsRecoveryModel>().AsSingle();
        Container.BindInterfacesAndSelfTo<PlayerBoostModel>().AsSingle();
    }

    private void bindGen() {
        Container.Bind<UpperWorldGen>().FromSubContainerResolve().ByMethod(InstallGen).WithKernel().AsSingle();
    }

    //Хз куда его правильно ставить сорян
    private void InstallGen(DiContainer diContainer) {
        diContainer.Bind<UpperWorldGen>().AsSingle();
        //diContainer.BindInstance("Hello world!");
    }
}