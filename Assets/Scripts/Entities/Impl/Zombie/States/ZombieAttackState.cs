using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Entities.Impl;
using Assets.Scripts.Entities.Pathfinding;

namespace Assets.Scripts.Entities
{
    public class ZombieAttackState : EntityState
    {
        public ZombieAttackState(ZombieStateMachine state_machine, Zombie entity, EntityStatsModel stats, IPathfindingStrategy strategy) : base(state_machine, entity, stats, strategy)
        {
            stateMachine = state_machine;
            entityModel = entity;
            entityStats = stats;
            pathfindingStrategy = strategy;
        }

        public override void Enter()
        {
            
        }

        public override bool Exit()
        {
            pathfindingStrategy.MoveToPreviousPoint(entityModel.gameObject);
            return true;
        }

        public override void Update()
        {
            Debug.Log(entityModel.CurrentTarget.transform);
            pathfindingStrategy.MoveTo(entityModel.CurrentTarget.transform, entityModel.gameObject);
        }
    }
}
