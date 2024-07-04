using UnityEngine;
using Zenject;

public class ResourcesFactorySceneInstaller : MonoInstaller
{
    [SerializeField] private Resource _resource;

    public override void InstallBindings()
    {
        Container.Bind<Resource>().FromInstance(_resource);
        Container.Bind<ResourcesFactory>().AsSingle();
    }
}
