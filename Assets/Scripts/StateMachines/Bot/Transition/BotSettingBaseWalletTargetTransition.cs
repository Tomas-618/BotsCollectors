using System;

public class BotSettingBaseWalletTargetTransition : BotTransition
{
    private readonly BaseWalletInfoOwner _baseWallet;
    private readonly DistanceChecker _distanceChecker;

    public BotSettingBaseWalletTargetTransition(BotState nextState, TargetInfoOwner bot,
        BaseWalletInfoOwner baseWallet, DistanceChecker distanceChecker) : base(nextState, bot)
    {
        _baseWallet = baseWallet != null ? baseWallet : throw new ArgumentNullException(nameof(baseWallet));
        _distanceChecker = distanceChecker != null ? distanceChecker : throw new ArgumentNullException(nameof(distanceChecker));
    }

    public override void Update()
    {
        if (_distanceChecker.IsNearestToPoint(_baseWallet.Target.TransformInfo.position) == false
            && BotInfo.HasTarget == false)
            Open();
    }
}
