using System.Collections.Generic;
using System.Linq;
using UnityEditor.Timeline.Actions;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace World {
public class UpperWorldGen : MonoBehaviour, IWorld {

        [SerializeField] private List<Tile> Tiles = new List<Tile>();
        [SerializeField] private Tilemap BackgroundTiles;

        public float scale = 1.0F;
        private string seed = "";
        
        private int[,] map = new int[101, 101];

        private void Start() {
            Generate("test");
        }

        public void Generate(string seed)
        {
            this.seed = seed;
            Random.InitState(seed.GetHashCode());

            GenerateCorners();

            GenerateBiomePatch(Random.Range(10, 81), Random.Range(10, 30), 
            20, 10, 1, 25);

            GenerateBiomePatch(Random.Range(60, 90), Random.Range(55, 90), 
            8, 16, 3, 25);

            GenerateBiomePatch(Random.Range(60, 90), Random.Range(60, 90), 
            10, 8, 3, 25);

            GenerateTilemap(map, BackgroundTiles);
        }

        #region Base Gen
        

        //Ugly ass nigga
        private void GenerateCorners() {
            var _x = Random.Range(0, 2) == 1 ? map.GetUpperBound(0) - 1 : 0;
            var _y = Random.Range(0, 2) == 1 ? map.GetUpperBound(0) - 1 : 0;
            map[_x, _y] = 2;
            var r = _x == map.GetUpperBound(0) - 1 ? 0 : map.GetUpperBound(0) - 1;
            var r1 = _y == map.GetUpperBound(0) - 1 ? 0 : map.GetUpperBound(0) - 1;
            map[r , _y] = 2;
            map[map.GetUpperBound(0)/2, r1] = 2;
            map[map.GetUpperBound(0)/2, map.GetUpperBound(1)/2] = 2;
            GenerateLine(_x, _y, map.GetUpperBound(0)/2, map.GetUpperBound(1)/2);
            GenerateLine(r, _y, map.GetUpperBound(0)/2, map.GetUpperBound(1)/2);
            GenerateLine(map.GetUpperBound(0)/2, map.GetUpperBound(1)/2, map.GetUpperBound(0)/2, r1);
            FillAreaFromCorner(99, 99, 2, 1);
            FillAreaFromCorner(1, 0, 2, 3);
        }

        private void GenerateBiomePatch(int startX, int startY, int sizeX, int sizeY, int fillIndex, int random_fill) {
            GeneratePatch(startX, startY, sizeX, sizeY, random_fill, fillIndex);
            GeneratePatch(startX, startY, sizeX -  3, sizeY - 3, 100, fillIndex);
        }

        private void GeneratePatch(int x, int y, int sizeX, int sizeY, int random_fill, int fill_index) {
            System.Random pseudoRandom = new System.Random(seed.GetHashCode());;
            for (int i = x - sizeX / 2 <= 0 ? 0 : x - sizeX / 2; i < (map.GetUpperBound(0) >= x + sizeX / 2 ? x + sizeX / 2 : map.GetUpperBound(0)); i++)
            {
                for (int j = y - sizeY / 2 <= 0 ? 0 : y - sizeY / 2; j < (map.GetUpperBound(0) >= y + sizeY / 2 ? y + sizeY / 2 : map.GetUpperBound(0)); j++)
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
        private int GetNeighborCount(int x, int y, int index) {
            int wallCount = 0;
            for(int i = x - 1; i <= x + 1; i++) {
                if(map[i, y] == index && i != x) {
                    wallCount++;
                }
            }
            for(int j = y - 1; j <= y + 1; j++) {
                if(map[x, j] == index && j != y) {
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

        #region RiverGenTest

        private void GenerateRiver() {
            
        }


        #endregion
    }
}