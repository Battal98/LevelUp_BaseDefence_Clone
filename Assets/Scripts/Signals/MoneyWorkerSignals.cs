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
        public UnityAction<Transform> onSetMoneyPosition = delegate { };
        public UnityAction <Transform> onThisMoneyTaken = delegate { };

        public Func<Transform, Transform> onGetTransformMoney = delegate { return null; };
        public Func<Transform, Transform, Transform> OnMyMoneyTaken = delegate { return null; };
    } 
}
