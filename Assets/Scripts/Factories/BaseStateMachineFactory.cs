using System;
using BasicStateMachine;

public class BaseStateMachineFactory
{
    private readonly BaseStorage _storage;
    private readonly Flag _flag;
    private readonly ResourcesScanner _scanner;
    private readonly int _amountToBuildBase;

    public BaseStateMachineFactory(BaseStorage storage, Flag flag,
        ResourcesScanner scanner, int amountToBuildBase)
    {
        if (amountToBuildBase < 0)
            throw new ArgumentOutOfRangeException(amountToBuildBase.ToString());

        _storage = storage != null ? storage : throw new ArgumentNullException(nameof(storage));
        _flag = flag != null ? flag : throw new ArgumentNullException(nameof(flag));
        _scanner = scanner != null ? scanner : throw new ArgumentNullException(nameof(scanner));
        _amountToBuildBase = amountToBuildBase;
    }

    public StateMachine<BaseState, BaseTransition> Create()
    {
        BotsBase botsBase = _storage.BotsBaseInfo;
        ResourcesBaseWallet wallet = _storage.Wallet;

        BaseIdleState idleState = new();
        BaseDistributionState distributionState = new(_scanner, botsBase);
        BasePrioritySetterState prioritySetterState = new(botsBase, wallet, _flag, _amountToBuildBase);

        BaseDistributionTransition distributionTransition = new(distributionState, botsBase, wallet,
            _scanner, _flag, _amountToBuildBase);
        BasePrioritySetterTransition prioritySetterTransition = new(prioritySetterState, botsBase, wallet,
            _flag, _amountToBuildBase);
        FromDistributionToIdleBaseTransition fromDistributionToIdleTransition = new(idleState, botsBase, _scanner);
        FromPrioritySetterToIdleBaseTransition fromPrioritySetterToIdleTransition = new(idleState, _flag);

        idleState.AddTransition(distributionTransition);
        idleState.AddTransition(prioritySetterTransition);

        distributionState.AddTransition(fromDistributionToIdleTransition);

        prioritySetterState.AddTransition(fromPrioritySetterToIdleTransition);

        return new StateMachine<BaseState, BaseTransition>(idleState);
    }
}
