using UnityEngine;

public class BotCreatingButton : MonoBehaviour
{
    [SerializeField] private InteractableButton _view;
    [SerializeField] private InterfaceReference<IReadOnlyBotsBaseEvents, BotsBase> _baseEvents;
    [SerializeField] private InterfaceReference<IReadOnlyBaseWalletEvents, ResourcesBaseWallet> _walletEvents;
    [SerializeField] private InterfaceReference<IReadOnlyBaseUIHandler, BaseUIHandler> _spawner;

    private bool _isBaseFull;
    private bool _isResourcesEnough;

    private void OnEnable()
    {
        _baseEvents.Value.BotsCountChanged += OnBotsCountChanged;
        _walletEvents.Value.CountChanged += OnResourcesCountChanged;
    }

    private void OnDisable()
    {
        _baseEvents.Value.BotsCountChanged -= OnBotsCountChanged;
        _walletEvents.Value.CountChanged -= OnResourcesCountChanged;
    }

    private void OnBotsCountChanged(bool canAddBot)
    {
        _isBaseFull = !canAddBot;
        ChangeState();
    }

    private void OnResourcesCountChanged(int count)
    {
        _isResourcesEnough = count >= _spawner.Value.ResourcesCountToCreate;
        ChangeState();
    }

    private void ChangeState() =>
        _view.gameObject.SetActive(_isBaseFull == false && _isResourcesEnough);
}
