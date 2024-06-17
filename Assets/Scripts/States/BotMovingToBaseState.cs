using System;
using UnityEngine;

public class BotMovingToBaseState : BotState
{
    private readonly BotsBase _base;

    public BotMovingToBaseState(BotStateMachine stateMachine, Bot bot, BotsBase @base) : base(stateMachine, bot) =>
        _base = @base != null ? @base : throw new ArgumentNullException(nameof(@base));

    public override void Update()
    {
        if (Entity.IsArrived)
            StateMachine.SetState<BotIdleState>();
    }

    public override void Enter()
    {
        Vector3 destination = _base.transform.position;

        Entity.SetDestination(destination);
    }

    public override void Exit() { }
}
