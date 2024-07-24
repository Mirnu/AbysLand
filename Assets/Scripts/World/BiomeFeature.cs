using System;
using UnityEngine.Tilemaps;

namespace World {
    [Serializable]
    public class BiomeFeature {
        public TileBase Tile;
        public float SpawnChance;
        public float NeighborChance;
        public FeatureLayer Layer;
        public int MaxSpawnAmount;
        public int index = 0;
    }
}
public enum FeatureLayer {
    Ground = 0,
    Decor1 = 1,
    Decor2 = 2,
    Decor3 = 3,
    Decor4 = 4
}