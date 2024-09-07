public interface IInteractor<T> where T : IInteractor<T>
{
    void Interact(ITarget<T> target);
}
