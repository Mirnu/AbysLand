using Assets.Scripts.Player.Stats;
using Assets.Scripts.Player.Systems;
using System;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Player.Handlers
{
    public class PlayerMoveHandler : IInitializable, IDisposable
    {
        private readonly PlayerMovement _movement;
        private readonly PlayerStatsModel _statsModel;

        public PlayerMoveHandler(PlayerMovement movement, PlayerStatsModel statsModel)
        {
            _movement = movement;
            _statsModel = statsModel;
        }

        private float _timeWalk = 0;

        public void Dispose()
        {
            _movement.PlayerMoved -= OnMoved;
        }
        
        public void Initialize()
        {
            _movement.PlayerMoved += OnMoved;
        }

        private void OnMoved()
        {
            _timeWalk += Time.deltaTime;
            if (_timeWalk > 5)
            {
                _statsModel.Food -= 1;
                _timeWalk = 0;
            }
        }
    }
}
