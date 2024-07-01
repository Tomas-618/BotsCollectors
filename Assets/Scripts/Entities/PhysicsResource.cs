using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(MeshCollider))]
public class PhysicsResource : MonoBehaviour
{
    [SerializeField] private NavMeshObstacle _obstacle;
    [SerializeField] private Animator _animator;

    private MeshCollider _meshCollider;

    private void Awake() =>
        _meshCollider = GetComponent<MeshCollider>();

    public void Enable()
    {
        enabled = true;
        _meshCollider.enabled = true;
        _obstacle.enabled = false;
        _animator.enabled = false;
    }

    public void Disable()
    {
        enabled = false;
        _meshCollider.enabled = false;
        _obstacle.enabled = true;
        _animator.enabled = true;
    }
}
