using System;
using Assets.Scripts.Player.Model;
using Assets.Scripts.World;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;
using Zenject;

namespace Assets.Scripts.Player.Handlers
{
    public class PlayerDestroyHandler : IInitializable, IDisposable, ITickable
    {
        protected TileBase _highlight;
        protected Tilemap _tilemap;
        protected PlayerModel _movement;

        protected Vector3Int _targeted;

        private readonly PlayerInput _input;
        private DamageableHandler _handler;

        public PlayerDestroyHandler (PlayerInput input, DamageableHandler handler, TileBase hilghlight, Tilemap tilemap, PlayerModel model) {
            _input = input;
            _handler = handler;
            _highlight = hilghlight;
            _tilemap = tilemap;
            _movement = model;
        }

        public void Initialize() { _input.Gameplay.Mouse.performed += TryBreak; }

        public void Dispose() { _input.Gameplay.Mouse.performed -= TryBreak; }

        private void TryBreak(InputAction.CallbackContext callback) {
            if(callback.ReadValue<float>() == 1 && _handler.CanDamage(_targeted)) {
                _handler.Damage(_targeted, 10);
            }
        }

        public void Tick()
        {
            var e = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue()) - _movement.Position;
            _targeted = new Vector3Int((int)Mathf.Floor(_movement.Position.x + (e.normalized.x * 10)), (int)Mathf.Floor(_movement.Position.y + (e.normalized.y < -0.01f ? -0.1f : e.normalized.y) * 10), 0);
            _tilemap.ClearAllTiles();
            _tilemap.SetTile(_targeted, _highlight);
        }
    }
}