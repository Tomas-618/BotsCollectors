using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class Bot : MonoBehaviour
{
    private readonly Queue<Resource> _targetPoint = new Queue<Resource>();

    private NavMeshAgent _agent;
    private Resource _resource;

    public bool HasResources => _targetPoint.Count > 0;

    public bool IsArrived => _agent.remainingDistance < _agent.stoppingDistance;

    private void Awake() =>
        _agent = GetComponent<NavMeshAgent>();

    public Resource TakeResource()
    {
        Resource resource = _resource;

        _resource = null;

        return resource;
    }

    public void CollectResource() =>
        _resource = _targetPoint.Dequeue();

    public Vector3 GetCurrentTargetPoint() =>
        _targetPoint.Peek().transform.position;

    public void AddNewResourceTarget(Resource targetPoint) =>
        _targetPoint.Enqueue(targetPoint ?? throw new ArgumentNullException(nameof(targetPoint)));

    public void SetDestination(Vector3 destination) =>
        _agent.SetDestination(destination);
}
