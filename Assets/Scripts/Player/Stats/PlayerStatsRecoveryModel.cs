using System;

namespace Assets.Scripts.Player.Stats
{
    public class PlayerStatsRecoveryModel
    {
        private float _healthRecovery = 0;
        private float _manaRecovery = 0;

        public event Action<float> HealthRecoveryChanged;
        public event Action<float> ManaRecoveryChanged;

        public event Action StatsRecoveryChanged;

        public float HealthRecoveryPerSec
        {
            get { return _healthRecovery; }
            set
            {
                _healthRecovery = value;
                HealthRecoveryChanged?.Invoke(_healthRecovery);
                StatsRecoveryChanged?.Invoke();
            }
        }

        public float ManaRecoveryPerSec
        {
            get { return _manaRecovery; }
            set
            {
                _manaRecovery = value;
                ManaRecoveryChanged?.Invoke(_manaRecovery);
                StatsRecoveryChanged?.Invoke();
            }
        }
    }
}
