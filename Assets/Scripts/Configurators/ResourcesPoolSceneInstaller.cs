using UnityEngine;
using Zenject;

public class ResourcesPoolSceneInstaller : MonoInstaller
{
    [SerializeField] private Transform _parent;
    [SerializeField] private int _maxResourcesCount;

    public override void InstallBindings()
    {
        Container.Bind<int>().FromInstance(_maxResourcesCount);
        Container.Bind<Transform>().FromInstance(_parent);
        Container.Bind<ResourcesPool>().AsSingle();
    }
}
