using UnityEngine;

[RequireComponent(typeof(Outline))]
public class BaseOutline : MonoBehaviour
{
    [SerializeField] private InterfaceReference<IReadOnlySelectableBaseEvents, SelectableBase> _events;

    private Outline _outline;

    private void Awake() =>
        _outline = GetComponent<Outline>();

    private void OnEnable() =>
        _events.Value.ChangedState += SetOutlineActivity;

    private void OnDisable() =>
        _events.Value.ChangedState -= SetOutlineActivity;

    private void SetOutlineActivity(bool isSelected) =>
        _outline.enabled = isSelected;
}
