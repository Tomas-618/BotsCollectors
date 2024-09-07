using System;
using UnityEngine;
using Zenject;

public class BasesSpawner : MonoBehaviour, IReadOnlyBasesSpawnerEvents
{
    [SerializeField] private BotsSpawner _botsSpawner;
    [SerializeField] private Transform _parent;
    [SerializeField] private float _height;

    private BasesFactory _factory;

    public event Action Spawned;

    public void Spawn(int botsCount, Vector2 spawnPosition)
    {
        BasePrefab prefab = _factory.Create(_parent);

        BaseStorage storage = prefab.Storage;

        prefab.transform.position = new Vector3(spawnPosition.x, _height, spawnPosition.y);

        Spawned?.Invoke();

        for (int i = 0; i < botsCount; i++)
        {
            if (_botsSpawner.TrySpawnNewBot(storage, out BotPrefab bot) == false)
            {
                return;
            }
        }
    }

    public void Spawn(BotInteractor botInteractor, Vector3 spawnPosition)
    {
        if (botInteractor == null)
            throw new ArgumentNullException(nameof(botInteractor));

        BasePrefab prefab = _factory.Create(_parent);

        BaseStorage storage = prefab.Storage;

        BotPrefab bot = botInteractor.Prefab;

        botInteractor.RemoveBotFromBase();

        prefab.transform.position = new Vector3(spawnPosition.x, _height, spawnPosition.z);
        storage.BotsBaseInfo.AddCreatedBot(bot.TargetInfoOwner);

        bot.SetBase(storage);

        Spawned?.Invoke();
    }

    [Inject]
    private void Construct(BasesFactory factory) =>
        _factory = factory ?? throw new ArgumentNullException(nameof(factory));
}
