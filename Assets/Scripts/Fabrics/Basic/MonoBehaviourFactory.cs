using System;
using UnityEngine;
using static UnityEngine.Object;

public abstract class MonoBehaviourFactory<T> where T : MonoBehaviour
{
    private readonly T _prefab;

    public MonoBehaviourFactory(T prefab) =>
        _prefab = prefab != null ? prefab : throw new ArgumentNullException(nameof(prefab));

    public virtual T Create(Transform parent) =>
        Instantiate(_prefab, parent != null ? parent : throw new ArgumentNullException(nameof(parent)));
}
