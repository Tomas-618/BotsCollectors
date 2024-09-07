using UnityEngine;
using BasicStateMachine;

public class BotLifeCycle : MonoBehaviour, IInitializable
{
    [SerializeField] private TargetInfoOwner _targetOwner;
    [SerializeField] private BotMover _mover;
    [SerializeField] private BotInteractor _interactor;
    [SerializeField] private BotsBaseInfoOwner _botsBaseInfoOwner;
    [SerializeField] private BaseWalletInfoOwner _baseWalletInfoOwner;
    [SerializeField] private DistanceChecker _distanceChecker;

    private StateMachine<BotState, BotTransition> _stateMachine;

    private void Update() =>
        _stateMachine.Update();

    public void Init()
    {
        _stateMachine = new BotStateMachineFactory(_targetOwner, _mover, _interactor,
            _botsBaseInfoOwner, _baseWalletInfoOwner, _distanceChecker)
            .Create();
    }
}
