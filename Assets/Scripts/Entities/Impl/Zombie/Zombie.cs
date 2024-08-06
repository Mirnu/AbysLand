using Assets.Scripts.Player;
using UnityEditor;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Entities.Impl
{
    public class Zombie : Entity
    {
        protected new EntityStatsModel _StatsModel;
        protected new ZombieStateMachine _StateMachine;

        [Inject]
        public void Construct()
        {
            _StatsModel = new EntityStatsModel();
            _StateMachine = new ZombieStateMachine(this, _StatsModel);
        }

        private void Start()
        {
            _StateMachine.Initialize();
        }

        private void Update()
        {
            _StateMachine.Update();
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.TryGetComponent(out PlayerFacade player))
            {
                player.TakeDamage(_StatsModel.Damage);
            }
        }
    }
}
