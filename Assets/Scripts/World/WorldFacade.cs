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
            //GenerateStageChanged += delegate { _gen.GenerateStageChanged };

        }

        public event Action<GenerateStage> GenerateStageChanged;
        public event Action GenerationCompleted;

        public bool CanDamageAt(Vector2 pos) => _gen.CanDamageAt(pos);

        public void DamageAt(Vector2 pos) => _gen.DamageAt(pos);

        public IEnumerator Generate(string seed) => _gen.Generate(seed);

        public void Place(Resource res) => _gen.Place(res);
    }
}