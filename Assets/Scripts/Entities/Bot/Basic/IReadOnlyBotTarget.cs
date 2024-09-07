public interface IReadOnlyBotTarget<T> where T : IInteractor<T>
{
    ITarget<T> CurrentTarget { get; }
}
