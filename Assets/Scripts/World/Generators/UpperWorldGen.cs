using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Assets.Scripts.Resources.Data;
using ModestTree;
using UnityEditor.Timeline.Actions;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace World {
public class UpperWorldGen : MonoBehaviour, IWorld {

        [SerializeField] private List<Tile> Tiles = new List<Tile>();
        [SerializeField] private Tilemap BackgroundTiles;
        [SerializeField] private List<Tilemap> DecorTiles;
        [Space]
        [SerializeField] private List<BiomeFeature> features;
        private TilemapPlayerInteraction _interactor;

        public float scale = 1.0F;
        private string seed = "";
        private int[,] map = new int[101, 101];
        private float _probability = 100f;

        private List<int[,]> decorMaps = new List<int[,]>();

        private Queue<Vector2Int> _border = new Queue<Vector2Int>();
        private List<Vector2Int> _processed = new List<Vector2Int>();
        private List<Vector2Int> _all = new List<Vector2Int>();

        public TileBase GetObjects(Vector2 pos) => _interactor.GetObjects(pos);

        public void DestroyAtTile(int points, Vector2Int tilePos) => _interactor.DestroyAtTile(points, tilePos);

        public void Put(Resource resource) => _interactor.Put(resource);

        private void Start() {
            for(int i = 0; i < 4; i++) { 
                var l = new int[101, 101];
                for (int k = 0; k < l.GetLength(0); k++) {
                    for (int j = 0; j < l.GetLength(1); j++) {
                        l[k, j] = -1;
                    }
                }
                decorMaps.Add(l);
            }

            Generate("gkjsagbvnadklfjbhvneoiabjne");
        }

        public void Generate(string seed)
        {
            this.seed = seed;
            Random.InitState(seed.GetHashCode());

            GenerateCorners();

            GenerateTilemap(map, BackgroundTiles);

            GenerateBiome(map, decorMaps[0], 2, new Vector2Int(0,  0), 10, 1, features);
            GenerateBiome(map, decorMaps[0], 2, new Vector2Int(40, 25), 7, 1, features);

            GenerateTilemap(map, BackgroundTiles);
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
            Debug.Log("----STARTED GENERATING TILES----");
            Debug.Log(" == " + tilemap.name + " == ");
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

        private void GenerateBiome(int[,] _ground_map, int[,] decor_map, int ground, Vector2Int center, float maxDistance, float probabilityFallof, List<BiomeFeature> features) {
            // Initial splotch
            _border.Enqueue(center);
            _all.Add(center);
            while (_border.Count > 0 && _probability > 0) {
                var _current = _border.Dequeue();
                if(Vector2.Distance(_current, center) <= maxDistance + Random.Range(0, probabilityFallof) && !_processed.Contains(_current)) {
                    var _currentNeighbors = GetNeighbors(_ground_map, _current);
                    _currentNeighbors.ForEach(x => {
                        if(Random.Range(0f, 100f) < _probability) {
                            _ground_map[x.x, x.y] = ground;
                            _border.Enqueue(x);
                            _all.Add(x);
                        }
                    });
                    _probability -= probabilityFallof * maxDistance / 100;
                    _processed.Add(_current);
                }
            }
            // edging 
            _all.ForEach(_current => {
                var _currentNeighbors = GetNeighbors(_ground_map, _current, 0);
                if (_currentNeighbors.Count() > 1) {
                    _currentNeighbors.ForEach(x => { 
                        map[x.x, x.y] = ground; 
                        _processed.Add(x);
                    });
                }
            });
            _processed.Except(_all).ToList().ForEach(x => _all.Add(x));
            _all.ForEach(x => {
                foreach (var f in features) {
                    //Потом тут нагенерю
                }
            });
        }

        #endregion

        #region RiverGenTest

        private void GenerateRiver() {
            
        }

        

        #endregion
    }
}