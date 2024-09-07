using System;

public class BasePrioritySetterTransition : BaseTransition
{
    private readonly BotsBase _botsBase;
    private readonly ResourcesBaseWallet _wallet;
    private readonly Flag _flag;
    private readonly int _amountToBuildBase;

    public BasePrioritySetterTransition(BaseState nextState, BotsBase botsBase,
        ResourcesBaseWallet wallet, Flag flag, int amountToBuildBase) : base(nextState)
    {
        if (amountToBuildBase < 0)
            throw new ArgumentOutOfRangeException(amountToBuildBase.ToString());

        _botsBase = botsBase != null ? botsBase : throw new ArgumentNullException(nameof(botsBase));
        _wallet = wallet != null ? wallet : throw new ArgumentNullException(nameof(wallet));
        _flag = flag != null ? flag : throw new ArgumentNullException(nameof(flag));
        _amountToBuildBase = amountToBuildBase;
    }

    public override void Update()
    {
        if (_botsBase.HasBots && _wallet.IsEnough(_amountToBuildBase) && _flag.IsFree)
            Open();
    }
}
