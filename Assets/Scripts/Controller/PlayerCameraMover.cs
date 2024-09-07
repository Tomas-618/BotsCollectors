using UnityEngine;

[RequireComponent(typeof(Camera))]
public class PlayerCameraMover : MonoBehaviour
{
    [SerializeField] private PlayerCameraInput _input;
    [SerializeField] private Transform _axisRotation;

    private Transform _transform;

    private void Awake() =>
        _transform = transform;

    private void OnEnable()
    {
        _input.Zoomed += Zoom;
        _input.Moved += Move;
        _input.Rotated += Rotate;
    }

    private void OnDisable()
    {
        _input.Zoomed -= Zoom;
        _input.Moved -= Move;
        _input.Rotated -= Rotate;
    }

    private void Zoom(Vector3 direction) =>
        _transform.Translate(direction);

    private void Move(Vector3 direction) =>
        _transform.Translate(direction);

    private void Rotate(Vector3 direction)
    {
        _transform.RotateAround(_axisRotation.position, Vector3.up, direction.x);
        _transform.RotateAround(_axisRotation.position, _transform.right, direction.y);
    }
}
