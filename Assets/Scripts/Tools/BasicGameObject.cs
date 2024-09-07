using UnityEngine;

public abstract class BasicGameObject : MonoBehaviour
{
    public void EnableObject()
    {
        if (gameObject.activeSelf == false)
            gameObject.SetActive(true);
    }

    public void DisableObject()
    {
        if (gameObject.activeSelf)
            gameObject.SetActive(false);
    }

    public void DestroyObject() =>
        Destroy(gameObject);
}
