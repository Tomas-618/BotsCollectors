using UnityEngine;

public class BotCreatingButton : MonoBehaviour
{
    [SerializeField] private InteractableButton _model;
    [SerializeField] private InterfaceReference<IReadOnlyBotsBaseEvents, BotsBase> _baseEvents;
    [SerializeField] private InterfaceReference<IReadOnlyBotsSpawner, BotsBaseUIHandler> _spawner;

    private bool _isBaseFull;
    private bool _isResourcesEnough;

    private void OnEnable()
    {
        _baseEvents.Value.EntitiesCountChanged += OnEntityAdded;
        _baseEvents.Value.ResourcesCountChanged += OnResourcesCountChanged;
    }

    private void OnDisable()
    {
        _baseEvents.Value.EntitiesCountChanged -= OnEntityAdded;
        _baseEvents.Value.ResourcesCountChanged -= OnResourcesCountChanged;
    }

    private void OnEntityAdded(bool canAddNewBot)
    {
        _isBaseFull = !canAddNewBot;
        ChangeState();
    }

    private void OnResourcesCountChanged(int count, bool isLess)
    {
        _isResourcesEnough = count >= _spawner.Value.ResourcesCountToCreate;
        ChangeState();
    }

    private void ChangeState() =>
        _model.gameObject.SetActive(_isBaseFull == false && _isResourcesEnough);
}
