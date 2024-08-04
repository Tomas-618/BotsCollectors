using System;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class BotsSpawner : BasicSpawnerOnCollider<BoxCollider>, IReadOnlyBotsSpawner, IInitializable<BotsFactory>
{
    [SerializeField, Min(0)] private int _resourcesCountToSpawn;

    [SerializeField] private BotsBase _base;
    [SerializeField] private BotsBasesSpawner _basesSpawner;
    [SerializeField] private Transform _parent;
    [SerializeField] private InteractableButton _button;

    private BotsFactory _factory;

    public int ResourcesCountToSpawn => _resourcesCountToSpawn;

    public bool CanSpawn => _base.ResourcesCount >= _resourcesCountToSpawn && _base.CanAddNewBot;

    private void Reset() =>
        _resourcesCountToSpawn = 3; 

    private void OnEnable() =>
        _button.Clicked += Spawn;

    private void OnDisable() =>
        _button.Clicked -= Spawn;

    public void Init(BotsFactory factory) =>
        _factory = factory ?? throw new ArgumentNullException(nameof(factory));

    private void Spawn()
    {
        if (CanSpawn == false)
            return;

        Bot entity = _factory.Create(_parent);

        entity.SetBase(_base);
        entity.SetBasesSpawner(_basesSpawner);

        _base.SpendResources(_resourcesCountToSpawn);
        _base.AddNewEntity(entity);

        entity.transform.position = GetRandomPosition();
    }
}
