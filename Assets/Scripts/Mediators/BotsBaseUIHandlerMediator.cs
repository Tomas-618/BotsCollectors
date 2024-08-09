using UnityEngine;

public class BotsBaseUIHandlerMediator : MonoBehaviour
{
    [field: SerializeField] public BotsBaseUIHandler UIHandler { get; private set; }

    public void Init(BotsSpawner botsSpawner) =>
        UIHandler.Init(botsSpawner);
} 
