using UnityEngine;
using UnityEngine.Rendering;
using Zenject;

namespace Assets.Scripts.Entities
{
    public abstract class Entity :  MonoBehaviour
    {
        [SerializeField]
        protected EntityStatsModel _StatsModel;
        protected EntityStateMachine _StateMachine;
    }
}
