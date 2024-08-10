using System;
using Assets.Scripts.Inventory.View;
using UnityEngine;

namespace Assets.Scripts.Inventory.Armor {
    [Serializable]
    public class ArmorSlotHack {
        public SpriteRenderer sprite;
        public ArmorSlotView slot;
    }
}