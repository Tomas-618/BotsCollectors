using System;

public interface IReadOnlySelectableBaseEvents
{
    event Action<bool> ChangedState;
}
