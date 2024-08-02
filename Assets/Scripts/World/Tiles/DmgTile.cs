using System;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Assets.Scripts.World {
    [Serializable]
    public class DmgTile {

        public Vector3Int Pos;
        public TileBase _default;
        public TileBase _dead;
        public int MaxHealth;

        public Action onDestroyed;

        private int _currentHealth;

        public DmgTile(TileBase def, DmgTile origin) {
            _default = def;
            Pos = origin.Pos;
            _dead = origin._dead;
            MaxHealth = origin.MaxHealth;
            onDestroyed = origin.onDestroyed;
        }

        public void Init()
        {
            _currentHealth = MaxHealth;
        }

        public void Damage(int amount) {
            _currentHealth -= amount;
            if(_currentHealth <= 0) {
                onDestroyed?.Invoke();
            }
        }
    }
}
