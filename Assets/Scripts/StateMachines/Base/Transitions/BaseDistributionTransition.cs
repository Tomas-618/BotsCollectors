using System;

public class BaseDistributionTransition : BaseTransition
{
    private readonly BotsBase _botsBase;
    private readonly ResourcesBaseWallet _wallet;
    private readonly ResourcesScanner _scanner;
    private readonly Flag _flag;
    private readonly int _amountToBuildBase;

    public BaseDistributionTransition(BaseState nextState, BotsBase botsBase, ResourcesBaseWallet wallet,
        ResourcesScanner scanner, Flag flag, int amountToBuildBase) : base(nextState)
    {
        if (amountToBuildBase < 0)
            throw new ArgumentOutOfRangeException(amountToBuildBase.ToString());

        _botsBase = botsBase != null ? botsBase : throw new ArgumentNullException(nameof(botsBase));
        _wallet = wallet != null ? wallet : throw new ArgumentNullException(nameof(wallet));
        _scanner = scanner != null ? scanner : throw new ArgumentNullException(nameof(scanner));
        _flag = flag != null ? flag : throw new ArgumentNullException(nameof(flag));
        _amountToBuildBase = amountToBuildBase;
    }

    public override void Update()
    {
        if (_wallet.IsEnough(_amountToBuildBase) && _flag.IsFree)
            return;

        if (_botsBase.HasFreeBots && _scanner.HasTargets)
            Open();
    }
}
