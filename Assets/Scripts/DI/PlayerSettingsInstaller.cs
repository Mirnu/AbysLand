using Assets.Scripts.Player.Stats;
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
        public PlayerStatsModel.Settings PlayerMoveHandler;
    }

    public override void InstallBindings()
    {
        Container.BindInstance(PlayerStates.PlayerMoveHandler).IfNotBound();
    }
}