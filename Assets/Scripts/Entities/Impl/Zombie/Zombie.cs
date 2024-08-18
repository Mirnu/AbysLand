using Assets.Scripts.Entities.Pathfinding;
using Assets.Scripts.Player;
using UnityEditor;
using UnityEngine;
using Zenject;
using Assets.Scripts.Entities;

namespace Assets.Scripts.Entities.Impl
{
    public class Zombie : Entity
    {
        protected new EntityStatsModel _StatsModel;
        protected new ZombieStateMachine _StateMachine;

        [Inject]
        public void Construct(IPathfindingStrategy pathfindingStrategy)
        {
            _StatsModel = new EntityStatsModel(damage: 5, hasAI: true, canAttack: true);
            _PathfindingStrategy = pathfindingStrategy;
            _StateMachine = new ZombieStateMachine(this, _StatsModel, _PathfindingStrategy);
        }

        private void Start()
        {
            _StateMachine.Initialize();
            Debug.Log(_PathfindingStrategy);
        }

        private void Update()
        {
            _StateMachine.Update();
        }

        public void TakeDamage(int damage)
        {
            _StatsModel.HP -= damage;
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.TryGetComponent(out PlayerFacade player))
            {
                player.TakeDamage(_StatsModel.Damage);
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.TryGetComponent(out PlayerFacade player))
            {
                CurrentTarget = player.gameObject;
                _StateMachine.ChangeState(_StateMachine.AttackState);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.TryGetComponent(out PlayerFacade player))
            {
                CurrentTarget = null;
                _StateMachine.ChangeState(_StateMachine.SearchState);
            }
        }
    }
}
