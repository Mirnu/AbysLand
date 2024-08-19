using Assets.Scripts.Entities.Impl;
using Assets.Scripts.Entities.Pathfinding;
using System.Collections;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;
using Zenject;


namespace Assets.Scripts.Entities {

    public class ZombieStateMachine : EntityStateMachine
    {
        public readonly ZombieAttackState AttackState;
        public readonly ZombieSearchState SearchState;
        public readonly ZombieHitState HitState;


        protected new Zombie entityModel;
        protected new EntityStatsModel stats;

        private EntityState _curState;
        private EntityState _prevState;

        public ZombieStateMachine(Zombie entity, EntityStatsModel stats, IPathfindingStrategy strategy) : base(entity, stats, strategy)
        {
            entityModel = entity;
            this.stats = stats;
            _PathfindingStrategy = strategy;

            AttackState = new ZombieAttackState(this, entityModel, this.stats, strategy);
            SearchState = new ZombieSearchState(this, entityModel, this.stats, strategy);
            HitState = new ZombieHitState(this, entityModel, this.stats, strategy);
        }

        public override void Initialize()
        {
            Debug.Log("ZM sm  init");
            Init(SearchState);
        }
        
        private bool Init(EntityState state)
        {
            _curState = state;
            Debug.Log(_curState);
            return ChangeState(state);
        }

        public override bool ChangeState(EntityState newState)
        {
            if (_curState == newState) return false;
            if (!_curState.Exit()) return false;
            _prevState = _curState;
            _curState = newState;
            Debug.Log(_curState);
            _curState.Enter();
            return true;
        }

        public void Update()
        {
            _curState.Update();
        }
    }
}
