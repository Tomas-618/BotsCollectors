using System;

public interface IReadOnlyBotsBaseEvents
{
    event Action<bool> BotsCountChanged;

    event Action PenultimateBotRemoved;
}
