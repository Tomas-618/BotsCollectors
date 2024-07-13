public class BuildBaseState : BotState
{
    public BuildBaseState(Bot bot) : base(bot) { }

    public override void Enter()
    {
        base.Enter();
        BotInfo.BuildNewBase();
    }
}
