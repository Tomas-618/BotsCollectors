using BasicStateMachine;

public abstract class BaseTransition : Transition<BaseState, BaseTransition>
{
    public BaseTransition(BaseState nextState) : base(nextState) { }
}
