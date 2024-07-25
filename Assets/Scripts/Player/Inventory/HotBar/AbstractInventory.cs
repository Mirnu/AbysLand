using System;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Resources.Data;
using UnityEngine;

namespace Assets.Scripts.Misc {
    [Serializable]
    public class AbstractInventory {
        private Resource[] _inventory = new Resource[] {};
        private readonly int _size = 0; 

        public AbstractInventory(int size) {
            _size = size;
            _inventory = new Resource[_size];
        }

        public Resource GetItem(int index) {
            return _inventory[index];
        }

        public List<Sprite> GetSprites() {
            return _inventory.Where(x => x != null).Select(x => x.SpriteInInventary).ToList();
        }

        public bool TryAddItem(Resource item) {
            if(Array.Exists(_inventory, x => x == null)) {
                Debug.Log("Added: " + item.Name);
                _inventory[Array.FindIndex(_inventory, x => x == null)] = item;
                return true;
            }
            return false;
        }

        public void RemoveItem(Resource item) {
            if(Array.Exists(_inventory, x => x == item)) {
                Debug.Log("Removed: " + item.Name);
                _inventory[Array.FindIndex(_inventory, x => x == item)] = item;
            }
        }
    }
}