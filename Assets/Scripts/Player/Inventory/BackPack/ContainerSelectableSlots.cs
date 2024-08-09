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
        private SlotInfoView _slotInfoView;

        private Resource _cursorResource;
        private int _cursorCount = 0;

        public ContainerSelectableSlots(List<SelectableSlotView> slots, SlotInfoView slotInfoView) {
            _slots = slots;
            _slotInfoView = slotInfoView;
        }

        public void Initialize()
        {
            //
            _slots[0].SetCount(15);
            //
            _slots.ForEach(x => {
                x.LeftMouseClick += delegate { 
                    bindLeftClick(x);
                };
                x.RightMouseClick += delegate {
                    bindRightClick(x);
                };
                x.OnCursorEnter += delegate {
                    _slotInfoView.Update(x.Get());
                };
                x.OnCursorExit += delegate {
                    _slotInfoView.Empty();
                };
            });
        }

        private void bindLeftClick(SelectableSlotView slot) {
            if(_cursorResource != null) {
                if(slot.Get() != null) {
                    if(slot.Get() != _cursorResource) { Replace(slot); } 
                    else { slot.SetCount(slot.GetCount() + _cursorCount); EmptyCursor(); }
                } else {
                    slot.Set(_cursorResource);
                    slot.SetCount(_cursorCount);
                    EmptyCursor();
                }
            } else {
                _cursorResource = slot.Get();
                _cursorCount = slot.GetCount();
                slot.Delete();
            }
        }

        private void bindRightClick(SelectableSlotView slot) {
            if (_cursorResource == null) {
                if(slot.Get() != null) {
                    _cursorResource = slot.Get();
                    _cursorCount = 1;
                    slot.Decrement();
                }
            } else {
                if (slot.Get() == null) {
                    slot.Set(_cursorResource);
                    slot.Increment();
                    if(_cursorCount > 1) { _cursorCount--; } 
                    else { EmptyCursor(); }
                } else if(slot.Get() == _cursorResource) {
                    //Бесконечная хуйня
                    _cursorCount++; slot.Decrement();
                } else {
                    Replace(slot);
                }
            }
        }

        private void EmptyCursor() {
            _cursorResource = null;
            _cursorCount = 0;
        }

        private void Replace(SelectableSlotView slot) {
            var _temp = slot.Get();
            var __ = slot.GetCount();
            slot.Set(_cursorResource);
            slot.SetCount(_cursorCount);
            _cursorResource = _temp;
            _cursorCount = __;
        }
    }
}
