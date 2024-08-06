using System;
using UnityEngine.Tilemaps;

namespace Assets.Scripts.World.Biomes {
    [Serializable]
    public class BiomeFeature {
        public float SpawnChance;
        public float NeighborChance;
        public FeatureLayer Layer;
        public TileBase FeatureTile;
    }
}
public enum FeatureLayer {
    Decor1 = 0,
    Decor2 = 1,
    Decor3 = 2,
    Decor4 = 3
}