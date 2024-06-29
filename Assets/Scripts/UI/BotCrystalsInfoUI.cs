using System;
using UnityEngine;
using TMPro;

public class BotCrystalsInfoUI : BasicBotEventsHandler
{
    [SerializeField] private TMP_Text _text;

    private int _count;

    protected override void OnCollect(int count)
    {
        if (count < 0)
            throw new ArgumentOutOfRangeException(count.ToString());

        SetCount(_count + count);
    }

    protected override void OnPut(int count)
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
