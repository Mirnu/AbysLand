using Assets.Scripts.Misc.Utils;
using System;
using System.Collections;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Game.Systems
{
    public class TimeController : IInitializable
    {
        private readonly Routine _routine;

        public TimeController(Routine routine)
        {
            _routine = routine;
        }

        private WaitForSeconds _secWait = new(1);

        private uint _seconds = 0;
        private uint _minutes = 0;
        private uint _hours = 0;
        private uint _days = 0;

        public event Action TimeChanged;

        public uint Seconds
        {
            get => _seconds;
            private set
            {
                _seconds = value;
                if (Seconds == 60)
                {
                    Minutes++;
                    _seconds = 0;
                }
                TimeChanged?.Invoke();
            }
        }

       public uint Minutes
        {
            get => _minutes;
            private set
            {
                _minutes = value;
                if (Minutes == 60)
                {
                    Hours++;
                    _minutes = 0;
                }
            }
        }

        public uint Hours
        {
            get => _hours;
            private set
            {
                _hours = value;
                if (Hours == 24)
                {
                    Days++;
                    _hours = 0;
                }
            }
        }

        public uint Days
        {
            get => _days;
            private set
            {
                _days = value;
            }
        }

        public void Initialize()
        {
            _routine.StartCoroutine(Tick());
        }

        private IEnumerator Tick()
        {
            while (true)
            {
                yield return _secWait;
                Seconds++;
            }
        }
    }
}
