using Assets.Scripts.Player;
using Assets.Scripts.Player.Hands;
using Assets.Scripts.Resources.Data;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Resources.Tools
{
    public class Tool : MonoBehaviour
    {
        protected Resource resource;
        protected PlayerFacade playerFacade;
        
        [Inject]
        public void Construct(Resource resource, PlayerFacade facade)
        {
            this.resource = resource;
            playerFacade = facade;
        }
    }
}
