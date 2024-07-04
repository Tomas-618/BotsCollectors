using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using BasicStateMachine;

[SelectionBase, RequireComponent(typeof(NavMeshAgent))]
public class Bot : MonoBehaviour, IReadOnlyBot
{
    private readonly Queue<ITarget> _targets = new Queue<ITarget>();

    [SerializeField, Min(0)] private float _distanceToInterract;

    [SerializeField] private BotHand _hand;

    private StateMachine<BotState, BotTransition> _stateMachine;
    private NavMeshAgent _agent;

    [field: SerializeField] public TransformLooker UILooker { get; private set; }

    public IReadOnlyBotHandEvents HandEvents => _hand;

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
            _hand.Take(resource);
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

    public void PutResource()
    {
        Resource resource = _hand.Throw();

        (resource != null ? resource : throw new ArgumentNullException(nameof(resource))).PoolComponent
            .ReturnToPool();

        _targets.Dequeue();
    }
}
