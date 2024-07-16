namespace Player.States.Tools
{
    public abstract class StateMachine
    {
        //public virtual IHandler Handler { get; private set; }

        public virtual bool ChangeState(State newState) => true;
        public virtual void Initialize(State startState) { }
    }
}
