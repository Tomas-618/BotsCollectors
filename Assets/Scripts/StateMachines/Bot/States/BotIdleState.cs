using System;

public class BotIdleState : BotState
{
    private readonly BotMover _mover;
    private readonly BotsBaseInfoOwner _base;

    public BotIdleState(TargetInfoOwner bot, BotMover mover, BotsBaseInfoOwner @base) : base(bot)
    {
        _mover = mover != null ? mover : throw new ArgumentNullException(nameof(mover));
        _base = @base != null ? @base : throw new ArgumentNullException(nameof(@base));
    }

    public override void Enter()
    {
        base.Enter();

        _mover.Stop();
        _base.Target.Add(BotInfo);
    }
}
