using Assets.Scripts.Player;
using Assets.Scripts.Resources.Data;
using System;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Resources.Tools
{
    public class Tool : MonoBehaviour
    {
        private Resource _resource;

        protected PlayerFacade playerFacade;
        
        [Inject]
        public void Construct(Resource resource, PlayerFacade facade)
        {
            _resource = resource;
            playerFacade = facade;
        }

        private void Awake()
        {
            IToolComponent[] components = GetComponents<IToolComponent>(); 

            foreach (var component in components)
            {
                component.Init(this);
            }
        }

        public bool TryGetResource<T>(out T resource) where T : Resource
        {
            if (_resource is T)
            {
                resource = (T)_resource;
                return true;
            }

            resource = default;
            return false;
        }

        public T GetResource<T>() where T : Resource
        {
            if (_resource is T)
                return (T)_resource;

            throw new Exception("Resource not found");
        }
    }
}
