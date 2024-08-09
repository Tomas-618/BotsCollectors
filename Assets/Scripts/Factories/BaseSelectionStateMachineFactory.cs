using System;
using BasicStateMachine;

public class BaseSelectionStateMachineFactory
{
    private readonly RaycasterHitInfoProvider _hitInfoProvider;

    public BaseSelectionStateMachineFactory(RaycasterHitInfoProvider hitInfoProvider) =>
        _hitInfoProvider = hitInfoProvider ?? throw new ArgumentNullException(nameof(hitInfoProvider));

    public StateMachine<BaseSelectionState, BaseSelectionTransition> Create(SelectableBase @base)
    {
        BaseSelectionIdleState idleState = new BaseSelectionIdleState();
        BaseSelectionFlagPlacementState flagPlacementState = new BaseSelectionFlagPlacementState(@base);

        BaseSelectionFlagPlacementTransition flagPlacementTransition = new(flagPlacementState, _hitInfoProvider, @base);
        BaseSelectionIdleTransition idleTransition = new(idleState, _hitInfoProvider, @base);
        BaseSelectionFlagIdleTransition flagIdleTransition = new(idleState, _hitInfoProvider, @base);

        idleState.AddTransition(flagPlacementTransition);
        flagPlacementState.AddTransition(idleTransition);
        flagPlacementState.AddTransition(flagIdleTransition);

        return new StateMachine<BaseSelectionState, BaseSelectionTransition>(idleState);
    }
}
