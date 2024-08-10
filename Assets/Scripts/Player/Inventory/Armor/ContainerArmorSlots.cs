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
        private DiContainer _container;

        public ContainerArmorSlots(List<ArmorSlotCont> armorSlots, List<AccessorySlotCont> accessorySlots, DiContainer container) {
            _armorSlots = armorSlots;
            _accessorySlots = accessorySlots;
            _container = container;
        }

        public void Initialize()
        {
            _armorSlots.ForEach(Hack => {
                //Breaking dry stupid nigga
                Hack.Slot.OnArmorChanged += delegate { updateArmor(Hack); };
                Hack.Cosmetic.OnArmorChanged += delegate { updateArmor(Hack); };
            });
        }

        private void updateArmor(ArmorSlotCont Hack) {
            Hack.Equip(
            Hack.Cosmetic.TryGet(out Resource res) ? ((ArmorResource)res) : 
            Hack.Slot.TryGet(out Resource armor) ? ((ArmorResource)armor) : null,
            _container
            ); 
            
        }

    }
}