using System;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Resources.Data;
using Assets.Scripts.World.Managers;
using UnityEngine;

namespace Assets.Scripts.World {
    public class WorldFacade
    {
        private IWorld _gen;
        private List<FirstTypeManager> _firstTypeManagers;
        private List<SecondTypeManager> _secondTypeManagers;

        public WorldFacade (IWorld gen, List<FirstTypeManager> firstTypeManagers, List<SecondTypeManager> secondTypeManagers) {
            _gen = gen;
            _firstTypeManagers = firstTypeManagers;
            _secondTypeManagers = secondTypeManagers;
        }

        public bool CanDamageAt(Vector2 pos) {
            return _firstTypeManagers.Any(x => x.ContainsPos(pos)) 
            || _secondTypeManagers.Any(x => x.transform.position == (Vector3)pos);
        }
        
        public void DamageAt(Vector2 pos) {
            if(_firstTypeManagers.Any(x => x.ContainsPos(pos))) {
                _firstTypeManagers.Find(x => x.ContainsPos(pos)).TryDestroyAtPos(Vector2Int.FloorToInt(pos), out InteractableGO gO);
            } else if(_secondTypeManagers.Any(x => x.transform.position == (Vector3)pos)) {
                _secondTypeManagers.Find(x => x.transform.position == (Vector3)pos).Interact();
            }
        }

        public void Place(Resource res) {
            
        }
    }
}