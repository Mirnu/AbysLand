using Assets.Scripts.Entities.Impl;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Entities
{
    public class ZombieSearchState : EntityState
    {
        public ZombieSearchState(ZombieStateMachine state_machine, Zombie entity, EntityStatsModel stats) : base(state_machine, entity, stats)
        {
            _StateMachine = state_machine;
            _EntityModel = entity;
            _EntityStats = stats;
        }

        public override void Update()
        {
            Debug.Log("Zombie update");
        }
        public override bool OnExit()
        {
            return true;
        }
    }
}
