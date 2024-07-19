using Assets.Scripts.Player.Hands;
using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Player.Handlers
{
    internal class PlayerRotationHandler : IInitializable, IDisposable
    {
        private readonly Hand _hand;
        private readonly PlayerAnimationHandler _animationHandler;

        private Dictionary<Animations, Vector3> _toolPositionMap = new()
        {
            {Animations.IdleDown, new Vector3(-0.08f,-0.091f, -0.1f) },
            {Animations.IdleUp, new Vector3(0.098f,-0.05f, 0.1f) },
            {Animations.IdleLeft, new Vector3(0.04f,-0.09f, -0.1f) },
            {Animations.IdleRight, new Vector3(0,-0.091f, -0.1f) }
        };

        public PlayerRotationHandler(Hand hand, PlayerAnimationHandler animationHandler) 
        { 
            _hand = hand;
            _animationHandler = animationHandler;
        }

        public void Dispose()
        {
            _animationHandler.AnimationChanged -= OnAnimationChanged;
        }

        public void Initialize()
        {
            _animationHandler.AnimationChanged += OnAnimationChanged;
        }

        private void OnAnimationChanged(Animations animation)
        {
            Vector3 position = _toolPositionMap[animation];
            _hand.Transform.localPosition = position;
        }
    }
}
