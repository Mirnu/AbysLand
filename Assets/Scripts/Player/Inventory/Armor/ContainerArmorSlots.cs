using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Inventory.View;
using Assets.Scripts.Resources.Data;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Inventory.Armor {
    public class ContainerArmorSlots : IInitializable {
        private Dictionary<SpriteRenderer, ArmorSlotView> _armorSlots = new Dictionary<SpriteRenderer, ArmorSlotView>();

        public ContainerArmorSlots(List<ArmorSlotHack> armorSlots) {
            armorSlots.ForEach(x => _armorSlots.Add(x.sprite, x.slot));
        }

        public void Initialize()
        {
            foreach (KeyValuePair<SpriteRenderer, ArmorSlotView> pair in _armorSlots) {
                pair.Value.OnArmorChanged += delegate { pair.Key.sprite = pair.Value.TryGet(out Resource res) ? ((ArmorResource)res).EquippedSprite : null; };
            }
        }
    }
}