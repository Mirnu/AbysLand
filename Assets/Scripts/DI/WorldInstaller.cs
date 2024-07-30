using Assets.Scripts.World;
using Assets.Scripts.World.Generators.GenerationStages;
using Assets.Scripts.World.Internal;
using System.Collections.Generic;
using UnityEngine.Tilemaps;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.DI
{
    public class WorldInstaller : MonoInstaller
    {
        [SerializeField] private List<Tile> Tiles;
        [SerializeField] private Tilemap BackgroundTiles;
        [SerializeField] private List<Tilemap> DecorTiles;
        [Space]
        [SerializeField] private List<BiomeFeature> features;

        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<CornersGenerator>().AsSingle();
            Container.BindInterfacesAndSelfTo<ArrangingTilesGenerator>().AsSingle();
            Container.BindInterfacesAndSelfTo<WorldModel>().AsSingle()
                .WithArguments(101, Tiles, BackgroundTiles, DecorTiles, features);
        }
    }
}
