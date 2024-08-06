using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Entities 
{
    public class EntitySpawner : ITickable
    {
        private Entity.Factory _entityFactory;
        bool _isSpawned = false;

        public EntitySpawner(Entity.Factory EntityFactory)
        {
            this._entityFactory = EntityFactory;
        }

        public void Tick()
        {
            if(!_isSpawned){
                _entityFactory.Create();
                _isSpawned = true;
            }     
        }
    }
}

