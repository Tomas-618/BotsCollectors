public interface IInitializable
{
    void Init();
}

public interface IInitializable<T>
{
    void Init(T element);
}

public interface IInitializable<T1, T2>
{
    void Init(T1 firstElement, T2 secondElement);
}

public interface IInitializable<T1, T2, T3>
{
    void Init(T1 firstElement, T2 secondElement, T3 thirdElement);
}
