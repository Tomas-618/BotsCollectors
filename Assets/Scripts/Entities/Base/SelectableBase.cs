using System;
using UnityEngine;

public class SelectableBase : MonoBehaviour, ICanOnlyChangeSelectableBaseState,
    IReadOnlySelectableBase, IReadOnlySelectableBaseEvents
{
    [SerializeField] private InterfaceReference<IReadOnlyBotsBase, BotsBase> _botsBase;
    [SerializeField] private Flag _flag;

    private bool _isSelected;

    public event Action<bool> ChangedState;

    public bool CanRemoveBot => _botsBase.Value.CanRemoveBot;

    public void SetSelectedState()
    {
        _isSelected = true;
        ChangedState?.Invoke(_isSelected);
    }

    public void SetUnselectedState()
    {
        _isSelected = false;
        ChangedState?.Invoke(_isSelected);
    }

    public void ActivateFlag(Vector3 position)
    {
        _flag.EnableObject();
        _flag.SetPosition(position);
    }
}
