using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using BasicStateMachine;

[SelectionBase, RequireComponent(typeof(NavMeshAgent))]
public class Bot : MonoBehaviour, IReadOnlyBotsEvents
{
    private readonly Queue<ITarget> _targets = new Queue<ITarget>();
    private readonly Queue<Resource> _resources = new Queue<Resource>();

    [SerializeField, Min(0)] private float _distanceToInterract;

    private NavMeshAgent _agent;
    private StateMachine<BotState, BotTransition> _stateMachine;

    public event Action<int> ResourceCollected;

    public event Action<int> ResourcesPut;

    public ITarget CurrentTarget => HasTargets ? _targets.Peek() : null;

    public bool HasTargets => _targets.Count > 0;

    public bool IsNearestToTarget
    {
        get
        {
            if (CurrentTarget == null)
                return true;

            return Vector3.Distance(transform.position, CurrentTarget.TransformInfo.position) <= _distanceToInterract;
        }
    }

    private void Awake()
    {
        BotStateMachineFactory stateMachineFactory = new BotStateMachineFactory(this);

        _agent = GetComponent<NavMeshAgent>();
        _stateMachine = stateMachineFactory.Create();
    }

    private void Update() =>
        _stateMachine.Update();

    public void Collect()
    {
        if ((CurrentTarget ?? throw new ArgumentNullException(nameof(CurrentTarget))) is Resource resource)
        {
            _targets.Dequeue();
            _resources.Enqueue(resource);
            resource.DisableObject();
            ResourceCollected?.Invoke(1);
        }
    }

    public void AddTarget(ITarget target) =>
        _targets.Enqueue(target ?? throw new ArgumentNullException(nameof(target)));

    public void Move()
    {
        if (CurrentTarget == null)
            return;

        _agent.SetDestination(CurrentTarget.TransformInfo.position);
    }

    public void ResetPath() =>
        _agent.ResetPath();

    public void PutAllResources()
    {
        foreach (Resource resource in _resources)
            (resource ?? throw new ArgumentNullException(nameof(resource))).PoolComponent.ReturnToPool();

        ResourcesPut?.Invoke(_resources.Count);
        _resources.Clear();
        _targets.Dequeue();
    }
}
