using System;
using UnityEngine;

public class TransformLooker : MonoBehaviour
{
    [SerializeField] private Transform _target;

    private Transform _transform;

    private void Start() =>
        _transform = transform;

    private void LateUpdate() =>
        _transform.LookAt(_target.position);

    public void Init(Transform target) =>
        _target = target != null ? target : throw new ArgumentNullException(nameof(target));
}
