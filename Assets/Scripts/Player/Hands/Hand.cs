using Assets.Scripts.Resources.Data;
using Assets.Scripts.Resources.Tools;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Player.Hands
{
    public class Hand
    {
        private Transform _transform;
        private DiContainer _container;

        private Resource _currentResource;
        private Tool _currentTool;
        private Resource _baseResource;

        public Resource CurrentResource => _currentResource;
        public Transform Transform => _transform;

        public Hand(Transform transform, DiContainer container, Resource starter)
        {
            _transform = transform;
            _container = container;
            _baseResource = starter;
        }

        public void Equip(Resource resource)
        {
            _currentResource = resource;
            if (_currentTool != null) 
                Object.Destroy(_currentTool.gameObject);
            _currentTool = createTool(resource);
        }

        public void EmptyHand() => Equip(_baseResource);

        private Tool createTool(Resource resource)
        {
            Tool tool = _container.
                InstantiatePrefabForComponent<Tool>(resource.Tool, _transform, new Object[] {CurrentResource});
            return tool;
        }
    }
}
