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
        public float Speed;
        [HideIf("WorkerType", WorkerType.SoldierAI)]
        public Transform StartTarget;

        [ShowIf("WorkerType", WorkerType.SoldierAI)]
        public SoldierAIData SoldierAIData;
    }

}