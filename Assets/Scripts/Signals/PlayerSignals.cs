using Enums;
using Extentions;
using UnityEngine.Events;

namespace Signals
{
    public class PlayerSignals : MonoSingleton<PlayerSignals>
    {
        public UnityAction<PlayerAnimationStates> onChangePlayerAnimationState = delegate { };
    }
}