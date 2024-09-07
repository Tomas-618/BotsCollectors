using System;
using UnityEngine;

public class LevelBuilder : MonoBehaviour
{
    [SerializeField, Range(0, 8)] private int _botsCount;

    [SerializeField] private BasesSpawner _basesSpawner;
    [SerializeField] private Vector2 _baseSpawnPosition;

    private void Awake() =>
        _basesSpawner.Spawn(_botsCount, _baseSpawnPosition);
}
