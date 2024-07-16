using Player.Handlers;
using System;
using UnityEngine;
using Zenject;

[CreateAssetMenu(fileName = "PlayerSettingsInstaller", menuName = "Installers/PlayerSettingsInstaller")]
public class PlayerSettingsInstaller : ScriptableObjectInstaller<PlayerSettingsInstaller>
{
    public PlayerStatesSettings PlayerStates;

    [Serializable]
    public class PlayerStatesSettings
    {
        public PlayerMoveHandler.Settings PlayerMoveHandler;
    }

    public override void InstallBindings()
    {
        Container.BindInstance(PlayerStates.PlayerMoveHandler).IfNotBound();
    }
}