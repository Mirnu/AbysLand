using System.Collections.Generic;
using Assets.Scripts.Player.Handlers;
using Assets.Scripts.Player.Hands;
using Assets.Scripts.Player.Model;
using Assets.Scripts.Player.Stats.UI;
using Assets.Scripts.Player.Systems;
using Assets.Scripts.Resources.Data;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace DI
{
    public class PlayerInstaller : MonoInstaller
    {
        [Header("Player")]
        [SerializeField] private Rigidbody _rigidbody;
        [SerializeField] private Animator _animator;
        [SerializeField] private Transform _handTransform;
        [SerializeField] private Transform _playerTransform;
        [SerializeField] private Resource _starterResource;

        [Header("UI")]
        [SerializeField] private PlayerStatesView _playerStatesView;
        [SerializeField] private List<Image> _hotbar_slots = new List<Image>();
        [SerializeField] private List<Image> _hotbar_slots_background = new List<Image>();
        [SerializeField] private Sprite _emptySlot;
        [SerializeField] private Sprite _selectedSlot;

        [Header("Menu")]
        [SerializeField] private GameObject _inventory;
        
        public override void InstallBindings()
        { 
            bindModels();
            bindHandlers();
            bindSystems();

            Container.BindInterfacesAndSelfTo<Hand>().AsSingle()
                .WithArguments(_handTransform, Container, _starterResource);

            Container.BindInterfacesAndSelfTo<PlayerHotbarUIHandler>().AsSingle()
                .WithArguments(_hotbar_slots, _hotbar_slots_background, _emptySlot, _selectedSlot);

            Container.BindInstance(new PlayerInput());
        }


        private void bindModels()
        {
            Container.BindInterfacesAndSelfTo<PlayerModel>().AsSingle()
                .WithArguments(_rigidbody, _animator, _playerTransform);
        }

        private void bindHandlers()
        {
            Container.BindInterfacesAndSelfTo<PlayerHotbarHandler>().AsSingle()
                .WithArguments(_starterResource);
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