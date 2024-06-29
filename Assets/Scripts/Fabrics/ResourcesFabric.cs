using UnityEngine;

public class ResourcesFabric : MonoBehavioursFabric<Resource>
{
    public ResourcesFabric(Resource prefab, Transform parent) : base(prefab, parent) { }

    public override Resource Create()
    {
        const float MaxAngle = 360;

        Resource entity = base.Create();

        entity.transform.rotation = Quaternion.Euler(Vector3.up * Random.Range(0, MaxAngle));

        return entity;
    }
}
