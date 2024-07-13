using System;
using UnityEngine;

public class Flag : BasicGameObject, ITarget
{
    public event Action Enabled;

    public event Action PositionChanged;

    public Transform TransformInfo { get; private set; }

    private void Awake() =>
        TransformInfo = transform;

    private void OnEnable() =>
        Enabled?.Invoke();

    public void SetPosition(Vector3 position)
    {
        TransformInfo.position = position;
        PositionChanged?.Invoke();
    }
}
