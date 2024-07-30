using Assets.Scripts.Resources.Data;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Assets.Scripts.World {
    public interface IWorldGenerator {
        public void Generate(string seed);
    }
}