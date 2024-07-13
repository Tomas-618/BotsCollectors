public class IdleTransition : BotTransition
{
    public IdleTransition(BotState nextState, Bot bot) : base(nextState, bot)
    {
        BotInfo.HandEvents.ResourceTaken += Open;
        BotInfo.HandEvents.ResourceThrew += Open;
        BotInfo.CollectedResourcesFromBase += Open;
        BotInfo.BaseBuilt += Open;
    }

    public void Dispose()
    {
        BotInfo.HandEvents.ResourceTaken -= Open;
        BotInfo.HandEvents.ResourceThrew -= Open;
        BotInfo.CollectedResourcesFromBase -= Open;
        BotInfo.BaseBuilt -= Open;
    }

    private void Open(int count) =>
        Open();
}
