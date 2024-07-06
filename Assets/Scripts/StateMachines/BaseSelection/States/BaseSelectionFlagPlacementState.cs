using System;

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
