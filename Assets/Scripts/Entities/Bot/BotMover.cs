using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class BotMover : MonoBehaviour
{
    private NavMeshAgent _agent;

    private void Awake() =>
        _agent = GetComponent<NavMeshAgent>();

    public void Move(Vector3 target) =>
        _agent.SetDestination(target);

    public void Stop() =>
        _agent.ResetPath();
}
