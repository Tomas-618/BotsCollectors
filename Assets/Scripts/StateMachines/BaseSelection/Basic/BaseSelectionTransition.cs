using BasicStateMachine;

public abstract class BaseSelectionTransition : Transition
{
    protected BaseSelectionTransition(BaseSelectionState nextState) : base(nextState) { }
}
