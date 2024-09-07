using System;
using UnityEngine;

public class TransformLooker : MonoBehaviour, IInitializable<PlayerCamera>
{
    [SerializeField] private Canvas _canvas;

    private Transform _transform;
    private Transform _target;

    private void Start() =>
        _transform = transform;

    private void LateUpdate() =>
        _transform.LookAt(_target.position);

    public void Init(PlayerCamera target)
    {
        _target = (target != null ? target : throw new ArgumentNullException(nameof(target))).transform;
        _canvas.worldCamera = target.CameraInfo;
    }
}
