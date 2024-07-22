using Assets.Scripts.Misc.Utils;
using Assets.Scripts.Player.Stats;
using Assets.Scripts.Player.Systems;
using System;
using System.Collections;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Player.Handlers
{
    public class PlayerFoodHandler : IInitializable, IDisposable
    {
        private readonly PlayerStatsModel _stats;
        private readonly PlayerBoostModel _boost;
        private readonly Routine _coroutine;
        private readonly HealSystem _heal;

        private bool isRecoveryHealth = false;
        private const string FOOD_HEAL = "food";

        public PlayerFoodHandler(PlayerStatsModel stats,
            PlayerBoostModel boost, Routine coroutine, HealSystem heal) 
        { 
            _stats = stats;
            _boost = boost;
            _coroutine = coroutine;
            _heal = heal;
        }

        public void Dispose()
        {
            _stats.FoodChanged -= OnFoodChanged;
        }

        public void Initialize()
        {
            _stats.FoodChanged += OnFoodChanged;
        }

        private void OnFoodChanged(int food)
        {
            _boost.SpeedBoost = food > 30 ? 1 : 0.5f;

            if (food == 0)
            {
                _coroutine.StartCoroutine(startStarve());
            }
            if (food <= 70)
            {
                _heal.LockByName(FOOD_HEAL);
            }
            else
            {
                _heal.UnLockByName(FOOD_HEAL);
            }
        }

        private IEnumerator startStarve()
        {
            WaitForSeconds delta = new WaitForSeconds(2);
            while (true)
            {
                yield return delta;

                if (_stats.Health > 3)
                    _stats.Health--;
            }
        }
    }
}
