using System;
using Assets.Scripts.Resources.Crafting;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using Zenject;

namespace Assets.Scripts.Inventory.Crafting {
    public class AutoCraftingButton : MonoBehaviour {

        [SerializeField] private Image imgDisplay;
        [SerializeField] private TextMeshProUGUI countDisplay;

        public RecipeComponent component;

        private Button _button;

        private void Awake() {
            _button = GetComponent<Button>();
        }

        public void SetEvent(UnityAction action) => _button.onClick.AddListener(action);

        [Inject]
        public void Construct(RecipeComponent recipe) {
            component = recipe;
            imgDisplay.sprite = recipe.resource.SpriteInInventary;
            countDisplay.text = recipe.count.ToString();
        }
    }
}