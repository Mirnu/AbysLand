using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Resources.Data;
using ModestTree;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Assets.Scripts.World {
public class UpperWorldGen : MonoBehaviour, IWorld {

        [SerializeField] private List<Tile> Tiles = new List<Tile>();
        [SerializeField] private Tilemap BackgroundTiles;
        [SerializeField] private List<Tilemap> DecorTiles;
        [Space]
        [SerializeField] private List<BiomeFeature> features;

        public float scale = 1.0F;
        private string seed = "";
        private int[,] map = new int[101, 101];
        //Заглушка
        private int[,] _durability = new int[101, 101];

        private List<int[,]> decorMaps = new List<int[,]>();

        private Queue<Vector2Int> _border = new Queue<Vector2Int>();
        private List<Vector2Int> _processed = new List<Vector2Int>();
        private List<Vector2Int> _all = new List<Vector2Int>();
        
        private Dictionary<BiomeFeature, int> _lastGenerated = new Dictionary<BiomeFeature, int>(); 

        public TileBase GetObjects(Vector2 pos) {
            return BackgroundTiles.GetTile(BackgroundTiles.WorldToCell(new Vector3(pos.x, pos.y, BackgroundTiles.transform.position.z)));
        }

        public void DestroyAtTile(int points, Vector2Int tilePos) {
            var l = map[tilePos.x, tilePos.y];
            if(l > points) {
                _durability[tilePos.x, tilePos.y] -= points;
            } else {
                _durability[tilePos.x, tilePos.y] = 0;
            }
        }

        public void Put(Resource resource) { 
            
        }

        private void Start() => Initialize();

        public void Initialize() {
            for(int i = 0; i < 4; i++) { 
                var l = new int[101, 101];
                for (int k = 0; k < l.GetLength(0); k++) {
                    for (int j = 0; j < l.GetLength(1); j++) {
                        l[k, j] = -1;
                    }
                }
                decorMaps.Add(l);
            }

            Generate("test");
        }

        public void Generate(string seed)
        {
            this.seed = seed;
            Random.InitState(seed.GetHashCode());

            GenerateCorners();

            GenerateTilemap(map, BackgroundTiles);

            GenerateBiome(map, 2, new Vector2Int(0,  0), 10, 10, features);
            GenerateBiome(map, 2, new Vector2Int(40, 25), 7, 8, features);

            GenerateTilemap(map, BackgroundTiles);

            _durability = map;

            GenerateTilemap(decorMaps[0], DecorTiles[0]);
        }

        #region Base Gen
        

        //Ugly ass nigga
        private void GenerateCorners() {
            map[map.GetUpperBound(0)/2, map.GetUpperBound(0)-1] = 2;
            map[map.GetUpperBound(0)/2, map.GetUpperBound(0)/2] = 2;
            map[0, map.GetUpperBound(1)/2] = 2;
            map[map.GetUpperBound(0) - 1, map.GetUpperBound(1)/2] = 2;
            GenerateLine(map.GetUpperBound(0)/2, map.GetUpperBound(0)/2, map.GetUpperBound(0)/2, map.GetUpperBound(1)-1);
            GenerateLine(map.GetUpperBound(0)/2, map.GetUpperBound(0)/2, 0, map.GetUpperBound(1)/2);
            GenerateLine(map.GetUpperBound(0)/2, map.GetUpperBound(0)/2, map.GetUpperBound(0), map.GetUpperBound(1)/2);
            FillAreaFromCorner(99, 99, 2, 1);
            FillAreaFromCorner(0, 99, 2, 3);
        }

        private void GenerateLine(int x0, int y0, int x1, int y1) {
            var l = x0 > x1 ? -1 : 1;
            var l1 = y0 > y1 ? -1 : 1;
            int j = y0;
            int i = x0;
            while (i != x1 || j != y1) {
                map[i, j] = 2;
                if(j != y1) {
                    j += l1;
                }
                if(i != x1) {
                    i+=l;
                }
            }
        }

        private void FillAreaFromCorner(int startX, int startY, int borderIndex, int fillIndex) {
            var b = startY < map.GetUpperBound(0) - 1 ? 1 : -1;
            var a = startX < map.GetUpperBound(0) - 1 ? 1 : -1;
            for(int i = startX; i < map.GetLength(0); i+=a) {
                if(map[i, startY] == borderIndex) { break; }
                for(int j = startY; j < map.GetLength(0); j+=b) {
                    if (map[i, j] == borderIndex) { break; }
                    map[i, j] = fillIndex;
                }
            }
        }

        #endregion

        #region General

        private List<Vector2Int> GetNeighbors(int[,] _map, Vector2Int pos) {
            List<Vector2Int> n = new List<Vector2Int>();
            for(int i = pos.x == 0 ? 0 : pos.x - 1; i <= pos.x + 1; i++) {
                if(i != pos.x) {
                    n.Add(new Vector2Int(i, pos.y));
                }
            }
            for(int j = pos.y == 0 ? 0 : pos.y - 1; j <= pos.y + 1; j++) {
                if(j >= _map.GetUpperBound(0) - 1) { break; }
                if(j != pos.y) {
                    n.Add(new Vector2Int(pos.x, j));
                }
            }
            return n;
        }

        private List<Vector2Int> GetNeighbors(int[,] _map, Vector2Int pos, int index) {
            List<Vector2Int> n = new List<Vector2Int>();
            for(int i = pos.x == 0 ? 0 : pos.x - 1; i <= pos.x + 1; i++) {
                if(_map[i, pos.y] == index && i != pos.x) {
                    n.Add(new Vector2Int(i, pos.y));
                }
            }
            for(int j = pos.y == 0 ? 0 : pos.y - 1; j <= pos.y + 1; j++) {
                if(j >= _map.GetUpperBound(0) - 1) { break; }
                if(_map[pos.x, j] == index && j != pos.y) {
                    n.Add(new Vector2Int(pos.x, j));
                }
            }
            return n;
        }

        private void GenerateTilemap(int[,] map, Tilemap tilemap) {
            tilemap.ClearAllTiles();
            for (int x = 0; x < map.GetUpperBound(0) ; x++)
            {
                for (int y = 0; y < map.GetUpperBound(1); y++)
                {
                    if (map[x, y] != -1)
                    {
                        tilemap.SetTile(new Vector3Int(x, y, 0), Tiles[map[x,y]]);
                    }
                }
            }
        }

        #endregion

        #region Biome

        private void GenerateBiome(int[,] _ground_map, int ground, Vector2Int center, float maxDistance, float random_procent, List<BiomeFeature> features) {
            // Initial splotch
            _border.Enqueue(center);
            _all.Add(center);
            while (_border.Count > 0) {
                var _current = _border.Dequeue();
                if(Vector2.Distance(_current, center) <= maxDistance + Random.Range(0, random_procent) && !_processed.Contains(_current)) {
                    var _currentNeighbors = GetNeighbors(_ground_map, _current);
                    _currentNeighbors.ForEach(x => {
                        if(Random.Range(0f, 100f) < random_procent * 10) {
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
                if (_currentNeighbors.Count() > 1) {
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
                foreach (var f in features) {
                    if(!_lastGenerated.ContainsKey(f)) { _lastGenerated[f] = 0; break; }
                    if(_lastGenerated[f] >= _all.Count / f.MaxSpawnAmount) {
                        decorMaps[(int)f.Layer][x.x, x.y] = f.index;
                        _lastGenerated[f] = 0;
                    }
                }
                foreach(var item in _lastGenerated.Keys.ToList())
                {
                    _lastGenerated[item]++;
                }
            });
            foreach(var item in _lastGenerated.Keys.ToList()) { _lastGenerated[item] = 0; }
        }

        #endregion

        #region RiverGenTest

        private void GenerateRiver() {
            
        }



        #endregion
    }
}