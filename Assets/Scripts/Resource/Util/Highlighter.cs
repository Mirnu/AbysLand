using Assets.Scripts.Player.Model;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;

namespace Assets.Scripts.Util {
    public class Highlighter {
        protected TileBase _highlight;
        protected Tilemap _tilemap;
        protected PlayerModel _movement;

        protected Vector3Int _targeted;

        public Highlighter(TileBase hilghlight, Tilemap tilemap, PlayerModel model) {
            _highlight = hilghlight;
            _tilemap = tilemap;
            _movement = model;
        }

        public Vector3Int Get(int reach) {
            var e = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue()) - _movement.Position;
            _targeted = new Vector3Int((int)Mathf.Floor(_movement.Position.x + (e.normalized.x * 10 * reach)), (int)Mathf.Floor(_movement.Position.y + (e.normalized.y < -0.01f ? e.normalized.y * 2 : e.normalized.y) * 10 * reach), 0);
            _tilemap.ClearAllTiles();
            _tilemap.SetTile(_targeted, _highlight);
            return _targeted;
        }
    }
}