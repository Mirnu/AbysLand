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

        public void Equip(ArmorResource armor, DiContainer _cont) {
            if (_current != null) 
                UnityEngine.Object.Destroy(_current);
            _current = armor;
            _current.EquippedSprite = createArmor(armor, _cont);
        }

        private ArmorMB createArmor(ArmorResource resource, DiContainer _cont)
        {
            ArmorMB armor = _cont.
                InstantiatePrefabForComponent<ArmorMB>(resource.EquippedSprite, Sprite.transform, new UnityEngine.Object[] {resource});
            return armor;
        }
    }
}