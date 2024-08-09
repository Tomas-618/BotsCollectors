using System;
using UnityEngine;
using Zenject;

[RequireComponent(typeof(Camera))]
public class PlayerCameraRaycaster : MonoBehaviour
{
    private PlayerCameraInput _input;
    private RaycasterHitInfoProvider _hitInfoProvider;

    public event Action Clicked;

    public RaycasterHitInfoProvider HitInfoProvider => _hitInfoProvider;

    private void Awake() =>
        _input = PlayerCameraInput.Instance;

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
