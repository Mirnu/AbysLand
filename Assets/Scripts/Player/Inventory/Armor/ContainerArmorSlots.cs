using System;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Inventory.View;
using Assets.Scripts.Player.Stats;
using Assets.Scripts.Resources.Data;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Inventory.Armor {
    public class ContainerArmorSlots : IInitializable, ITickable {
        private List<ArmorSlotCont> _armorSlots = new List<ArmorSlotCont>();
        private List<AccessorySlotCont> _accessorySlots = new List<AccessorySlotCont>();
        private DiContainer _container;
        private PlayerStatsModel _model;

        public ContainerArmorSlots(List<ArmorSlotCont> armorSlots, List<AccessorySlotCont> accessorySlots, DiContainer container, PlayerStatsModel stats) {
            _armorSlots = armorSlots;
            _model = stats;
            _accessorySlots = accessorySlots;
            _container = container;
        }

        public void Initialize()
        {
            //_model.Health = 100;
            _model.ArmorHandle += armorCalc;
            _armorSlots.ForEach(Hack => {
                //Breaking dry stupid nigga
                Hack.Slot.OnArmorChanged += delegate { updateArmor(Hack); };
                Hack.Cosmetic.OnArmorChanged += delegate { updateArmor(Hack); };
            });
        }

        private int armorCalc((int o, int n) tuple)
        {
            // Пока что так потом переделаю
            if(tuple.o > tuple.n) {
                var dmg = tuple.o - tuple.n;
                _armorSlots.Where(x => x != null && x.CurrentResource != null).ToList().ForEach(x => {
                    dmg -= x.CurrentResource.ProtectionAmount;
                });
                return tuple.o - dmg;
            }
            return tuple.n;
        }

        public void Tick()
        {
            if(Input.GetKeyDown(KeyCode.F)) {
                _model.Health -= 10;
            }
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