using UnityEngine;
using Unity.AI.Navigation;

[RequireComponent(typeof(NavMeshSurface))]
public class NavMeshSurfaceBuilder : MonoBehaviour
{
    [SerializeField] private InterfaceReference<IReadOnlyBasesSpawnerEvents, BasesSpawner> _basesSpawnerEvents;

    private NavMeshSurface _surface;

    private void Awake() =>
        _surface = GetComponent<NavMeshSurface>();

    private void OnEnable() =>
        _basesSpawnerEvents.Value.Spawned += Build;

    private void OnDisable() =>
        _basesSpawnerEvents.Value.Spawned -= Build;

    private void Build() =>
        _surface.BuildNavMesh();
}
