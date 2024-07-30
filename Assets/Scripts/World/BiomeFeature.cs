using System;

namespace Assets.Scripts.World {
    [Serializable]
    public class BiomeFeature {
        public float SpawnChance;
        public float NeighborChance;
        public FeatureLayer Layer;
        public int MaxSpawnAmount;
        public int index = 0;
    }
}
public enum FeatureLayer {
    Decor1 = 0,
    Decor2 = 1,
    Decor3 = 2,
    Decor4 = 3
}