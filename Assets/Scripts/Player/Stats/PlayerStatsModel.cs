using Assets.Scripts.Player.Stats.UI;
using System;

namespace Assets.Scripts.Player.Stats
{
    public class PlayerStatsModel
    {
        private int _health;
        private int _mana;
        private int _food;
        private float _speed;

        public event Action<int> HealthChanged;
        public event Action<int> ManaChanged;
        public event Action<int> FoodChanged;
        public event Action<float> SpeedChanged;

        public event Action StatsChanged;
        private readonly PlayerStatsMaxModel _maxModel;
        private readonly PlayerBoostModel _boostModel;


        public PlayerStatsModel(Settings settings, PlayerStatsMaxModel maxModel,
            PlayerBoostModel boostModel,
            int health = 95, int mana = 100, int food = 100, float? speed = default) 
        { 
            _health = health;
            _mana = mana;   
            _food = food;
            _speed = speed ?? settings.Speed;
            _maxModel = maxModel;
            _boostModel = boostModel;
        }

        public int Health { 
            get { return _health; }
            set {
                int newValue = Math.Clamp(value, 0, _maxModel.HealthMax);
                if (newValue == _health) return;
                _health = newValue;
                HealthChanged?.Invoke(_health);
                StatsChanged?.Invoke();
            }
        }

        public int Mana
        {
            get { return _mana; }
            set {
                int newValue = Math.Clamp(value, 0, _maxModel.ManaMax);
                if (newValue == _mana) return;
                _mana = newValue;
                ManaChanged?.Invoke(_mana);
                StatsChanged?.Invoke();
            }
        }

        public int Food
        {
            get { return _food; }
            set { 
                int newValue = Math.Clamp(value, 0, _maxModel.FoodMax);
                if (newValue == _food) return;
                _food = newValue;
                FoodChanged?.Invoke(_food);
                StatsChanged?.Invoke();
            }
        }

        public float Speed
        {
            get { return _speed * _boostModel.SpeedBoost; }
            set
            {
                if (value == _speed) return;
                _speed = value;
                SpeedChanged?.Invoke(_speed);
                StatsChanged?.Invoke();
            }
        }

        [Serializable]
        public class Settings
        {
            public float Speed;
        }
    }
}
