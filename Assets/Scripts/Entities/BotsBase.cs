using System;
using UnityEngine;
using Zenject;

public class BotsBase : MonoBehaviour
{
    [SerializeField] private InterfaceReference<IReadOnlyResourcesSpawnerEvents, ResourcesSpawnerOnPlane> _events;

    private BotService[] _services;
    private int _currentServiceIndex;
    private int _resourcesCount;
    
    private void OnEnable() =>
        _events.Value.Spawned += SetResourcesTargetsToBot;

    private void OnDisable() =>
        _events.Value.Spawned -= SetResourcesTargetsToBot;

    private void Update()
    {
        foreach (BotService service in _services)
            service.Update();
    }

    public void ResieveResource(Resource resource)
    {
        if (resource == null)
            throw new ArgumentNullException(nameof(resource));

        _resourcesCount++;
    }

    private void SetResourcesTargetsToBot(Resource[] resources)
    {
        foreach (Resource entity in resources)
        {
            int serviceIndex = UnityEngine.Random.Range(0, _services.Length);

            _services[serviceIndex].AddNewResourceTarget(entity);
        }
    }

    [Inject]
    private void Construct(BotService[] services) =>
        _services = services ?? throw new ArgumentNullException(nameof(services));
}
