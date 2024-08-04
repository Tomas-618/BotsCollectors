using System;
using System.Collections.Generic;
using UnityEngine;

public class BotsBase : MonoBehaviour, IReadOnlyBotsBaseEvents, ITarget
{
    [SerializeField, Min(3)] private int _maxEntitiesCount;
    [SerializeField, Min(0)] private int _minEntitiesCount;

    [SerializeField] private List<Bot> _entities;
    [SerializeField] private ResourcesGround _resourcesGround;
    [SerializeField] private TransformLooker _playerCameraLooker;
    [SerializeField] private SelectableBase _selectableBase;

    private int _resourcesCount;
    private bool _hasPriorityToBuildNew;

    public event Action<bool> EntitiesCountChanged;

    public event Action<int, bool> ResourcesCountChanged;

    [field: SerializeField] public Flag FlagInfo { get; private set; }

    [field: SerializeField] public BotsSpawnerMediator SpawnerMediator { get; private set; }

    [field: SerializeField] public int ResourcesCountToCreateNew { get; private set; }

    public Transform TransformInfo { get; private set; }

    public int ResourcesCount => _resourcesCount;

    public bool CanAddNewBot => _entities.Count < _maxEntitiesCount;

    private void Awake() =>
        TransformInfo = transform;

    private void OnEnable()
    {
        SubscribeOnResourcesGroundEvent();
        FlagInfo.Enabled += OnFlagEnabled;
    }

    private void OnDisable()
    {
        UnsubscribeOnResourcesGroundEvent();
        FlagInfo.Enabled -= OnFlagEnabled;
    }

    public void Init(ResourcesGround resourcesGround, Camera playerCamera)
    {
        _resourcesGround = resourcesGround != null ? resourcesGround : throw new ArgumentNullException(nameof(resourcesGround));
        _playerCameraLooker.Init(playerCamera);

        SubscribeOnResourcesGroundEvent();
    }

    public void SpendResources(int count)
    {
        if (count < 0)
            throw new ArgumentOutOfRangeException(count.ToString());

        if (count <= _resourcesCount)
            _resourcesCount -= count;

        ResourcesCountChanged?.Invoke(_resourcesCount, true);
    }

    public void AddNewEntities(Bot[] entities)
    {
        if (entities == null)
            throw new ArgumentNullException(nameof(entities));

        foreach (Bot entity in entities)
            AddNewEntity(entity);
    }

    public void AddNewEntity(Bot entity)
    {
        if (CanAddNewBot == false)
            return;

        _entities.Add(entity != null ? entity : throw new ArgumentNullException(nameof(entity)));
        _selectableBase.enabled = _entities.Count > _minEntitiesCount;

        SetTargetToEntity(entity);

        EntitiesCountChanged?.Invoke(CanAddNewBot);
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

    public void SetResourcesTargetsToEntities()
    {
        int iterationAmount = Mathf.Min(_resourcesGround.TargetsCount, _entities.Count);

        for (int i = 0; i < iterationAmount; i++)
        {
            ITarget currentTarget = _resourcesGround.TakeTheNearestTargetToBase(this);

            _entities[i].SetTarget(currentTarget);
        }
    }

    private void SubscribeOnResourcesGroundEvent()
    {
        if (_resourcesGround == null)
            return;

        _resourcesGround.SpawnedNew += SetResourcesTargetsToEntities;
    }

    private void UnsubscribeOnResourcesGroundEvent() =>
        _resourcesGround.SpawnedNew -= SetResourcesTargetsToEntities;

    private void SetTargetToEntity(Bot entity)
    {
        if (entity == null)
            throw new ArgumentNullException(nameof(entity));

        if (_resourcesGround.TargetsCount == 0)
        {
            entity.RemoveCurrentTarget();

            return;
        }

        ITarget currentResource = _resourcesGround.TakeTheNearestTargetToBase(this);

        entity.SetTarget(currentResource);
    }

    private void OnFlagEnabled()
    {
        _hasPriorityToBuildNew = true;

        if (_resourcesGround.HasTargets)
            return;

        Bot firstEntity = _entities[0];

        UpdatePriority(firstEntity);
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
}
