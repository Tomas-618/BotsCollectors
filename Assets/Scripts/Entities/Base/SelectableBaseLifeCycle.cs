using System;
using UnityEngine;
using BasicStateMachine;

public class SelectableBaseLifeCycle : MonoBehaviour, IInitializable<PlayerCameraRaycaster>
{
    [SerializeField] private InterfaceReference<IReadOnlyBotsBaseEvents, BotsBase> _botsBaseEvents;
    [SerializeField] private SelectableBase _base;

    private StateMachine<BaseSelectionState, BaseSelectionTransition> _stateMachine;
    private PlayerCameraRaycaster _cameraRaycaster;

    private void OnEnable() =>
        AddListenersOnEvents();

    private void OnDisable() =>
        RemoveListenersOnEvents();

    public void Init(PlayerCameraRaycaster cameraRaycaster)
    {
        _cameraRaycaster = cameraRaycaster != null ? cameraRaycaster : throw new ArgumentNullException(nameof(cameraRaycaster));
        _stateMachine = new BaseSelectionStateMachineFactory(_base, _cameraRaycaster.HitInfoProvider).Create();

        AddListenersOnEvents();
    }

    private void UpdateStateMachine() =>
        _stateMachine.Update();

    private void AddListenersOnEvents()
    {
        if (_cameraRaycaster == null)
            return;

        _cameraRaycaster.Clicked += UpdateStateMachine;
        _botsBaseEvents.Value.BotRemoved += UpdateStateMachine;
    }

    private void RemoveListenersOnEvents()
    {
        _cameraRaycaster.Clicked -= UpdateStateMachine;
        _botsBaseEvents.Value.BotRemoved -= UpdateStateMachine;
    }
}
