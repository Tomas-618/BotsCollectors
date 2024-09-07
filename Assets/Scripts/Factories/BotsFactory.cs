using System;
using UnityEngine;

public class BotsFactory : MonoBehaviourFactory<BotPrefab, BaseStorage>
{
    private readonly BasesSpawner _botsBasesSpawner;
    private readonly PlayerCameraRaycaster _playerCamera;

    public BotsFactory(BotPrefab prefab, BasesSpawner botsBasesSpawner, PlayerCameraRaycaster raycaster) : base(prefab)
    {
        _botsBasesSpawner = botsBasesSpawner != null ? botsBasesSpawner :
            throw new ArgumentNullException(nameof(botsBasesSpawner));
        _playerCamera = raycaster != null ? raycaster : throw new ArgumentNullException(nameof(raycaster));
    }

    public override BotPrefab Create(Transform parent, BaseStorage storage)
    {
        BotPrefab prefab = base.Create(parent, storage);

        BotsBase botsBase = storage.BotsBaseInfo;

        prefab.SetBase(storage);
        botsBase.AddCreatedBot(prefab.TargetInfoOwner);

        prefab.Init(_botsBasesSpawner, _playerCamera);

        return prefab;
    }
}
