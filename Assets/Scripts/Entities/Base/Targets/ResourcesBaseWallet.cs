using System;
using UnityEngine;

public class ResourcesBaseWallet : MonoBehaviour, ITarget<BotInteractor>, IReadOnlyBaseWalletEvents
{
    private int _count;

    public event Action<Vector3> PositionChanged;

    public event Action Added;

    public event Action Removed;

    public event Action<int> CountChanged;

    public Transform TransformInfo { get; private set; }

    private void Awake() =>
        TransformInfo = transform;

    public bool IsEnough(int count)
    {
        if (count < 0)
            throw new ArgumentOutOfRangeException($"{nameof(count)}: {count.ToString()}");

        return count <= _count;
    }

    public void Add(Resource element)
    {
        if (element == null)
            throw new ArgumentNullException(nameof(element));

        _count++;

        CountChanged?.Invoke(_count);
        Added?.Invoke();
    }

    public void RemoveAmount(int amount)
    {
        if (amount < 0 || IsEnough(amount) == false)
            throw new ArgumentOutOfRangeException($"{nameof(amount)}: {amount.ToString()}");

        _count -= amount;

        CountChanged?.Invoke(_count);
        Removed?.Invoke();
    }

    public void Interact(BotInteractor interactor)
    {
        Resource resource = interactor.Hand.Throw();

        Add(resource);
        resource.PoolComponent.ReturnToPool();
    }
}
