using System;
using UnityEngine;
using Zenject;

public class BotsBasesSpawner : MonoBehaviour
{
    [SerializeField] private BotsSpawner _botsSpawner;
    [SerializeField] private Transform _parent;
    [SerializeField] private float _height;

    private BotsBasesFactory _factory;

    public void Spawn(int botsCount, Vector2 spawnPosition)
    {
        BotsBase @base = _factory.Create(_parent);

        @base.UIHandlerMediator.Init(_botsSpawner);
        @base.transform.position = new Vector3(spawnPosition.x, _height, spawnPosition.y);

        for (int i = 0; i < botsCount; i++)
        {
            if (_botsSpawner.TrySpawnNewBot(@base, out Bot newBot) == false)
            {
                return;
            }

            @base.AddNewEntity(newBot);
        }
    }

    public void SpawnByBot(Bot bot, Vector3 spawnPosition)
    {
        if (bot == null)
            throw new ArgumentNullException(nameof(bot));

        BotsBase @base = _factory.Create(_parent);

        @base.UIHandlerMediator.Init(_botsSpawner);
        @base.transform.position = new Vector3(spawnPosition.x, _height, spawnPosition.z);
        @base.AddNewEntity(bot);
    }

    [Inject]
    private void Construct(BotsBasesFactory factory) =>
        _factory = factory ?? throw new ArgumentNullException(nameof(factory));
}
