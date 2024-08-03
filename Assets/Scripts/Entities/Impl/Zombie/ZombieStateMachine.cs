using Assets.Scripts.Entities.Impl;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;


namespace Assets.Scripts.Entities {

    public class ZombieStateMachine : EntityStateMachine, ITickable
    {
        public readonly ZombieAttackState AttackState;
        public readonly ZombieSearchState SearchState;


        protected new Zombie _EntityModel;
        protected new EntityStatsModel _Stats;

        private EntityState cur_state;
        private EntityState prev_state;

        public ZombieStateMachine(Zombie entity, EntityStatsModel stats) : base(entity, stats)
        {
            _EntityModel = entity;
            _Stats = stats;

            AttackState = new ZombieAttackState(this, _EntityModel, _Stats);
            SearchState = new ZombieSearchState(this, _EntityModel, _Stats);
        }

        public override void Initialize()
        {
            Init(SearchState);
        }
        
        private bool Init(EntityState state)
        {
            return ChangeState(state);
        }

        public override bool ChangeState(EntityState new_state)
        {
            if (cur_state == new_state) return false;
            if (!cur_state.OnExit()) return false;
            prev_state = cur_state;
            cur_state = new_state;
            return true;
        }
        
        public void Tick()
        {
            cur_state.Update();
        }
    }
}
