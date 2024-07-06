using BasicStateMachine;

public abstract class BaseSelectionTransition : Transition<BaseSelectionState, BaseSelectionTransition>
{
    protected BaseSelectionTransition(BaseSelectionState nextState) : base(nextState) { }
}
