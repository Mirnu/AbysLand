using Assets.Scripts.Misc.Utils;
using Assets.Scripts.World;
using System;
using Zenject;

namespace Assets.Scripts.Game
{
    public class WorldInitializer : IDisposable
    {
        private readonly IWorld _world;
        private readonly Routine _routine;

        private const string SEED = "seed";

        public event Action Initilized;

        public WorldInitializer(IWorld world, Routine routine) 
        {
            _world = world;
            _routine = routine; 
        }

        public void Initialize()
        {
            _world.GenerationCompleted += OnGenerationCompleted;
            _routine.StartCoroutine(_world.Generate(SEED));
        }

        public void Dispose()
        {
            _world.GenerationCompleted -= OnGenerationCompleted;
        }

        private void OnGenerationCompleted()
        {
            Initilized?.Invoke();
        }
    }
}
