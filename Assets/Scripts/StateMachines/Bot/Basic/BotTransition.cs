using System;
using BasicStateMachine;

public abstract class BotTransition : Transition<BotState, BotTransition>
{
    private readonly TargetInfoOwner _bot;

    protected BotTransition(BotState nextState, TargetInfoOwner bot) : base(nextState) =>
        _bot = bot != null ? bot : throw new ArgumentNullException(nameof(bot));

    public TargetInfoOwner BotInfo => _bot;
}
