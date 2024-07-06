using UnityEngine;

public class Flag : BasicGameObject, ITarget
{
    public Transform TransformInfo { get; private set; }

    private void Awake() =>
        TransformInfo = transform;

    public void SetPosition(Vector3 position) =>
        TransformInfo.position = position;
}
