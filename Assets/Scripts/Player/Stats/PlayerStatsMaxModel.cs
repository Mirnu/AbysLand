using System;

namespace Assets.Scripts.Player.Stats.UI
{
    internal class PlayerStatsMaxModel
    {
        private int _healthMax;
        private int _manaMax;
        private int _foodMax;

        public event Action<int> HealthChanged;
        public event Action<int> ManaChanged;
        public event Action<int> FoodChanged;

        public event Action StatsMaxChanged;

        // Сами прокидываем значения в фабрике (удобно для сохранений)
        public PlayerStatsMaxModel(int healthMax = 100, int manaMax = 100, int foodMax = 100)
        {
            _healthMax = healthMax;
            _manaMax = manaMax;
            _foodMax = foodMax;
        }

        public int HealthMax
        {
            get { return _healthMax; }
            set
            {
                _healthMax = value;
                HealthChanged?.Invoke(_healthMax);
                StatsMaxChanged?.Invoke();
            }
        }

        public int ManaMax
        {
            get { return _manaMax; }
            set
            {
                _manaMax = value;
                ManaChanged?.Invoke(_manaMax);
                StatsMaxChanged?.Invoke();
            }
        }

        public int FoodMax
        {
            get { return _foodMax; }
            set
            {
                _foodMax = value;
                FoodChanged?.Invoke(_foodMax);
                StatsMaxChanged?.Invoke();
            }
        }
    }
}
