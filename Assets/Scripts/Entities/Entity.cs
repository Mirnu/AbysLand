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
        protected EntityPathfindingStrategy _PathfindingStrategy;
        protected int _CurHP;

        public class Factory: PlaceholderFactory<EntityPathfindingStrategy, Entity>
        {

        }
    }
}
