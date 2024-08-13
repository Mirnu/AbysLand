using System;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Player.Inventory.View;
using Assets.Scripts.Resources.Crafting;
using Assets.Scripts.Resources.Data;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Player.Inventory.BackPack
{
    public class ContainerSelectableSlots : IInitializable
    {
        public List<RecipeComponent> components = new List<RecipeComponent>();
        public Action onInvChanged;

        private List<SelectableSlotView> _slots = new List<SelectableSlotView>();
        private SlotInfoView _slotInfoView;

        private Resource _cursorResource;
        private int _cursorCount = 0;
        private bool _;

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
                    UpdateDict();
                };
                x.RightMouseClick += delegate {
                    bindRightClick(x);
                    UpdateDict();
                };
                x.OnCursorEnter += delegate {
                    _slotInfoView.Update(x.TryGet(out Resource res) ? res : null);
                };
                x.OnCursorExit += delegate {
                    _slotInfoView.Empty();
                };
            });
        }

        private void bindLeftClick(SelectableSlotView slot) {
            if(_cursorResource != null) {
                if(slot.TryGet(out Resource res)) {
                    if(res != _cursorResource) { Replace(slot); } 
                    else { slot.SetCount(slot.GetCount() + _cursorCount); EmptyCursor(); }
                } else if(slot.TrySet(_cursorResource)) {
                    //?
                    //slot.TrySet(_cursorResource);
                    slot.SetCount(_cursorCount);
                    EmptyCursor();
                }
            } else {
                slot.TryGet(out Resource res);
                _cursorResource = res;
                _cursorCount = slot.GetCount();
                slot.Delete();
            }
        }

        private void UpdateDict() {
            components.Clear();
            _slots.ForEach(slot => {
                if (slot.TryGet(out Resource res)) {
                    if(components.Any(x => x.resource == res)) { 
                        components.Find(x => x.resource == res).count += slot.GetCount();
                    } else {
                        components.Add(new RecipeComponent(res, slot.GetCount()));
                    }
                }
            });
            onInvChanged?.Invoke();
        }

        public void bindCraftLeft(SelectableSlotView slot) {
            if(_cursorResource != null) { return; }
             slot.TryGet(out Resource res);
                _cursorResource = res;
                _cursorCount = slot.GetCount();
                slot.Delete();
        }

        private void bindRightClick(SelectableSlotView slot) {
            _ = slot.TryGet(out Resource res);
            if (_cursorResource == null) {
                if(_) {
                    _cursorResource = res;
                    _cursorCount = 1;
                    slot.Decrement();
                }
            } else {
                if (!_) {
                    slot.TrySet(_cursorResource);
                    slot.Increment();
                    if(_cursorCount > 1) { _cursorCount--; } 
                    else { EmptyCursor(); }
                } else if(res == _cursorResource) {
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
            slot.TryGet(out Resource res);
            var _temp = res;
            var __ = slot.GetCount();
            slot.TrySet(_cursorResource);
            slot.SetCount(_cursorCount);
            _cursorResource = _temp;
            _cursorCount = __;
        }
    }
}
