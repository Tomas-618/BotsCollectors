public class MoveState : BotState
{
    public MoveState(Bot bot) : base(bot) { }

    public override void Enter()
    {
        base.Enter();
        BotInfo.Move();
    }
}
