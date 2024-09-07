using UnityEngine;
using Zenject;

public class BasesFactorySceneInstaller : MonoInstaller
{
    [SerializeField] private BasePrefab _prefab;
    [SerializeField] private ResourcesScanner _scanner;
    [SerializeField] private BotsSpawner _botsSpawner;

    public override void InstallBindings()
    {
        Container.Bind<BasePrefab>().FromInstance(_prefab);
        Container.Bind<ResourcesScanner>().FromInstance(_scanner);
        Container.Bind<BotsSpawner>().FromInstance(_botsSpawner);
        Container.Bind<BasesFactory>().AsSingle();
    }
}
