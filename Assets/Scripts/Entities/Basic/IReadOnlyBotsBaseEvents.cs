using System;

public interface IReadOnlyBotsBaseEvents
{
    event Action<bool> EntitiesCountChanged;

    event Action<int, bool> ResourcesCountChanged;
}