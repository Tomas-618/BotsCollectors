using System;
using BasicStateMachine;

public class BotStateMachineFactory : IFabric<StateMachine<BotState, BotTransition>>
{
    private readonly Bot _bot;

    public BotStateMachineFactory(Bot bot) =>
        _bot = bot != null ? bot : throw new ArgumentNullException(nameof(bot));

    public StateMachine<BotState, BotTransition> Create()
    {
        IdleState idleState = new IdleState(_bot);
        MoveState moveState = new MoveState(_bot);
        CollectState collectState = new CollectState(_bot);
        PutState putState = new PutState(_bot);

        IdleTransition idleTransition = new IdleTransition(idleState, _bot);
        MoveTransition moveTransition = new MoveTransition(moveState, _bot);
        CollectTransition collectTransition = new CollectTransition(collectState, _bot);
        PutTransition putTransition = new PutTransition(putState, _bot);

        idleState.AddTransition(moveTransition);

        moveState.AddTransition(collectTransition);
        moveState.AddTransition(putTransition);

        collectState.AddTransition(idleTransition);
        putState.AddTransition(idleTransition);

        return new StateMachine<BotState, BotTransition>(idleState);
    }
}
