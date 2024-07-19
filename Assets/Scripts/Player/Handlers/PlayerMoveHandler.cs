using Assets.Scripts.Player.Model;
using System;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Player.Handlers
{
    public class PlayerMoveHandler : IInitializable, ITickable
    {
        private PlayerModel _model;
        private PlayerInput _input;
        private Settings _settings;

        public PlayerMoveHandler(PlayerModel model, Settings settings, PlayerInput input)
        {
            _model = model;
            _settings = settings;
            _input = input;
        }

        public void Initialize() 
        { 
            _input.Enable();
        }

        public void Tick()
        {
            Vector2 direction = _input.Gameplay.Movement.ReadValue<Vector2>();
            if (direction.x > 0 && direction.y > 0)
            {
                direction.x = 0.7f;
                direction.y = 0.7f;
            }
            Vector2 deltaPos = direction * Time.deltaTime * _settings.Speed;
            _model.MovePosition(deltaPos);
        }

        [Serializable]
        public class Settings
        {
            public float Speed;
        }
    }
}
