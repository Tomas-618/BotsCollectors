public class BotIdleState : BotState
{
    public BotIdleState(BotStateMachine stateMachine, Bot bot) : base(stateMachine, bot) { }

    public override void Update()
    {
        if (Entity.HasResources)
            StateMachine.SetState<BotMovingToResourceState>();
    }

    public override void Enter() { }

    public override void Exit() { }
}
