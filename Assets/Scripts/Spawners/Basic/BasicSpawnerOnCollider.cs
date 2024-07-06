using UnityEngine;

public abstract class BasicSpawnerOnCollider<T> : MonoBehaviour where T : Collider
{
    [SerializeField] private T _collider;
    [SerializeField] private float _height;

    protected Vector3 GetRandomPosition()
    {
        Vector3 randomPosition = _collider.GetRandomPosition();

        randomPosition.y = _height;

        return randomPosition;
    }
}
