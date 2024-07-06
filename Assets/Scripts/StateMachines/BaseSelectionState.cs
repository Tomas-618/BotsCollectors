using System;
using BasicStateMachine;

public class BaseSelectionStateMachineFactory
{
    private readonly RaycasterHitInfoProvider _hitInfoProvider;

    public BaseSelectionStateMachineFactory(RaycasterHitInfoProvider hitInfoProvider) =>
        _hitInfoProvider = hitInfoProvider ?? throw new ArgumentNullException(nameof(hitInfoProvider));

    //public StateMachine<BaseSelectionState, BaseSelectionTransition> Create(Vector3 position)
    //{

    //    Create();
    //    return ;
    //}

    public StateMachine<BaseSelectionState, BaseSelectionTransition> Create(SelectableBase @base)
    {
        BaseSelectionIdleState idleState = new BaseSelectionIdleState();
        BaseSelectionFlagPlacementState flagPlacementState = new BaseSelectionFlagPlacementState(@base);

        BaseSelectionFlagPlacementTransition flagPlacementTransition = new(flagPlacementState, _hitInfoProvider, @base);
        BaseSelectionIdleTransition idleTransition = new(idleState, _hitInfoProvider);
        BaseSelectionFlagIdleTransition flagIdleTransition = new(idleState, _hitInfoProvider, @base);

        idleState.AddTransition(flagPlacementTransition);
        flagPlacementState.AddTransition(idleTransition);
        flagPlacementState.AddTransition(flagIdleTransition);

        return new StateMachine<BaseSelectionState, BaseSelectionTransition>(idleState);
    }
}

public abstract class BaseSelectionState : State<BaseSelectionState, BaseSelectionTransition> { }

public abstract class BaseSelectionTransition : Transition<BaseSelectionState, BaseSelectionTransition>
{
    protected BaseSelectionTransition(BaseSelectionState nextState) : base(nextState) { }
}

public class BaseSelectionIdleState : BaseSelectionState { }

public class BaseSelectionFlagPlacementState : BaseSelectionState
{
    private readonly SelectableBase _base;

    public BaseSelectionFlagPlacementState(SelectableBase @base) =>
        _base = @base != null ? @base : throw new ArgumentNullException(nameof(@base));

    public override void Enter()
    {
        base.Enter();
        _base.ChangeState();
    }

    public override void Exit() =>
        _base.ChangeState();
}

public class BaseSelectionIdleTransition : BaseSelectionTransition
{
    private readonly RaycasterHitInfoProvider _hitInfoProvider;

    public BaseSelectionIdleTransition(BaseSelectionState nextState, RaycasterHitInfoProvider hitInfoProvider) : base(nextState) =>
        _hitInfoProvider = hitInfoProvider ?? throw new ArgumentNullException(nameof(hitInfoProvider));

    public override void Update()
    {
        if (_hitInfoProvider.HitInfo.transform.GetComponent<BasesSpawnZone>() == false)
            Open();
    }
}

public class BaseSelectionFlagIdleTransition : BaseSelectionTransition
{
    private readonly SelectableBase _base;
    private readonly RaycasterHitInfoProvider _hitInfoProvider;

    public BaseSelectionFlagIdleTransition(BaseSelectionState nextState,
        RaycasterHitInfoProvider hitInfoProvider, SelectableBase @base) : base(nextState)
    {
        _hitInfoProvider = hitInfoProvider ?? throw new ArgumentNullException(nameof(hitInfoProvider));
        _base = @base != null ? @base : throw new ArgumentNullException(nameof(@base));
    }

    public override void Update()
    {
        if (_hitInfoProvider.HitInfo.transform.GetComponent<BasesSpawnZone>() == false)
            return;

        _base.FlagInfo.EnableObject();
        _base.FlagInfo.SetPosition(_hitInfoProvider.HitInfo.point);

        Open();
    }
}

public class BaseSelectionFlagPlacementTransition : BaseSelectionTransition
{
    private readonly RaycasterHitInfoProvider _hitInfoProvider;
    private readonly SelectableBase _base;

    public BaseSelectionFlagPlacementTransition(BaseSelectionState nextState,
        RaycasterHitInfoProvider hitInfoProvider, SelectableBase @base) : base(nextState)
    {
        _hitInfoProvider = hitInfoProvider ?? throw new ArgumentNullException(nameof(hitInfoProvider));
        _base = @base != null ? @base : throw new ArgumentNullException(nameof(@base));
    }

    public override void Update()
    {
        if (_hitInfoProvider.HitInfo.transform.TryGetComponent(out SelectableBase @base) == false)
            return;

        if (_base == @base)
            Open();
    }
}
