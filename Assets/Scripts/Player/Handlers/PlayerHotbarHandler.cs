using System.Collections.Generic;
using Assets.Scripts.Inventory;
using Assets.Scripts.Player.Hands;
using Assets.Scripts.Resources.Data;
using Zenject;

namespace Assets.Scripts.Player.Handlers {
    public class PlayerHotbarHandler : ITickable, IInitializable
    {
        private HotbarInventory _inventory;
        private PlayerHotbarUIHandler _handler;
        private PlayerInput _input;
        private Hand _hand;

        private int _index = 0;

        public PlayerHotbarHandler(PlayerHotbarUIHandler handler, PlayerInput input, Hand hand, Resource temp_res) {
            //Потом переделаем чтоб сохраняло
            _inventory = new HotbarInventory(3);
            _inventory.TryAddItem(temp_res);

            _handler = handler;
            _input = input;
            _hand = hand;
        }

        public void Initialize()
        {
            _handler.UpdateHotbar(_inventory.GetSprites());
            _hand.Equip(_inventory.GetItem(0));
        }

        public void Tick()
        {
            if(_input.Gameplay.Hotbar.triggered) {
                _index = (int)_input.Gameplay.Hotbar.ReadValue<float>();
                _handler.SwitchToSlot(_index);
                if(_inventory.GetItem(_index) != null) {
                    _hand.Equip(_inventory.GetItem(_index));
                } else {
                    _hand.EmptyHand();
                } 
                _handler.UpdateHotbar(_inventory.GetSprites());
            }
        }
    }
}