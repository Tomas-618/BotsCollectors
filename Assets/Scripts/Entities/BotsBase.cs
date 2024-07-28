using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BotsBase : MonoBehaviour, IReadOnlyBotsBaseEvents, ITarget, IResourcesWorker
{
    [SerializeField, Min(3)] private int _maxEntitiesCount;
    [SerializeField, Min(0)] private int _minEntitiesCount;

    [SerializeField] private List<Bot> _entities;
    [SerializeField] private SelectableBase _selectableBase;

    private List<ITarget> _desiredResources;
    private int _resourcesCount;
    private bool _hasPriorityToBuildNew;

    public event Action<bool> EntitiesCountChanged;

    public event Action<int, bool> ResourcesCountChanged;

    [field: SerializeField] public Flag FlagInfo { get; private set; }

    [field: SerializeField] public int ResourcesCountToCreateNew { get; private set; }

    public Transform TransformInfo { get; private set; }

    public int ResourcesCount => _resourcesCount;

    public bool CanAddNewBot => _entities.Count < _maxEntitiesCount;

    private void Awake()
    {
        _entities.ForEach(entity => entity.SetBase(this));

        TransformInfo = transform;
        _desiredResources = new List<ITarget>();
    }

    private void OnEnable() =>
        FlagInfo.Enabled += OnFlagEnabled;

    private void OnDisable() =>
        FlagInfo.Enabled -= OnFlagEnabled;

    public void SpendResources(int count)
    {
        if (count < 0)
            throw new ArgumentOutOfRangeException(count.ToString());

        if (count <= _resourcesCount)
            _resourcesCount -= count;

        ResourcesCountChanged?.Invoke(_resourcesCount, true);
    }

    public void AddNewDesiredResource(ITarget resource) =>
        _desiredResources.Add(resource ?? throw new ArgumentNullException(nameof(resource)));

    public void AddNewEntity(Bot entity)
    {
        if (CanAddNewBot == false)
            return;

        _entities.Add(entity != null ? entity : throw new ArgumentNullException(nameof(entity)));
        _selectableBase.enabled = _entities.Count > _minEntitiesCount;

        SetTargetToEntity(entity);

        EntitiesCountChanged?.Invoke(CanAddNewBot);
    }

    public void SetResourcesTargetsToEntities()
    {
        int iterationAmount = Mathf.Min(_desiredResources.Count, _entities.Count);

        SortResourcesByAscendingDistance();

        for (int i = 0; i < iterationAmount; i++)
        {
            ITarget currentTarget = _desiredResources[0];

            _desiredResources.Remove(currentTarget);
            _entities[i].SetTarget(currentTarget);
        }
    }

    public void PutResource(Bot entity, Resource resource)
    {
        if (entity == null)
            throw new ArgumentNullException(nameof(entity));

        if (resource == null)
            throw new ArgumentNullException(nameof(resource));

        AddResource(entity);
        SetTargetToEntity(entity);
    }

    private void SetTargetToEntity(Bot entity)
    {
        if (entity == null)
            throw new ArgumentNullException(nameof(entity));

        if (_desiredResources.Count == 0)
        {
            entity.RemoveCurrentTarget();

            return;
        }

        ITarget currentResource = _desiredResources[0];

        _desiredResources.Remove(currentResource);
        entity.SetTarget(currentResource);
    }

    private void SortResourcesByAscendingDistance()
    {
        _desiredResources = _desiredResources
            .OrderBy(target => Vector3.Distance(TransformInfo.position, target.TransformInfo.position))
            .ToList();
    }

    private void AddNewDesiredResources(ITarget[] resources) =>
        _desiredResources.AddRange(resources ?? throw new ArgumentNullException(nameof(resources)));

    private void OnFlagEnabled()
    {
        Bot entity = GetRandomEntity();

        _hasPriorityToBuildNew = true;
        UpdatePriority(entity);
    }

    private void AddResource(Bot entity)
    {
        if (entity == null)
            throw new ArgumentNullException(nameof(entity));

        _resourcesCount++;
        UpdatePriority(entity);

        ResourcesCountChanged?.Invoke(_resourcesCount, false);
    }

    private void UpdatePriority(Bot entity)
    {
        if (entity == null)
            throw new ArgumentNullException(nameof(entity));

        if (_hasPriorityToBuildNew)
        {
            if (_resourcesCount >= ResourcesCountToCreateNew)
            {
                _hasPriorityToBuildNew = false;
                SetFlagTargetToEntity(entity);
            }
        }
    }

    private void SetFlagTargetToEntity(Bot entity)
    {
        _entities.Remove(entity);
        _selectableBase.enabled = _entities.Count > _minEntitiesCount;

        entity.SetPriorityToBuildNewBase();

        EntitiesCountChanged?.Invoke(CanAddNewBot);
    }

    private Bot GetRandomEntity()
    {
        int randomEntityIndex = UnityEngine.Random.Range(0, _entities.Count);

        return _entities[randomEntityIndex];
    }
}
