using UnityEngine;
using UnityEngine.Rendering;
using Zenject;
using Assets.Scripts.Entities.Pathfinding;

namespace Assets.Scripts.Entities
{
    public abstract class Entity :  MonoBehaviour
    {
        protected EntityStatsModel statsModel;
        protected EntityStateMachine stateMachine;
        protected IPathfindingStrategy pathfindingStrategy;
        public GameObject CurrentTarget { get; protected set; }
        protected int curHP;

        public class Factory: PlaceholderFactory<IPathfindingStrategy, Entity>
        {

        }
    }
}
