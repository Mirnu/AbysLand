using Assets.Scripts.World.Biomes;
using Assets.Scripts.World.Internal;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Assets.Scripts.World.Generators.GenerationStages
{
    public class ArrangingBaseTilesGenerator : IGenerator
    {
        private const int COST_GENERATION = 10;
        private const string NAME_GENEARATION = "Arranging Ground";

        private int[,] map;

        public int CostGeneration => COST_GENERATION;

        public string NameGeneration => NAME_GENEARATION;

        public List<Tile> Tiles;
        public Tilemap BackgroundTiles;

        public ArrangingBaseTilesGenerator(WorldModel model)
        {
            map = model.Map;
            BackgroundTiles = model.BackgroundTiles;
            Tiles = model.Tiles;
        }

        public IEnumerator Generate()
        {
            GenerateTilemap(map, BackgroundTiles);

            yield return null;
        }

        private void GenerateTilemap(int[,] map, Tilemap tilemap)
        {
            tilemap.ClearAllTiles();
            for (int x = 0; x < map.GetUpperBound(0); x++)
            {
                for (int y = 0; y < map.GetUpperBound(1); y++)
                {
                    if (map[x, y] != -1)
                    {
                        tilemap.SetTile(new Vector3Int(x, y, 0), Tiles[map[x, y]]);
                    }
                }
            }
        }

    }
}
