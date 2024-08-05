using Assets.Scripts.Game.Systems;
using Assets.Scripts.Misc.Constants;
using UnityEngine.SceneManagement;
using Zenject;

namespace Assets.Scripts.Game.States
{
    public class LoadingState : IInitializable
    {
        private readonly GameStateObserver _gameStateObserver;

        public LoadingState(GameStateObserver gameStateObserver)
        {
            _gameStateObserver = gameStateObserver;
        }

        public void Initialize()
        {
            _gameStateObserver.Subscribe(GameState.LoadingGameplay, OnLoadingGameplay);
        }

        private void OnLoadingGameplay()
        {
            SceneManager.LoadScene(Scenes.Game);
        }
    }
}
