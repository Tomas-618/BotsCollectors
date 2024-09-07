using System;

public class BotSettingBaseWalletTargetState : BotState
{
    private readonly BaseWalletInfoOwner _baseWallet;

    public BotSettingBaseWalletTargetState(TargetInfoOwner bot, BaseWalletInfoOwner baseWallet) : base(bot) =>
        _baseWallet = baseWallet != null ? baseWallet : throw new ArgumentNullException(nameof(baseWallet));

    public override void Enter()
    {
        base.Enter();

        BotInfo.SetTarget(_baseWallet.Target);
    }
}
