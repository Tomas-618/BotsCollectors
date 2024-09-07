using UnityEngine;

public abstract class BasicBotEventsHandler : MonoBehaviour
{
    [SerializeField] private InterfaceReference<IReadOnlyBotHandEvents, BotHand> _handEvents;

    private void OnEnable()
    {
        _handEvents.Value.ResourceTaken += OnCollect;
        _handEvents.Value.ResourceThrew += OnPut;
    }

    private void OnDisable()
    {
        _handEvents.Value.ResourceTaken -= OnCollect;
        _handEvents.Value.ResourceThrew -= OnPut;
    }

    protected abstract void OnCollect();

    protected virtual void OnPut() { }
}