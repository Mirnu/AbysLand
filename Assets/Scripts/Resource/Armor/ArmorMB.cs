using Assets.Scripts.Player;
using Assets.Scripts.Resources.Data;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Resources.Armors
{
    public class ArmorMB : MonoBehaviour
    {
        protected ArmorResource resource;
        protected PlayerFacade playerFacade;
        
        [Inject]
        public void Construct(ArmorResource resource, PlayerFacade facade)
        {
            this.resource = resource;
            playerFacade = facade;
        }
    }
}