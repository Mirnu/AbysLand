using Assets.Scripts.Resources.Data;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Assets.Scripts.Inventory {
    public class SlotView : MonoBehaviour, IPointerEnterHandler {
        [SerializeField] private Image slotBackground;
        [SerializeField] private Image itemView;

        private Resource _currentResource;
        private int _currentAmount;

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

        public void OnHover(PointerEventData data) {

        }

        public void OnPointerEnter(PointerEventData eventData) => OnHover(eventData);
    }
}