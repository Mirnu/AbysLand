using System;
using UnityEngine;

namespace Assets.Scripts.World.Blocks
{
    public class Block : MonoBehaviour
    {
        public event Action OnLeftClick;
        public event Action OnRightClick;
        public event Action OnMiddleClick;
        public event Action OnDestroyed;

        public void Destroy()
        {
            OnDestroyed?.Invoke();
        }
    }
}
