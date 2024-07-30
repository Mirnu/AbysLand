namespace Assets.Scripts.World {
    public class WorldFacade {

        private IWorldGenerator _gen;
        private IWorldInteractor _interactor;

        public WorldFacade (IWorldGenerator gen, IWorldInteractor interactor) {
            _gen = gen;
            _interactor = interactor;
        }
        
    }

}