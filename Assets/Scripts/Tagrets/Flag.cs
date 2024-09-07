using System;
using UnityEngine;

public class Flag : BasicGameObject, ITarget<BotInteractor>
{
    private IBot _bot;

    public event Action<Vector3> PositionChanged;

    public Transform TransformInfo { get; private set; }

    public bool IsFree => gameObject.activeSelf && _bot == null;

    private void Awake() =>
        TransformInfo = transform;

    public void SetBot(IBot bot) =>
        _bot = bot ?? throw new ArgumentNullException(nameof(bot));

    public void SetPosition(Vector3 position)
    {
        TransformInfo.position = position;
        PositionChanged?.Invoke(position);
    }

    private void RemoveBot() =>
        _bot = null;

    public void Interact(BotInteractor interactor)
    {
        DisableObject();
        RemoveBot();

        interactor.BasesSpawner.Spawn(interactor, TransformInfo.position);
    }
}
