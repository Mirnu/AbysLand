using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Resources.Data;
using Assets.Scripts.World.Generators.GenerationStages;
using Assets.Scripts.World.Internal;
using Assets.Scripts.World.Managers;
using UnityEngine;
using UnityEngine.Tilemaps;
using Zenject;
using Random = UnityEngine.Random;

namespace Assets.Scripts.World {
    public class UpperWorldGen : MonoBehaviour, IWorld {
        
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

        [Inject]
        public void Construct(WorldModel model, List<IGenerator> generators, List<FirstTypeManager> firstTypeManagers, List<SecondTypeManager> secondTypeManagers) {
            map = model.Map;
            _size = model.Size;
            _durability = model.Durability;
            _firstTypeManagers = firstTypeManagers;
            _secondTypeManagers = secondTypeManagers;

            AddGenerators(generators);
        }

        public bool CanDamageAt(Vector2 pos) {
            return _firstTypeManagers.Any(x => x.ContainsPos(pos)) 
            || _secondTypeManagers.Any(x => x.transform.position == (Vector3)pos);
        }
        
        public void DamageAt(Vector2 pos) {
            if(_firstTypeManagers.Any(x => x.ContainsPos(pos))) {
                _firstTypeManagers.Find(x => x.ContainsPos(pos)).TryDestroyAtPos(Vector2Int.FloorToInt(pos), out InteractableGO gO);
            } else if(_secondTypeManagers.Any(x => x.transform.position == (Vector3)pos)) {
                _secondTypeManagers.Find(x => x.transform.position == (Vector3)pos).Interact();
            }
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