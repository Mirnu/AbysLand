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
        protected new EntityStatsModel statsModel;
        protected new ZombieStateMachine stateMachine;

        [Inject]
        public void Construct(IPathfindingStrategy pathfindingStrategy)
        {
            statsModel = new EntityStatsModel(damage: 5, hasAI: true, canAttack: true);
            base.pathfindingStrategy = pathfindingStrategy;
            stateMachine = new ZombieStateMachine(this, statsModel, base.pathfindingStrategy);
        }

        private void Start()
        {
            stateMachine.Initialize();
            Debug.Log(pathfindingStrategy);
        }

        private void Update()
        {
            stateMachine.Update();
        }

        public void TakeDamage(int damage)
        {
            statsModel.HP -= damage;
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.TryGetComponent(out PlayerFacade player))
            {
                CurrentTarget = player.gameObject;
                stateMachine.ChangeState(stateMachine.HitState);
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.TryGetComponent(out PlayerFacade player))
            {
                CurrentTarget = player.gameObject;
                stateMachine.ChangeState(stateMachine.AttackState);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.TryGetComponent(out PlayerFacade player))
            {
                CurrentTarget = null;
                stateMachine.ChangeState(stateMachine.SearchState);
            }
        }
    }
}
