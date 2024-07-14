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

    public event Action StartedToBuildBase;

    public event Action CollectedResourcesFromBase;

    [field: SerializeField] public TransformLooker UILooker { get; private set; }

    public IReadOnlyBotHandEvents HandEvents => _hand;

    public ITarget CurrentTarget => HasTargets ? _targets.Peek() : null;

    public bool HasTargets => _targets.Count > 0;

    public bool HasPriorityToBuildNewBase { get; private set; }

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

    public ITarget[] TakeAllTargets()
    {
        ITarget[] targets = _targets.ToArray();

        _targets.Clear();

        return targets;
    }

    public void Collect()
    {
        if (CurrentTarget is Resource resource)
        {
            _hand.Take(resource);
            RemoveCurrentTarget();
        }
    }

    public void AddResourcesAsTargets(ITarget[] resources, BotsBase @base)
    {
        if (_hand.HasResource)
            AddTarget(@base);

        foreach (ITarget resource in resources)
        {
            AddTarget(resource);
            AddTarget(@base);
        }
    }

    public void AddTarget(ITarget target) =>
        _targets.Enqueue(target ?? throw new ArgumentNullException(nameof(target)));

    public void MoveToCurrentTarget()
    {
        if (CurrentTarget == null)
            return;

        _agent.SetDestination(CurrentTarget.TransformInfo.position);
    }

    public void BuildNewBase()
    {
        if (CurrentTarget is Flag flag)
        {
            flag.PositionChanged -= MoveToCurrentTarget;
            flag.DisableObject();
            RemoveCurrentTarget();

            StartedToBuildBase?.Invoke();
        }
    }

    public void SetPriorityToBuildNewBase(Flag flag)
    {
        flag.PositionChanged += MoveToCurrentTarget;
        HasPriorityToBuildNewBase = true;
    }

    public void ResetPath() =>
        _agent.ResetPath();

    public void PutResource()
    {
        Resource resource = _hand.Throw();

        if (resource != null)
            resource.PoolComponent.ReturnToPool();
    }

    public void CollectResourcesFromBase()
    {
        if (CurrentTarget is BotsBase @base)
        {
            HasPriorityToBuildNewBase = false;
            @base.SpendResources(@base.ResourcesCountToCreateNew);

            CollectedResourcesFromBase?.Invoke();
        }
    }

    public void RemoveCurrentTarget()
    {
        if (HasTargets == false)
            return;

        _targets.Dequeue();
    }
}
