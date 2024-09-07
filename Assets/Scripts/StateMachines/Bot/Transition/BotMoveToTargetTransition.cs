public class BotMoveToTargetTransition : BotTransition
{
    public BotMoveToTargetTransition(BotState nextState, TargetInfoOwner bot) : base(nextState, bot) { }

    public override void Update()
    {
        if (BotInfo.HasTarget)
            Open();
    }
}
