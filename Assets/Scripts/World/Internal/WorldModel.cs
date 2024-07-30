using System.Collections.Generic;
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
        public List<BiomeFeature> Features;
        public List<int[,]> DecorMaps = new List<int[,]>();

        public WorldModel(int size, List<Tile> tiles, Tilemap backgroundTiles, 
            List<Tilemap> decorTiles, List<BiomeFeature> features)
        {
            _size = size;
            Map = new int[size, size];
            Durability = new int[size, size];
            Tiles = tiles;
            BackgroundTiles = backgroundTiles;
            DecorTiles = decorTiles;
            Features = features;
        }

        public int Size => _size;
    }
}
