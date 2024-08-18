using Assets.Scripts.Entities.Pathfinding;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using Zenject;
using static UnityEngine.GraphicsBuffer;

namespace Assets.Scripts.Entities.Pathfinding
{
    public class AStarPathfindingStrategy : IPathfindingStrategy
    {
        public Vector3 _PreviousPoint { get; set; }

        public void MoveTo(Transform target, GameObject self)
        {
            self.TryGetComponent<NavMeshAgent>(out NavMeshAgent agent);
            _PreviousPoint = target.transform.position; 
            agent.SetDestination(new Vector3(target.transform.position.x, target.transform.position.y));
        }

        public void MoveToPreviousPoint(GameObject self)
        {
            self.TryGetComponent<NavMeshAgent>(out NavMeshAgent agent);
            agent.SetDestination(new Vector3(_PreviousPoint.x, _PreviousPoint.y));
        }
    }
}
