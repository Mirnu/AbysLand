using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Entities.Pathfinding
{
    public abstract class EntityPathfindingStrategy
    {
        public abstract List<Transform> GetPath(Transform target);
    }
}
