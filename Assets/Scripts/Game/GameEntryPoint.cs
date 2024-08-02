using Assets.Scripts.Player;
using Zenject;

namespace Assets.Scripts.Game
{
    public class GameEntryPoint : IInitializable
    {
        private readonly PlayerFacade _player;
        private readonly WorldInitializer _worldInitializer;

        public GameEntryPoint(PlayerFacade player, WorldInitializer worldInitializer)
        {
            _player = player;
            _worldInitializer = worldInitializer;
        }

        public void Initialize()
        {
            _worldInitializer.Initilized += OnWorldInitilized;
            _worldInitializer.Initialize();
        }

        private void OnWorldInitilized()
        {
            //_player.Initialize();
        }
    }
}
