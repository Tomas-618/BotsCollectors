using System;
using System.Collections.Generic;

public class BotStateMachine
{
    private readonly Dictionary<Type, BotState> _allStates;

    private BotState _current;

    public BotStateMachine(BotStatesFabric fabric, Bot bot)
    {
        _allStates = (fabric ?? throw new ArgumentNullException(nameof(fabric))).Create(this, bot);
        _current = _allStates[typeof(BotIdleState)];
    }

    public void Update() =>
        _current?.Update();

    public void SetState<TState>() where TState : BotState
    {
        if (_allStates.TryGetValue(typeof(TState), out BotState newState))
        {
            _current?.Exit();
            _current = newState ?? throw new ArgumentNullException(nameof(newState));
            _current.Enter();
        }
    }
}
