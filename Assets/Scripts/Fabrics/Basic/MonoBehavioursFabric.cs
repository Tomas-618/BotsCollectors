using System;
using UnityEngine;
using static UnityEngine.Object;

public abstract class MonoBehavioursFabric<T> : IFabric<T> where T : MonoBehaviour
{
    private readonly T _prefab;
    private readonly Transform _parent;

    public MonoBehavioursFabric(T prefab, Transform parent)
    {
        _prefab = prefab ?? throw new ArgumentNullException(nameof(prefab));
        _parent = parent ?? throw new ArgumentNullException(nameof(parent));
    }

    public virtual T Create() =>
        Instantiate(_prefab, _parent);
}
