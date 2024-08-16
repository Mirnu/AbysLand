using Assets.Scripts.Misc.Utils;
using Assets.Scripts.Player.Hands;
using Assets.Scripts.Player.Model;
using Assets.Scripts.Player.Systems;
using System;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

namespace Assets.Scripts.Player.Handlers
{
    public enum Animations
    {
        IdleUp,
        IdleRight,
        IdleDown,
        IdleLeft,
        WalkUp,
        WalkRight,
        WalkDown,
        WalkLeft
    }


    public class PlayerAnimationController : IInitializable, IDisposable
    {
        public event Action<Animations> AnimationChanged;

        private readonly PlayerModel _model;
        private readonly PlayerMovement _playerMovement;
        private readonly PlayerDirectionController _directionController;

        private int _currentAnimationPosition;
        private bool isWalk = false;

        private readonly Hand _hand;
        private readonly AngleUtils _angleUtils;

        public PlayerAnimationController(PlayerModel model, PlayerMovement playerMovement,
            Hand hand, AngleUtils angleUtils, PlayerDirectionController directionController)
        {
            _model = model;
            _playerMovement = playerMovement;
            _hand = hand;
            _angleUtils = angleUtils;
            _directionController = directionController;
        }

        public void Dispose()
        {
            _playerMovement.StartMoved -= OnStartWalk;
            _playerMovement.StopMoved -= OnStopWalk;
            _directionController.DirectionChanged -= OnDirectionChanged;
        }

        public void Initialize()
        {
            ChangeAnimation((int)Animations.IdleDown);
            _directionController.DirectionChanged += OnDirectionChanged;
            _playerMovement.StartMoved += OnStartWalk;
            _playerMovement.StopMoved += OnStopWalk;
        }

        private void OnDirectionChanged(Direction direction)
        {
            ChangeAnimation((int)direction);
        }

        public void ChangeAnimation(int animation)
        {
            _currentAnimationPosition = !isWalk ? animation : animation + 4;
            if (_currentAnimationPosition != animation)
            {
                AnimationChanged?.Invoke((Animations)_currentAnimationPosition);
            }
            
            _model.SetMoveAnimation(_currentAnimationPosition);

            if (_hand.IsEmpty)
                _model.SetArmMoveAnimation(_currentAnimationPosition);
        }

        private void OnStartWalk()
        {
            isWalk = true;
            ChangeAnimation(_currentAnimationPosition);
        }

        private void OnStopWalk()
        {
            isWalk = false;
            ChangeAnimation(_currentAnimationPosition - 4);
        }
    }
}