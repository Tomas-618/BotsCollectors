using System;

public abstract class BotState
{
    private readonly BotStateMachine _stateMachine;
    private readonly Bot _entity;

    public BotState(BotStateMachine stateMachine, Bot bot)
    {
        _stateMachine = stateMachine ?? throw new ArgumentNullException(nameof(stateMachine));
        _entity = bot != null ? bot : throw new ArgumentNullException(nameof(bot));
    }

    protected BotStateMachine StateMachine => _stateMachine;

    protected Bot Entity => _entity;

    public abstract void Update();

    public abstract void Enter();

    public abstract void Exit();
}
