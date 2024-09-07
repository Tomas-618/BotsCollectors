using BasicStateMachine;

public abstract class BaseTransition : Transition
{
    public BaseTransition(BaseState nextState) : base(nextState) { }
}
