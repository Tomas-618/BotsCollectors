using UnityEngine;

public class ResourcesFactory : MonoBehaviourFactory<Resource>
{
    public ResourcesFactory(Resource prefab) : base(prefab) { }

    public override Resource Create(Transform parent)
    {
        const float MaxAngle = 360;

        Resource entity = base.Create(parent);

        entity.Physics.transform.rotation = Quaternion.Euler(Vector3.up * Random.Range(0, MaxAngle));

        return entity;
    }
}
