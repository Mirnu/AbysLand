using Assets.Scripts.Entities.Impl;
using Assets.Scripts.Entities.Pathfinding;
using Assets.Scripts.Player;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

namespace Assets.Scripts.Entities
{
    public class ZombieHitState : EntityState
    {
        protected new ZombieStateMachine stateMachine;
        public ZombieHitState(ZombieStateMachine state_machine, Zombie entity, EntityStatsModel stats, IPathfindingStrategy strategy) : base(state_machine, entity, stats, strategy)
        {
            stateMachine = state_machine;
            entityModel = entity;
            entityStats = stats;
            pathfindingStrategy = strategy;
        }

        public override void Enter()
        {
            if (entityModel.CurrentTarget.TryGetComponent<PlayerFacade>(out PlayerFacade target))
            {
                target.TakeDamage(entityStats.Damage);
            }
            stateMachine.ChangeState(stateMachine.AttackState);
        }

        public override bool Exit()
        {
            return true;
        }

        public override void Update()
        {
        }
    }
}
