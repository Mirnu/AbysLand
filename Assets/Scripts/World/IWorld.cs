using Assets.Scripts.Resources.Data;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace World {
    public interface IWorld {
        public abstract void Generate(string seed);
        public abstract TileBase GetObjects(Vector2 pos);
        public abstract void DestroyAtTile(int points, Vector2Int tilePos);
        public abstract void Put(Resource resource);
    }
}