using Assets.Scripts.Entities;
using Assets.Scripts.Entities.Impl;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using Assets.Scripts.Entities.Pathfinding;

namespace Assets.Scripts.Entities
{
    public abstract class EntityStateMachine
    {
        protected EntityStatsModel _Stats;
        protected Entity _EntityModel;
        protected IPathfindingStrategy _PathfindingStrategy;

        public EntityStateMachine(Entity entity, EntityStatsModel stats, IPathfindingStrategy strategy)
        {
            _EntityModel = entity;
            _Stats = stats;
            _PathfindingStrategy = strategy;
        }

        public abstract void Initialize();
        public abstract bool ChangeState(EntityState new_state);
    }
}
