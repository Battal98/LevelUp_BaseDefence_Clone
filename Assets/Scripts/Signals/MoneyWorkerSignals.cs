using Extentions;
using UnityEngine.Events;
using UnityEngine;
using Data.ValueObject.AIDatas;
using System;
using Enums;
using System.Collections.Generic;

namespace Signals
{
    public class MoneyWorkerSignals : MonoSingleton<MoneyWorkerSignals>
    {
        public Func<WorkerType, WorkerAITypeData> onGetMoneyAIData = delegate { return null; };
        public UnityAction onSendMoneyPositionToWorkers = delegate { };
        public UnityAction<Stackable> onSetStackable = delegate { };
        public UnityAction onThisMoneyTaken = delegate { };

        public Func<Transform, Transform> onGetTransformMoney = delegate { return null; };
        public Func<Vector3> onSendWaitPosition = delegate { return Vector3.zero; };

    } 
}
