public class OnBaseState : BotState
{
    public OnBaseState(Bot bot) : base(bot) { }

    public override void Enter()
    {
        base.Enter();
        BotInfo.PutResource();

        if (BotInfo.HasPriorityToBuildNewBase)
            BotInfo.CollectResourcesFromBaseToBuildNew();
    }
}
