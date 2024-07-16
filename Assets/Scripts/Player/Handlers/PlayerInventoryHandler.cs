using Player.Model;
using Unity.VisualScripting;

namespace Player.Handlers {
    public class PlayerInventoryHandler : IInitializable
    {
        private PlayerModel _model;
        private IInventory _inventory;

        public PlayerInventoryHandler(PlayerModel model, IInventory inventory) {
            _model = model;
            _inventory = inventory;
        }

        public void Initialize()
        {
            //TODO: подгружать из сохранения
            _inventory.Clear();
        }
        //Тут типа взаимодействие с ui пон да?
    }
}