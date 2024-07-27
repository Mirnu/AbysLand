using System.Collections.Generic;
using Assets.Scripts.Player.Inventory.View;
using Assets.Scripts.Resources.Data;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Player.Inventory.BackPack
{
    public class ContainerSelectableSlots : IInitializable
    {
        private List<SelectableSlotView> _slots = new List<SelectableSlotView>();
        private Resource _cursorResource;

        public ContainerSelectableSlots(List<SelectableSlotView> slots) {
            _slots = slots;
        }

        public void Initialize()
        {
            _slots.ForEach(x => {
                x.LeftMouseClick += delegate { 
                    bindToCursor(x);
                };
            });
        }

        private void bindToCursor(SelectableSlotView slot) {
            if(_cursorResource != null) {
                if(slot.Get() != null) {
                    var _temp = slot.Get();
                    slot.Set(_cursorResource);
                    _cursorResource = _temp;
                } else if (_cursorResource != null) {
                    slot.Set(_cursorResource);
                    _cursorResource = null;
                }
            } else {
                _cursorResource = slot.Get();
                slot.Delete();
            }
        }
    }
}
