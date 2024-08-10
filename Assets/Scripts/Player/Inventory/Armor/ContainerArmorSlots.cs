using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Inventory.View;
using Assets.Scripts.Resources.Data;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Inventory.Armor {
    public class ContainerArmorSlots : IInitializable {
        private List<ArmorSlotCont> _armorSlots = new List<ArmorSlotCont>();
        private List<AccessorySlotCont> _accessorySlots = new List<AccessorySlotCont>();

        public ContainerArmorSlots(List<ArmorSlotCont> armorSlots, List<AccessorySlotCont> accessorySlots) {
            _armorSlots = armorSlots;
            _accessorySlots = accessorySlots;
        }

        public void Initialize()
        {
            _armorSlots.ForEach(Hack => {
                //Breaking dry stupid nigga
                Hack.Slot.OnArmorChanged += delegate { updateArmorSprite(Hack); };
                Hack.Cosmetic.OnArmorChanged += delegate { updateArmorSprite(Hack); };
            });
        }

        private void updateArmorSprite(ArmorSlotCont Hack) {
            Hack.Sprite.sprite = 
            Hack.Cosmetic.TryGet(out Resource res) ? ((ArmorResource)res).EquippedSprite : 
            Hack.Slot.TryGet(out Resource armor) ? ((ArmorResource)armor).EquippedSprite : null; 
        }

    }
}