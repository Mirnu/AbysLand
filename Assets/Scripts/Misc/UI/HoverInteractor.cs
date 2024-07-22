using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace Assets.Scripts.Misc.UI
{
    public class HoverInteractor : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] private UnityEvent Enter;
        [SerializeField] private UnityEvent Exit;

        public void OnPointerExit(PointerEventData eventData) => Exit?.Invoke(); 

        public void OnPointerEnter(PointerEventData eventData) => Enter?.Invoke();
    }
}
