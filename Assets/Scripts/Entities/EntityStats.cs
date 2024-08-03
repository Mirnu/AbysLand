using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Entities
{
    public class EntityStats : ScriptableObject
    {
        public int HP;
        public bool HasAI;
        public bool CanDie;
        public float Speed;
        public int Damage;
    }
}
