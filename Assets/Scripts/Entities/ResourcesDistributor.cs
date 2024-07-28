using System;
using System.Collections.Generic;

public class ResourcesDistributor
{
    private readonly List<IResourcesWorker> _botsBases;
    private readonly IReadOnlyResourcesSpawnerEvents _spawnerEvents;

    public ResourcesDistributor(IReadOnlyResourcesSpawnerEvents spawnerEvents, IResourcesWorker botsBases)
    {
        _botsBases = new List<IResourcesWorker>();
        _spawnerEvents = spawnerEvents ?? throw new ArgumentNullException(nameof(spawnerEvents));

        _botsBases.Add(botsBases ?? throw new ArgumentNullException(nameof(botsBases)));
        _spawnerEvents.Spawned += OnSpawned;
    }

    public void Dispose() =>
        _spawnerEvents.Spawned -= OnSpawned;

    private void OnSpawned(ITarget[] resources)
    {
        for (int i = 0; i < resources.Length; i++)
        {
            int botsBaseIndex = i % _botsBases.Count;

            _botsBases[botsBaseIndex].AddNewDesiredResource(resources[i]);
        }

        _botsBases.ForEach(botsBase => botsBase.SetResourcesTargetsToEntities());
    }
}
