using Assets.Scripts.Player.Inventory.View;
using Assets.Scripts.Resources.Data;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Assets.Scripts.Inventory.View {
    public class SlotView : MonoBehaviour
    {
        //tool прокинуть посмотреть работает ли
        [SerializeField] protected Image itemView;

        protected Image slotBackground;
        
        private TextMeshProUGUI _countDisplay;
        private Resource _currentResource;
        private int _currentAmount = 0;

        private void Awake() {
            slotBackground = GetComponent<Image>();   
            _countDisplay = GetComponentInChildren<TextMeshProUGUI>(); 
        }

        public Resource Get() {
            return _currentResource;
        }

        public void Delete() {
            _currentAmount = 0;
            _currentResource = null;
            itemView.sprite = null;
            UpdateCount();
        }

        public void Set(Resource newResource) {
            _currentResource = newResource;
            itemView.sprite = _currentResource.SpriteInInventary;
        }

        public int GetCount() {
            return _currentAmount;
        }

        public void SetCount(int amount) {
            _currentAmount = amount;
            UpdateCount();
        }

        public void Increment() { SetCount(_currentAmount + 1); }

        public void Decrement() { SetCount(_currentAmount - 1); }

        public bool CanSubstract(int amount) {
            return _currentResource != null && _currentAmount > amount;
        }

        private void UpdateCount() {
            if(_countDisplay == null) { _countDisplay = GetComponentInChildren<TextMeshProUGUI>();}
            _countDisplay.text = _currentAmount.ToString();
        }
    }
}