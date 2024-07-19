using Assets.Scripts.Player.Handlers;
using Assets.Scripts.Player.Hands;
using Assets.Scripts.Player.Model;
using Assets.Scripts.Resources.Data;
using UnityEngine;
using Zenject;

namespace DI
{
    public class PlayerInstaller : MonoInstaller
    {
        [SerializeField] private Rigidbody2D _rigidbody;
        [SerializeField] private Animator _animator;
        [SerializeField] private Transform _handTransform;

        public FoodResource Food;
        
        public override void InstallBindings()
        { 
            Container.BindInterfacesAndSelfTo<PlayerModel>().AsSingle()
                .WithArguments(_rigidbody, _animator);

            Container.BindInterfacesAndSelfTo<Hand>().AsSingle()
               .WithArguments(_handTransform, Container);

            Container.BindInterfacesAndSelfTo<ToolMock>().AsSingle()
                .WithArguments(Food);

            Container.BindInterfacesAndSelfTo<PlayerMoveHandler>().AsSingle().NonLazy();

            Container.BindInterfacesAndSelfTo<PlayerRotationHandler>().AsSingle().NonLazy();

            Container.BindInstance(new PlayerInput());

            Container.BindInterfacesAndSelfTo<PlayerAnimationHandler>().AsSingle().NonLazy();
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