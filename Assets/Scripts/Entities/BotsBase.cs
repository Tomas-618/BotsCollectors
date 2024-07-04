using System;
using System.Collections.Generic;
using UnityEngine;

public class BotsBase : MonoBehaviour, IReadOnlyBotsBaseEvents, ITarget
{
    [SerializeField, Min(3)] private int _maxEntitiesCount;

    [SerializeField] private InterfaceReference<IReadOnlyResourcesSpawnerEvents, ResourcesSpawnerOnPlane> _events;
    [SerializeField] private List<Bot> _entities;

    private int _resourcesCount;

    public event Action<bool> EntityAdded;

    public event Action<int> ResourcesCountChanged;

    public Transform TransformInfo { get; private set; }

    public int ResourcesCount => _resourcesCount;

    public bool CanAddNewBot => _entities.Count < _maxEntitiesCount;

    private void Awake() =>
        TransformInfo = transform;

    private void OnEnable()
    {
        _events.Value.Spawned += SetResourcesTargetsToBot;
        _entities.ForEach(entity => entity.HandEvents.ResourceThrew += AddResource);
    }

    private void OnDisable()
    {
        _events.Value.Spawned -= SetResourcesTargetsToBot;
        _entities.ForEach(entity => entity.HandEvents.ResourceThrew -= AddResource);
    }

    public void SpendResources(int count)
    {
        if (count < 0)
            throw new ArgumentOutOfRangeException(count.ToString());

        if (count <= _resourcesCount)
            _resourcesCount -= count;

        ResourcesCountChanged?.Invoke(_resourcesCount);
    }

    public void AddNewEntity(Bot entity)
    {
        if (CanAddNewBot == false)
            return;

        _entities.Add(entity != null ? entity : throw new ArgumentNullException(nameof(entity)));

        entity.HandEvents.ResourceThrew += AddResource;
        EntityAdded?.Invoke(CanAddNewBot);
    }

    private void SetResourcesTargetsToBot(Resource[] resources)
    {
        for (int i = 0; i < resources.Length; i++)
        {
            int currentEntityIndex = i % _entities.Count;

            _entities[currentEntityIndex].AddTarget(resources[i]);
            _entities[currentEntityIndex].AddTarget(this);
        }
    }

    private void AddResource() =>
        ResourcesCountChanged?.Invoke(++_resourcesCount);
}
