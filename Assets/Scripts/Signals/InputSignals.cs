using Extentions;
using Keys;
using UnityEngine.Events;
using Enums;

namespace Signals
{
    public class InputSignals : MonoSingleton<InputSignals>
    { 
        public UnityAction<HorizontalInputParams> onInputDragged = delegate{  };
        public UnityAction<bool> onInputTakenActive = delegate { };
        public UnityAction<HorizontalInputParams> onJoystickInputDraggedforTurret = delegate (HorizontalInputParams arg0) { };
        public UnityAction<InputType> onInputHandlerChange = delegate (InputType arg0) { };
        public UnityAction onCharacterInputRelease = delegate { };
    }
}