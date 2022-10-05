using Enums;
using Extentions;
using UnityEngine;
using UnityEngine.Events;

namespace Signals
{
    public class CoreGameSignals : MonoSingleton<CoreGameSignals>
    {
        public UnityAction onPlay = delegate { };
        public UnityAction onReset = delegate { };
        public UnityAction<Transform> onSetCameraTarget = delegate { };
        public UnityAction<GameStates> onGetGameState = delegate { };
        public UnityAction<GameStates> onSetGameState = delegate { };

        public UnityAction<int> onUpdateGemScore = delegate { };
        public UnityAction<int> onUpdateMoneyScore = delegate { };
    }
}