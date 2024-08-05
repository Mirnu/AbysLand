using Assets.Scripts.Game;
using Assets.Scripts.Menu.Common;
using Assets.Scripts.Misc.Constants;
using UnityEngine.SceneManagement;
using Zenject;

namespace Assets.Scripts.Menu
{
    public class NewGameButton : ClickableButton
    {
        private GameManager _gameManager;

        [Inject]
        private void Construct(GameManager gameManager)
        {
            _gameManager = gameManager;
        }

        protected override void OnClick()
        {
            _gameManager.StartLoadingGameplay();
        }
    }
}
