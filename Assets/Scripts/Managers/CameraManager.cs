using Cinemachine;
using Enums;
using Signals;
using Sirenix.OdinInspector;
using UnityEngine;
using DG.Tweening;

namespace Managers
{
    public class CameraManager : MonoBehaviour
    {
        #region Self Variables

        #region Public Variables

        [SerializeField] private CinemachineStateDrivenCamera stateDrivenCamera;

        #endregion

        #region Private Variables

        [ShowInInspector] private Vector3 _initialPosition;
        private Animator _animator;
        private Transform _playerTarget;

        #endregion

        #endregion

        private void Awake()
        {
            GetReferences();
            GetInitialPosition();
        }

        private void GetReferences()
        {
            _animator = GetComponent<Animator>();
        }

        #region Event Subscriptions

        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            CoreGameSignals.Instance.onPlay += OnPlay;
            CoreGameSignals.Instance.onReset += OnReset;
            CoreGameSignals.Instance.onSetCameraTarget += OnSetCameraTarget;
            CoreGameSignals.Instance.onEnterTurret += OnEnterTurret;
            CoreGameSignals.Instance.onFinish += OnFinish;
            CoreGameSignals.Instance.onLevel += OnLevel;
        }

        private void UnsubscribeEvents()
        {
            CoreGameSignals.Instance.onPlay -= OnPlay;
            CoreGameSignals.Instance.onReset -= OnReset;
            CoreGameSignals.Instance.onSetCameraTarget -= OnSetCameraTarget;
            CoreGameSignals.Instance.onEnterTurret -= OnEnterTurret;
            CoreGameSignals.Instance.onFinish -= OnFinish;
            CoreGameSignals.Instance.onLevel -= OnLevel;
        }

        private void OnDisable()
        {
            UnsubscribeEvents();
        }

        #endregion

        private void GetInitialPosition()
        {
            _initialPosition = transform.GetChild(0).localPosition;
        }

        private void OnSetCameraTarget(Transform _target)
        {
            _playerTarget = _target;
            stateDrivenCamera.Follow = _playerTarget;
            stateDrivenCamera.Follow = _playerTarget.transform;
            SetCameraState(CameraStatesType.IdleCamera);
        }

        private void SetCameraState(CameraStatesType cameraState)
        {
            _animator.Play(cameraState.ToString());
        }

        public void OnFinish()
        {
            stateDrivenCamera.Follow = _playerTarget.transform;
            SetCameraState(CameraStatesType.FinishCamera);
        }

        private void OnEnterTurret()
        {
            SetCameraState(CameraStatesType.TurretCamera);
            DOVirtual.DelayedCall(2f,() => stateDrivenCamera.Follow = null);
           
        }

        private void OnLevel()
        {
            stateDrivenCamera.Follow = _playerTarget.transform;
            SetCameraState(CameraStatesType.IdleCamera);
        }

        private void OnPlay()
        {
            GetInitialPosition();
        }

        private void OnReset()
        {
            SetCameraState(CameraStatesType.IdleCamera);
            OnSetCameraTarget(_playerTarget);
        }



    }
}