using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Entities.Impl;

namespace Assets.Scripts.Entities
{
    public class ZombieAttackState : EntityState
    {
        public ZombieAttackState(ZombieStateMachine state_machine, Zombie entity, EntityStatsModel stats) : base(state_machine, entity, stats)
        {
            _StateMachine = state_machine;
            _EntityModel = entity;
            _EntityStats = stats;
        }

        public override bool OnExit()
        {
            return true;
        }

        public override void Update()
        {
            
        }
    }
}
