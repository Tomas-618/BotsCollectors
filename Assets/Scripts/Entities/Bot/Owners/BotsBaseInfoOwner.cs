using System;
using UnityEngine;

public class BotsBaseInfoOwner : MonoBehaviour, IInitializable<BotsBase>
{
    [SerializeField] private TargetInfoOwner _targetInfoOwner;

    private BotsBase _target;

    public IAddable<TargetInfoOwner> Target => _target;

    public void Init(BotsBase target) =>
        _target = target != null ? target : throw new ArgumentNullException(nameof(target));

    public void RemoveBotFromBase() =>
        _target.RemoveBot(_targetInfoOwner);
}
