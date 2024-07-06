using System;
using UnityEngine;

public class BotsFactory : MonoBehaviourFactory<Bot>
{
    private Camera _playerCamera;

    public BotsFactory(Bot prefab, Camera playerCamera) : base(prefab) =>
        _playerCamera = playerCamera != null ? playerCamera : throw new ArgumentNullException(nameof(playerCamera));

    public override Bot Create(Transform parent)
    {
        Bot entity = base.Create(parent);

        entity.UILooker.Init(_playerCamera.transform);

        return entity;
    }
}
