using UnityEngine;

public class Resource : MonoBehaviour, ITarget
{
    [field: SerializeField] public ResourcePoolComponent PoolComponent { get; private set; }

    public Transform TransformInfo { get; private set; }

    private void Awake() =>
        TransformInfo = transform;

    public void EnableObject() =>
    gameObject.SetActive(true);

    public void DisableObject() =>
        gameObject.SetActive(false);

    public void Destroy() =>
        Destroy(gameObject);
}
