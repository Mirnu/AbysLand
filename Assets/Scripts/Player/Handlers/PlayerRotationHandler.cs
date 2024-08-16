using Assets.Scripts.Player.Hands;
using Assets.Scripts.Player.Systems;
using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Player.Handlers
{
    internal class PlayerRotationHandler : IInitializable, IDisposable
    {
        private readonly Hand _hand;
        private readonly PlayerDirectionController _directionController;

        /*private Dictionary<Animations, Vector3> _toolPositionMap = new()
        {
            {Direction.Down, new Vector3(-0.08f,-0.091f, -0.1f) },
            {Direction.Up, new Vector3(0.098f,-0.05f, 0.1f) },
            {Animations.IdleLeft, new Vector3(0.04f,-0.09f, -0.1f) },
            {Animations.IdleRight, new Vector3(0,-0.091f, -0.1f) }
        };*/

        public PlayerRotationHandler(Hand hand, PlayerDirectionController directionController) 
        { 
            _hand = hand;
            _directionController = directionController;
        }

        public void Dispose()
        {
            _directionController.DirectionChanged -= OnDirectionChanged;
        }

        public void Initialize()
        {
            _directionController.DirectionChanged += OnDirectionChanged;
        }

        private void OnDirectionChanged(Direction direction)
        {
           // Vector3 position = _toolPositionMap[direction];
            //_hand.Transform.localPosition = position;
        }
    }
}
