using System;
using UnityEngine;

public class TransformLooker : MonoBehaviour
{
    [SerializeField] private Canvas _canvas;

    private Transform _transform;
    private Transform _target;

    private void Start() =>
        _transform = transform;

    private void LateUpdate() =>
        _transform.LookAt(_target.position);

    public void Init(Camera target)
    {
        _target = (target != null ? target : throw new ArgumentNullException(nameof(target))).transform;
        _canvas.worldCamera = target;
    }
}
