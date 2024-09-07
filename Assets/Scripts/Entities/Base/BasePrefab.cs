using UnityEngine;

public class BasePrefab : MonoBehaviour, IInitializable<ResourcesScanner, PlayerCameraRaycaster, BotsSpawner>
{
    [SerializeField] private InterfaceReference<IInitializable<ResourcesScanner>, BaseLifeCycle> _baseLifeCycle;
    [SerializeField] private InterfaceReference<IInitializable<PlayerCameraRaycaster>,
        SelectableBaseLifeCycle> _selectableBaseLifeCycle;
    [SerializeField] private InterfaceReference<IInitializable<PlayerCamera>, TransformLooker> _transformLooker;
    [SerializeField] private InterfaceReference<IInitializable<BotsSpawner>, BaseUIHandler> _handler;

    [field: SerializeField] public BaseStorage Storage { get; private set; }

    public void Init(ResourcesScanner scanner, PlayerCameraRaycaster raycaster, BotsSpawner botsSpawner)
    {
        _baseLifeCycle.Value.Init(scanner);
        _selectableBaseLifeCycle.Value.Init(raycaster);
        _transformLooker.Value.Init(raycaster);
        _handler.Value.Init(botsSpawner);
    }
}
