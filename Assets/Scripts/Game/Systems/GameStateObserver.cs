using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Game.Systems
{
    public class GameStateObserver : IInitializable, IDisposable
    {
        private readonly GameManager _gameManager;

        public GameStateObserver(GameManager gameManager)
        {
            _gameManager = gameManager;
        }

        private Dictionary<GameState, List<Action>> _subscribersMap = new();



        ///<summary>
        ///Если не отписаться, то отпишется автоматически, при выходе из игры
        ///</summary>
        public void Subscribe(GameState state, Action subcriber)
        {
            Debug.Log(state.ToString());
            List<Action> subscribers = _subscribersMap.ContainsKey(state) ?
                _subscribersMap[state] :
                new List<Action>();
            subscribers.Add(subcriber);
            _subscribersMap[state] = subscribers;
        }

        public void Unsubscribe(GameState state, Action subcriber)
        {
            _subscribersMap[state].Remove(subcriber);
        }

        public void Initialize()
        {
            _gameManager.GameStateChanged += OnGameStateChanged;
        }

        private void OnGameStateChanged(GameState state)
        {
            if (!_subscribersMap.ContainsKey(state)) return;
            foreach (var subscriber in _subscribersMap[state])
            {
                Debug.Log(-1);
                subscriber();
            }
        }

        public void Dispose()
        {
            _gameManager.GameStateChanged += OnGameStateChanged;
        }
    }
}
