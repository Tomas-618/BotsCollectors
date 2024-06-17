using UnityEngine;

public class ResourcesFabric : MonoBehavioursFabric<Resource>
{
    public ResourcesFabric(Resource prefab, Transform parent) : base(prefab, parent) { }
}
