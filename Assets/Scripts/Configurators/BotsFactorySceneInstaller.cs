using UnityEngine;
using Zenject;

public class BotsFactorySceneInstaller : MonoInstaller
{
    [SerializeField] private Bot _prefab;
    [SerializeField] private Camera _camera;

    public override void InstallBindings()
    {
        Container.Bind<Bot>().FromInstance(_prefab);
        Container.Bind<Camera>().FromInstance(_camera).AsSingle();
        Container.Bind<BotsFactory>().AsSingle();
    }
}
