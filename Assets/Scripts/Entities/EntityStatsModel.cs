using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.CullingGroup;

namespace Assets.Scripts.Entities
{
    [CreateAssetMenuAttribute(fileName = "entity_data", menuName = "New entity data")]
    public class EntityStatsModel : ScriptableObject
    {
        private int _maxHP;
        private float _speed;
        private int _damage;
        private bool _canDie;
        private bool _hasAI;
        private bool _canAttack;

/*        protected EntityStats _MaxStats;*/

        public event Action<int> HpChanged;
        public event Action<float> SpeedChanged;
        public event Action<int> DamageChanged;
        public event Action<bool> CanDieChanged;
        public event Action<bool> HasAIChanged;
        public event Action<bool> CanAttackChanged;

        public event Action StatsChanged;

        /*protected EntityStatsModel(EntityStats stats)
        {
            this._MaxStats = stats;
            this._hp = stats.hp;
            this._speed = stats.speed;
            this._damage = stats.damage;
            this._canDie = stats.canDie;
            this._hasAI = stats.hasAI;
        }*/

        public int HP
        {
            get => _maxHP;
            set
            {
                int new_value = value > 0 ? value : 1;
                if (new_value == _maxHP) return;
                _maxHP = new_value;
                HpChanged?.Invoke(_maxHP);
                StatsChanged?.Invoke();
            }
        }
        public float Speed
        {
            get => _speed;
            set
            {
                float new_value = value > 0 ? value : 1;
                if (new_value == _speed) return;
                _speed = new_value;
                SpeedChanged?.Invoke(_speed);
                StatsChanged?.Invoke();
            }
        }
        public int Damage
        {
            get => _damage;
            set
            {
                int new_value = value > 0 ? value : 0;
                if (new_value == _damage) return;
                _damage = new_value;
                DamageChanged?.Invoke(_damage);
                StatsChanged?.Invoke();
            }
        }
        public bool CanDie
        {
            get => _canDie;
            set
            {
                _canDie = value;
                CanDieChanged?.Invoke(_canDie);
                StatsChanged?.Invoke();
            }
        }
        public bool HasAI
        {
            get => _hasAI;
            set
            {
                _hasAI = value;
                HasAIChanged?.Invoke(_hasAI);
                StatsChanged?.Invoke();
            }
        }
        public bool CanAttack
        {
            get => _canAttack;
            set
            {
                _canAttack = value;
                CanAttackChanged?.Invoke(_canAttack);
                StatsChanged?.Invoke();
            }
        }
    }
}
