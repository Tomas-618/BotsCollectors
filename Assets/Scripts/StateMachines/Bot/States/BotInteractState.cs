using System;

public class BotInteractState : BotState
{
    private readonly BotInteractor _interactor;

    public BotInteractState(TargetInfoOwner bot, BotInteractor interactor) : base(bot) =>
        _interactor = interactor != null ? interactor : throw new ArgumentNullException(nameof(interactor));

    public override void Enter()
    {
        base.Enter();

        _interactor.Interact(BotInfo.CurrentTarget);
        BotInfo.RemoveCurrentTarget();
    }
}
