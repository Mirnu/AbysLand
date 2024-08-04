using System.Collections.Generic;
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
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;
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

        [Header("UI")]
        [SerializeField] private PlayerStatesView _playerStatesView;
        [SerializeField] private List<HotbarSlotView> _hotbar_slots = new List<HotbarSlotView>();
        [SerializeField] private List<SelectableSlotView> _inventory_slots = new List<SelectableSlotView>();

        [Header("Menu")]
        [SerializeField] private GameObject _inventory;
        [Space]
        [SerializeField] private TileBase _tile;
        [SerializeField] private TileBase _tile1;
        [SerializeField] private Tilemap highlightTilemap;
        [SerializeField] private Tilemap highlightTilemap1;
        [SerializeField] private Tilemap tilemap;
        [SerializeField] private List<DmgTile> healthDict = new List<DmgTile>();

        public override void InstallBindings()
        { 
            bindModels();
            bindHandlers();
            bindSystems();

            Container.BindInterfacesAndSelfTo<Hand>().AsSingle()
                .WithArguments(_handTransform, Container, _starterResource);

            Container.BindInterfacesAndSelfTo<ContainerHotbarSlots>().AsSingle()
                .WithArguments(_hotbar_slots, _starterResource);

            Container.BindInterfacesAndSelfTo<ContainerSelectableSlots>().AsSingle()
                .WithArguments(_inventory_slots);

            Container.BindInstance(new PlayerInput());
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

            Container.BindInterfacesAndSelfTo<DamageableHandler>().AsSingle()
                .WithArguments(tilemap, healthDict);


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