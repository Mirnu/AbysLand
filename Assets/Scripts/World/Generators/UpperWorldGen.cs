using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Assets.Scripts.Resources.Data;
using UnityEditor.Timeline.Actions;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace World {
public class UpperWorldGen : MonoBehaviour, IWorld {

        [SerializeField] private List<Tile> Tiles = new List<Tile>();
        [SerializeField] private Tilemap BackgroundTiles;
        [SerializeField] private List<Tilemap> decorativeTiles = new List<Tilemap>();
        private Dictionary<BiomeFeature, int> featureCount = new Dictionary<BiomeFeature, int>();

        public float scale = 1.0F;
        private string seed = "";
        

        private int[,] map = new int[101, 101];

        public TileBase GetObjects(Vector2 pos) => _interactor.GetObjects(pos);

        public void DestroyAtTile(int points, Vector2Int tilePos) => _interactor.DestroyAtTile(points, tilePos);

        public void Put(Resource resource) => _interactor.Put(resource);

        private void Start() {
            Generate("test-1");
        }

        public void Generate(string seed)
        {
            this.seed = seed;
            Random.InitState(seed.GetHashCode());

            GenerateCorners();

            GenerateTilemap(map, BackgroundTiles);

            GenerateBiome(new Vector2Int(0, 0), new Vector2Int(25, 50), Tiles[1], features1.ToArray());
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

        private void GenerateBiomePatch(int startX, int startY, int sizeX, int sizeY, int fillIndex, int random_fill) {
            GeneratePatch(startX, startY, sizeX, sizeY, random_fill, fillIndex);
            GeneratePatch(startX, startY, sizeX -  3, sizeY - 3, 100, fillIndex);
        }

        private void GeneratePatch(int x, int y, int sizeX, int sizeY, int random_fill, int fill_index) {
            System.Random pseudoRandom = new System.Random(seed.GetHashCode());
            for (int i = x - sizeX / 2 <= 0 ? 0 : x - sizeX / 2; i <= (map.GetUpperBound(0) >= x + sizeX / 2 ? x + sizeX / 2 : map.GetUpperBound(0)); i++)
            {
                for (int j = y - sizeY / 2 <= 0 ? 0 : y - sizeY / 2; j <= (map.GetUpperBound(0) >= y + sizeY / 2 ? y + sizeY / 2 : map.GetUpperBound(0)); j++)
                {
                    if(pseudoRandom.Next(0, 100) < random_fill) { map[i, j] = fill_index; } 
                }
            }
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

        //Прямые соседи (слева справа сверху снизу)
        private int GetNeighborCount(int x, int y, TileBase tile, Tilemap tilemap) {
            int wallCount = 0;
            for(int i = x - 1; i <= x + 1; i++) {
                if(tilemap.GetTile(new Vector3Int(i, y, 0)) == tile && i != x) {
                    wallCount++;
                }
            }
            for(int j = y - 1; j <= y + 1; j++) {
                if(tilemap.GetTile(new Vector3Int(x, j, 0)) == tile && j != y) {
                    wallCount++;
                }
            }
            return wallCount;    
        }

        private Dictionary<int, int[]> GetNeighbors(int x, int y) {
            Dictionary<int, int[]> n = new Dictionary<int, int[]>();
            for(int i = x == 0 ? 0 : x - 1; i <= x + 1; i++) {
                if(map[i, y] != -1 && i != x) {
                    n[map[i, y]] = new int[]{i, y};
                }
            }
            for(int j = y == 0 ? 0 : y - 1; j <= y + 1; j++) {
                if(j >= map.GetUpperBound(0) - 1) { break; }
                if(map[x, j] != -1 && j != y) {
                    n[map[x, j]] = new int[]{x, j};
                }
            }
            return n;
        }

        private void GenerateTilemap(int[,] map, Tilemap tilemap) {
            Debug.Log("----STARTED GENERATING TILES----");
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

        private void GenerateBiome(Vector2Int lowerLeft, Vector2Int upperRight, Tile groundBase, BiomeFeature[] features) {
            for(int i = lowerLeft.x; i < upperRight.x; i++) {
                for(int j = lowerLeft.y; j < upperRight.y; j++) {
                    BackgroundTiles.SetTile(new Vector3Int(i, j, 0), groundBase);
                    foreach(var f in features) {
                        var ch = Random.Range(0f, 100f);
                        if(ch < f.SpawnChance && (!featureCount.Keys.Contains(f) || featureCount[f] < f.MaxSpawnAmount)) {
                            if(!featureCount.Keys.Contains(f)) { featureCount[f] = 0; }
                            featureCount[f] += 1;
                            BackgroundTiles.SetTile(new Vector3Int(i, j, 0), f.Tile);
                            if(Random.Range(0f, 100f) < f.NeighborChance) {
                                featureCount[f] += 1;
                                BackgroundTiles.SetTile(new Vector3Int(i + Random.Range(-1, 2), j + Random.Range(-1, 2), (int)f.Layer), f.Tile);
                            }
                        }
                    }
                }
            }
        }

        #endregion

        #region RiverGenTest

        private void GenerateRiver() {
            
        }

        

        #endregion
    }
}