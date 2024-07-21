using Assets.Scripts.Player.Handlers;
using Assets.Scripts.Player.Hands;
using Assets.Scripts.Player.Model;
using Assets.Scripts.Player.Stats.UI;
using Assets.Scripts.Player.Systems;
using Assets.Scripts.Resources.Data;
using UnityEngine;
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

        [Header("UI")]
        [SerializeField] private PlayerStatesView _playerStatesView;

        public FoodResource Food;
        
        public override void InstallBindings()
        { 
            bindModels();
            bindHandlers();

            Container.BindInterfacesAndSelfTo<Hand>().AsSingle()
               .WithArguments(_handTransform, Container);

            Container.BindInterfacesAndSelfTo<ToolMock>().AsSingle()
                .WithArguments(Food);
            Container.BindInstance(new PlayerInput());
        }


        private void bindModels()
        {
            Container.BindInterfacesAndSelfTo<PlayerModel>().AsSingle()
                .WithArguments(_rigidbody, _animator, _playerTransform);
        }

        private void bindHandlers()
        {
            Container.BindInterfacesAndSelfTo<PlayerMovement>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<PlayerRotationHandler>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<PlayerAnimationHandler>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<PlayerMoveHandler>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo <PlayerFoodHandler>().AsSingle().NonLazy();
        }
    }

    class ToolMock : IInitializable
    {
        private Hand _hand;
        private FoodResource _foodResource;

        public ToolMock(Hand hand, FoodResource resource) 
        { 
            _hand = hand;
            _foodResource = resource;
        }

        public void Initialize()
        {
            _hand.Equip(_foodResource);
        }
    }
}