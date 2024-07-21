using UnityEngine;
using Zenject;

namespace Assets.Scripts.Player.Handlers {
    public class PlayerHotbarHandler 
    {
        private AbstractInventory _inventory;

        public PlayerHotbarHandler(AbstractInventory inventory) {
            _inventory = inventory;
        }

        
    }
}