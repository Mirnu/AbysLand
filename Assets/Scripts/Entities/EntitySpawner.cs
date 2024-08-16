using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using Assets.Scripts.Entities.Pathfinding;

namespace Assets.Scripts.Entities 
{
    public class EntitySpawner : ITickable
    {
        private Entity.Factory _entityFactory;
        private EntityPathfindingStrategy _pathfindingStrategy;
        bool _isSpawned = false;

        public EntitySpawner(EntityPathfindingStrategy pathfindingStrategy, Entity.Factory EntityFactory)
        {
            this._entityFactory = EntityFactory;
            this._pathfindingStrategy = pathfindingStrategy;
        }

        public void Tick()
        {
            if(!_isSpawned){
                _entityFactory.Create(_pathfindingStrategy);
                _isSpawned = true;
            }     
        }
    }
}

