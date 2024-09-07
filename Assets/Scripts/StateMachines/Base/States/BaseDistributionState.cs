using System;
using UnityEngine;

public class BaseDistributionState : BaseState
{
    private readonly ResourcesScanner _scanner;
    private readonly BotsBase _botsBase;

    public BaseDistributionState(ResourcesScanner scanner, BotsBase botsBase)
    {
        _scanner = scanner != null ? scanner : throw new ArgumentNullException(nameof(scanner));
        _botsBase = botsBase != null ? botsBase : throw new ArgumentNullException(nameof(botsBase));
    }

    public override void Enter()
    {
        base.Enter();

        TargetInfoOwner[] bots = _botsBase.TakeFreeBots();

        int iterationAmount = Mathf.Min(_scanner.TargetsCount, bots.Length);

        for (int i = 0; i < iterationAmount; i++)
        {
            ITarget<BotInteractor> currentTarget = _scanner.TakeTheNearestTargetToBase(_botsBase);

            bots[i].SetTarget(currentTarget);
        }
    }
}
