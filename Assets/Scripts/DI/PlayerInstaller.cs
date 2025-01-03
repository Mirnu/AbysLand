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
using UnityEngine.UI;
using Zenject;

namespace DI
{
    public class PlayerInstaller : MonoInstaller
    {
        [Header("Player")]
        [SerializeField] private Rigidbody _rb;
        [SerializeField] private Animator _animator;
        [SerializeField] private Transform _playerTransform;

        [Header("Tool")]
        [SerializeField] private Animator _armAnimator;
        [SerializeField] private Transform _handTransform;
        [SerializeField] private Resource _starterResource;
        [SerializeField] private Resource _foodResource;
        [SerializeField] private List<GameObject> _handPoints;

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
        [SerializeField] private AutoCraftingButton button;
        [SerializeField] private GridLayoutGroup layoutGroup;
        [SerializeField] private List<SelectableSlotView> craftSlot;
        [SerializeField] private SelectableSlotView craftResultSlot;


        public override void InstallBindings()
        { 
            bindModels();
            bindHandlers();
            bindSystems();
            bindControllers();

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

            Container.BindInterfacesAndSelfTo<AutoCraftingUIManager>().AsSingle()
                .WithArguments(button, layoutGroup);

            // Container.BindInterfacesAndSelfTo<CraftingUIManager>().AsSingle()
            //     .WithArguments(craftSlot, craftResultSlot);
        }

        private void bindModels()
        {
            Container.BindInterfacesAndSelfTo<PlayerModel>().AsSingle()
                .WithArguments(_rb, _animator, _playerTransform, _armAnimator);
        }

        private void bindHandlers()
        {
            Container.BindInterfacesAndSelfTo<PlayerRotationHandler>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<PlayerMoveHandler>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<PlayerFoodHandler>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<PlayerHealthHandler>().AsSingle().NonLazy();
        }

        private void bindControllers()
        {
            Container.BindInterfacesAndSelfTo<PlayerAnimationController>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<ArmAnimationController>().AsSingle()
                .WithArguments(_handPoints).NonLazy();
            Container.BindInterfacesAndSelfTo<PlayerDirectionController>().AsSingle().NonLazy();
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