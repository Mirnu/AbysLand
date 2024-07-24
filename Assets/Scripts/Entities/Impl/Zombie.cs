using Assets.Scripts.Player;
using UnityEngine;

namespace Assets.Scripts.Entities.Impl
{
    public class Zombie : Entity
    {
        private int _damage = 10;

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.TryGetComponent(out PlayerFacade player))
            {
                player.TakeDamage(_damage);
            }
        }
    }
}
