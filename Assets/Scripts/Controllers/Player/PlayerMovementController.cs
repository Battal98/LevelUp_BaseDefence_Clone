using Data.UnityObject;
using Data.ValueObject;
using Enums;
using Keys;
using Managers;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Controllers
{
    public class PlayerMovementController : MonoBehaviour
    {
        #region Self Variables

        #region Public Variables

        #endregion

        #region Serialized Variables

        [SerializeField] private Rigidbody myRigidbody;
        [SerializeField] private CD_MovementList cdMovementList;

        #endregion

        #region Private Variables

        [ShowInInspector] [Header("Data")] private PlayerMovementData _playerMovementData;
        [SerializeField]
        private bool _isReadyToMove, _isReadyToPlay;
        private InputParams _inputParams;

        #endregion

        #endregion

        public void SetMovementData(PlayerMovementData dataMovementData)
        {
            _playerMovementData = dataMovementData;
        }

        public void EnableMovement()
        {
            _isReadyToMove = true;
        }

        public void DeactiveMovement()
        {
            _isReadyToMove = false;
        }

        public void UpdateInputValue(InputParams inputParam)
        {
            _inputParams = inputParam;
        }

        public void IsReadyToPlay(bool state)
        {
            _isReadyToPlay = state;
        }

        private void FixedUpdate()
        {
            if (_isReadyToPlay)
            {
                cdMovementList.MovementTypeList[0].DoMovement(ref _isReadyToMove,
                    ref myRigidbody, ref _inputParams, ref _playerMovementData);
            }
            else
            {
                Stop();
            }
        }

        public void Stop()
        {
            myRigidbody.velocity = Vector3.zero;
            myRigidbody.angularVelocity = Vector3.zero;
        }

        public void OnReset()
        {
            Stop();
            _isReadyToPlay = false;
            _isReadyToMove = false;
        }
    }
}