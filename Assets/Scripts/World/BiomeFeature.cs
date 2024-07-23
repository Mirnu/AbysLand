using UnityEngine.Tilemaps;

namespace World {
    public class BiomeFeature {
        public TileBase Tile;
        public float SpawnChance;
        public float NeighborChance;
        public FeatureLayer Layer;
        public int MaxSpawnAmount;

        public BiomeFeature(TileBase tile, float spawnChance, float neighborChance, int maxSpawn, FeatureLayer layer) {
            Tile = tile;
            SpawnChance = spawnChance;
            NeighborChance = neighborChance;
            Layer = layer;
            MaxSpawnAmount = maxSpawn;
        }
    }
}
public enum FeatureLayer {
    Ground = 0,
    Decor1 = -1,
    Decor2 = -2,
    Decor3 = -3,
    Decor4 = -4
}