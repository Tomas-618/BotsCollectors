using System;

public class BaseSelectionFlagIdleTransition : BaseSelectionTransition
{
    private readonly ICanOnlyUseFlagMethods _base;
    private readonly RaycasterHitInfoProvider _hitInfoProvider;

    public BaseSelectionFlagIdleTransition(BaseSelectionState nextState,
        RaycasterHitInfoProvider hitInfoProvider, ICanOnlyUseFlagMethods @base) : base(nextState)
    {
        _hitInfoProvider = hitInfoProvider ?? throw new ArgumentNullException(nameof(hitInfoProvider));
        _base = @base != null ? @base : throw new ArgumentNullException(nameof(@base));
    }

    public override void Update()
    {
        if (_hitInfoProvider.HasHit == false || _hitInfoProvider.HitInfo.transform.GetComponent<BasesSpawnZone>() == false)
            return;

        _base.EnableFlag();
        _base.SetFlagPosition(_hitInfoProvider.HitInfo.point);

        Open();
    }
}
