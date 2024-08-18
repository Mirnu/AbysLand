using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Misc.Utils;
using Assets.Scripts.Resources.Data;
using Assets.Scripts.World.Generators.GenerationStages;
using Assets.Scripts.World.Internal;
using Assets.Scripts.World.Managers;
using UnityEngine;
using UnityEngine.Tilemaps;
using Zenject;
using Random = UnityEngine.Random;

namespace Assets.Scripts.World {
    public class UpperWorldGen {
        
        public float scale = 1.0F;
        private string seed = "";
        private int _size;
        private int[,] map;
        //Заглушка
        private int[,] _durability;

        private List<FirstTypeManager> _firstTypeManagers;
        private List<SecondTypeManager> _secondTypeManagers;

        private Dictionary<IGenerator, GenerateStage> _sequentialGeneration = new();
        public event Action<GenerateStage> GenerateStageChanged;
        public event Action GenerationCompleted;

        private Routine _routine;

        public UpperWorldGen(Routine routine, WorldModel model, List<IGenerator> generators, 
            List<FirstTypeManager> firstTypeManagers, List<SecondTypeManager> secondTypeManagers) {
            map = model.Map;
            _size = model.Size;
            _durability = model.Durability;
            _firstTypeManagers = firstTypeManagers;
            _secondTypeManagers = secondTypeManagers;

            _routine = routine;
            AddGenerators(generators);
        }

        public void Place(Resource res) {
            
        }

        private void AddGenerators(List<IGenerator> generators)
        {
            int allCost = generators.Sum(x => x.CostGeneration);
            _sequentialGeneration = generators.OrderBy((x) => x.Order)
                .ToDictionary(x => x, x => new GenerateStage {
                    NameGeneration = x.NameGeneration, 
                    Cost = (float)x.CostGeneration / allCost
                });
        }

        public IEnumerator Generate(string seed)
        {
            Debug.Log(1);
            this.seed = seed;
            Random.InitState(seed.GetHashCode());

            foreach (var generator in _sequentialGeneration)
            {
                Debug.Log(generator.Key.NameGeneration);
                GenerateStageChanged?.Invoke(generator.Value);
                yield return generator.Key.Generate();
            }

            GenerationCompleted?.Invoke();
        }
    }

    public struct GenerateStage 
    {
        public string NameGeneration;
        public float Cost;
    }
}