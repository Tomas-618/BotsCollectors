using UnityEngine;
using Zenject;

public class ResourcesDistributorSceneInstaller : MonoInstaller
{
    [SerializeField] private ResourcesSpawnerOnPlane _spawner;
    [SerializeField] private BotsBase _botsBase;

    public override void InstallBindings()
    {
        Container.Bind<IReadOnlyResourcesSpawnerEvents>().To<ResourcesSpawnerOnPlane>().FromInstance(_spawner);
        Container.Bind<IResourcesWorker>().To<BotsBase>().FromInstance(_botsBase);
        Container.Bind<ResourcesDistributor>().AsSingle().NonLazy();
    }
}
