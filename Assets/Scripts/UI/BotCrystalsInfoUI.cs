using System;
using UnityEngine;
using TMPro;

public class BotCrystalsInfoUI : MonoBehaviour
{
    [SerializeField] private InterfaceReference<IReadOnlyBotsEvents, Bot> _botsEvents;
    [SerializeField] private TMP_Text _text;

    private int _count;

    private void OnEnable()
    {
        _botsEvents.Value.ResourceCollected += OnCollect;
        _botsEvents.Value.ResourcesPut += OnPut;
    }

    private void OnDisable()
    {
        _botsEvents.Value.ResourceCollected -= OnCollect;
        _botsEvents.Value.ResourcesPut -= OnPut;
    }

    private void OnCollect(int count)
    {
        if (count < 0)
            throw new ArgumentOutOfRangeException(count.ToString());

        SetCount(_count + count);
    }

    private void OnPut(int count)
    {
        if (count < 0)
            throw new ArgumentOutOfRangeException(count.ToString());

        SetCount(_count - count);
    }

    private void SetCount(int count)
    {
        _count = Mathf.Clamp(count, 0, int.MaxValue);
        ChangeText(_count);
    }

    private void ChangeText(int count) =>
        _text.text = count.ToString();
}
