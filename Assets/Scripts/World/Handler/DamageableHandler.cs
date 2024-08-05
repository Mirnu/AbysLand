using System;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Resources.Data;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.Tilemaps;
using Zenject;

namespace Assets.Scripts.World {
    [Serializable][JsonObject(MemberSerialization.OptIn)]
    public class DamageableHandler : IInitializable, IWorldInteractor {

        [JsonProperty] public List<DmgTile> _damagableTiles = new List<DmgTile>();
        private Tilemap _tilemap;

        public DamageableHandler(Tilemap tilemap, List<DmgTile> _dict) {
            _damagableTiles = _dict;
            _tilemap = tilemap;
        }

        public void Initialize()
        {

            _damagableTiles.ForEach(x => {
                x.Init();
                _tilemap.SetTile(x.Pos, x._default);
                x.onDestroyed += delegate { DestroyTile(x); };
            });
        }

        public void DestroyTile(DmgTile tile) {
            _tilemap.SetTile(tile.Pos, tile._dead);
            _damagableTiles.Remove(tile);
        }

        public bool CanDamage(Vector3Int pos) { return _damagableTiles.Any(x => x.Pos == pos); }

        public void Place(Vector3Int pos, DmgTile tile) {  tile.Pos = pos; _damagableTiles.Add(tile); _tilemap.SetTile(tile.Pos, tile._default); }

        public void Damage(Vector3Int pos, int amount) {
            var tile = _damagableTiles.Find(x => x.Pos == pos);
            tile.Damage(amount);
        }

        public TileBase GetObjects(Vector2 pos)
        {
            return _tilemap.GetTile(new Vector3Int((int)Mathf.Floor(pos.x), (int)Mathf.Floor(pos.y)));
        }

        public void Put(Resource resource)
        {
            
        }
    }
}