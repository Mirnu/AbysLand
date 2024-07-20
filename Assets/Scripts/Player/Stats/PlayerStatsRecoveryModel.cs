using System;

namespace Assets.Scripts.Player.Stats
{
    internal class PlayerStatsRecoveryModel
    {
        private int _healthRecovery = 0;
        private int _manaRecovery = 0;
        private int _foodRecovery = 0;

        public event Action<int> HealthRecoveryChanged;
        public event Action<int> ManaRecoveryChanged;
        public event Action<int> FoodRecoveryChanged;

        public event Action StatsRecoveryChanged;

        public int HealthRecovery
        {
            get { return _healthRecovery; }
            set
            {
                _healthRecovery = value;
                HealthRecoveryChanged?.Invoke(_healthRecovery);
                StatsRecoveryChanged?.Invoke();
            }
        }

        public int ManaRecovery
        {
            get { return _manaRecovery; }
            set
            {
                _manaRecovery = value;
                ManaRecoveryChanged?.Invoke(_manaRecovery);
                StatsRecoveryChanged?.Invoke();
            }
        }

        public int FoodRecovery
        {
            get { return _foodRecovery; }
            set
            {
                _foodRecovery = value;
                FoodRecoveryChanged?.Invoke(_foodRecovery);
                StatsRecoveryChanged?.Invoke();
            }
        }
    }
}
