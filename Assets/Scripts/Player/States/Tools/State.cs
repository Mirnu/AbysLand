namespace Player.States.Tools
{
    public class State
    {
        protected StateMachine _stateMachine;

        public State(StateMachine stateMachine)
        {
            _stateMachine = stateMachine;
        }

        public virtual bool Enter() => true;
        public virtual bool Exit() => true;
        public virtual void Update() { }
    }
}