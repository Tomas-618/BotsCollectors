using UnityEngine;

public class BaseStorage : MonoBehaviour
{
    [field: SerializeField] public ResourcesBaseWallet Wallet { get; private set; }

    [field: SerializeField] public BotsBase BotsBaseInfo { get; private set; }

    [field: SerializeField] public PhysicalBase Physical { get; private set; }
}
