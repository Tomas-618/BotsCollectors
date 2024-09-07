using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ResourcesScanner : MonoBehaviour
{
    [SerializeField] private InterfaceReference<IReadOnlyResourcesSpawnerEvents, ResourcesSpawnerOnPlane> _spawnerEvents;

    private Queue<ITarget<BotInteractor>> _targets = new Queue<ITarget<BotInteractor>>();

    public event Action SpawnedNew;

    public int TargetsCount => _targets.Count;

    public bool HasTargets => TargetsCount > 0;

    private void OnEnable() =>
        _spawnerEvents.Value.Spawned += OnSpawned;

    private void OnDisable() =>
        _spawnerEvents.Value.Spawned -= OnSpawned;

    public ITarget<BotInteractor> TakeTheNearestTargetToBase(BotsBase @base)
    {
        if (@base == null)
            throw new ArgumentNullException(nameof(@base));

        _targets = new Queue<ITarget<BotInteractor>>(_targets
            .OrderBy(target => Vector3.Distance(@base.transform.position, target.TransformInfo.position)));

        return _targets.Dequeue();
    }

    private void OnSpawned(ITarget<BotInteractor>[] resources)
    {
        _targets = new Queue<ITarget<BotInteractor>>(resources);

        SpawnedNew?.Invoke();
    }
}
