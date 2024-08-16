using Assets.Scripts.Misc.Utils;
using Assets.Scripts.Player.Hands;
using Assets.Scripts.Player.Model;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using Zenject;

namespace Assets.Scripts.Player.Systems
{
    public enum ArmTurn {
        Start,
        Middle,
        End
    }

    public sealed class ArmAnimationController : IFixedTickable
    {
        private readonly Hand _hand;
        private readonly PlayerModel _model;
        private readonly PlayerDirectionController _directionController;
        private readonly AngleUtils _angleUtils;

        private readonly List<GameObject> _handPoints;

        private bool IsChangeable;
        private int _differenceBetweenDirection => (int)_directionController.Direction;

        public ArmAnimationController(Hand hand, PlayerModel model, List<GameObject> handPoints,
            PlayerDirectionController directionController, AngleUtils angleUtils)
        {
            _hand = hand;
            _model = model;
            _directionController = directionController;
            _angleUtils = angleUtils;
            _handPoints = handPoints;  
        }

        public void FixedTick()
        {
            int hours = _angleUtils.GetHours();
            if (_hand.IsEmpty) return;

            _model.SetArmMoveAnimation(hours + 8);
            GameObject handPoint = _handPoints[hours];
            _hand.Transform.localPosition = handPoint.transform.localPosition;
        }     
    }
}
