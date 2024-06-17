using UnityEngine;
using Zenject;

public class BotsServicesSceneInstaller : MonoInstaller
{
    [SerializeField] private Bot[] _entities;
    [SerializeField] private BotsBase _base;

    public override void InstallBindings()
    {
        BotService[] services = new BotService[_entities.Length];

        BotStatesFabric fabric = new BotStatesFabric(_base);

        for (int i = 0; i < _entities.Length; i++)
            services[i] = new BotService(new BotStateMachine(fabric, _entities[i]), _entities[i]);

        Container.Bind<BotService[]>().FromInstance(services);
    }
}
