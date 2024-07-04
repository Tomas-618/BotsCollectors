using System;

public interface IReadOnlyBotsBaseEvents
{
    event Action<bool> EntityAdded;

    event Action<int> ResourcesCountChanged;
}