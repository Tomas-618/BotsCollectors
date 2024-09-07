using System;

public interface IReadOnlyBaseWalletEvents
{
    event Action Added;

    event Action Removed;

    event Action<int> CountChanged;
}
