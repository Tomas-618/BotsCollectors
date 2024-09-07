using UnityEngine;

public interface IReadOnlySelectableBase
{
    bool CanRemoveBot { get; }

    void ActivateFlag(Vector3 position);
}
