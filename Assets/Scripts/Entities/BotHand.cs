using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(FixedJoint))]
public class BotHand : MonoBehaviour
{
    [SerializeField, Min(0)] private float _distanceDelta;
    [SerializeField, Min(0)] private float _rotationDelta;

    private Transform _transform;
    private FixedJoint _fixedJoint;
    private Resource _resource;
    private Coroutine _coroutine;

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

        _resource.Physics.Disable();
        _fixedJoint.connectedBody = null;
        _resource = null;

        return resource;
    }

    private IEnumerator ProcessTaking(float distanceDelta, float rotationDelta)
    {
        Transform resourceTransform = _resource.transform;

        while (resourceTransform.position != _transform.position || resourceTransform.rotation != _transform.rotation)
        {
            resourceTransform.position = Vector3.MoveTowards(resourceTransform.position, _transform.position, distanceDelta);
            resourceTransform.rotation = Quaternion.RotateTowards(resourceTransform.rotation, _transform.rotation, rotationDelta);

            yield return null;
        }

        _fixedJoint.connectedBody = _resource.RigidbodyInfo;
        _resource.RigidbodyInfo.isKinematic = false;
        _coroutine = null;
    }
}
