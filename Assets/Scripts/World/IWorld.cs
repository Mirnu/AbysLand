using Assets.Scripts.Resources.Data;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Assets.Scripts.World {
    public interface IWorld {
        public void Generate(string seed);
        public TileBase GetObjects(Vector2 pos);
        public void DestroyAtTile(int points, Vector2Int tilePos);
        public void Put(Resource resource);
    }
}