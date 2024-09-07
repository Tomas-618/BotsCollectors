using System;
using UnityEngine;

public class TargetInfoOwner : MonoBehaviour, IBot, IReadOnlyBotTarget<BotInteractor>
{
    public ITarget<BotInteractor> CurrentTarget { get; private set; }

    public bool HasTarget => CurrentTarget != null;

    public void SetTarget(ITarget<BotInteractor> target) =>
        CurrentTarget = target ?? throw new ArgumentNullException(nameof(target));

    public void RemoveCurrentTarget() =>
        CurrentTarget = null;
}
