using System;
using UnityEngine;

public class BasesFactory : MonoBehaviourFactory<BasePrefab>
{
    private readonly ResourcesScanner _resourcesScanner;
    private readonly PlayerCameraRaycaster _cameraRaycaster;
    private readonly BotsSpawner _botsSpawner;

    public BasesFactory(BasePrefab prefab, ResourcesScanner resourcesGround,
        PlayerCameraRaycaster cameraRaycaster, BotsSpawner botsSpawner) : base(prefab)
    {
        _resourcesScanner = resourcesGround != null ? resourcesGround : throw new ArgumentNullException(nameof(resourcesGround));
        _cameraRaycaster = cameraRaycaster != null ? cameraRaycaster : throw new ArgumentNullException(nameof(cameraRaycaster));
        _botsSpawner = botsSpawner != null ? botsSpawner : throw new ArgumentNullException(nameof(botsSpawner));
    }

    public override BasePrefab Create(Transform parent)
    {
        BasePrefab prefab = base.Create(parent);

        prefab.Init(_resourcesScanner, _cameraRaycaster, _botsSpawner);

        return prefab;
    }
}
