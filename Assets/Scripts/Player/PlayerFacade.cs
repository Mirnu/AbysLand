using Player.Model;
using UnityEngine;
using Zenject;

namespace Player
{
    public class PlayerFacade : MonoBehaviour
    {
        [Inject]
        public void Construct(PlayerModel model)
        {

        }
    }
}