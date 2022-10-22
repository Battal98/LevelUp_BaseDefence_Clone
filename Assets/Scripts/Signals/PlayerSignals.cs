using Enums.Player;
using Extentions;
using UnityEngine.Events;

namespace Signals
{
    public class PlayerSignals : MonoSingleton<PlayerSignals>
    {
        public UnityAction<PlayerAnimationStates> onChangePlayerAnimationState = delegate { };
        public UnityAction onHealthVisualClose = delegate { };
        public UnityAction onHealthVisualOpen = delegate { };
        public UnityAction onHealthUpgrade = delegate { };

        public UnityAction onResetPlayerStack = delegate { };
    }
}