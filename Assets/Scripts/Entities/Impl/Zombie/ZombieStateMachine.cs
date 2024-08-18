using Assets.Scripts.Entities.Impl;
using Assets.Scripts.Entities.Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;


namespace Assets.Scripts.Entities {

    public class ZombieStateMachine : EntityStateMachine
    {
        public readonly ZombieAttackState AttackState;
        public readonly ZombieSearchState SearchState;


        protected new Zombie _EntityModel;
        protected new EntityStatsModel _Stats;

        private EntityState _curState;
        private EntityState _prevState;

        public ZombieStateMachine(Zombie entity, EntityStatsModel stats, IPathfindingStrategy strategy) : base(entity, stats, strategy)
        {
            _EntityModel = entity;
            _Stats = stats;
            _PathfindingStrategy = strategy;

            AttackState = new ZombieAttackState(this, _EntityModel, _Stats, strategy);
            SearchState = new ZombieSearchState(this, _EntityModel, _Stats, strategy);
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
            if (!_curState.OnExit()) return false;
            _prevState = _curState;
            _curState = newState;
            Debug.Log(_curState);
            return true;
        }

        public void Update()
        {
            _curState.Update();
        }
    }
}
