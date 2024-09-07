using System;
using BasicStateMachine;

public class BotStateMachineFactory
{
    private readonly TargetInfoOwner _entity;
    private readonly BotMover _mover;
    private readonly BotInteractor _interactor;
    private readonly BotsBaseInfoOwner _botsBaseInfoOwner;
    private readonly BaseWalletInfoOwner _baseWalletInfoOwner;
    private readonly DistanceChecker _distanceChecker;

    public BotStateMachineFactory(TargetInfoOwner entity, BotMover mover, BotInteractor interactor,
        BotsBaseInfoOwner botsBaseInfoOwner, BaseWalletInfoOwner baseWalletInfoOwner,
        DistanceChecker distanceChecker)
    {
        _entity = entity != null ? entity : throw new ArgumentNullException(nameof(entity));
        _mover = mover != null ? mover : throw new ArgumentNullException(nameof(mover));
        _interactor = interactor != null ? interactor : throw new ArgumentNullException(nameof(interactor));
        _botsBaseInfoOwner = botsBaseInfoOwner != null ? botsBaseInfoOwner :
            throw new ArgumentNullException(nameof(botsBaseInfoOwner));
        _baseWalletInfoOwner = baseWalletInfoOwner != null ? baseWalletInfoOwner :
            throw new ArgumentNullException(nameof(baseWalletInfoOwner));
        _distanceChecker = distanceChecker != null ? distanceChecker :
            throw new ArgumentNullException(nameof(distanceChecker));
    }

    public StateMachine<BotState, BotTransition> Create()
    {
        BotIdleState idleState = new(_entity, _botsBaseInfoOwner);
        BotMoveToTargetState moveToTargetState = new(_entity, _mover);
        BotInteractState interactState = new(_entity, _interactor);
        BotSettingBaseWalletTargetState settingBaseWalletTargetState = new(_entity, _baseWalletInfoOwner);

        BotMoveToTargetTransition moveToTargetTransition = new(moveToTargetState, _entity);
        BotInteractTransition interactTransition = new(interactState, _entity, _distanceChecker);
        BotIdleTransition idleTransition = new(idleState, _entity, _baseWalletInfoOwner, _distanceChecker);
        BotSettingBaseWalletTargetTransition settingBaseWalletTargetTransition = new(settingBaseWalletTargetState,
            _entity, _baseWalletInfoOwner, _distanceChecker);

        idleState.AddTransition(moveToTargetTransition);

        moveToTargetState.AddTransition(interactTransition);

        interactState.AddTransition(idleTransition);
        interactState.AddTransition(settingBaseWalletTargetTransition);

        settingBaseWalletTargetState.AddTransition(moveToTargetTransition);

        return new StateMachine<BotState, BotTransition>(idleState);
    }
}
