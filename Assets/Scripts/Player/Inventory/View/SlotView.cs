using Assets.Scripts.Player.Inventory.View;
using Assets.Scripts.Resources.Data;
using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Assets.Scripts.Inventory.View {
    public class SlotView : MonoBehaviour
    {
        //tool прокинуть посмотреть работает ли
        [SerializeField] protected Image itemView;

        protected Image slotBackground;
        
        private Resource _currentResource;
        private int _currentAmount;

        private void OnEnable() {
            slotBackground = GetComponent<Image>();    
        }

        public Resource Get() {
            return _currentResource;
        }

        public void Delete() {
            _currentAmount = 0;
            _currentResource = null;
            itemView.sprite = null;
        }

        public void Set(Resource newResource) {
            _currentResource = newResource;
            itemView.sprite = _currentResource.SpriteInInventary;
        }

        public void Add(int amount) {
            if(_currentResource == null) { return; }
            _currentAmount += amount;
        }
    }
}