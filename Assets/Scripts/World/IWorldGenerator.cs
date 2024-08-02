using System.Collections;

namespace Assets.Scripts.World {
    public interface IWorldGenerator {
        public IEnumerator Generate(string seed);
    }
}