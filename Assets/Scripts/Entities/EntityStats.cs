using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Entities
{
    public class EntityStats : ScriptableObject
    {
        public int hp;
        public bool hasAI;
        public bool canDie;
        public float speed;
        public int damage;
    }
}
