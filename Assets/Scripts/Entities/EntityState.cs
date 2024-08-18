using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Entities.Pathfinding;

namespace Assets.Scripts.Entities
{
    public abstract class EntityState
    {
        protected EntityStateMachine stateMachine;
        protected Entity entityModel;
        protected EntityStatsModel entityStats;
        protected IPathfindingStrategy pathfindingStrategy;
        public EntityState(EntityStateMachine state_machine, Entity entity, EntityStatsModel stats, IPathfindingStrategy strategy)
        {
            stateMachine = state_machine;
            entityModel = entity;
            entityStats = stats;
            pathfindingStrategy = strategy;
        }

        public abstract void Enter();

        public abstract void Update();

        public abstract bool Exit();
    }
}
