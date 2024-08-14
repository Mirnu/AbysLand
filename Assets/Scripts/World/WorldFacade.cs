using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Resources.Data;
using Assets.Scripts.World.Managers;
using UnityEngine;

namespace Assets.Scripts.World {
    public class WorldFacade : MonoBehaviour, IWorld
    {
        private UpperWorldGen _gen;

        public WorldFacade (UpperWorldGen gen) {
            _gen = gen;
        }

        public event Action<GenerateStage> GenerateStageChanged;
        public event Action GenerationCompleted;

        public bool CanDamageAt(Vector2 pos, float a) => _gen.CanDamageAt(pos, a);

        public void DamageAt(Vector2 pos, float a) => _gen.DamageAt(pos, a);

        public IEnumerator Generate(string seed) => _gen.Generate(seed);

        public void Place(Resource res) => _gen.Place(res);
    }
}