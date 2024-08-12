using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.World.Managers {
    // 1 система блоков
    public class FirstTypeManager : IInitializable
    {
        private List<InteractableGO> _interactables = new List<InteractableGO>();

        public FirstTypeManager(List<InteractableGO> interactables) {
            _interactables = interactables;
        }

        public bool ContainsPos(Vector2 Pos) { return _interactables.Any(x => x.Pos == Pos); }

        public bool TryDestroyAtPos(Vector2Int Pos, out InteractableGO interactable) {
            interactable = _interactables.First(x => x.Pos == Pos);
            return _interactables.Any(x => x.Pos == Pos);
        }

        public void Initialize()
        {
            _interactables.ForEach(x => { 
                // Типа рофл плэйсхолдер пон да?
                x.Init(delegate { x.Go.transform.Rotate(0, 0, 25); }, delegate{});
            });
        }
    }

    [Serializable]
    public class InteractableGO {
        public Vector2Int Pos;
        public GameObject Go;
        public int Health;
        public int MaxHealth;

        public Action OnDamaged;
        public Action OnDestroyed;

        public void Init(Action onDamaged, Action onDestroyed) {
            Pos = new Vector2Int((int)Go.transform.position.x, (int)Go.transform.position.y);
            Health = MaxHealth;
            OnDamaged = onDamaged;
            OnDestroyed = onDestroyed;
        }

        public void Damage(int amount) {
            if(Health > amount) { OnDamaged.Invoke(); }
            else { OnDestroyed?.Invoke(); }
        }
    }
}