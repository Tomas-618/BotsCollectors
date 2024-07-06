public class MoveTransition : BotTransition
{
    public MoveTransition(BotState nextState, Bot bot) : base(nextState, bot) { }

    public override void Update()
    {
        if (BotInfo.HasTargets)
            Open();
    }
}
