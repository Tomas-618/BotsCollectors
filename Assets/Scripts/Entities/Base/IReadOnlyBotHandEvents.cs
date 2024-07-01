using System;

public interface IReadOnlyBotHandEvents
{
    event Action ResourceTaken;

    event Action ResourceThrew;
}
