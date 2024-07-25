using Assets.Scripts.Player.Model;
using Assets.Scripts.Player.Stats;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Player
{
    public class PlayerFacade : MonoBehaviour
    {
        private PlayerStatsModel _stats;

        [Inject]
        public void Construct(PlayerModel model, PlayerStatsModel stats)
        {
            _stats = stats;
        }

        public void TakeDamage(int damage)
        {
            _stats.Health -= damage;
        }
    }
}