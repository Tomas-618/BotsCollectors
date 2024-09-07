using System;

public class BotIdleTransition : BotTransition
{
    private readonly BaseWalletInfoOwner _baseWallet;
    private readonly DistanceChecker _distanceChecker;

    public BotIdleTransition(BotState nextState, TargetInfoOwner bot, BaseWalletInfoOwner baseWallet,
        DistanceChecker distanceChecker) : base(nextState, bot)
    {
        _baseWallet = baseWallet != null ? baseWallet : throw new ArgumentNullException(nameof(baseWallet));
        _distanceChecker = distanceChecker != null ? distanceChecker : throw new ArgumentNullException(nameof(distanceChecker));
    }

    public override void Update()
    {
        if (_distanceChecker.IsNearestToPoint(_baseWallet.Target.TransformInfo.position)
            && BotInfo.HasTarget == false)
            Open();
    }
}
