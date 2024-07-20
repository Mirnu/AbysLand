using System;

namespace Assets.Scripts.Player.Stats
{
    public class PlayerStatsModel
    {
        private int _health;
        private int _mana;
        private int _food;

        public event Action<int> HealthChanged;
        public event Action<int> ManaChanged;
        public event Action<int> FoodChanged;

        public event Action StatsChanged;

        // Сами прокидываем значения в фабрике (удобно для сохранений)
 
        public PlayerStatsModel(int health = 100, int mana = 100, int food = 100) 
        { 
            _health = health;
            _mana = mana;   
            _food = food;
        }

        public int Health { 
            get { return _health; }
            set {
                _health = value; 
                HealthChanged?.Invoke(_health);
                StatsChanged?.Invoke();
            }
        }

        public int Mana
        {
            get { return _mana; }
            set { 
                _mana = value; 
                ManaChanged?.Invoke(_mana);
                StatsChanged?.Invoke();
            }
        }

        public int Food
        {
            get { return _food; }
            set { 
                _food = value; 
                FoodChanged?.Invoke(_food);
                StatsChanged?.Invoke();
            }
        }
    }
}
