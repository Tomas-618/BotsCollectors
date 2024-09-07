using TMPro;
using UnityEngine;

[RequireComponent(typeof(TMP_Text))]
public class BotsBaseUI : MonoBehaviour
{
    [SerializeField] private InterfaceReference<IReadOnlyBaseWalletEvents, ResourcesBaseWallet> _walletEvents;

    private TMP_Text _text;

    private void Awake() =>
        _text = GetComponent<TMP_Text>();

    private void OnEnable() =>
        _walletEvents.Value.CountChanged += ChangeValue;

    private void OnDisable() =>
        _walletEvents.Value.CountChanged -= ChangeValue;

    private void ChangeValue(int count) =>
        _text.text = count.ToString();
}
