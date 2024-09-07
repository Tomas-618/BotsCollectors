using System;
using UnityEngine;

public interface ITarget<T> where T : IInteractor<T>
{
    event Action<Vector3> PositionChanged;

    Transform TransformInfo { get; }

    abstract void Interact(T interactor);
}
