using System;

public class BaseSelectionIdleTransition : BaseSelectionTransition
{
    private readonly RaycasterHitInfoProvider _hitInfoProvider;

    public BaseSelectionIdleTransition(BaseSelectionState nextState, RaycasterHitInfoProvider hitInfoProvider) : base(nextState) =>
        _hitInfoProvider = hitInfoProvider ?? throw new ArgumentNullException(nameof(hitInfoProvider));

    public override void Update()
    {
        if (_hitInfoProvider.HasHit == false || _hitInfoProvider.HitInfo.transform.GetComponent<BasesSpawnZone>() == false)
            Open();
    }
}
