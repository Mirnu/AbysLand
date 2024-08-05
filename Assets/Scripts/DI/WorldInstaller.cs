using Assets.Scripts.World;
using Assets.Scripts.World.Generators.GenerationStages;
using Assets.Scripts.World.Internal;
using System.Collections.Generic;
using UnityEngine.Tilemaps;
using UnityEngine;
using Zenject;
using Assets.Scripts.World.Biomes;

namespace Assets.Scripts.DI
{
    public class WorldInstaller : MonoInstaller
    {
        [SerializeField] private List<Tile> Tiles;
        [SerializeField] private Tilemap BackgroundTiles;
        [SerializeField] private List<Tilemap> DecorTiles;
        [Space]
        [SerializeField] private List<Biome> biomes;
        [Space]
        [SerializeField] private Tilemap tilemap;
        [SerializeField] private List<DmgTile> healthDict = new List<DmgTile>();

        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<CornersGenerator>().AsSingle();
            Container.BindInterfacesAndSelfTo<ArrangingBaseTilesGenerator>().AsSingle();
            Container.BindInterfacesAndSelfTo<ArrangingBiomesGenerator>().AsSingle();
            Container.BindInterfacesAndSelfTo<DamageableHandler>().AsSingle()
                .WithArguments(tilemap, healthDict);
            Container.BindInterfacesAndSelfTo<WorldSaver>().AsSingle()
                .WithArguments(BackgroundTiles);
            Container.BindInterfacesAndSelfTo<WorldModel>().AsSingle()
                .WithArguments(101, Tiles, BackgroundTiles, DecorTiles, biomes);
        }
    }
}
