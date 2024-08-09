using UnityEngine;
using Zenject;

public class BotsBasesFactorySceneInstaller : MonoInstaller
{
    [SerializeField] private BotsBase _prefab;
    [SerializeField] private ResourcesGround _resourcesGround;
    [SerializeField] private PlayerCameraRaycaster _cameraRaycaster;

    public override void InstallBindings()
    {
        Container.Bind<BotsBase>().FromInstance(_prefab);
        Container.Bind<ResourcesGround>().FromInstance(_resourcesGround);
        Container.Bind<PlayerCameraRaycaster>().FromInstance(_cameraRaycaster);
        Container.Bind<BotsBasesFactory>().AsSingle();
    }
}
