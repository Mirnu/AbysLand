using System;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.World.Generators.GenerationStages;
using Assets.Scripts.World.Internal;
using UnityEngine;
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
        private List<int[,]> decorMaps;

        private Queue<IGenerator> _sequentialGeneration = new();
        public Action<string> GenerateStageChanged;

        [Inject]
        public void Construct(WorldModel model, CornersGenerator cornersGen,
            ArrangingTilesGenerator arrangingTilesGen) {
            map = model.Map;
            _size = model.Size;
            _durability = model.Durability;
            decorMaps = model.DecorMaps;

            _sequentialGeneration.Enqueue(cornersGen);
            _sequentialGeneration.Enqueue(arrangingTilesGen);
        }

        private void Start() => Initialize();

        public void Initialize() {
            for(int i = 0; i < 4; i++) { 
                var l = new int[_size, _size];
                for (int k = 0; k < l.GetLength(0); k++) {
                    for (int j = 0; j < l.GetLength(1); j++) {
                        l[k, j] = -1;
                    }
                }
                decorMaps.Add(l);
            }

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
        }
    }
}