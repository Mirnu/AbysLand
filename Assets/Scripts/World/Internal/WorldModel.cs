using System.Collections.Generic;
using Assets.Scripts.World.Biomes;
using UnityEngine.Tilemaps;

namespace Assets.Scripts.World.Internal
{
    public class WorldModel
    {
        private int _size;

        public int[,] Map;
        public int[,] Durability;

        public List<Tile> Tiles;
        public Tilemap BackgroundTiles;
        public List<Tilemap> DecorTiles;
        public List<Biome> Biomes;

        public WorldModel(int size, List<Tile> tiles, Tilemap backgroundTiles, 
            List<Tilemap> decorTiles, List<Biome> features)
        {
            _size = size;
            Map = new int[size, size];
            Durability = new int[size, size];
            Tiles = tiles;
            BackgroundTiles = backgroundTiles;
            DecorTiles = decorTiles;
            Biomes = features;
        }

        public int Size => _size;
    }
}
