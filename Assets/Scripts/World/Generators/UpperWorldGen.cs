using System;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.World.Generators.GenerationStages;
using Assets.Scripts.World.Internal;
using UnityEngine;
using UnityEngine.Tilemaps;
using Zenject;
using Random = UnityEngine.Random;

namespace Assets.Scripts.World {
    public class UpperWorldGen : MonoBehaviour, IWorldGenerator {
        

        public float scale = 1.0F;
        private string seed = "";
        private int _size;
        private int[,] map;
        //Заглушка
        private int[,] _durability;
        private WorldSaver _saver;
        private Tilemap _;

        private Queue<IGenerator> _sequentialGeneration = new();
        public Action<string> GenerateStageChanged;

        [Inject]
        public void Construct(WorldModel model, CornersGenerator cornersGen,
            ArrangingBaseTilesGenerator arrangingTilesGen, ArrangingBiomesGenerator biomeGen) {
            map = model.Map;
            _size = model.Size;
            _durability = model.Durability;
            _saver = model.Saver;
            _ = model.BackgroundTiles;

            _sequentialGeneration.Enqueue(cornersGen);
            _sequentialGeneration.Enqueue(arrangingTilesGen);
            _sequentialGeneration.Enqueue(biomeGen);
        }

        private void Start() => Initialize();

        public void Initialize() {

            StartCoroutine(Generate("test"));
        }

        public IEnumerator Generate(string seed)
        {
            this.seed = seed;
            Random.InitState(seed.GetHashCode());

            foreach (var generator in _sequentialGeneration)
            {
                GenerateStageChanged?.Invoke(generator.NameGeneration);
                Debug.Log(generator.NameGeneration);
                yield return generator.Generate();
            }

            _saver.SaveMap(_);
        }
    }
}