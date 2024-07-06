using System;
using UnityEngine;

public class SelectableBase : MonoBehaviour, IReadOnlySelectableBaseEvents
{
    private bool _isSelected;

    public event Action<bool> ChangedState;

    [field: SerializeField] public Flag FlagInfo { get; private set; }

    public void ChangeState()
    {
        _isSelected = !_isSelected;
        ChangedState?.Invoke(_isSelected);
    }
}
