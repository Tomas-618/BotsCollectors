using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BotsBase : MonoBehaviour, IReadOnlyBotsBaseEvents, ITarget
{
    private readonly ResourcesSorter _resourcesSorter = new ResourcesSorter();

    [SerializeField, Min(3)] private int _maxEntitiesCount;

    [SerializeField] private InterfaceReference<IReadOnlyResourcesSpawnerEvents, ResourcesSpawnerOnPlane> _spawnerEvents;
    [SerializeField] private List<Bot> _entities;
    [SerializeField] private SelectableBase _selectableBase;
    [SerializeField] private Flag _flag;
    [SerializeField] private int _minEntitiesCount;

    private int _resourcesCount;
    private bool _hasPriorityToBuildNew;

    public event Action<bool> EntitiesCountChanged;

    public event Action<int, bool> ResourcesCountChanged;

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

        ResourcesCountChanged?.Invoke(_resourcesCount, true);
    }

    public void AddNewEntity(Bot entity)
    {
        if (CanAddNewBot == false)
            return;

        _entities.Add(entity != null ? entity : throw new ArgumentNullException(nameof(entity)));

        entity.HandEvents.ResourceThrew += AddResource;
        _selectableBase.enabled = _entities.Count > _minEntitiesCount;

        ITarget[] resources = TakeResourcesFromAllEntities();

        SetResourcesTargetsToEntities(resources);

        EntitiesCountChanged?.Invoke(CanAddNewBot);
    }

    private void SetResourcesTargetsToEntities(ITarget[] resources)
    {
        _resourcesSorter.Clear();
        _resourcesSorter.AddGroups(_entities.Count);

        _resourcesSorter.AddForEachGroup(resources);
        _resourcesSorter.SortInEachGroupByAscendingDistanceToObject(TransformInfo);

        for (int i = 0; i < _entities.Count; i++)
        {
            ITarget[] group = _resourcesSorter.GetGroupByIndex(i);

            _entities[i].AddResourcesAsTargets(group, this);
        }

        _entities.ForEach(entity => entity.MoveToCurrentTarget());
    }

    private void OnFlagEnabled()
    {
        _hasPriorityToBuildNew = true;
        UpdatePriority();
    }

    private void AddResource()
    {
        _resourcesCount++;
        UpdatePriority();

        ResourcesCountChanged?.Invoke(_resourcesCount, false);
    }

    private void UpdatePriority()
    {
        if (_hasPriorityToBuildNew)
        {
            if (_resourcesCount >= ResourcesCountToCreateNew)
            {
                _hasPriorityToBuildNew = false;
                SetFlagTargetToRandomEntity(_flag);
            }
        }
    }

    private void SetFlagTargetToRandomEntity(Flag flag)
    {
        int randomEntityIndex = UnityEngine.Random.Range(0, _entities.Count);

        Bot desiredEntity = _entities[randomEntityIndex];

        ITarget[] resources = TakeResourcesFromAllEntities();

        _entities.RemoveAt(randomEntityIndex);
        _selectableBase.enabled = _entities.Count > _minEntitiesCount;

        desiredEntity.AddTarget(this);
        desiredEntity.AddTarget(flag);
        desiredEntity.SetPriorityToBuildNewBase(flag);

        desiredEntity.MoveToCurrentTarget();
        SetResourcesTargetsToEntities(resources);

        EntitiesCountChanged?.Invoke(CanAddNewBot);
    }

    private ITarget[] TakeResourcesFromAllEntities()
    {
        List<ITarget> resources = new List<ITarget>();

        foreach (Bot entity in _entities)
        {
            ITarget[] targets = entity.TakeAllTargets()
               .Where(target => target is Resource)
               .ToArray();

            resources.AddRange(targets);
        }

        return resources.ToArray();
    }
}
