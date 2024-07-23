using Assets.Scripts.Resources.Data;
using UnityEngine;

namespace Assets.Scripts.Resources.Tools.Impl
{
    public class Apple : Tool
    {
        private void OnEnable()
        {
            Debug.Log("Current food: " + ((FoodResource)resource).Satiety);
        }
    }
}
