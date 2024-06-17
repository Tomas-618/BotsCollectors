using System;
using System.Collections.Generic;

public class BotStatesFabric : IFabric<Dictionary<Type, BotState>, BotStateMachine, Bot>
{
    private readonly BotsBase _base;

    public BotStatesFabric(BotsBase @base) =>
        _base = @base ?? throw new ArgumentNullException(nameof(@base));

    public Dictionary<Type, BotState> Create(BotStateMachine stateMachine, Bot bot)
    {
        return new Dictionary<Type, BotState>
        {
            [typeof(BotIdleState)] = new BotIdleState(stateMachine, bot),
            [typeof(BotMovingToResourceState)] = new BotMovingToResourceState(stateMachine, bot),
            [typeof(BotMovingToBaseState)] = new BotMovingToBaseState(stateMachine, bot, _base),
        };
    }
}
