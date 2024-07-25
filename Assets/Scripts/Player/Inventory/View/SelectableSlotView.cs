using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.Scripts.Player.Inventory.View
{
    public class SelectableSlotView : MonoBehaviour, IPointerClickHandler
    {
        public event Action LeftMouseClick;
        public event Action RightMouseClick;

        public void OnPointerClick(PointerEventData eventData)
        {
            if (eventData.button == PointerEventData.InputButton.Left)
            {
                LeftMouseClick?.Invoke();
            }
            else if (eventData.button == PointerEventData.InputButton.Right)
            {
                RightMouseClick?.Invoke();
            }
        }
    }
}
