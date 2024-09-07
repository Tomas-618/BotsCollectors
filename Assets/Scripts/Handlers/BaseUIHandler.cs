using System;
using UnityEngine;

public class BaseUIHandler : MonoBehaviour, IReadOnlyBaseUIHandler, IInitializable<BotsSpawner>
{
    [SerializeField, Min(0)] private int _resourcesCountToCreateBot;

    [SerializeField] private InteractableButton _button;
    [SerializeField] private BaseStorage _storage;

    private BotsSpawner _botsSpawner;

    public int ResourcesCountToCreate => _resourcesCountToCreateBot;

    private void OnEnable() =>
        _button.Clicked += OnClicked;

    private void OnDisable() =>
        _button.Clicked -= OnClicked;

    public void Init(BotsSpawner botsSpawner) =>
        _botsSpawner = botsSpawner != null ? botsSpawner : throw new ArgumentNullException(nameof(botsSpawner));

    public void OnClicked()
    {
        ResourcesBaseWallet wallet = _storage.Wallet;
        BotsBase botsBase = _storage.BotsBaseInfo;

        if (_botsSpawner.TrySpawnNewBot(_storage, out BotPrefab bot) == false)
            return;

        wallet.RemoveAmount(_resourcesCountToCreateBot);
    }
}
