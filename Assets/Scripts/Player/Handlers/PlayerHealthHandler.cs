using Assets.Scripts.Player.Stats;
using Assets.Scripts.Player.Stats.UI;
using Assets.Scripts.Player.Systems;
using System;
using System.Collections;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Player.Handlers
{
    public class PlayerHealthHandler : IInitializable, IDisposable
    {
        private readonly PlayerStatsModel _stats;
        private readonly PlayerStatsMaxModel _statsMax;
        private readonly HealSystem _heal;

        private const string FOOD_HEAL = "food";

        public PlayerHealthHandler(PlayerStatsModel stats,
            PlayerStatsMaxModel statsMax,
            HealSystem heal) 
        { 
            _stats = stats;
            _statsMax = statsMax;
            _heal = heal;
        }

        public void Dispose()
        {
            _stats.HealthChanged -= OnHealthChanged;
        }

        public void Initialize()
        {
            _stats.HealthChanged += OnHealthChanged;
            OnHealthChanged(_stats.Health);
        }

        private void OnHealthChanged(int health)
        {
            Debug.Log(health);
            if (health < _statsMax.HealthMax && _stats.Food > 70 
                && !_heal.IsHealedByName(FOOD_HEAL))
            {
                _heal.HealByTime(FOOD_HEAL, 2, 4);
            }
            if (health == _statsMax.HealthMax)
            {
                _heal.StopHealByName(FOOD_HEAL);
            }
        }
    }
}
