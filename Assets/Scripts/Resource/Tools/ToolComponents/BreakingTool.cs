using Assets.Scripts.World;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Resources.Tools.ToolComponents
{
    public class BreakingTool : MonoBehaviour, IToolComponent
    {
        private Tool _tool;
        public IWorld _world;

        [Inject]
        public void Construct(IWorld world)
        {
            _world = world;
        }

        public void Init(Tool tool)
        {
            _tool = tool;
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
                    Vector3 mousePos3D = hit.point;
                    Vector2Int pos = new Vector2Int((int)mousePos3D.x, (int)mousePos3D.y);
                    _world.DamageAt(pos, 100);
                    Debug.Log(2);
                }

            }
        }
    }
}
