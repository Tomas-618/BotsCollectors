public class IdleTransition : BotTransition
{
    public IdleTransition(BotState nextState, Bot bot) : base(nextState, bot)
    {
        BotInfo.ResourceCollected += Open;
        BotInfo.ResourcesPut += Open;
    }

    public void Dispose()
    {
        BotInfo.ResourceCollected -= Open;
        BotInfo.ResourcesPut -= Open;
    }

    private void Open(int count) =>
        Open();
}
