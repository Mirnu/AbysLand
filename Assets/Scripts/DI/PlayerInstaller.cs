using System;
using System.Collections.Generic;
using Assets.Scripts.Inventory.Armor;
using Assets.Scripts.Inventory.Crafting;
using Assets.Scripts.Inventory.View;
using Assets.Scripts.Player.Handlers;
using Assets.Scripts.Player.Hands;
using Assets.Scripts.Player.Inventory.BackPack;
using Assets.Scripts.Player.Inventory.Hotbar;
using Assets.Scripts.Player.Inventory.View;
using Assets.Scripts.Player.Model;
using Assets.Scripts.Player.Stats.UI;
using Assets.Scripts.Player.Systems;
using Assets.Scripts.Resources.Data;
using Assets.Scripts.World;
using TMPro;
using UnityEngine;
using Zenject;

namespace DI
{
    public class PlayerInstaller : MonoInstaller
    {
        [Header("Player")]
        [SerializeField] private Rigidbody _rb;
        [SerializeField] private Animator _animator;
        [SerializeField] private Transform _handTransform;
        [SerializeField] private Transform _playerTransform;
        [SerializeField] private Resource _starterResource;
        [SerializeField] private Resource _foodResource;

        [Header("UI")]
        [SerializeField] private PlayerStatesView _playerStatesView;
        [SerializeField] private List<HotbarSlotView> _hotbar_slots = new List<HotbarSlotView>();
        [SerializeField] private List<SelectableSlotView> _inventory_slots = new List<SelectableSlotView>();

        [Header("Menu")]
        [SerializeField] private GameObject _inventory;

        [Header("Inventory")]
        [SerializeField] private TextMeshProUGUI nameText;
        [SerializeField] private TextMeshProUGUI infoText;
        [Space]
        [SerializeField] private List<ArmorSlotCont> armorSlots;
        [SerializeField] private List<AccessorySlotCont> accessorySlots;
        [Space]
        [SerializeField] private List<SelectableSlotView> craftSlot;
        [SerializeField] private SelectableSlotView craftResultSlot;


        public override void InstallBindings()
        { 
            bindModels();
            bindHandlers();
            bindSystems();

            Container.BindInterfacesAndSelfTo<Hand>().AsSingle()
                .WithArguments(_handTransform, Container, _starterResource);

            bindInventoryUI();

            Container.BindInstance(new PlayerInput());
        }

        private void bindInventoryUI() {
            Container.BindInterfacesAndSelfTo<ContainerHotbarSlots>().AsSingle()
                .WithArguments(_hotbar_slots, _starterResource, _foodResource);

            Container.BindInterfacesAndSelfTo<ContainerSelectableSlots>().AsSingle()
                .WithArguments(_inventory_slots);

            Container.BindInterfacesAndSelfTo<SlotInfoView>().AsSingle()
                .WithArguments(nameText, infoText);

            Container.BindInterfacesAndSelfTo<ContainerArmorSlots>().AsSingle()
                .WithArguments(armorSlots, accessorySlots);

            Container.BindInterfacesAndSelfTo<CraftingUIManager>().AsSingle()
                .WithArguments(craftSlot, craftResultSlot);
        }

        private void bindModels()
        {
            Container.BindInterfacesAndSelfTo<PlayerModel>().AsSingle()
                .WithArguments(_rb, _animator, _playerTransform);
        }

        private void bindHandlers()
        {
            Container.BindInterfacesAndSelfTo<PlayerRotationHandler>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<PlayerAnimationHandler>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<PlayerMoveHandler>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<PlayerFoodHandler>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<PlayerHealthHandler>().AsSingle().NonLazy();
        }

        private void bindSystems()
        {
            Container.BindInterfacesAndSelfTo<PlayerMovement>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<HealSystem>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<PlayerInventory>().AsSingle().
                WithArguments(_inventory).
                NonLazy();
        }
    }
}