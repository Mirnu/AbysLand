using Player.Model;
using System;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

namespace Player.Handlers
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

    public class PlayerAnimationHandler : IInitializable, IDisposable, ITickable
    {
        private readonly PlayerModel _model;
        private readonly PlayerInput _input;

        private Animations _currentAnimationPosition = Animations.IdleDown;
        private int _currentAnimationId => (int)_currentAnimationPosition;
        private bool isWalk = false;

        public PlayerAnimationHandler(PlayerModel model, PlayerInput input)
        {
            _model = model;
            _input = input; 
        }

        public void Dispose()
        {
            _input.Gameplay.Movement.performed -= OnStartWalk;
            _input.Gameplay.Movement.canceled -= OnStopWalk;
        }

        public void Initialize()
        {
            _model.SetMoveAnimation(_currentAnimationId);
            _input.Gameplay.Movement.performed += OnStartWalk;
            _input.Gameplay.Movement.canceled += OnStopWalk;
        }

        public void Tick()
        {
            float x = Input.mousePosition.x;
            float y = Input.mousePosition.y;

            if (x < Screen.width * 0.3f)
            {
                CheckOnChangeAnimation(Animations.IdleLeft);
            }
            else if (x > Screen.width * 0.7f)
            {
                CheckOnChangeAnimation(Animations.IdleRight);
            }
            else if (y < Screen.width / 2)
            {
                CheckOnChangeAnimation(Animations.IdleDown);
            }
            else if (y > Screen.width / 2)
            {
                CheckOnChangeAnimation(Animations.IdleUp);
            }
        }

        public void CheckOnChangeAnimation(Animations animation)
        {
            _currentAnimationPosition = animation;

            if (!isWalk)
            {
                _model.SetMoveAnimation(_currentAnimationId);
            }
            else
            {
                _model.SetMoveAnimation(_currentAnimationId + 4);
            }
        }

        private void OnStartWalk(InputAction.CallbackContext context)
        {
            isWalk = true;
        }

        private void OnStopWalk(InputAction.CallbackContext context)
        {
            isWalk = false;
        }
    }
}