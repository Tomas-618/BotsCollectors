using System;
using UnityEngine;

public class SelectableBase : MonoBehaviour, ICanOnlyChangeSelectableBaseState,
    ISelectableBase, ICanOnlyUseFlagMethods, IReadOnlySelectableBaseEvents
{
    private bool _isSelected;

    public event Action<bool> ChangedState;

    [field: SerializeField] public Flag FlagInfo { get; private set; }

    public void ChangeState()
    {
        _isSelected = !_isSelected;
        ChangedState?.Invoke(_isSelected);
    }

    public void EnableFlag() =>
        FlagInfo.EnableObject();

    public void DisableFlag() =>
        FlagInfo.DisableObject();

    public void SetFlagPosition(Vector3 position) =>
        FlagInfo.SetPosition(position);
}
