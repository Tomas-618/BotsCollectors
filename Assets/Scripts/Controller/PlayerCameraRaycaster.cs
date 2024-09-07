using System;
using UnityEngine;
using Zenject;

public class PlayerCameraRaycaster : PlayerCamera
{
    [SerializeField] private PlayerCameraInput _input;

    private RaycasterHitInfoProvider _hitInfoProvider;

    public event Action Clicked;

    public RaycasterHitInfoProvider HitInfoProvider => _hitInfoProvider;

    private void OnEnable() =>
        _input.Clicked += OnClicked;

    private void OnDisable() =>
        _input.Clicked -= OnClicked;

    private void Update() =>
        _hitInfoProvider.Update();

    private void OnClicked() =>
        Clicked?.Invoke();

    [Inject]
    private void Construct(RaycasterHitInfoProvider hitInfoProvider) =>
        _hitInfoProvider = hitInfoProvider ?? throw new ArgumentNullException(nameof(hitInfoProvider));
}
