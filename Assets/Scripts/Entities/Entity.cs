using UnityEngine;
using UnityEngine.Rendering;
using Zenject;

namespace Assets.Scripts.Entities
{
    public abstract class Entity :  MonoBehaviour
    {
        protected EntityStatsModel _StatsModel;
        protected EntityStateMachine _StateMachine;
        protected int _CurHP;

        public class Factory: PlaceholderFactory<Entity>
        {

        }
    }
}
