using System;
using UnityEngine;
using Zenject;

[RequireComponent(typeof(BoxCollider))]
public class BotsSpawner : BasicSpawnerOnCollider<BoxCollider>, IReadOnlyBotsSpawner
{
    [SerializeField, Min(0)] private int _resourcesCountToSpawn;

    [SerializeField] private BotsBase _base;
    [SerializeField] private Transform _parent;
    [SerializeField] private InteractableButton _button;

    private BotsFactory _fabric;

    public int ResourcesCountToSpawn => _resourcesCountToSpawn;

    public bool CanSpawn => _base.ResourcesCount >= _resourcesCountToSpawn && _base.CanAddNewBot;

    private void Reset() =>
        _resourcesCountToSpawn = 3;

    private void OnEnable() =>
        _button.Clicked += Spawn;

    private void OnDisable() =>
        _button.Clicked -= Spawn;

    private void Spawn()
    {
        if (CanSpawn == false)
            return;

        Bot entity = _fabric.Create(_parent);

        _base.SpendResources(_resourcesCountToSpawn);
        _base.AddNewEntity(entity);

        entity.transform.position = GetRandomPosition();
    }

    [Inject]
    private void Construct(BotsFactory fabric) =>
        _fabric = fabric ?? throw new ArgumentNullException(nameof(fabric));
}
