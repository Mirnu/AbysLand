using Assets.Scripts.World.Blocks;
using System;
using System.Collections.Generic;
using Object = UnityEngine.Object;
using Zenject;
using Assets.Scripts.Resources.Data;

namespace Assets.Scripts.World.Managers {
    // 1 система блоков
    public class FirstTypeManager : IInitializable
    {
        private List<InteractableGO> _interactables = new List<InteractableGO>();

        public FirstTypeManager(List<InteractableGO> interactables) {
            _interactables = interactables;
        }

        public void Place(Resource resource)
        {

        }

        public void Initialize()
        {
            _interactables.ForEach(x => { 
                // Типа рофл плэйсхолдер пон да?
                x.Init(delegate { x.Go.transform.Rotate(0, 0, 25); }, 
                    delegate{ Object.Destroy(x.Go.gameObject); });
            });
        }
    }

    [Serializable]
    public class InteractableGO {
        public Block Go;
        public int Health;
        public int MaxHealth;

        public Action OnDamaged;
        public Action OnDestroyed;

        public void Init(Action onDamaged, Action onDestroyed) {
            Health = MaxHealth;
            Go.OnLeftClick += onDamaged;
            Go.OnDestroyed += onDestroyed;
        }

        public void Damage(int amount) {
            if(Health > amount) { OnDamaged?.Invoke(); }
            else { OnDestroyed?.Invoke(); }
        }
    }
}