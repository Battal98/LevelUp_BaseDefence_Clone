using Data.UnityObject;
using Data.ValueObject;
using Keys;
using Signals;
using UnityEngine;

namespace Managers
{ 
    public class InputManager : MonoBehaviour
    {
        #region Self Variables

        #region Public Variables
        
        #endregion

        #region Serialized Variables
        
        [SerializeField] private FloatingJoystick joystickInput;
        
        #endregion

        #region Private Variables
        private bool _hasTouched;
        #endregion
        
        #endregion
        private void Update()
        {
            JoystickInputUpdate();
        }
        private void JoystickInputUpdate()
        {
            if (Input.GetMouseButton(0) && !_hasTouched)
            {
                _hasTouched = true;
            }
            if (!_hasTouched) return;
            InputSignals.Instance.onInputDragged?.Invoke(new HorizontalInputParams()
            {
                MovementVector = new Vector2(joystickInput.Horizontal, joystickInput.Vertical)
            });
            _hasTouched = joystickInput.Direction.sqrMagnitude > 0;
            
        }
    }
}