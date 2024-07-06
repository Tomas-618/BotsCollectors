public class IdleTransition : BotTransition
{
    public IdleTransition(BotState nextState, Bot bot) : base(nextState, bot)
    {
        BotInfo.HandEvents.ResourceTaken += Open;
        BotInfo.HandEvents.ResourceThrew += Open;
    }

    public void Dispose()
    {
        BotInfo.HandEvents.ResourceTaken -= Open;
        BotInfo.HandEvents.ResourceThrew -= Open;
    }

    private void Open(int count) =>
        Open();
}
