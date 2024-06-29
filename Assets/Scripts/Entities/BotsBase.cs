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
        _entities.ForEach(entity => entity.ResourcesPut += AddResources);
    }

    private void OnDisable()
    {
        _events.Value.Spawned -= SetResourcesTargetsToBot;
        _entities.ForEach(entity => entity.ResourcesPut -= AddResources);
    }

    private void SetResourcesTargetsToBot(Resource[] resources)
    {
        foreach (Resource resource in resources)
        {
            int serviceIndex = UnityEngine.Random.Range(0, _entities.Count);

            _entities[serviceIndex].AddTarget(resource);
        }

        _entities.ForEach(entity =>
        {
            if (entity.HasTargets)
                entity.AddTarget(this);
        });
    }

    private void AddResources(int count)
    {
        _resourcesCount += count;
        ResourcesCountChanged?.Invoke(_resourcesCount);
    }
}
