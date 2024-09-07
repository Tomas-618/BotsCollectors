using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Resource : BasicGameObject, ITarget<BotInteractor>
{
    [field: SerializeField] public ResourcePoolComponent PoolComponent { get; private set; }

    [field: SerializeField] public PhysicsResource Physics { get; private set; }

    public Transform TransformInfo { get; private set; }

    public Rigidbody RigidbodyInfo { get; private set; }

    public event Action<Vector3> PositionChanged;

    private void Awake()
    {
        TransformInfo = transform;
        RigidbodyInfo = GetComponent<Rigidbody>();
    }

    private void OnDisable()
    {
        TransformInfo.rotation = Quaternion.identity;
        Physics.transform.localPosition = Vector3.zero;
    }

    public void Interact(BotInteractor interactor) =>
        interactor.Hand.Take(this);
}
