using System;
using System.Collections.Generic;
using Pool;

public class ResourcesPool
{
    private readonly ObjectsPool<Resource> _entities;

    public ResourcesPool(ResourcesFabric fabric, int count)
    {
        _entities = new ObjectsPool<Resource>((fabric ?? throw new ArgumentNullException(nameof(fabric))).Create, count);
        Count = count;

        foreach (Resource entity in _entities.AllEntities)
            entity.GetComponent<ResourcePoolComponent>().Init(this);

        _entities.PutIn += PutIn;
        _entities.PutOut += PutOut;
        _entities.Removed += Destroy;
    }

    public event Action StoredAllEntities;

    public int Count { get; }

    public void Dispose()
    {
        _entities.PutInAllUnstoredEntities();

        _entities.PutIn -= PutIn;
        _entities.PutOut -= PutOut;
        _entities.Removed -= Destroy;
    }

    public Resource[] GetEntityFrom(int count)
    {
        if (count < 0)
            throw new ArgumentOutOfRangeException(count.ToString());

        List<Resource> entities = new List<Resource>();

        for (int i = 0; i < count; i++)
            entities.Add(_entities.GetEntityFrom());

        return entities
            .ToArray();
    }

    public void PutEntityIn(Resource resource)
    {
        _entities.PutInEntity(resource != null ? resource : throw new ArgumentNullException(nameof(resource)));

        if (_entities.NonstoredEntities.Count == 0)
            StoredAllEntities?.Invoke();
    }

    private void PutIn(Resource resource) =>
        resource.DisableObject();

    private void PutOut(Resource resource) =>
        resource.EnableObject();

    private void Destroy(Resource resource) =>
        resource.Destroy();
}
