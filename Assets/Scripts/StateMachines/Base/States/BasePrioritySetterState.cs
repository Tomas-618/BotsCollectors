using System;

public class BasePrioritySetterState : BaseState
{
    private readonly BotsBase _botsBase;
    private readonly ResourcesBaseWallet _wallet;
    private readonly Flag _flag;
    private readonly int _amountToBuildBase;

    public BasePrioritySetterState(BotsBase botsBase, ResourcesBaseWallet wallet, Flag flag, int amountToBuildBase)
    {
        if (amountToBuildBase < 0)
            throw new ArgumentOutOfRangeException(amountToBuildBase.ToString());

        _botsBase = botsBase != null ? botsBase : throw new ArgumentNullException(nameof(botsBase));
        _wallet = wallet != null ? wallet : throw new ArgumentNullException(nameof(wallet));
        _flag = flag != null ? flag : throw new ArgumentNullException(nameof(flag));
        _amountToBuildBase = amountToBuildBase;
    }

    public override void Enter()
    {
        base.Enter();

        TargetInfoOwner bot = _botsBase.TakeFreeBot();

        _flag.SetBot(bot);
        bot.SetTarget(_flag);
        _wallet.RemoveAmount(_amountToBuildBase);
    }
}
