public interface IReadOnlyBotsSpawner
{
    int ResourcesCountToSpawn { get; }

    bool CanSpawn { get; }
}
