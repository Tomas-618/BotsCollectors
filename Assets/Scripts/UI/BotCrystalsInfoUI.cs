using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class BotCrystalsInfoUI : BasicBotEventsHandler
{
    private Image _image;

    private void Awake() =>
        _image = GetComponent<Image>();

    protected override void OnCollect() =>
        _image.enabled = true;

    protected override void OnPut() =>
        _image.enabled = false;
}
