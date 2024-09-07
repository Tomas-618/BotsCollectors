using UnityEngine;

public class DistanceChecker : MonoBehaviour
{
    [SerializeField, Min(0)] private float _min;

    public bool IsNearestToPoint(Vector3 point) =>
        Vector3.Distance(transform.position, point) <= _min;
}
