using System;
using BasicStateMachine;

public abstract class BotState : State
{
    private readonly TargetInfoOwner _bot;

    protected BotState(TargetInfoOwner bot) =>
        _bot = bot != null ? bot : throw new ArgumentNullException(nameof(bot));

    public TargetInfoOwner BotInfo => _bot;
}
