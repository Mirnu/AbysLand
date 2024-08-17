using Assets.Scripts.World;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Resources.Tools.ToolComponents
{
    public class BreakingTool : MonoTool
    {
        public IWorld _world;

        [Inject]
        public void Construct(IWorld world)
        {
            _world = world;
        }

        protected override void OnRaycast(RaycastHit hit)
        {
            
        }
    }
}
