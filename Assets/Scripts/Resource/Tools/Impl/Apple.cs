using Assets.Scripts.Player.Hands;
using Assets.Scripts.Resources.Data;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Resources.Tools.Impl
{
    public class Apple : Tool
    {
        private FoodResource _resource;

        [Inject]
        public void Construct(Resource resource)
        {
            _resource = (FoodResource)resource;
        }

        private void OnEnable()
        {
            Debug.Log("Current food: " + _resource.Satiety);
        }
    }
}
