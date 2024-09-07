using System;
using UnityEngine;

public class BaseWalletInfoOwner : MonoBehaviour, IInitializable<ResourcesBaseWallet>
{
    private ResourcesBaseWallet _target;
    
    public ITarget<BotInteractor> Target => _target;

    public void Init(ResourcesBaseWallet target) =>
        _target = target != null ? target : throw new ArgumentNullException(nameof(target));
}
