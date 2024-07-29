﻿using System;
using UnityEngine;
using Zenject;

public class BotsBasesSpawner : MonoBehaviour
{
    private BotsBasesFactory _factory;

    [SerializeField] private Camera _playerCamera;
    [SerializeField] private ResourcesGround _resourcesGround;
    [SerializeField] private Transform _parent;
    [SerializeField] private float _height;

    public void Spawn(Bot bot, Vector3 spawnPosition)
    {
        if (bot == null)
            throw new ArgumentNullException(nameof(bot));

        BotsBase @base = _factory.Create(_parent);

        spawnPosition.y = _height;

        @base.Init(_resourcesGround, _playerCamera);
        @base.AddNewEntity(bot);
        @base.transform.position = spawnPosition;

        bot.SetBase(@base);
    }

    [Inject]
    private void Construct(BotsBasesFactory factory) =>
        _factory = factory ?? throw new ArgumentNullException(nameof(factory));
}
