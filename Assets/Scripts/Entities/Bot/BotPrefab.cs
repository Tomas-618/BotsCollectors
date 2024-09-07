using UnityEngine;

public class BotPrefab : MonoBehaviour, IInitializable<BasesSpawner, PlayerCamera>
{
    [SerializeField] private InterfaceReference<IInitializable<BasesSpawner>, BotInteractor> _interactor;
    [SerializeField] private InterfaceReference<IInitializable<PlayerCamera>, TransformLooker> _transformLookerUI;
    [SerializeField] private InterfaceReference<IInitializable<BotsBase>, BotsBaseInfoOwner> _botsBaseInfoOwner;
    [SerializeField] private InterfaceReference<IInitializable<ResourcesBaseWallet>, BaseWalletInfoOwner> _baseWalletInfoOwner;
    [SerializeField] private InterfaceReference<IInitializable, BotLifeCycle> _lifeCycle;

    [field: SerializeField] public TargetInfoOwner TargetInfoOwner { get; private set; }

    public void Init(BasesSpawner basesSpawner, PlayerCamera playerCamera)
    {
        _interactor.Value.Init(basesSpawner);
        _transformLookerUI.Value.Init(playerCamera);
        _lifeCycle.Value.Init();
    }

    public void SetBase(BaseStorage storage)
    {
        _botsBaseInfoOwner.Value.Init(storage.BotsBaseInfo);
        _baseWalletInfoOwner.Value.Init(storage.Wallet);
    }
}
