using System;
using System.Collections.Generic;
using Assets.Scripts.Player.Inventory.BackPack;
using Assets.Scripts.Player.Inventory.View;
using Assets.Scripts.Resources.Crafting;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Assets.Scripts.Inventory.Crafting {
    public class AutoCraftingUIManager : IInitializable, IDisposable
    {
        private GridLayoutGroup _group;
        private AutoCraftingButton _prefab;
        private List<AutoCraftingButton> _currentPrefabs = new List<AutoCraftingButton>();
        private DiContainer _container;
        private ContainerSelectableSlots _selectableSlots;
        private RecipeContainer _recipeContainer;

        public AutoCraftingUIManager(AutoCraftingButton button, DiContainer container, RecipeContainer recipeContainer, ContainerSelectableSlots selectableSlots, GridLayoutGroup group) {
            _prefab = button;
            _container = container;
            _selectableSlots = selectableSlots;
            _group = group;
            _recipeContainer = recipeContainer;
        }

        public void UpdateCraftMenu() {
            _currentPrefabs.ForEach(x => UnityEngine.Object.Destroy(x));
            var _retrieved = _selectableSlots.components;
            // need to check 4 resources
            var all = _recipeContainer.RetrieveAllAvailable(_retrieved);
            all.ForEach(x => {
                // Кидает еррор что Unable to resolve 'RecipeComponent' while building object with type 'AutoCraftingButton'.
                _currentPrefabs.Add(_container.InstantiatePrefabForComponent<AutoCraftingButton>(_prefab, _group.transform, new object[]{x}));
            });

        }

        public void Dispose() { _selectableSlots.onInvChanged -= delegate { UpdateCraftMenu(); }; }

        public void Initialize() { _selectableSlots.onInvChanged += delegate { UpdateCraftMenu(); }; }
    }
}