using System;
using BasicStateMachine;

public class BaseSelectionStateMachineFactory
{
    private readonly SelectableBase _base;
    private readonly RaycasterHitInfoProvider _hitInfoProvider;

    public BaseSelectionStateMachineFactory(SelectableBase @base, RaycasterHitInfoProvider hitInfoProvider)
    {
        _base = @base != null ? @base : throw new ArgumentNullException(nameof(@base));
        _hitInfoProvider = hitInfoProvider ?? throw new ArgumentNullException(nameof(hitInfoProvider));
    }

    public StateMachine<BaseSelectionState, BaseSelectionTransition> Create()
    {
        BaseSelectionIdleState idleState = new BaseSelectionIdleState();
        BaseSelectionFlagPlacementState flagPlacementState = new BaseSelectionFlagPlacementState(_base);

        BaseSelectionFlagPlacementTransition flagPlacementTransition = new(flagPlacementState, _hitInfoProvider, _base);
        BaseSelectionIdleTransition idleTransition = new(idleState, _hitInfoProvider, _base);
        BaseSelectionFlagIdleTransition flagIdleTransition = new(idleState, _hitInfoProvider, _base);

        idleState.AddTransition(flagPlacementTransition);
        flagPlacementState.AddTransition(idleTransition);
        flagPlacementState.AddTransition(flagIdleTransition);

        return new StateMachine<BaseSelectionState, BaseSelectionTransition>(idleState);
    }
}
