using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BotsBase : MonoBehaviour, IReadOnlyBotsBaseEvents, ITarget
{
    [SerializeField, Min(3)] private int _maxEntitiesCount;

    [SerializeField] private InterfaceReference<IReadOnlyResourcesSpawnerEvents, ResourcesSpawnerOnPlane> _spawnerEvents;
    [SerializeField] private List<Bot> _entities;
    [SerializeField] private SelectableBase _selectableBase;
    [SerializeField] private Flag _flag;
    [SerializeField] private int _minEntitiesCount;

    private int _resourcesCount;
    private bool _hasPriorityToBuildNew;

    public event Action<bool> EntityAdded;

    public event Action<int> ResourcesCountChanged;

    [field: SerializeField] public int ResourcesCountToCreateNew { get; private set; }

    public Transform TransformInfo { get; private set; }

    public int ResourcesCount => _resourcesCount;

    public bool CanAddNewBot => _entities.Count < _maxEntitiesCount;

    private void Awake() =>
        TransformInfo = transform;

    private void OnEnable()
    {
        _spawnerEvents.Value.Spawned += SetResourcesTargetsToEntities;
        _entities.ForEach(entity => entity.HandEvents.ResourceThrew += AddResource);
        _flag.Enabled += OnFlagEnabled;
    }

    private void OnDisable()
    {
        _spawnerEvents.Value.Spawned -= SetResourcesTargetsToEntities;
        _entities.ForEach(entity => entity.HandEvents.ResourceThrew -= AddResource);
        _flag.Enabled -= OnFlagEnabled;
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
        _selectableBase.enabled = _entities.Count > _minEntitiesCount;

        EntityAdded?.Invoke(CanAddNewBot);
    }

    private void SetResourcesTargetsToEntities(ITarget[] resources)
    {
        for (int i = 0; i < resources.Length; i++)
        {
            int currentEntityIndex = i % _entities.Count;

            if (_entities[currentEntityIndex].LastTarget is Resource)
                _entities[currentEntityIndex].AddTarget(this);

            _entities[currentEntityIndex].AddTarget(resources[i]);
            _entities[currentEntityIndex].AddTarget(this);
        }
    }

    private void OnFlagEnabled() =>
        _hasPriorityToBuildNew = true;

    private void AddResource()
    {
        _resourcesCount++;

        if (_hasPriorityToBuildNew)
        {
            if (_resourcesCount >= ResourcesCountToCreateNew)
            {
                _hasPriorityToBuildNew = false;
                SetFlagTargetToRandomEntity(_flag);
            }
        }

        ResourcesCountChanged?.Invoke(_resourcesCount);
    }

    private void SetFlagTargetToRandomEntity(Flag flag)
    {
        int randomEntityIndex = UnityEngine.Random.Range(0, _entities.Count);

        Bot entity = _entities[randomEntityIndex];

        ITarget[] targets = entity.TakeAllTargets()
            .Where(target => target is Resource)
            .ToArray();

        _entities.RemoveAt(randomEntityIndex);
        _selectableBase.enabled = _entities.Count > _minEntitiesCount;

        entity.AddTarget(this);
        entity.AddTarget(flag);
        entity.SetPriorityToBuildNewBase(flag);

        entity.MoveToCurrentTarget();
        SetResourcesTargetsToEntities(targets);
    }
}
