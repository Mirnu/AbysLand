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
            _StateMachine = state_machine;
            _EntityModel = entity;
            _EntityStats = stats;
            _PathfindingStrategy = strategy;
        }

        public override bool OnExit()
        {
            _PathfindingStrategy.MoveToPreviousPoint(_EntityModel.gameObject);
            return true;
        }

        public override void Update()
        {
            Debug.Log(_EntityModel.CurrentTarget.transform);
            _PathfindingStrategy.MoveTo(_EntityModel.CurrentTarget.transform, _EntityModel.gameObject);
        }
    }
}
