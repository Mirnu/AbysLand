using System;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Misc.Saving;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.Scripting;
using UnityEngine.Tilemaps;
using Zenject;

namespace Assets.Scripts.World
{
    public class WorldSaver {

        // не работает тварина

        private JsonSerializerSettings _settings;

        private List<JTile> _tiles = new List<JTile>();
        //БОЛЬШОЙ КОСТЫЛЬ, НЕ НАШЕЛ КАК СОХРАНИТЬ Tile или TileBase В JSON
        private List<TileBase> _allTiles = new List<TileBase>();
        private Tilemap _;

        public WorldSaver(Tilemap tmp, List<TileBase> list) {
            // Костыль
            _allTiles = list;
            _ = tmp;
            _settings = new JsonSerializerSettings() { 
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                Formatting = Formatting.Indented
            };
        }

        public void SaveMap(Tilemap tmp) {
            Debug.Log("Saving map at: " + Application.persistentDataPath
              + "/MySaveData.dat");
            Repository.SetData(convert(tmp), _settings);
            Repository.SaveState();
        }

        public void LoadMap(Tilemap tmp) {
            Debug.Log("-----Loading map-----");
            Repository.LoadState();
            var l = Repository.GetData<JTile[]>(_settings);
            l.ToList().ForEach(x => {
                tmp.SetTile(x.Pos + new Vector3Int(10, 10, 0), _allTiles[_allTiles.Count > x.TileIndex ? x.TileIndex : 0]);
            });
        }

        private JTile[] convert(Tilemap tmp) {
            _tiles.Clear();
            tmp.CompressBounds();
            for (int i = tmp.cellBounds.xMin; i < tmp.cellBounds.xMax; i++) {
                for (int j = tmp.cellBounds.yMin; j < tmp.cellBounds.yMax; j++) {
                    if(tmp.GetTile(new Vector3Int(i, j)) != null) {
                        var t = tmp.GetTile(new Vector3Int(i, j));
                        if (!_allTiles.Contains(t)) { _allTiles.Add(t); }
                        _tiles.Add(new JTile(_allTiles.IndexOf(t), new Vector3Int(i, j)));
                    }
                }
            }
            return _tiles.ToArray();
        }
    }

    [Serializable][JsonObject(MemberSerialization.OptIn)]
    public class JTile {
        [JsonProperty] public int TileIndex;
        [JsonProperty] public Vector3Int Pos;
        public JTile(int tileIndex, Vector3Int _pos) { TileIndex = tileIndex; Pos = _pos; }
    }
}