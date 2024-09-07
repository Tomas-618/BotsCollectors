using UnityEngine;

public interface IReadOnlyBotsBase
{
    Transform BotsParent { get; }

    bool HasBots { get; }

    bool HasFreeBots { get; }

    bool CanAddBot { get; }

    bool CanRemoveBot { get; }
}
