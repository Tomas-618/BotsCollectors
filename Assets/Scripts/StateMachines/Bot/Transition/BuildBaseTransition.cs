public class BuildBaseTransition : BotTransition
{
    public BuildBaseTransition(BotState nextState, Bot bot) : base(nextState, bot) { }

    public override void Update()
    {
        if (BotInfo.IsNearestToTarget && BotInfo.CurrentTarget is Flag)
            Open();
    }
}
