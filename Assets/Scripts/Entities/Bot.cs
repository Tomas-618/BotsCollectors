using System;
using UnityEngine;
using UnityEngine.AI;
using BasicStateMachine;

[SelectionBase, RequireComponent(typeof(NavMeshAgent))]
public class Bot : MonoBehaviour, IReadOnlyBot
{
    [SerializeField, Min(0)] private float _distanceToInterract;

    [SerializeField] private BotHand _hand;
    [SerializeField] private BotsBase _base;

    private StateMachine<BotState, BotTransition> _stateMachine;
    private BotsBasesSpawner _basesSpawner;
    private NavMeshAgent _agent;
    private Transform _transform;

    public event Action BuiltBase;

    public event Action CollectedResourcesFromBase;

    [field: SerializeField] public TransformLooker UILooker { get; private set; }

    public IReadOnlyBotHandEvents HandEvents => _hand;

    public ITarget CurrentTarget { get; private set; }

    public bool HasTarget => CurrentTarget != null;

    public bool HasPriorityToBuildNewBase { get; private set; }

    public bool IsNearestToTarget
    {
        get
        {
            if (HasTarget == false)
                return true;

            return Vector3.Distance(transform.position, CurrentTarget.TransformInfo.position) <= _distanceToInterract;
        }
    }

    private void Awake()
    {
        BotStateMachineFactory stateMachineFactory = new BotStateMachineFactory(this);

        _transform = transform;
        _agent = GetComponent<NavMeshAgent>();
        _stateMachine = stateMachineFactory.Create();
    }

    private void Update() =>
        _stateMachine.Update();

    public void SetTarget(ITarget target) =>
        CurrentTarget = target ?? throw new ArgumentNullException(nameof(target));

    public void ResetPath() =>
        _agent.ResetPath();

    public void MoveToTarget()
    {
        if (HasTarget)
            _agent.SetDestination(CurrentTarget.TransformInfo.position);
    }

    public void CollectResource()
    {
        if (CurrentTarget is Resource resource)
        {
            _hand.Take(resource);
            SetTarget(_base);
        }
    }

    public void PutResource()
    {
        Resource resource = _hand.Throw();

        if (resource == null)
            return;

        _base.PutResource(this, resource);
        resource.PoolComponent.ReturnToPool();
    }

    public void SetPriorityToBuildNewBase(Flag flag)
    {
        (flag ?? throw new ArgumentNullException(nameof(flag))).PositionChanged += MoveToTarget;
        HasPriorityToBuildNewBase = true;

        if (HasTarget == false)
            SetTarget(_base);
    }

    public void CollectResourcesFromBaseToBuildNew()
    {
        _base.SpendResources(_base.ResourcesCountToCreateNew);
        SetTarget(_base.FlagInfo);

        CollectedResourcesFromBase?.Invoke();
    }

    public void SetBase(BotsBase @base, Transform parent)
    {
        _base = @base != null ? @base : throw new ArgumentNullException(nameof(@base));

        _transform.SetParent(parent);
    }

    public void SetBasesSpawner(BotsBasesSpawner basesSpawner) =>
        _basesSpawner = basesSpawner != null ? basesSpawner : throw new ArgumentNullException(nameof(basesSpawner));

    public void BuildNewBase()
    {
        if (CurrentTarget is Flag flag)
        {
            flag.PositionChanged -= MoveToTarget;
            flag.DisableObject();

            RemoveCurrentTarget();
            HasPriorityToBuildNewBase = false;

            _basesSpawner.SpawnByBot(this, flag.transform.position);

            BuiltBase?.Invoke();
        }
    }

    public void RemoveCurrentTarget() =>
        CurrentTarget = null;
}
