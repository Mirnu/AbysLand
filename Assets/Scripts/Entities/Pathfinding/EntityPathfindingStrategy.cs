using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Entities.Pathfinding
{
    public class EntityPathfindingStrategy: MonoBehaviour
    {
        public virtual List<Transform> GetPath(Transform target)
        {
            return null;
        }
    }
}
