using Assets.Scripts.Player.Model;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Player
{
    public class PlayerFacade : MonoBehaviour
    {
        [Inject]
        public void Construct(PlayerModel model)
        {

        }
    }
}