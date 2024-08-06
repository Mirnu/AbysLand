using Assets.Scripts.Saving;
using System;
using Zenject;

namespace Assets.Scripts.Player.Stats.LaU
{
    public class PlayerStatsLaU : IInitializable, IDisposable
    {
        private readonly PlayerStatsMaxModel _playerStatsMaxModel;
        private readonly PlayerStatsModel _playerStatsModel;

        private readonly SavingManager _saving;

        public PlayerStatsLaU(
            PlayerStatsMaxModel playerStatsMaxModel,
            PlayerStatsModel playerStatsModel,
            SavingManager saving
        )
        {
            _playerStatsMaxModel = playerStatsMaxModel;
            _playerStatsModel = playerStatsModel;
            _saving = saving;
        }

        public void Dispose()
        {
            _saving.SaveData(new PlayerStatsMaxModel.Template
            {
                HealthMax = _playerStatsModel.Health,
                ManaMax = _playerStatsModel.Mana,
                FoodMax = _playerStatsModel.Food
            });
            _saving.SaveData(new PlayerStatsModel.Template()
            {
                Health = _playerStatsModel.Health,
                Mana = _playerStatsModel.Mana,
                Food = _playerStatsModel.Food,
                Speed = _playerStatsModel.Speed
            });
        }

        public void Initialize()
        {
            _playerStatsMaxModel.Init(_saving.GetData<PlayerStatsMaxModel.Template>());
            _playerStatsModel.Init(_saving.GetData<PlayerStatsModel.Template>());
        }
    }
}
