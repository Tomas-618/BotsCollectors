using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ResourcesGround : MonoBehaviour
{
    [SerializeField] private InterfaceReference<IReadOnlyResourcesSpawnerEvents, ResourcesSpawnerOnPlane> _spawnerEvents;

    private Queue<ITarget> _targets = new Queue<ITarget>();

    public event Action SpawnedNew;

    public int TargetsCount => _targets.Count;

    public bool HasTargets => _targets.Count > 0;

    private void OnEnable() =>
        _spawnerEvents.Value.Spawned += OnSpawned;

    private void OnDisable() =>
        _spawnerEvents.Value.Spawned -= OnSpawned;

    public ITarget TakeTheNearestTargetToBase(BotsBase @base)
    {
        if (@base == null)
            throw new ArgumentNullException(nameof(@base));

        _targets = new Queue<ITarget>(_targets
            .OrderBy(target => Vector3.Distance(@base.TransformInfo.position, target.TransformInfo.position)));

        return _targets.Dequeue();
    }

    private void OnSpawned(ITarget[] resources)
    {
        _targets = new Queue<ITarget>(resources);

        SpawnedNew?.Invoke();
    }
}
