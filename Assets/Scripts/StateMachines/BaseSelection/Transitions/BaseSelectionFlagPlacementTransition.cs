using System;

public class BaseSelectionFlagPlacementTransition : BaseSelectionTransition
{
    private readonly RaycasterHitInfoProvider _hitInfoProvider;
    private readonly IReadOnlySelectableBase _base;

    public BaseSelectionFlagPlacementTransition(BaseSelectionState nextState,
        RaycasterHitInfoProvider hitInfoProvider, IReadOnlySelectableBase @base) : base(nextState)
    {
        _hitInfoProvider = hitInfoProvider ?? throw new ArgumentNullException(nameof(hitInfoProvider));
        _base = @base ?? throw new ArgumentNullException(nameof(@base));
    }

    public override void Update()
    {
        if (_base.CanRemoveBot == false)
            return;

        if (_hitInfoProvider.HasHit == false)
            return;
        
        if (_hitInfoProvider.HitInfo.transform.TryGetComponent(out IReadOnlySelectableBase @base) == false)
            return;

        if (_base == @base)
            Open();
    }
}
