using Assets.Scripts.Misc.Utils;
using Assets.Scripts.Player.Stats;
using Assets.Scripts.Player.Stats.UI;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Player.Systems
{
    public class HealSystem : ITickable
    {
        private readonly PlayerStatsMaxModel _statsMax;
        private readonly PlayerStatsModel _stats;
        private readonly PlayerStatsRecoveryModel _recovery;
        private readonly Routine _routine;

        public HealSystem(PlayerStatsMaxModel statsMax, PlayerStatsModel stats,
            PlayerStatsRecoveryModel recovery, Routine routine) 
        { 
            _statsMax = statsMax;
            _stats = stats;
            _recovery = recovery;
            _routine = routine;
        }

        private Dictionary<string, HealData> _healMap = new();

        public void LockByName(string name)
        {
            if (!_healMap.ContainsKey(name)) return;
            _healMap[name].IsLocked = true;
        }

        public void UnLockByName(string name)
        {
            if (!_healMap.ContainsKey(name)) return;
            _healMap[name].IsLocked = false;
        }

        public bool IsHealedByName(string name) => _healMap.ContainsKey(name);

        public void HealByTime(string name, int health, float delta = 1)
        {
            _healMap[name] = new HealData
            {
                StartTime = Time.time,
                Delta = delta,
                Heal = health
            };

            Debug.Log("+");
            _recovery.HealthRecoveryPerSec += (float)health / delta;
        }

        public void StopHealByName(string name)
        {
            if (!_healMap.ContainsKey(name)) return;
             HealData heal = _healMap[name];
            _recovery.HealthRecoveryPerSec -= (float)heal.Heal / heal.Delta;
            _healMap.Remove(name);
        }

        public void Tick()
        {
            List<string> changed = new();
            foreach (var (name, healData) in _healMap)
            {
                if (Time.time >= healData.StartTime + healData.Delta &&
                    !healData.IsLocked)
                {
                    changed.Add(name);
                }
            }

            foreach (var name in changed)
            {
                HealData healData = _healMap[name];
                StopHealByName(name);
                HealByTime(name, healData.Heal, healData.Delta);
                _stats.Health += healData.Heal;
            }
        }
    }

    public class HealData
    {
        public float StartTime;
        public float Delta;
        public int Heal;
        public bool IsLocked;
    }
}
