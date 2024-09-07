using System;

public class BotMoveToTargetState : BotState
{
    private readonly BotMover _mover;

    public BotMoveToTargetState(TargetInfoOwner bot, BotMover mover) : base(bot) =>
        _mover = mover != null ? mover : throw new ArgumentNullException(nameof(mover));

    public override void Enter()
    {
        base.Enter();

        _mover.Move(BotInfo.CurrentTarget.TransformInfo.position);

        BotInfo.CurrentTarget.PositionChanged += _mover.Move;
    }

    public override void Exit() =>
        BotInfo.CurrentTarget.PositionChanged -= _mover.Move;
}
