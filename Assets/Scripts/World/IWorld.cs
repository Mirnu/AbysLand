using System;
using System.Collections;
using Assets.Scripts.Resources.Data;
using UnityEngine;

namespace Assets.Scripts.World {
    public interface IWorld {
        public IEnumerator Generate(string seed);
        public event Action<GenerateStage> GenerateStageChanged;
        public event Action GenerationCompleted;
        public bool CanDamageAt(Vector2 pos);
        public void DamageAt(Vector2 pos);
        public void Place(Resource res);
    }
}