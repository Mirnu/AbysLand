using System;
using Assets.Scripts.Inventory.View;
using Assets.Scripts.Resources.Armors;
using Assets.Scripts.Resources.Data;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Inventory.Armor {
    [Serializable]
    public class ArmorSlotCont {
        public GameObject Sprite;
        public ArmorSlotView Slot;
        public ArmorSlotView Cosmetic;

        private ArmorResource _current;
        private ArmorMB _currentMb;

        public ArmorResource CurrentResource => _current;

        public void Equip(ArmorResource armor, DiContainer _cont) {
            _current = armor;
            if (_currentMb != null) 
                UnityEngine.Object.Destroy(_currentMb.gameObject);
            
            _currentMb = createArmor(_current, _cont);
        }

        private ArmorMB createArmor(ArmorResource resource, DiContainer _cont)
        {
            if(resource == null) { return null; }
            ArmorMB armor = _cont.
                InstantiatePrefabForComponent<ArmorMB>(resource.EquippedSprite, Sprite.transform, new UnityEngine.Object[] {CurrentResource});
            return armor;
        }
    }
}