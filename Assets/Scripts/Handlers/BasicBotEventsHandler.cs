using UnityEngine;

public abstract class BasicBotEventsHandler : MonoBehaviour
{
    [SerializeField] private InterfaceReference<IReadOnlyBotsEvents, Bot> _events;

    private void OnEnable()
    {
        _events.Value.ResourceCollected += OnCollect;
        _events.Value.ResourcesPut += OnPut;
    }

    private void OnDisable()
    {
        _events.Value.ResourceCollected -= OnCollect;
        _events.Value.ResourcesPut -= OnPut;
    }

    protected abstract void OnCollect();

    protected abstract void OnPut();
}