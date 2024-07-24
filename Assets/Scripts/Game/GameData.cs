namespace Assets.Scripts.Game
{
    public enum Complexity
    { 
        Normal,
        Hard
    }

    public class GameData
    {
        private Complexity _complexity;
        public Complexity Complexity { 
            get { return _complexity; } 
            set { _complexity = value; }
        }
    }
}
