public class CollectState : BotState
{
    public CollectState(Bot bot) : base(bot) { }

    public override void Enter()
    {
        base.Enter();
        BotInfo.CollectResource();
    }
}
