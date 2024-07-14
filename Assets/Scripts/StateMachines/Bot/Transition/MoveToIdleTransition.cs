public class MoveToIdleTransition : BotTransition
{
    public MoveToIdleTransition(BotState nextState, Bot bot) : base(nextState, bot) { }

    private void Open(int count)
    {
        if (BotInfo.HasTargets == false)
            Open();
    }
}
