using UnityEngine;

public static class ColliderExtension
{
    public static Vector3 GetRandomPosition(this Collider collider)
    {
        Vector3 position;

        position.x = Random.Range(collider.transform.position.x - Random.Range(0, collider.bounds.extents.x),
            collider.transform.position.x + Random.Range(0, collider.bounds.extents.x));

        position.y = Random.Range(collider.transform.position.y - Random.Range(0, collider.bounds.extents.y),
            collider.transform.position.y + Random.Range(0, collider.bounds.extents.y));

        position.z = Random.Range(collider.transform.position.z - Random.Range(0, collider.bounds.extents.z),
            collider.transform.position.z + Random.Range(0, collider.bounds.extents.z));

        return position;
    }
}
