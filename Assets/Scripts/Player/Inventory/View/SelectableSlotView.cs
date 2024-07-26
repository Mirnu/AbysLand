using System;
using Assets.Scripts.Inventory.View;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.Scripts.Player.Inventory.View
{
    public class SelectableSlotView : SlotView, IPointerClickHandler
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
