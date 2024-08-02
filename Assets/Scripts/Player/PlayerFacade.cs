using Assets.Scripts.Player.Model;
using Assets.Scripts.Player.Stats;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Player
{
    public class PlayerFacade : MonoBehaviour
    {
        private PlayerStatsModel _stats;
        private GameObjectContext _playerContext;

        private void Awake() 
        {
            _playerContext = GetComponent<GameObjectContext>();
        }

        [Inject]
        public void Construct(PlayerStatsModel stats)
        {
            _stats = stats;
        }

        public void TakeDamage(int damage)
        {
            _stats.Health -= damage;
        }

        public void Enable() {
            _playerContext.Run();
        }
    }
}