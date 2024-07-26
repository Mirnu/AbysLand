using System;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Inventory.View;
using Assets.Scripts.Player.Inventory.View;
using Assets.Scripts.Resources.Data;
using Assets.Scripts.Resources.Tools;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

namespace Assets.Scripts.Player.Inventory.Hotbar
{
    public class ContainerHotbarSlots : IInitializable, IDisposable
    {
        private List<HotbarSlotView> _slots = new List<HotbarSlotView>();
        private readonly PlayerInput _input;
        private Resource mock;

        public ContainerHotbarSlots (PlayerInput input, List<HotbarSlotView> slots, Resource _mock) {
            _slots = slots;
            _input = input;
            mock = _mock;
        }

        public void Initialize()
        {
            //
            _slots[0].Set(mock);
            //
            _slots.ForEach(x => x.GetComponent<SelectableSlotView>().enabled = false);
            _input.Gameplay.Hotbar.performed += HotbarChangeState;
            _input.Gameplay.Inventory.performed += HotbarChangeSelectability;
        }

        public void Dispose()
        {
            _input.Gameplay.Hotbar.performed -= HotbarChangeState;
            _input.Gameplay.Inventory.performed -= HotbarChangeSelectability;
        }

        private void HotbarChangeSelectability(InputAction.CallbackContext context) {
            if(_slots.Any(x => x.IsSelected)) { _slots.Find(x => x.IsSelected).Deselect(); }
            _slots.ForEach(x => x.GetComponent<SelectableSlotView>().enabled = !x.GetComponent<SelectableSlotView>().enabled);
        }

        private void HotbarChangeState(InputAction.CallbackContext context) {
            var index = context.ReadValue<float>();
            if(index < _slots.Count) {
                if(_slots.Any(x => x.IsSelected)) { _slots.Find(x => x.IsSelected).Deselect(); }
                _slots[(int)index].Select();
            }
        }
    }
}