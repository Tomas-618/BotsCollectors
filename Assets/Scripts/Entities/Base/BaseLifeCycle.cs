using UnityEngine;
using BasicStateMachine;

public class BaseLifeCycle : MonoBehaviour, IInitializable<ResourcesScanner>
{
    [SerializeField] private BaseStorage _storage;
    [SerializeField] private Flag _flag;
    [SerializeField] private int _amountToBuildBase;

    private StateMachine<BaseState, BaseTransition> _stateMachine;

    private void Update() =>
        _stateMachine.Update();

    public void Init(ResourcesScanner scanner) =>
        _stateMachine = new BaseStateMachineFactory(_storage, _flag, scanner,
            _amountToBuildBase).Create();
}
