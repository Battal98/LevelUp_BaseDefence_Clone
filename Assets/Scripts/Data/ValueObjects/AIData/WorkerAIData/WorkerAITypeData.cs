using System;
using Enums;
using UnityEngine;
using Sirenix.OdinInspector;

namespace Data.ValueObject.AIDatas
{
    [Serializable]
    public class WorkerAITypeData
    {
        public WorkerType WorkerType;
        [HideIf("WorkerType", WorkerType.SoldierAI)]
        public int CapacityOrDamage;
        [HideIf("WorkerType", WorkerType.SoldierAI)]
        public float MinSpeed;
        [HideIf("WorkerType", WorkerType.SoldierAI)]
        public float MaxSpeed;
        [HideIf("WorkerType", WorkerType.SoldierAI)]
        public int MaxWorkerValue;

        [ShowIf("WorkerType", WorkerType.SoldierAI)]
        public SoldierAIData SoldierAIData;
    }

}