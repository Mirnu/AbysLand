using Assets.Scripts.Resources.Tools.Complementary;
using Assets.Scripts.World;
using Assets.Scripts.World.Blocks;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Resources.Tools.ToolComponents
{
    [RequireComponent(typeof(RaycastableTool))]
    public class BreakingTool : MonoBehaviourTool, IRaycastable
    {
        public IWorld _world;

        [Inject]
        public void Construct(IWorld world)
        {
            _world = world;
        }

        public void OnRaycast(RaycastHit hit)
        {
            if (hit.collider.TryGetComponent(out Block block))
            {
                block.Destroy();
            }
        }
    }
}
