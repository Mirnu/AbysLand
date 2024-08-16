using Assets.Scripts.Misc.Utils;
using System;
using Zenject;

namespace Assets.Scripts.Player.Systems
{
    public enum Direction
    {
        Up,
        Right,
        Down,
        Left
    }

    public class PlayerDirectionController : IFixedTickable
    {
        private readonly AngleUtils _angleUtils;
        private Direction _direction = Direction.Down;

        public event Action<Direction> DirectionChanged;
        public Direction Direction
        {
            get => _direction;
            set
            {
                if (_direction == value) return;
                _direction = value;
                DirectionChanged?.Invoke(_direction);
            }
        }


        public PlayerDirectionController(AngleUtils angleUtils)
        {
            _angleUtils = angleUtils;
        }

        public void FixedTick()
        {
            int interval = _angleUtils.GetInterval();
            Direction = (Direction)interval;
        }
    }
}
