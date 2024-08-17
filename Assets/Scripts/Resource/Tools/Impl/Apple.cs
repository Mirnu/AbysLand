using Assets.Scripts.Resources.Data;
using UnityEngine;

namespace Assets.Scripts.Resources.Tools.Impl
{
    public class Apple : Tool
    {
        private FoodResource _resource => GetResource<FoodResource>();

        private void OnEnable()
        {
            Debug.Log("Current food: " + _resource.Satiety);
        }
    }
}
