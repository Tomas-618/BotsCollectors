public class IdleState : BotState
{
    public IdleState(Bot bot) : base(bot) { }

    public override void Enter()
    {
        base.Enter();
        BotInfo.ResetPath();
    }
}
