using System;

public class FromDistributionToIdleBaseTransition : BaseTransition
{
    private readonly BotsBase _botsBase;
    private readonly ResourcesScanner _scanner;

    public FromDistributionToIdleBaseTransition(BaseState nextState, BotsBase botsBase,
        ResourcesScanner scanner) : base(nextState)
    {
        _botsBase = botsBase != null ? botsBase : throw new ArgumentNullException(nameof(botsBase));
        _scanner = scanner != null ? scanner : throw new ArgumentNullException(nameof(scanner));
    }

    public override void Update()
    {
        if (_botsBase.HasFreeBots == false || _scanner.HasTargets == false)
            Open();
    }
}
