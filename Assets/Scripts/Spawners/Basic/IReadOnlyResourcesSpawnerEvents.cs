using System;

public interface IReadOnlyResourcesSpawnerEvents
{
    event Action<Resource[]> Spawned;
}
