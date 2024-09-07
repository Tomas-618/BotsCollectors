using UnityEngine;
using Zenject;

public class BotsFactorySceneInstaller : MonoInstaller
{
    [SerializeField] private BotPrefab _prefab;
    [SerializeField] private BasesSpawner _basesSpawner;
    [SerializeField] private PlayerCameraRaycaster _cameraRaycaster;

    public override void InstallBindings()
    {
        Container.Bind<BotPrefab>().FromInstance(_prefab);
        Container.Bind<BasesSpawner>().FromInstance(_basesSpawner);
        Container.Bind<PlayerCameraRaycaster>().FromInstance(_cameraRaycaster).AsSingle();
        Container.Bind<BotsFactory>().AsSingle();

        Container.Bind<Camera>().FromInstance(_cameraRaycaster.CameraInfo);
    }
}
