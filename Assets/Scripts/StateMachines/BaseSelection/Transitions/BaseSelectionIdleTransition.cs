using System;

public class BaseSelectionIdleTransition : BaseSelectionTransition
{
    private readonly RaycasterHitInfoProvider _hitInfoProvider;
    private readonly IReadOnlySelectableBase _base;

    public BaseSelectionIdleTransition(BaseSelectionState nextState, RaycasterHitInfoProvider hitInfoProvider,
        IReadOnlySelectableBase @base) : base(nextState)
    {
        _hitInfoProvider = hitInfoProvider ?? throw new ArgumentNullException(nameof(hitInfoProvider));
        _base = @base ?? throw new ArgumentNullException(nameof(@base));
    }

    public override void Update()
    {
        if (_base.CanRemoveBot == false)
            Open();

        if (_hitInfoProvider.HasHit == false || _hitInfoProvider.HitInfo.transform.GetComponent<BasesSpawnZone>() == null)
            Open();
    }
}
