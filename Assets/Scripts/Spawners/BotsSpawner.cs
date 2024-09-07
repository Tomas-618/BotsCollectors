using System;
using UnityEngine;
using Zenject;

public class BotsSpawner : MonoBehaviour
{
    [SerializeField] private float _height;

    private BotsFactory _factory;

    public bool TrySpawnNewBot(BaseStorage baseStorage, out BotPrefab prefab)
    {
        BotsBase botsBase = baseStorage.BotsBaseInfo;
        PhysicalBase physicalBase = baseStorage.Physical;

        prefab = null;

        if (botsBase.CanAddBot == false)
            return false;

        prefab = _factory.Create(botsBase.BotsParent, baseStorage);
        prefab.transform.position = GetRandomPositionOnCollider(physicalBase.ColliderInfo, _height);

        return true;
    }

    private Vector3 GetRandomPositionOnCollider(Collider collider, float height)
    {
        Vector3 randomPosition = collider.GetRandomPosition();

        randomPosition.y = height;

        return randomPosition;
    }

    [Inject]
    private void Construct(BotsFactory factory) =>
        _factory = factory ?? throw new ArgumentNullException(nameof(factory));
}
