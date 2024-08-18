using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using Assets.Scripts.Entities.Pathfinding;

namespace Assets.Scripts.Entities 
{
    public class EntitySpawner : ITickable
    {
        private Entity.Factory _EntityFactory;
        private IPathfindingStrategy _PathfindingStrategy;
        private int _spawns = 0;

        public EntitySpawner(IPathfindingStrategy pathfindingStrategy, Entity.Factory EntityFactory)
        {
            _EntityFactory = EntityFactory;
            _PathfindingStrategy = pathfindingStrategy;
        }

        public void Tick()
        {
            if(_spawns < 1){
                _EntityFactory.Create(_PathfindingStrategy);
                _spawns += 1;
            }     
        }
    }
}

