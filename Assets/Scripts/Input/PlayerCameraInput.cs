using System;
using UnityEngine;
using static UnityEngine.InputSystem.InputAction;

public class PlayerCameraInput : MonoBehaviour
{
    private PlayerCameraInputActions _actions;

    public event Action<Vector3> Zoomed;

    public event Action<Vector3> Moved;

    public event Action<Vector3> Rotated;

    public event Action Clicked;

    private void Awake() =>
        _actions = new PlayerCameraInputActions();

    private void OnEnable()
    {
        _actions.Enable();
        _actions.PlayerCamera.Zoom.performed += OnZoom;
        _actions.PlayerCamera.Move.performed += OnMove;
        _actions.PlayerCamera.Rotate.performed += OnRotate;
        _actions.PlayerCamera.Click.performed += OnChoose;
    }

    private void OnDisable()
    {
        _actions.Disable();
        _actions.PlayerCamera.Zoom.performed -= OnZoom;
        _actions.PlayerCamera.Move.performed -= OnMove;
        _actions.PlayerCamera.Rotate.performed -= OnRotate;
        _actions.PlayerCamera.Click.performed -= OnChoose;
    }

    private void OnZoom(CallbackContext context)
    {
        Vector3 direction = Vector3.forward * context.ReadValue<Vector2>().y;

        Zoomed?.Invoke(direction);
    }

    private void OnMove(CallbackContext context)
    {
        Vector3 direction = -context.ReadValue<Vector2>();

        Moved?.Invoke(direction);
    }

    private void OnRotate(CallbackContext context)
    {
        Vector3 direction = new Vector3(context.ReadValue<Vector2>().x, -context.ReadValue<Vector2>().y);

        Rotated?.Invoke(direction);
    }

    private void OnChoose(CallbackContext context) =>
        Clicked?.Invoke();
}
