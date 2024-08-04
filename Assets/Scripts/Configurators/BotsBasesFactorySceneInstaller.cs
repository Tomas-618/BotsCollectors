using UnityEngine;
using Zenject;

public class BotsBasesFactorySceneInstaller : MonoInstaller
{
    [SerializeField] private BotsBase _prefab;
    [SerializeField] private ResourcesGround _resourcesGround;

    public override void InstallBindings()
    {
        Container.Bind<BotsBase>().FromInstance(_prefab);
        Container.Bind<ResourcesGround>().FromInstance(_resourcesGround);
        Container.Bind<BotsBasesFactory>().AsSingle();
    }
}
