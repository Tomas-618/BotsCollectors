public interface IFabric<out T>
{
    T Create();
}

public interface IFabric<out T1, in T2>
{
    T1 Create(T2 parameter);
}

public interface IFabric<out T1, in T2, in T3>
{
    T1 Create(T2 parameter1, T3 parameter2);
}
