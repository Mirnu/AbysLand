using Assets.Scripts.World.Internal;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Assets.Scripts.World.Generators.GenerationStages
{
    public class ArrangingTilesGenerator : IGenerator
    {
        private const int COST_GENERATION = 10;
        private const string NAME_GENEARATION = "Arranging Tiles";

        private int[,] map;

        public int CostGeneration => COST_GENERATION;

        public string NameGeneration => NAME_GENEARATION;

        private Queue<Vector2Int> _border = new Queue<Vector2Int>();
        private List<Vector2Int> _processed = new List<Vector2Int>();
        private List<Vector2Int> _all = new List<Vector2Int>();

        public List<Tile> Tiles;
        public Tilemap BackgroundTiles;
        public List<Tilemap> DecorTiles;
        public List<BiomeFeature> features;
        public List<int[,]> decorMaps;

        private Dictionary<BiomeFeature, int> _lastGenerated = new Dictionary<BiomeFeature, int>();

        public ArrangingTilesGenerator(WorldModel model)
        {
            map = model.Map;
            DecorTiles = model.DecorTiles;
            BackgroundTiles = model.BackgroundTiles;
            Tiles = model.Tiles;
            features = model.Features;
            decorMaps = model.DecorMaps;
        }

        public IEnumerator Generate()
        {
            GenerateTilemap(map, BackgroundTiles);

            GenerateBiome(map, 2, new Vector2Int(0, 0), 10, 10, features);
            GenerateBiome(map, 2, new Vector2Int(40, 25), 7, 8, features);

            GenerateTilemap(map, BackgroundTiles);
            
            // GenerateTilemap(decorMaps[0], DecorTiles[0]); - call bug
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

        private void GenerateBiome(int[,] _ground_map, int ground, Vector2Int center, float maxDistance, float random_procent, List<BiomeFeature> features)
        {
            // Initial splotch
            _border.Enqueue(center);
            _all.Add(center);
            while (_border.Count > 0)
            {
                var _current = _border.Dequeue();
                if (Vector2.Distance(_current, center) <= maxDistance + Random.Range(0, random_procent) && !_processed.Contains(_current))
                {
                    var _currentNeighbors = GetNeighbors(_ground_map, _current);
                    _currentNeighbors.ForEach(x => {
                        if (Random.Range(0f, 100f) < random_procent * 10)
                        {
                            _ground_map[x.x, x.y] = ground;
                            _border.Enqueue(x);
                            _all.Add(x);
                        }
                    });
                    _processed.Add(_current);
                }
            }
            // edging (smoothing)
            _all.ForEach(_current => {
                var _currentNeighbors = GetNeighbors(_ground_map, _current, 0);
                if (_currentNeighbors.Count() > 1)
                {
                    _currentNeighbors.ForEach(x => {
                        map[x.x, x.y] = ground;
                        _processed.Add(x);
                    });
                }
            });
            // getting all cells into _all
            _processed.Except(_all).ToList().ForEach(x => _all.Add(x));
            // Spawning biome features
            _all.ForEach(x => {
                foreach (var f in features)
                {
                    if (!_lastGenerated.ContainsKey(f)) { _lastGenerated[f] = 0; break; }
                    if (_lastGenerated[f] >= _all.Count / f.MaxSpawnAmount)
                    {
                        decorMaps[(int)f.Layer][x.x, x.y] = f.index;
                        _lastGenerated[f] = 0;
                    }
                }
                foreach (var item in _lastGenerated.Keys.ToList())
                {
                    _lastGenerated[item]++;
                }
            });
            foreach (var item in _lastGenerated.Keys.ToList()) { _lastGenerated[item] = 0; }
        }

        private List<Vector2Int> GetNeighbors(int[,] _map, Vector2Int pos)
        {
            List<Vector2Int> n = new List<Vector2Int>();
            for (int i = pos.x == 0 ? 0 : pos.x - 1; i <= pos.x + 1; i++)
            {
                if (i != pos.x)
                {
                    n.Add(new Vector2Int(i, pos.y));
                }
            }
            for (int j = pos.y == 0 ? 0 : pos.y - 1; j <= pos.y + 1; j++)
            {
                if (j >= _map.GetUpperBound(0) - 1) { break; }
                if (j != pos.y)
                {
                    n.Add(new Vector2Int(pos.x, j));
                }
            }
            return n;
        }

        private List<Vector2Int> GetNeighbors(int[,] _map, Vector2Int pos, int index)
        {
            List<Vector2Int> n = new List<Vector2Int>();
            for (int i = pos.x == 0 ? 0 : pos.x - 1; i <= pos.x + 1; i++)
            {
                if (_map[i, pos.y] == index && i != pos.x)
                {
                    n.Add(new Vector2Int(i, pos.y));
                }
            }
            for (int j = pos.y == 0 ? 0 : pos.y - 1; j <= pos.y + 1; j++)
            {
                if (j >= _map.GetUpperBound(0) - 1) { break; }
                if (_map[pos.x, j] == index && j != pos.y)
                {
                    n.Add(new Vector2Int(pos.x, j));
                }
            }
            return n;
        }
    }
}
