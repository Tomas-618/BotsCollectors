using System;
using UnityEngine;

public class BotInteractor : MonoBehaviour, IInteractor<BotInteractor>, IInitializable<BasesSpawner>
{
    [field: SerializeField] public BotPrefab Prefab { get; private set; }

    [field: SerializeField] public BotHand Hand { get; private set; }

    [field: SerializeField] public BotsBaseInfoOwner BotsBase { get; private set; }

    public BasesSpawner BasesSpawner { get; private set; }

    public void Init(BasesSpawner spawner) =>
        BasesSpawner = spawner != null ? spawner : throw new ArgumentNullException(nameof(spawner));

    public void Interact(ITarget<BotInteractor> target) =>
        target.Interact(this);

    public void RemoveBotFromBase() =>
        BotsBase.RemoveBotFromBase();
}
