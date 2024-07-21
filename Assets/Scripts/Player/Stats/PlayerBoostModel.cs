namespace Assets.Scripts.Player.Stats
{
    public class PlayerBoostModel
    {
        private float _speedBoost = 1;

        public float SpeedBoost { 
            get { return _speedBoost; } 
            set { _speedBoost = value; } 
        }
    }
}
