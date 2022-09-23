using Extentions;
using UnityEngine.Events;
using Data.ValueObject.AIDatas;
using System;
using Enums;

namespace Signals
{
    public class WorkerSignals : MonoSingleton<WorkerSignals>
    {
        public Func<WorkerType, WorkerAITypeData> onGetMoneyAIData = delegate { return null; };
    } 
}
