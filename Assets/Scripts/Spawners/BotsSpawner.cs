using System;
using UnityEngine;
using Zenject;

public class BotsSpawner : MonoBehaviour
{
    [SerializeField] private BotsBasesSpawnerMediator _basesSpawnerMediator;
    [SerializeField] private float _height;

    private BotsFactory _factory;

    public bool TrySpawnNewBot(BotsBase @base, out Bot newBot)
    {
        newBot = null;

        if (@base.CanAddNewBot == false)
            return false;

        newBot = _factory.Create(@base.BotsParent);

        newBot.SetBasesSpawner(_basesSpawnerMediator.Entity);
        newBot.transform.position = GetRandomPositionOnCollider(@base.Collider, _height);

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
