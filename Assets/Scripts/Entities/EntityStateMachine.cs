using Assets.Scripts.Entities;
using Assets.Scripts.Entities.Impl;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Entities
{
    public abstract class EntityStateMachine
    {
        protected EntityStatsModel _Stats;
        protected Entity _EntityModel;

        public EntityStateMachine(Entity entity, EntityStatsModel stats)
        {
            _EntityModel = entity;
            _Stats = stats;
        }

        public abstract void Initialize();
        public abstract bool ChangeState(EntityState new_state);
    }
}
