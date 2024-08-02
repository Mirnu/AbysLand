using System;
using Assets.Scripts.Player.Model;
using Assets.Scripts.Player.Systems;
using Assets.Scripts.World;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;
using Zenject;

namespace Assets.Scripts.Player.Handlers
{
    public class PlayerBreakHandler : ITickable, IInitializable, IDisposable
    {
        private TileBase _highlight;
        private Tilemap _tilemap;
        private PlayerModel _movement;
        private DamageableHandler _handler;
        private readonly PlayerInput _input;

        private Vector3Int _targeted;

        public PlayerBreakHandler(TileBase hilghlight, Tilemap tilemap, DamageableHandler handler, PlayerModel model, PlayerInput input) {
            _highlight = hilghlight;
            _tilemap = tilemap;
            _input = input;
            _movement = model;
            _handler = handler;
        }

        public void Initialize() { _input.Gameplay.Mouse.performed += TryBreak; }

        public void Dispose() { _input.Gameplay.Mouse.performed -= TryBreak; }

        public void Tick()
        {
            var e = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue()) - _movement.Position;
            _targeted = new Vector3Int((int)Mathf.Floor(_movement.Position.x + (e.normalized.x * 10)), (int)Mathf.Floor(_movement.Position.y + (e.normalized.y < -0.01f ? -0.1f : e.normalized.y) * 10), 0);
            _tilemap.ClearAllTiles();
            _tilemap.SetTile(_targeted, _highlight);
        }

        private void TryBreak(InputAction.CallbackContext callback) {
            if(callback.ReadValue<float>() == 1 && _handler.CanDamage(_targeted)) {
                _handler.Damage(_targeted, 10);
            }
        }
    }
}