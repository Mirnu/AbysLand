using System;

namespace Assets.Scripts.Game
{
    public enum GameState
    {
        LoadingGameplay, // Состояние означает, что началась загрузка геймплея
        Generating, // Состояние означает, что мир начал генерироваться
        Gameplay, // Состояние означает, что игрок начал игровой цикл
    }

    public class GameManager
    {
        private GameState _currentState;
        public GameState CurrentState
        {
            get { return _currentState; }
            set { 
                _currentState = value; 
                GameStateChanged?.Invoke(value);
            }
        }

        public event Action<GameState> GameStateChanged;

        public void StartLoadingGameplay()
        {
            CurrentState = GameState.LoadingGameplay;
        }

        public void StartGameplay()
        {
            CurrentState = GameState.Gameplay;
        }

        public void StartGenerate()
        {
            CurrentState = GameState.Generating;
        }
    }
}
