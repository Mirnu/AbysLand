using Assets.Scripts.Resources.Crafting;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Assets.Scripts.Inventory.Crafting {
    public class AutoCraftingButton : MonoBehaviour {

        [SerializeField] private Image imgDisplay;
        [SerializeField] private TextMeshProUGUI countDisplay;

        [Inject]
        public void Construct(RecipeComponent recipe) {
            imgDisplay.sprite = recipe.resource.SpriteInInventary;
            countDisplay.text = recipe.count.ToString();
        }
    }
}