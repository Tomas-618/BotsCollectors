using System;

public class BotService
{
    private Bot _bot;
    private BotStateMachine _stateMachine;

    public BotService(BotStateMachine stateMachine, Bot bot)
    {
        _stateMachine = stateMachine ?? throw new ArgumentNullException(nameof(stateMachine));
        _bot = bot != null ? bot : throw new ArgumentNullException(nameof(bot));
    }

    public void AddNewResourceTarget(Resource targetPoint) =>
        _bot.AddNewResourceTarget(targetPoint);

    public void Update() =>
        _stateMachine.Update();
}
