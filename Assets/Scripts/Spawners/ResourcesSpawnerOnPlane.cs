using System;
using UnityEngine;
using Zenject;

[RequireComponent(typeof(MeshCollider))]
public class ResourcesSpawnerOnPlane : MonoBehaviour, IReadOnlyResourcesSpawnerEvents
{
    [SerializeField, Min(0)] private int _minCount;
    [SerializeField, Min(0)] private float _minDelay;
    [SerializeField, Min(0)] private float _maxDelay;

    [SerializeField] private float _height;

    private MeshCollider _collider;
    private ResourcesPool _pool;

    public event Action<Resource[]> Spawned;

    private void OnValidate()
    {
        if (_minDelay >= _maxDelay)
            _minDelay = _maxDelay - 1;
    }

    private void Awake() =>
        _collider = GetComponent<MeshCollider>();

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

        Resource[] entities = _pool.GetEntityFrom(randomCount);

        foreach (Resource entity in entities)
        {
            if (entity == false)
                continue;

            entity.transform.localPosition = GetRandomPosition( _height);
        }

        Spawned?.Invoke(entities);
    }

    private Vector3 GetRandomPosition(float height)
    {
        Vector3 randomPosition = _collider.GetRandomPosition();

        randomPosition.y = 0;
        Vector3 resultPosition = randomPosition + Vector3.up * height;

        return resultPosition;
    }

    [Inject]
    private void Construct(ResourcesPool pool) =>
        _pool = pool ?? throw new ArgumentNullException(nameof(pool));
}
