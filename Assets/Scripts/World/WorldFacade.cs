using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Resources.Data;
using Assets.Scripts.World.Managers;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.World {
    public class WorldFacade : MonoBehaviour, IWorld
    {
        private UpperWorldGen _gen;

        [Inject]
        public void Construct(UpperWorldGen gen) {
            Debug.Log("Construct WorldFacade");
            _gen = gen;
        }

        public event Action<GenerateStage> GenerateStageChanged;
        public event Action GenerationCompleted;

        private void Start()
        {
            _gen.GenerateStageChanged += OnGenerateStageChanged;
            _gen.GenerationCompleted += OnGenerationCompleted;
        }

        private void OnDestroy()
        {
            _gen.GenerateStageChanged -= OnGenerateStageChanged;
            _gen.GenerationCompleted -= OnGenerationCompleted;
        }

        public void OnGenerationCompleted()
        {
            GenerationCompleted?.Invoke();
        }

        public void OnGenerateStageChanged(GenerateStage stage)
        {
            GenerateStageChanged?.Invoke(stage);
        }

        public IEnumerator Generate(string seed) => _gen.Generate(seed);

        public void Place(Resource res) => _gen.Place(res);
    }
}