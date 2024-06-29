public class CollectTransition : BotTransition
{
    public CollectTransition(BotState nextState, Bot bot) : base(nextState, bot) { }

    public override void Update()
    {
        if (BotInfo.IsNearestToTarget && BotInfo.CurrentTarget is Resource)
            Open();
    }
}
