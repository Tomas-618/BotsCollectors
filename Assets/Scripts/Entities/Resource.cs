using UnityEngine;

[RequireComponent(typeof(ResourcePoolComponent))]
public class Resource : MonoBehaviour
{
    public void EnableObject() =>
        gameObject.SetActive(true);

    public void DisableObject() =>
        gameObject.SetActive(false);

    public void Destroy() =>
        Destroy(gameObject);
}
