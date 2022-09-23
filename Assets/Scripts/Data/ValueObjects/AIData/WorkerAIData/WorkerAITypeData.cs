using System;
using Enums;
using UnityEngine;

namespace Data.ValueObject.AIDatas
{
    [Serializable]
    public class WorkerAITypeData
    {
        public WorkerType WorkerType;
        public int CapacityOrDamage;
        public float Speed;
        public Transform StartTarget;
    }

}