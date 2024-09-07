using System;

public class BaseSelectionFlagIdleTransition : BaseSelectionTransition
{
    private readonly IReadOnlySelectableBase _base;
    private readonly RaycasterHitInfoProvider _hitInfoProvider;

    public BaseSelectionFlagIdleTransition(BaseSelectionState nextState,
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

        if (_hitInfoProvider.HitInfo.transform.GetComponent<BasesSpawnZone>() == null)
            return;

        _base.ActivateFlag(_hitInfoProvider.HitInfo.point);

        Open();
    }
}
