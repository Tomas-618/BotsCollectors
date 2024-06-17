using System;
using UnityEngine;
using Zenject;

[RequireComponent(typeof(MeshCollider))]
public class ResourcesSpawnerOnPlane : MonoBehaviour, IReadOnlyResourcesSpawnerEvents
{
    [SerializeField, Min(0)] private int _minCount;

    [SerializeField] private float _height;

    private MeshCollider _collider;
    private ResourcesPool _pool;

    public event Action<Resource[]> Spawned;

    private void Awake() =>
        _collider = GetComponent<MeshCollider>();

    private void Start() =>
        Spawn();

    private void Spawn()
    {
        int randomCount = UnityEngine.Random.Range(_minCount, _pool.Count + 1);

        Resource[] entities = _pool.GetFrom(randomCount);

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
