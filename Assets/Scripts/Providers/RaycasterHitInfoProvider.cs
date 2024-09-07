using System;
using UnityEngine;

public class RaycasterHitInfoProvider
{
    private Camera _camera;

    public RaycasterHitInfoProvider(Camera camera) =>
        _camera = camera != null ? camera : throw new ArgumentNullException(nameof(camera));

    public RaycastHit HitInfo { get; private set; }

    public bool HasHit { get; private set; }

    public void Update()
    {
        Ray ray = _camera.ScreenPointToRay(Input.mousePosition);

        HasHit = Physics.Raycast(ray, out RaycastHit hitInfo);
        HitInfo = hitInfo;
    }
}
