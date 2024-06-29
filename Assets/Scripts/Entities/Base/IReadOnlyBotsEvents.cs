using System;

public interface IReadOnlyBotsEvents
{
    event Action<int> ResourceCollected;

    event Action<int> ResourcesPut;
}