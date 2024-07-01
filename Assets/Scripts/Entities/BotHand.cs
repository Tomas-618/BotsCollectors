using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(FixedJoint))]
public class BotHand : MonoBehaviour, IReadOnlyBotHandEvents
{
    [SerializeField, Min(0)] private float _distanceDelta;
    [SerializeField, Min(0)] private float _rotationDelta;

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

        _coroutine = StartCoroutine(ProcessTaking(_distanceDelta, _rotationDelta));
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

    private IEnumerator ProcessTaking(float distanceDelta, float rotationDelta)
    {
        Transform resourceTransform = _resource.transform;

        float minPositionDelta = 0.05f;
        float minRotationAngle = 0.3f;

        while (Vector3.Distance(resourceTransform.position, _transform.position) > minPositionDelta
            || Quaternion.Angle(resourceTransform.rotation, _transform.rotation) > minRotationAngle)
        {
            resourceTransform.position = Vector3.MoveTowards(resourceTransform.position,
                _transform.position,
                distanceDelta);

            resourceTransform.rotation = Quaternion.RotateTowards(resourceTransform.rotation,
                _transform.rotation,
                rotationDelta);

            yield return null;
        }

        _fixedJoint.connectedBody = _resource.RigidbodyInfo;
        _resource.RigidbodyInfo.isKinematic = false;

        ResourceTaken?.Invoke();
        _coroutine = null;
    }
}
