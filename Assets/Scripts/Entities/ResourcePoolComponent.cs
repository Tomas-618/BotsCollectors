using System;
using UnityEngine;

public class ResourcePoolComponent : MonoBehaviour
{
    [SerializeField] private Resource _resource;

    private ResourcesPool _entity;

    public void Init(ResourcesPool entity) =>
        _entity = entity ?? throw new ArgumentNullException(nameof(entity));

    public void ReturnToPool() =>
        _entity.PutEntityIn(_resource);
}