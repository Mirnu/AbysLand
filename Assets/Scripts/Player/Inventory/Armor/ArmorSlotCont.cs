using System;
using Assets.Scripts.Inventory.View;
using UnityEngine;

namespace Assets.Scripts.Inventory.Armor {
    [Serializable]
    public class ArmorSlotCont {
        public SpriteRenderer Sprite;
        public ArmorSlotView Slot;
        public ArmorSlotView Cosmetic;
    }
}