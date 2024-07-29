using UnityEngine;
using UnityEngine.Rendering;

namespace Assets.Scripts.Entities
{
    public abstract class Entity :  MonoBehaviour
    {
        protected int hp;
        protected float speed;
        protected int _damage;
        public bool canDie { get; protected set; }
        public bool hasAI { get; protected set; }
        [SerializeField]
        protected EntityStats _stats;

        protected Entity(EntityStats stats)
        {
            _stats = stats;
            this.hp = _stats.hp;
            this.canDie = _stats.canDie;
            this.hasAI = _stats.hasAI;
            this._damage = _stats.damage;
        }
    }
}
