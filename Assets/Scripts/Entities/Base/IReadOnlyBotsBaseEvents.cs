using System;

public interface IReadOnlyBotsBaseEvents
{
    event Action<int> ResourcesCountChanged;
}