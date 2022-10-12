using Data.UnityObject;
using Data.ValueObject;
using Keys;
using Signals;
using UnityEngine;
using Enums;

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
        private InputType _inputHandlers = InputType.Character;
        private bool _hasTouched;
        #endregion

        #endregion

        #region Event Subscriptions

        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            InputSignals.Instance.onInputHandlerChange += OnInputHandlerChange;
        }

        private void UnsubscribeEvents()
        {
            InputSignals.Instance.onInputHandlerChange -= OnInputHandlerChange;
        }

        private void OnDisable()
        {
            UnsubscribeEvents();
        }

        #endregion Event Subscriptions


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

            CharacterInputHandler();

            _hasTouched = joystickInput.Direction.sqrMagnitude > 0;
            
        }

        private void CharacterInputHandler()
        {
            switch (_inputHandlers)
            {
                case InputType.Character:
                    InputSignals.Instance.onInputDragged?.Invoke(new HorizontalInputParams()
                    {
                        MovementVector = new Vector2(joystickInput.Horizontal, joystickInput.Vertical)
                    });
                    break;

                case InputType.Turret when joystickInput.Vertical <= -0.6f:
                    _inputHandlers = InputType.Character;
                    InputSignals.Instance.onCharacterInputRelease?.Invoke();
                    return;

                case InputType.Turret:
                    InputSignals.Instance.onJoystickInputDraggedforTurret?.Invoke(new HorizontalInputParams()
                    {
                        MovementVector = new Vector2(joystickInput.Horizontal, joystickInput.Vertical)
                    });
                    if (joystickInput.Direction.sqrMagnitude != 0)
                    {
                        InputSignals.Instance.onInputDragged?.Invoke(new HorizontalInputParams()
                        {
                            MovementVector = Vector2.zero
                        });
                    }
                    break;

                case InputType.Drone:
                    break;
            }
        }
        private void OnInputHandlerChange(InputType inputHandlers)
        {
            _inputHandlers = inputHandlers;
        }
    }
}