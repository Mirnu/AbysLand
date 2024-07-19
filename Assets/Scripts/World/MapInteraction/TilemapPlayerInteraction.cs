
using Resource;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace World {
    public class TilemapPlayerInteraction {
        
        private Tilemap _tilemap;
        private int[,] _durability;

        public TilemapPlayerInteraction(Tilemap tilemap, int[,] d_map) {
            _tilemap = tilemap;
            _durability = d_map;
        }

        public TileBase GetObjects(Vector2 pos) {
            return _tilemap.GetTile(_tilemap.WorldToCell(new Vector3(pos.x, pos.y, _tilemap.transform.position.z)));
        }

        public void DestroyAtTile(int points, Vector2Int tilePos) {
            var l = _durability[tilePos.x, tilePos.y];
            if(l > points) {
                _durability[tilePos.x, tilePos.y] -= points;
            } else {
                _durability[tilePos.x, tilePos.y] = 0;
            }
        }

        public void Put(ResourceInTheWorld resource) { }
    }
}