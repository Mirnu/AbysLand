using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Entities.Pathfinding;

namespace Assets.Scripts.Entities
{
    public abstract class EntityState
    {
        protected EntityStateMachine _StateMachine;
        protected Entity _EntityModel;
        protected EntityStatsModel _EntityStats;
        protected IPathfindingStrategy _PathfindingStrategy;
        public EntityState(EntityStateMachine state_machine, Entity entity, EntityStatsModel stats, IPathfindingStrategy strategy)
        {
            _StateMachine = state_machine;
            _EntityModel = entity;
            _EntityStats = stats;
            _PathfindingStrategy = strategy;
        }

        public abstract void Update();

        public abstract bool OnExit();
    }
}
