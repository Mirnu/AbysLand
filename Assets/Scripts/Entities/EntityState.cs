using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Entities
{
    public abstract class EntityState
    {
        protected EntityStateMachine _StateMachine;
        protected Entity _EntityModel;
        protected EntityStatsModel _EntityStats;
        public EntityState(EntityStateMachine state_machine, Entity entity, EntityStatsModel stats)
        {
            _StateMachine = state_machine;
            _EntityModel = entity;
            _EntityStats = stats;
        }

        public abstract void Update();

        public abstract bool OnExit();
    }
}
