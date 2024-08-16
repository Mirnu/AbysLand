using Assets.Scripts.Player.Model;
using Assets.Scripts.Player.Stats;
using System;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Player.Systems
{
    public class PlayerMovement : IInitializable, ITickable
    {
        private PlayerModel _model;
        private PlayerInput _input;
        private PlayerStatsModel _stats;

        public event Action StartMoved;
        public event Action StopMoved;
        public event Action PlayerMoved;

        private bool isStaying = true;

        public PlayerMovement(PlayerModel model,
            PlayerStatsModel playerStatsModel, PlayerInput input)
        {
            _model = model;
            _stats = playerStatsModel;
            _input = input;
        }

        public void Initialize()
        {
            _input.Enable();
        }

        public void Tick()
        {
            Vector2 direction = _input.Gameplay.Movement.ReadValue<Vector2>();
            if (direction.x != 0 && direction.y != 0)
            {
                direction.x *= 0.7f;
                direction.y *= 0.7f;
            }
            if ((direction.x != 0 || direction.y != 0) && isStaying)
            {
                StartMoved?.Invoke();
                isStaying = false;
            }
            if (direction.x == 0 && direction.y == 0 && !isStaying)
            {
                StopMoved?.Invoke();
                isStaying = true;
            }
            if (direction.x != 0 || direction.y != 0)
            {
                PlayerMoved?.Invoke();  
            }
            float speed = Time.deltaTime * _stats.Speed;

            Vector3 deltaPos = new Vector3(direction.x * speed, direction.y * speed, 
                direction.y * speed * 0.01f); //fck mgk num cuz map not ready

            _model.MovePosition(deltaPos);
        }
    }
}