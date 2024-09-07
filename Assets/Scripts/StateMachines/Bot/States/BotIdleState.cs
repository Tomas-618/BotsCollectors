using System;

public class BotIdleState : BotState
{
    private readonly BotsBaseInfoOwner _base;

    public BotIdleState(TargetInfoOwner bot, BotsBaseInfoOwner @base) : base(bot) =>
        _base = @base != null ? @base : throw new ArgumentNullException(nameof(@base));

    public override void Enter()
    {
        base.Enter();

        _base.Target.Add(BotInfo);
    }
}
