using System;
using UnityEngine;
using Zenject;

public class ResourcePoolComponent : MonoBehaviour
{
    [SerializeField] private Resource _resource;

    private ResourcesPool _entity;

    public void ReturnToPool() =>
        _entity.GetIn(_resource);

    [Inject]
    private void Construct(ResourcesPool entity) =>
        _entity = entity ?? throw new ArgumentNullException(nameof(entity));
}