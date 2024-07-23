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

        public Resource CurrentResource => _currentResource;
        public Transform Transform => _transform;

        public Hand(Transform transform, DiContainer container)
        {
            Debug.Log("Hand Resolved");
            _transform = transform;
            _container = container;
        }

        public void Equip(Resource resource)
        {
            _currentResource = resource;
            if (_currentTool != null) 
                Object.Destroy(_currentTool.gameObject);
            _currentTool = createTool(resource);
        }

        public void EmptyHand() {
            _currentResource = null;
            _currentTool = null;
        }

        private Tool createTool(Resource resource)
        {
            Tool tool = _container.
                InstantiatePrefabForComponent<Tool>(resource.Tool, _transform, new Object[] {CurrentResource});
            return tool;
        }
    }
}
