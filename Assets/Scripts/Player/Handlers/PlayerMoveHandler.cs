using Player.Model;
using System;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

namespace Player.Handlers
{
    public class PlayerMoveHandler : IInitializable, ITickable
    {
        private PlayerModel _model;
        private PlayerInput _input = new PlayerInput();

        public PlayerMoveHandler(PlayerModel model)
        {
            _model = model;
        }


        public void Initialize() 
        { 
            _input.Enable();
        }

        public void Tick()
        {
            Vector2 direction = _input.Gameplay.Movement.ReadValue<Vector2>() * 100 * Time.deltaTime;
            _model.MovePosition(direction);
        }
    }
}
