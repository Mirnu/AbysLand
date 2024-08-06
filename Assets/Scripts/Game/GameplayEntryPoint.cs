using Assets.Scripts.Player;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Game
{
    public class GameplayEntryPoint : IInitializable
    {
        private readonly PlayerFacade _player;
        private readonly WorldInitializer _worldInitializer;
        private readonly GameManager _gameManager;

        public GameplayEntryPoint(PlayerFacade player, WorldInitializer worldInitializer,
            GameManager gameManager)
        {
            _gameManager = gameManager;
            _player = player;
            _worldInitializer = worldInitializer;
            _worldInitializer.Initilized += delegate { 
                Debug.Log("Player alive"); 
                _player.Enable(); 
            };
        }

        public void Initialize()
        {
            _gameManager.StartGenerate();
        }
    }
}
