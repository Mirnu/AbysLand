using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.CullingGroup;

namespace Assets.Scripts.Entities
{
    public class EntityStatsModel
    {
        private int _MaxHP;
        private float _Speed;
        private int _Damage;
        private bool _CanDie;
        private bool _HasAI;
        private bool _CanAttack;

/*        protected EntityStats _MaxStats;*/

        public event Action<int> HpChanged;
        public event Action<float> SpeedChanged;
        public event Action<int> DamageChanged;
        public event Action<bool> CanDieChanged;
        public event Action<bool> HasAIChanged;
        public event Action<bool> CanAttackChanged;

        public event Action StatsChanged;

        public EntityStatsModel(int hp = 1, float speed = 1f, int damage = 0, bool canDie = true, bool hasAI = false, bool canAttack = false)
        {
            this._MaxHP = hp;
            this._Speed = speed;
            this._Damage = damage;
            this._CanDie = canDie;
            this._HasAI = hasAI;
            this._CanAttack = canAttack;
        }

        public int HP
        {
            get => _MaxHP;
            set
            {
                int new_value = value > 0 ? value : 1;
                if (new_value == _MaxHP) return;
                _MaxHP = new_value;
                HpChanged?.Invoke(_MaxHP);
                StatsChanged?.Invoke();
            }
        }
        public float Speed
        {
            get => _Speed;
            set
            {
                float new_value = value > 0 ? value : 1;
                if (new_value == _Speed) return;
                _Speed = new_value;
                SpeedChanged?.Invoke(_Speed);
                StatsChanged?.Invoke();
            }
        }
        public int Damage
        {
            get => _Damage;
            set
            {
                int new_value = value > 0 ? value : 0;
                if (new_value == _Damage) return;
                _Damage = new_value;
                DamageChanged?.Invoke(_Damage);
                StatsChanged?.Invoke();
            }
        }
        public bool CanDie
        {
            get => _CanDie;
            set
            {
                _CanDie = value;
                CanDieChanged?.Invoke(_CanDie);
                StatsChanged?.Invoke();
            }
        }
        public bool HasAI
        {
            get => _HasAI;
            set
            {
                _HasAI = value;
                HasAIChanged?.Invoke(_HasAI);
                StatsChanged?.Invoke();
            }
        }
        public bool CanAttack
        {
            get => _CanAttack;
            set
            {
                _CanAttack = value;
                CanAttackChanged?.Invoke(_CanAttack);
                StatsChanged?.Invoke();
            }
        }
    }
}
