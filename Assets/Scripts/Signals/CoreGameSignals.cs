using Enums;
using Extentions;
using UnityEngine;
using UnityEngine.Events;
using System;

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
        public Func<bool> onHasEnoughMoney = delegate { return default; };
        public Func<bool> onHasEnoughGem;

        public UnityAction onEnterTurret = delegate { };
        public UnityAction onLevel = delegate { };
        public UnityAction onFinish = delegate { };
        public UnityAction onPreNextLevel = delegate { };

        public UnityAction<TurretLocationType, GameObject> onSetCurrentTurret = delegate (TurretLocationType arg0, GameObject o) { };

        public UnityAction onStartMoneyPayment = delegate { };
        public UnityAction onStopMoneyPayment = delegate { };

        public UnityAction<int> onTakePlayerDamage = delegate { };

    }
}