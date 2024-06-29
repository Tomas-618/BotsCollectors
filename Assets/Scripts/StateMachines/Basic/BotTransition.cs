using System;
using BasicStateMachine;

public abstract class BotTransition : Transition<BotState, BotTransition>
{
    private readonly Bot _bot;

    public BotTransition(BotState nextState, Bot bot) : base(nextState) =>
        _bot = bot ?? throw new ArgumentNullException(nameof(bot));

    protected Bot BotInfo => _bot;
}
