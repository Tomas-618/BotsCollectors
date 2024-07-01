using System;

public interface IReadOnlyBotsEvents
{
    event Action ResourceCollected;

    event Action ResourcesPut;
}