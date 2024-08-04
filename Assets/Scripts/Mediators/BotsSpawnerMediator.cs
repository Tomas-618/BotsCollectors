using UnityEngine;

public class BotsSpawnerMediator : MonoBehaviour
{
    [SerializeField] private InterfaceReference<IInitializable<BotsFactory>, BotsSpawner> _entity;

    public void Init(BotsFactory factory) =>
        _entity.Value.Init(factory);
}
