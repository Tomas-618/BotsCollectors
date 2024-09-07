using System;
using System.Collections.Generic;
using UnityEngine;

public class BotsBase : MonoBehaviour, IReadOnlyBotsBase, IReadOnlyBotsBaseEvents, IAddable<TargetInfoOwner>
{
    [SerializeField, Min(1)] private int _maxBotsCount;
    [SerializeField, Min(0)] private int _minBotsCount;

    private List<TargetInfoOwner> _bots = new List<TargetInfoOwner>();
    private List<TargetInfoOwner> _freeBots = new List<TargetInfoOwner>();

    public event Action<bool> BotsCountChanged;

    public event Action BotRemoved;

    [field: SerializeField] public Transform BotsParent { get; private set; }
    
    public bool HasBots => _bots.Count > 1 && HasFreeBots;

    public bool HasFreeBots => _freeBots.Count > 0;

    public bool CanAddBot => _bots.Count < _maxBotsCount;

    public bool CanRemoveBot => _bots.Count > _minBotsCount;

    private void OnValidate()
    {
        if (_minBotsCount >= _maxBotsCount)
            _minBotsCount = _maxBotsCount - 1;
    }

    public void AddCreatedBot(TargetInfoOwner bot)
    {
        if (bot == null)
            throw new ArgumentNullException(nameof(bot));

        if (CanAddBot == false)
            return;

        _bots.Add(bot);
        Add(bot);

        bot.transform.SetParent(BotsParent);

        BotsCountChanged?.Invoke(CanAddBot);
    }

    public void Add(TargetInfoOwner bot)
    {
        if (bot == null)
            throw new ArgumentNullException(nameof(bot));

        if (_bots.Contains(bot) == false)
            throw new InvalidOperationException();

        if (_freeBots.Contains(bot))
            return;

        _freeBots.Add(bot);
    }

    public void RemoveBot(TargetInfoOwner bot)
    {
        if (bot == null)
            throw new ArgumentNullException(nameof(bot));

        if (CanRemoveBot == false)
            return;

        _freeBots.Remove(bot);
        _bots.Remove(bot);

        BotsCountChanged?.Invoke(CanAddBot);
        BotRemoved?.Invoke();
    }

    public TargetInfoOwner TakeFreeBot()
    {
        TargetInfoOwner bot = _freeBots[0];

        _freeBots.Remove(bot);

        return bot;
    }

    public TargetInfoOwner[] TakeFreeBots()
    {
        TargetInfoOwner[] bots = _freeBots.ToArray();

        _freeBots.Clear();

        return bots;
    }
}
