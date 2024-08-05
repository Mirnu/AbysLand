namespace Assets.Scripts.World {
    public class WorldFacade
    {
        private IWorld _gen;
        private IWorldInteractor _interactor;

        public WorldFacade (IWorld gen, IWorldInteractor interactor) {
            _gen = gen;
            _interactor = interactor;
        }
        
    }
}