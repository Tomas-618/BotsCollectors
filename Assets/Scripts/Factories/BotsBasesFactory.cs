using System;
using UnityEngine;

public class BotsBasesFactory : MonoBehaviourFactory<BotsBase>
{
    private readonly Camera _playerCamera;
    private readonly ResourcesGround _resourcesGround;
    private readonly PlayerCameraRaycaster _cameraRaycaster;
    private readonly BotsFactory _botsFactory;

    public BotsBasesFactory(BotsBase prefab, Camera playerCamera,
        ResourcesGround resourcesGround, PlayerCameraRaycaster cameraRaycaster, BotsFactory botsFactory) : base(prefab)
    {
        _playerCamera = playerCamera != null ? playerCamera : throw new ArgumentNullException(nameof(playerCamera));
        _resourcesGround = resourcesGround != null ? resourcesGround : throw new ArgumentNullException(nameof(resourcesGround));
        _cameraRaycaster = cameraRaycaster != null ? cameraRaycaster : throw new ArgumentNullException(nameof(cameraRaycaster));
        _botsFactory = botsFactory ?? throw new ArgumentNullException(nameof(botsFactory));
    }

    public override BotsBase Create(Transform parent)
    {
        BotsBase entity = base.Create(parent);

        entity.Init(_resourcesGround, _cameraRaycaster, _playerCamera);

        return entity;
    }
}
