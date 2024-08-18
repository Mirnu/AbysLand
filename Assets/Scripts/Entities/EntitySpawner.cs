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
        int spawns = 0;

        public EntitySpawner(IPathfindingStrategy pathfindingStrategy, Entity.Factory EntityFactory)
        {
            this._EntityFactory = EntityFactory;
            this._PathfindingStrategy = pathfindingStrategy;
        }

        public void Tick()
        {
            if(spawns < 1){
                _EntityFactory.Create(_PathfindingStrategy);
                spawns += 1;
            }     
        }
    }
}

