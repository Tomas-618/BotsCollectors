using System;
using UnityEngine;
using BasicStateMachine;

public class SelectableBase : MonoBehaviour, ICanOnlyChangeSelectableBaseState,
    ISelectableBase, ICanOnlyUseFlagMethods, IReadOnlySelectableBaseEvents
{
    [SerializeField] private Flag _flagInfo;

    private StateMachine<BaseSelectionState, BaseSelectionTransition> _stateMachine;
    private PlayerCameraRaycaster _cameraRaycaster;
    private bool _isSelected;

    public event Action<bool> ChangedState;

    public bool IsEnabled => enabled;

    private void OnEnable() =>
        SubscribeOnCameraRaycasterEvent();

    private void OnDisable() =>
        UnsubscribeOnCameraRaycasterEvent();

    public void Init(PlayerCameraRaycaster cameraRaycaster)
    {
        _cameraRaycaster = cameraRaycaster != null ? cameraRaycaster : throw new ArgumentNullException(nameof(cameraRaycaster));
        _stateMachine = new BaseSelectionStateMachineFactory(_cameraRaycaster.HitInfoProvider).Create(this);

        SubscribeOnCameraRaycasterEvent();
    }

    public void ChangeState()
    {
        _isSelected = !_isSelected;
        ChangedState?.Invoke(_isSelected);
    }

    public void EnableFlag() =>
        _flagInfo.EnableObject();

    public void DisableFlag() =>
        _flagInfo.DisableObject();

    public void SetFlagPosition(Vector3 position) =>
        _flagInfo.SetPosition(position);

    private void OnClicked() =>
        _stateMachine.Update();

    private void SubscribeOnCameraRaycasterEvent()
    {
        if (_cameraRaycaster == null)
            return;

        _cameraRaycaster.Clicked += OnClicked;
    }

    private void UnsubscribeOnCameraRaycasterEvent() =>
        _cameraRaycaster.Clicked -= OnClicked;
}
