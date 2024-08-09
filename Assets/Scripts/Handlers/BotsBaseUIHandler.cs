using System;
using UnityEngine;

public class BotsBaseUIHandler : MonoBehaviour, IReadOnlyBotsSpawner
{
    [SerializeField, Min(0)] private int _resourcesCountToCreateNewBot;

    [SerializeField] private InteractableButton _button;
    [SerializeField] private BotsBase _base;

    private BotsSpawner _botsSpawner;

    public int ResourcesCountToCreate => _resourcesCountToCreateNewBot;

    private void OnEnable() =>
        _button.Clicked += OnClicked;

    private void OnDisable() =>
        _button.Clicked -= OnClicked;

    public void Init(BotsSpawner botsSpawner) =>
        _botsSpawner = botsSpawner != null ? botsSpawner : throw new ArgumentNullException(nameof(botsSpawner));

    public void OnClicked()
    {
        if (_botsSpawner.TrySpawnNewBot(_base, out Bot newBot) == false)
            return;

        _base.SpendResources(_resourcesCountToCreateNewBot);
        _base.AddNewEntity(newBot);
    }
}
