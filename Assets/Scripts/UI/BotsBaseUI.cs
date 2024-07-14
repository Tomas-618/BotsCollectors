using TMPro;
using UnityEngine;

[RequireComponent(typeof(TMP_Text))]
public class BotsBaseUI : MonoBehaviour
{
    [SerializeField] private InterfaceReference<IReadOnlyBotsBaseEvents, BotsBase> _events;

    private TMP_Text _text;

    private void Awake() =>
        _text = GetComponent<TMP_Text>();

    private void OnEnable() =>
        _events.Value.ResourcesCountChanged += ChangeValue;

    private void OnDisable() =>
        _events.Value.ResourcesCountChanged -= ChangeValue;

    private void ChangeValue(int count, bool isLess) =>
        _text.text = count.ToString();
}
