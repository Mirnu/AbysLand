using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Menu.Common
{
    [RequireComponent(typeof(Button))]
    public class ClickableButton : MonoBehaviour
    {
        protected Button button;

        private void Awake()
        {
            button = GetComponent<Button>();
            button.onClick.AddListener(OnClick);
        }

        protected virtual void OnClick()
        {
          
        }
    }
}
