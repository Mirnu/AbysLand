using System;
using UnityEngine;

namespace Assets.Scripts.Resources.Data {

    [CreateAssetMenu(menuName = "Resources/New Armor Resource")]
    public class ArmorResource : Resource {

        public ArmorSlotType SlotType;
        public int ProtectionAmount;
        public Sprite EquippedSprite;

    }

    public enum ArmorSlotType {
        Head, 
        Chest, 
        Legs, 
        Backpack,
        Accessory
    }

}