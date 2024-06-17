using UnityEngine;
using Zenject;

public class ResourcesPoolSceneInstaller : MonoInstaller
{
    [SerializeField] private int _maxResourcesCount;

    public override void InstallBindings()
    {
        Container.Bind<int>().FromInstance(_maxResourcesCount);
        Container.Bind<ResourcesPool>().AsSingle();
    }
}
