using Data.ValueObject;
using Keys;
using UnityEngine;

namespace Data.UnityObject
{
    [CreateAssetMenu(fileName = "Joystick", menuName = "Movement/Joystick", order = 0)]
    public class CD_JoystickMovement : CD_Movement
    {
        public override void DoMovement( ref bool _isReadyToMove,
          ref Rigidbody _rigidbody,
          ref InputParams inputParams,
          ref PlayerDatas _moveData)
        {
            JoystickMove(ref _rigidbody,
                ref _moveData,
                ref inputParams);
        }

        private void JoystickMove(ref Rigidbody _rigidbody,
            ref PlayerDatas _playerMovementData,
            ref InputParams _inputParams)
        {
            Vector3 _movement = new Vector3(_inputParams.Values.x * _playerMovementData.PlayerJoystickSpeed,
                0,
                _inputParams.Values.z * _playerMovementData.PlayerJoystickSpeed);

            _rigidbody.velocity = _movement;
            if (_movement != Vector3.zero)
            {
                Quaternion _newDirect = Quaternion.LookRotation(_movement);
                _rigidbody.transform
                    .rotation = _newDirect;
            }
        }

    }
}