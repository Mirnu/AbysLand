using Assets.Scripts.Resources.Tools;
using Assets.Scripts.World;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Resources.Tools.Impl
{
    public class Fist : Tool
    {
        private IWorldInteractor _world;

        [Inject]
        public void Construct()
        {

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
                    _world.Damage((Vector3Int)pos, 100);
                }

            }
        }
    }
}
