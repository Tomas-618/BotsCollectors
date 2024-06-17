using UnityEngine;
using Zenject;

public class ResourcesFabricSceneInstaller : MonoInstaller
{
    [SerializeField] private Resource _resource;
    [SerializeField] private Transform _resourcesParent;

    public override void InstallBindings()
    {
        Container.Bind<Resource>().FromInstance(_resource);
        Container.Bind<Transform>().FromInstance(_resourcesParent);
        Container.Bind<ResourcesFabric>().AsSingle();
    }
}
