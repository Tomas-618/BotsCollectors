public class PutTransition : BotTransition
{
    public PutTransition(BotState nextState, Bot bot) : base(nextState, bot) { }

    public override void Update()
    {
        if (BotInfo.IsNearestToTarget && BotInfo.CurrentTarget is BotsBase)
            Open();
    }
}
