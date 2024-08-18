using UnityEngine;
using UnityEngine.Rendering;
using Zenject;
using Assets.Scripts.Entities.Pathfinding;

namespace Assets.Scripts.Entities
{
    public abstract class Entity :  MonoBehaviour
    {
        protected EntityStatsModel _StatsModel;
        protected EntityStateMachine _StateMachine;
        protected IPathfindingStrategy _PathfindingStrategy;
        public GameObject CurrentTarget { get; protected set; }
        protected int _CurHP;

        public class Factory: PlaceholderFactory<IPathfindingStrategy, Entity>
        {

        }
    }
}
