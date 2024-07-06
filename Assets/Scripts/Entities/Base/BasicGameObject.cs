using UnityEngine;

public abstract class BasicGameObject : MonoBehaviour
{
    public virtual void EnableObject() =>
        gameObject.SetActive(true);

    public virtual void DisableObject() =>
        gameObject.SetActive(false);

    public virtual void DestroyObject() =>
        Destroy(gameObject);
}
