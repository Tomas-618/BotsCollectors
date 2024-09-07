using System;

public class BotInteractTransition : BotTransition
{
    private readonly DistanceChecker _distanceChecker;
    
    public BotInteractTransition(BotState nextState, TargetInfoOwner bot, DistanceChecker distanceChecker) : base(nextState, bot) =>
        _distanceChecker = distanceChecker != null ? distanceChecker : throw new ArgumentNullException(nameof(distanceChecker));

    public override void Update()
    {
        if (_distanceChecker.IsNearestToPoint(BotInfo.CurrentTarget.TransformInfo.position))
            Open();
    }
}
