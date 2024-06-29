public class PutState : BotState
{
    public PutState(Bot bot) : base(bot) { }

    public override void Enter()
    {
        base.Enter();
        BotInfo.PutAllResources();
    }
}
