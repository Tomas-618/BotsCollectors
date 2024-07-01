using System;
using System.Collections.Generic;
using UnityEngine;

public class BotsBase : MonoBehaviour, IReadOnlyBotsBaseEvents, ITarget
{
    [SerializeField] private InterfaceReference<IReadOnlyResourcesSpawnerEvents, ResourcesSpawnerOnPlane> _events;
    [SerializeField] private List<Bot> _entities;

    private int _resourcesCount;

    public event Action<int> ResourcesCountChanged;

    public Transform TransformInfo { get; private set; }

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
