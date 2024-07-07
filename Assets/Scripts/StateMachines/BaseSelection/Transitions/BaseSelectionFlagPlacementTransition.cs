using System;

public class BaseSelectionFlagPlacementTransition : BaseSelectionTransition
{
    private readonly RaycasterHitInfoProvider _hitInfoProvider;
    private readonly ISelectableBase _base;

    public BaseSelectionFlagPlacementTransition(BaseSelectionState nextState,
        RaycasterHitInfoProvider hitInfoProvider, ISelectableBase @base) : base(nextState)
    {
        _hitInfoProvider = hitInfoProvider ?? throw new ArgumentNullException(nameof(hitInfoProvider));
        _base = @base != null ? @base : throw new ArgumentNullException(nameof(@base));
    }

    public override void Update()
    {
        if (_hitInfoProvider.HasHit == false || _hitInfoProvider.HitInfo.transform.TryGetComponent(out ISelectableBase @base) == false)
            return;

        if (_base == @base)
            Open();
    }
}
