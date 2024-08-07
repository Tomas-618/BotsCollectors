using System;
using UnityEngine;
using Zenject;

[RequireComponent(typeof(BoxCollider))]
public class ResourcesSpawnerOnPlane : BasicSpawnerOnCollider<BoxCollider>, IReadOnlyResourcesSpawnerEvents
{
    [SerializeField, Min(0)] private int _minCount;
    [SerializeField, Min(0)] private float _minDelay;
    [SerializeField, Min(0)] private float _maxDelay;

    private ResourcesPool _pool;

    public event Action<Resource[]> Spawned;

    private void OnValidate()
    {
        if (_minDelay >= _maxDelay)
            _minDelay = _maxDelay - 1;
    }

    private void OnEnable() =>
        _pool.StoredAllEntities += StartSpawning;

    private void OnDisable() =>
        _pool.StoredAllEntities -= StartSpawning;

    private void Start() =>
        StartSpawning();

    private void StartSpawning() =>
        Invoke(nameof(Spawn), UnityEngine.Random.Range(_minDelay, _maxDelay));

    private void Spawn()
    {
        int randomCount = UnityEngine.Random.Range(_minCount, _pool.Count + 1);

        Resource[] entities = _pool.GetEntitiesFrom(randomCount);

        foreach (Resource entity in entities)
        {
            if (entity == null)
                continue;

            entity.transform.position = GetRandomPosition();
        }

        Spawned?.Invoke(entities);
    }

    [Inject]
    private void Construct(ResourcesPool pool) =>
        _pool = pool ?? throw new ArgumentNullException(nameof(pool));
}
