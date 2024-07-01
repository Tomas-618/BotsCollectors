using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Resource : MonoBehaviour, ITarget
{
    [field: SerializeField] public ResourcePoolComponent PoolComponent { get; private set; }

    [field: SerializeField] public PhysicsResource Physics { get; private set; }

    public Transform TransformInfo { get; private set; }

    public Rigidbody RigidbodyInfo { get; private set; }

    private void Awake()
    {
        TransformInfo = transform;
        RigidbodyInfo = GetComponent<Rigidbody>();
    }

    public void EnableObject() =>
    gameObject.SetActive(true);

    public void DisableObject()
    {
        TransformInfo.rotation = Quaternion.identity;
        gameObject.SetActive(false);
    }

    public void Destroy() =>
        Destroy(gameObject);
}
