using System;
using System.Collections.Generic;
using Assets.Scripts.Inventory.View;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

namespace Assets.Scripts.Player.Systems
{
    public class PlayerInventory : IDisposable, IInitializable
    {
        private readonly PlayerInput _input;
        private GameObject _inventory;

        public PlayerInventory(PlayerInput input, 
            GameObject inventory)
        {
            _input = input;
            _inventory = inventory;
        }

        public void Dispose()
        {
            _input.Gameplay.Inventory.performed -= OnInventoryChangedState;
        }

        public void Initialize()
        {
            Debug.Log("Init");
            _input.Gameplay.Inventory.performed += OnInventoryChangedState;
        }

        private void OnInventoryChangedState(InputAction.CallbackContext context)
        {
            Debug.Log("is changed state");
            _inventory.SetActive(!_inventory.activeSelf);
        }
    }
}
