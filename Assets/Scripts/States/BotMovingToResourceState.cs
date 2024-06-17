using UnityEngine;

public class BotMovingToResourceState : BotState
{
    public BotMovingToResourceState(BotStateMachine stateMachine, Bot bot) : base(stateMachine, bot) { }

    public override void Update()
    {
        if (Entity.IsArrived)
            StateMachine.SetState<BotMovingToBaseState>();
    }

    public override void Enter()
    {
        Vector3 destination = Entity.GetCurrentTargetPoint();

        Entity.SetDestination(destination);
    }

    public override void Exit()
    {
        Entity.CollectResource();
        Entity.HideCurrentResource();
    }
}
