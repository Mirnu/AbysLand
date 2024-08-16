using Assets.Scripts.Misc.Utils;
using Assets.Scripts.Player.Hands;
using Assets.Scripts.Player.Model;
using System;
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

        private bool IsChangeable;
        private int _differenceBetweenDirection => (int)_directionController.Direction;

        public ArmAnimationController(Hand hand, PlayerModel model,
            PlayerDirectionController directionController, AngleUtils angleUtils)
        {
            _hand = hand;
            _model = model;
            _directionController = directionController;
            _angleUtils = angleUtils;
        }

        public void FixedTick()
        {
            //if (!_hand.IsEmpty) return;
            int hours = _angleUtils.GetHours();
            _model.SetArmMoveAnimation(hours + 8);
        }     
    }
}
