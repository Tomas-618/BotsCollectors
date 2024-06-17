using UnityEngine;

public class Resource : MonoBehaviour
{
    public void EnableObject() =>
        gameObject.SetActive(true);

    public void DisableObject() =>
        gameObject.SetActive(true);

    public void Destroy() =>
        Destroy(gameObject);
}
