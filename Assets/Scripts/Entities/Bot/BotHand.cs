using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(FixedJoint))]
public class BotHand : MonoBehaviour, IReadOnlyBotHandEvents
{
    [SerializeField, Min(0.1f)] private float _distanceChangingTime;
    [SerializeField, Min(0.1f)] private float _rotationChangingTime;

    private Transform _transform;
    private FixedJoint _fixedJoint;
    private Resource _resource;
    private Coroutine _coroutine;

    public event Action ResourceTaken;

    public event Action ResourceThrew;

    private void Awake()
    {
        _transform = transform;
        _fixedJoint = GetComponent<FixedJoint>();
    }

    public void Take(Resource resource)
    {
        if (_resource != null)
            Throw();

        _resource = resource != null ? resource : throw new ArgumentNullException(nameof(resource));
        _resource.Physics.Enable();

        _coroutine = StartCoroutine(ProcessTaking(_distanceChangingTime, _rotationChangingTime));
    }

    public Resource Throw()
    {
        if (_resource == null)
            return null;

        if (_coroutine != null)
            StopCoroutine(_coroutine);

        Resource resource = _resource;

        _resource.RigidbodyInfo.isKinematic = true;
        _resource.Physics.Disable();
        _fixedJoint.connectedBody = null;
        _resource = null;

        ResourceThrew?.Invoke();

        return resource;
    }

    private IEnumerator ProcessTaking(float distanceChangingTime, float rotationChangingTime)
    {
        Transform resourceTransform = _resource.transform;

        Vector3 initialResourcePosition = resourceTransform.position;

        Quaternion initialResourceRotation = resourceTransform.rotation;

        float pathRunningTime = 0;

        float minPositionDelta = 0.05f;
        float minRotationAngle = 1;

        while (Vector3.Distance(resourceTransform.position, _transform.position) > minPositionDelta
            || Quaternion.Angle(resourceTransform.rotation, _transform.rotation) > minRotationAngle)
        {
            pathRunningTime += Time.deltaTime;

            resourceTransform.position = Vector3.Lerp(initialResourcePosition,
                _transform.position,
                pathRunningTime / distanceChangingTime);

            resourceTransform.rotation = Quaternion.Lerp(initialResourceRotation,
                _transform.rotation,
                pathRunningTime / rotationChangingTime);

            yield return null;
        }

        _fixedJoint.connectedBody = _resource.RigidbodyInfo;
        _resource.RigidbodyInfo.isKinematic = false;

        ResourceTaken?.Invoke();
        _coroutine = null;
    }
}
