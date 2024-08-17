using Assets.Scripts.Resources.Tools;
using UnityEngine;

namespace Assets.Scripts.Resources
{
    public class MonoTool : MonoBehaviour
    {
        protected Tool tool;

        public void Init(Tool tool)
        {
            this.tool = tool;
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
                    OnRaycast(hit);
                }
            }
        }

        protected virtual void OnRaycast(RaycastHit hit) { }
    }
}
