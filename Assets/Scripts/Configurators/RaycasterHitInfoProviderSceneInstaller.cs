using Zenject;

public class RaycasterHitInfoProviderSceneInstaller : MonoInstaller
{
    public override void InstallBindings() =>
        Container.Bind<RaycasterHitInfoProvider>().AsSingle();
}
