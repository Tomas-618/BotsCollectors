using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ResourcesSorter
{
    private readonly List<List<ITarget>> _groups;

    public ResourcesSorter() =>
        _groups = new List<List<ITarget>>();

    public void Clear() =>
        _groups.Clear();

    public void AddGroups(int count)
    {
        for (int i = 0; i < count; i++)
            _groups.Add(new List<ITarget>());
    }

    public void AddForEachGroup(ITarget[] targets)
    {
        if (targets == null)
            throw new ArgumentNullException(nameof(targets));

        for (int i = 0; i < targets.Length; i++)
        {
            int currentTargetsIndex = i % _groups.Count;

            _groups[currentTargetsIndex].Add(targets[i]);
        }
    }

    public void SortInEachGroupByAscendingDistanceToObject(Transform gameObjectTransform)
    {
        if (gameObjectTransform == null)
            throw new ArgumentNullException(nameof(gameObjectTransform));

        for (int i = 0; i < _groups.Count; i++)
        {
            _groups[i] = _groups[i]
                .OrderBy(target => Vector3.Distance(gameObjectTransform.position, target.TransformInfo.position))
                .ToList();
        }
    }

    public ITarget[] GetGroupByIndex(int index) =>
        _groups[index].ToArray();
}
