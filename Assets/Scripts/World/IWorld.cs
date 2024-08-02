using System;
using System.Collections;

namespace Assets.Scripts.World {
    public interface IWorld {
        public IEnumerator Generate(string seed);
        public event Action<GenerateStage> GenerateStageChanged;
        public event Action GenerationCompleted;
    }
}