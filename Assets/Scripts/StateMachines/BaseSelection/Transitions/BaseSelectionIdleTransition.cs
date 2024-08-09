using System;

public class BaseSelectionIdleTransition : BaseSelectionTransition
{
    private readonly RaycasterHitInfoProvider _hitInfoProvider;
    private readonly IReadOnlySelectableBaseEvents _baseEvents;

    public BaseSelectionIdleTransition(BaseSelectionState nextState, RaycasterHitInfoProvider hitInfoProvider,
        IReadOnlySelectableBaseEvents baseEvents) : base(nextState)
    {
        _hitInfoProvider = hitInfoProvider ?? throw new ArgumentNullException(nameof(hitInfoProvider));
        _baseEvents = baseEvents ?? throw new ArgumentNullException(nameof(baseEvents));

        _baseEvents.Disabled += Open;
    }

    public void Dispose() =>
        _baseEvents.Disabled -= Open;

    public override void Update()
    {
        if (_hitInfoProvider.HasHit == false || _hitInfoProvider.HitInfo.transform.GetComponent<BasesSpawnZone>() == false)
            Open();
    }
}
