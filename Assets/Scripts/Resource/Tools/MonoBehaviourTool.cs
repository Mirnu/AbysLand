using Assets.Scripts.Resources.Tools;
using UnityEngine;

namespace Assets.Scripts.Resources
{
    public class MonoBehaviourTool : MonoBehaviour
    {
        protected Tool tool;

        public void Init(Tool tool)
        {
            this.tool = tool;
        }
    }
}
