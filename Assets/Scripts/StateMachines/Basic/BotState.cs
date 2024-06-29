using System;
using BasicStateMachine;

public abstract class BotState : State<BotState, BotTransition>
{
    private readonly Bot _bot;

    protected BotState(Bot bot) =>
        _bot = bot ?? throw new ArgumentNullException(nameof(bot));

    protected Bot BotInfo => _bot;
}
