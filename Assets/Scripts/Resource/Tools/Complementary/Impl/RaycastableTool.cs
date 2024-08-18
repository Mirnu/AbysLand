using UnityEngine;

namespace Assets.Scripts.Resources.Tools.Complementary
{
    public class RaycastableTool : MonoBehaviour
    {
        private IRaycastable[] _raycastables;

        private void Awake()
        {
            _raycastables = GetComponents<IRaycastable>();
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                Vector2 mousePos2D = Camera.main.ScreenPointToRay(Input.mousePosition).origin;
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit))
                {
                    foreach (var raycastable in _raycastables)
                    {
                        raycastable.OnRaycast(hit);
                    }
                }
            }
        }
    }
}
