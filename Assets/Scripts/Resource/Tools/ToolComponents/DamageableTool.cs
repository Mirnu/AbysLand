using Assets.Scripts.Entities.Impl;
using UnityEngine;

namespace Assets.Scripts.Resources.Tools.ToolComponents
{
    public class DamageableTool : MonoTool
    {
        protected override void OnRaycast(RaycastHit hit)
        {
            if (hit.collider.TryGetComponent(out Zombie zombie))
            {
                Debug.Log("zombie");
            }
        }
    }
}
