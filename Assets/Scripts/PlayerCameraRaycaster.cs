using BasicStateMachine;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class PlayerCameraRaycaster : MonoBehaviour
{
    [SerializeField] private SelectableBase _base;

    private PlayerCameraInput _input;
    private RaycasterHitInfoProvider _hitInfoProvider;
    private StateMachine<BaseSelectionState, BaseSelectionTransition> _stateMachine;
    
    private void Awake()
    {
        _input = PlayerCameraInput.Instance;
        _hitInfoProvider = new RaycasterHitInfoProvider(GetComponent<Camera>());
        _stateMachine = new BaseSelectionStateMachineFactory(_hitInfoProvider).Create(_base);
    }

    private void OnEnable() =>
        _input.Clicked += OnClicked;

    private void OnDisable() =>
        _input.Clicked -= OnClicked;

    private void Update() =>
        _hitInfoProvider.Update();

    private void OnClicked()
    {
        if (_hitInfoProvider.HasHit == false)
            return;

        _stateMachine.Update();

        #region Hueta
        //if (Physics.Raycast(ray, out RaycastHit hitInfo))
        //{
        //    Transform hitTransform = hitInfo.transform;

        //    if (hitTransform.TryGetComponent(out SelectableBase target))
        //    {
        //        if (_target == target)
        //        {
        //            _target.ChangeState();
        //            _target = null;
        //        }
        //        else
        //        {
        //            _target = target;
        //            _target.ChangeState();
        //        }
        //    }
        //    else if (hitTransform.GetComponent<BasesSpawnZone>())
        //    {
        //        if (_target != null)
        //        {
        //            _target.FlagInfo.EnableObject();
        //            _target.FlagInfo.SetPosition(hitInfo.point);

        //            _target.ChangeState();
        //            _target = null;
        //        }
        //    }
        //    else
        //    {
        //        if (_target != null)
        //        {
        //            _target.ChangeState();
        //            _target = null;
        //        }
        //    }
        //}
        //else
        //{
        //    if (_target != null)
        //    {
        //        _target.ChangeState();
        //        _target = null;
        //    }
        //}
        #endregion
    }
}
