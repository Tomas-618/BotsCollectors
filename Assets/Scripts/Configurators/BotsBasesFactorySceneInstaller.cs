using UnityEngine;
using Zenject;

public class BotsBasesFactorySceneInstaller : MonoInstaller
{
    [SerializeField] private BotsBase _prefab;

    public override void InstallBindings()
    {
        Container.Bind<BotsBase>().FromInstance(_prefab);
        Container.Bind<BotsBasesFactory>().AsSingle();
    }
}
