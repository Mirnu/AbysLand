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
       
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<PlayerModel>().AsSingle()
                .WithArguments(_rigidbody);
            Container.BindInterfacesAndSelfTo<PlayerMoveHandler>().AsSingle().NonLazy();
        }

        class Settings
        {

        }
    }
}