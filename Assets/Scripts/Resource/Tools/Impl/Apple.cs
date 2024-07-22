using Assets.Scripts.Player;
using Assets.Scripts.Resources.Data;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Resources.Tools.Impl
{
    public class Apple : Tool
    {
        private FoodResource _resource;
        private PlayerFacade _playerFacade;

        [Inject]
        public void Construct(Resource resource, PlayerFacade facade)
        {
            _resource = (FoodResource)resource;
            _playerFacade = facade;
        }

        private void OnEnable()
        {
            Debug.Log("Current food: " + _resource.Satiety);
        }
    }
}
