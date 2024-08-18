using Assets.Scripts.Entities.Impl;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEditorInternal;
using UnityEngine;
using Assets.Scripts.Entities.Pathfinding;
using System.Runtime.InteropServices.WindowsRuntime;

namespace Assets.Scripts.Entities
{
    public class ZombieSearchState : EntityState
    {
        public ZombieSearchState(ZombieStateMachine state_machine, Zombie entity, EntityStatsModel stats, IPathfindingStrategy strategy) : base(state_machine, entity, stats, strategy)
        {
            stateMachine = state_machine;
            entityModel = entity;
            entityStats = stats;
        }

        public override void Update()
        {
            // Ходим рандомно и ковыряемся в носу
        }
        public override bool Exit()
        {
            return true;
        }

        public override void Enter() { }
    }
}
