using Assets.Scripts.Entities.Impl;
using Assets.Scripts.Resources.Tools.Complementary;
using UnityEngine;

namespace Assets.Scripts.Resources.Tools.ToolComponents
{
    [RequireComponent(typeof(RaycastableTool))]
    public class DamageableTool : MonoBehaviourTool, IRaycastable
    {
        public void OnRaycast(RaycastHit hit)
        {
            if (hit.collider.TryGetComponent(out Zombie zombie))
            {
                Debug.Log("zombie");
            }
        }
    }
}
