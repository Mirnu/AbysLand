using System;
using UnityEngine;
using Zenject;
using Player.Model;
using Player.Handlers;

namespace DI
{
    public class PlayerInstaller : MonoInstaller
    {
        [SerializeField] private Rigidbody2D _rigidbody;
        [SerializeField] private Animator _animator;
       
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<PlayerModel>().AsSingle()
                .WithArguments(_rigidbody, _animator);
            Container.BindInterfacesAndSelfTo<PlayerMoveHandler>().AsSingle().NonLazy();
            Container.BindInstance(new PlayerInput());
            Container.BindInterfacesAndSelfTo<PlayerAnimationHandler>().AsSingle().NonLazy();
        }
    }
}