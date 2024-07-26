using System.Collections.Generic;
using Assets.Scripts.Player.Inventory.View;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Player.Inventory.BackPack
{
    public class ContainerSelectableSlots : IInitializable
    {
        private List<SelectableSlotView> _slots = new List<SelectableSlotView>();

        public ContainerSelectableSlots(List<SelectableSlotView> slots) {
            _slots = slots;
        }

        public void Initialize()
        {
            _slots.ForEach(x => {
                x.LeftMouseClick += delegate { 
                    Debug.Log(" :: " + _slots.IndexOf(x)); 
                };
            });
        }
    }
}
