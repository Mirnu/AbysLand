using Assets.Scripts.Player.Stats;
using Zenject;
using Assets.Scripts.Game;
using Assets.Scripts.Player.Stats.LaU;
using UnityEngine;
using System.Collections.Generic;
using Assets.Scripts.Resources.Crafting;

public class GameplayInstaller : MonoInstaller
{

    [Space][Header("Recipes")]
    [SerializeField] private List<Recipe> recipes = new List<Recipe>();  

    public override void InstallBindings()
    {
        Container.BindInterfacesAndSelfTo<RecipeContainer>().AsSingle()
            .WithArguments(recipes);

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