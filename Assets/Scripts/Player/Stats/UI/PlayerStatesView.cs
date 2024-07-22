using TMPro;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Player.Stats.UI
{
    internal class PlayerStatesView : MonoBehaviour
    {
        [Header("States")]
        [SerializeField] private TMP_Text _healthStateView;
        [SerializeField] private TMP_Text _manaStateView;
        [SerializeField] private TMP_Text _foodStateView;

        [Header("Recovery")]
        [SerializeField] private TMP_Text _healthRecoveryView;
        [SerializeField] private TMP_Text _manaRecoveryView;


        private PlayerStatsModel _playerStatsModel;
        private PlayerStatsMaxModel _playerStatsMaxModel;
        private PlayerStatsRecoveryModel _playerStatsRecoveryModel;

        [Inject]
        public void Construct(PlayerStatsModel playerStatsModel, 
            PlayerStatsMaxModel playerStatsMaxModel,
            PlayerStatsRecoveryModel playerStatsRecoveryModel)
        {
            _playerStatsMaxModel = playerStatsMaxModel;
            _playerStatsModel = playerStatsModel;
            _playerStatsRecoveryModel = playerStatsRecoveryModel;
        }

        private void OnEnable()
        {
            _playerStatsModel.StatsChanged += OnStatsChanged;
            _playerStatsMaxModel.StatsMaxChanged += OnStatsChanged;
            _playerStatsRecoveryModel.StatsRecoveryChanged += OnStatsRecoveryChanged;
            OnStatsChanged();
            OnStatsRecoveryChanged();
        }

        private void OnDisable()
        {
            _playerStatsModel.StatsChanged -= OnStatsChanged;
            _playerStatsMaxModel.StatsMaxChanged -= OnStatsChanged;
            _playerStatsRecoveryModel.StatsRecoveryChanged -= OnStatsRecoveryChanged;
        }

        private void OnStatsChanged()
        {
            _healthStateView.text = $"{_playerStatsModel.Health}/" +
                $"{_playerStatsMaxModel.HealthMax}";
            _manaStateView.text = $"{_playerStatsModel.Mana}/" +
                $"{_playerStatsMaxModel.ManaMax}";
            _foodStateView.text = $"{_playerStatsModel.Food}/" +
                $"{_playerStatsMaxModel.FoodMax}";
        }

        private void OnStatsRecoveryChanged()
        {
            _healthRecoveryView.text = "+" +
                _playerStatsRecoveryModel.HealthRecoveryPerSec + " здоровья/сек";
            _manaRecoveryView.text = "+" +
                _playerStatsRecoveryModel.ManaRecoveryPerSec + " здоровья/сек";
        }
    }
}
