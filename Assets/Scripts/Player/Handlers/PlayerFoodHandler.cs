using Assets.Scripts.Misc.Utils;
using Assets.Scripts.Player.Stats;
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

        public PlayerFoodHandler(PlayerStatsModel stats,
            PlayerBoostModel boost, Routine coroutine) 
        { 
            _stats = stats;
            _boost = boost;
            _coroutine = coroutine;
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
                Debug.Log("Startes");
                _coroutine.StartCoroutine(startStarve());
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
