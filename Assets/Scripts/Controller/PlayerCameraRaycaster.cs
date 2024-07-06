using BasicStateMachine;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class PlayerCameraRaycaster : MonoBehaviour
{
    [SerializeField] private SelectableBase _base;

    private PlayerCameraInput _input;
    private RaycasterHitInfoProvider _hitInfoProvider;
    private StateMachine<BaseSelectionState, BaseSelectionTransition> _stateMachine;
    
    private void Awake()
    {
        _input = PlayerCameraInput.Instance;
        _hitInfoProvider = new RaycasterHitInfoProvider(GetComponent<Camera>());
        _stateMachine = new BaseSelectionStateMachineFactory(_hitInfoProvider).Create(_base);
    }

    private void OnEnable() =>
        _input.Clicked += OnClicked;

    private void OnDisable() =>
        _input.Clicked -= OnClicked;

    private void Update() =>
        _hitInfoProvider.Update();

    private void OnClicked() =>
        _stateMachine.Update();
}
