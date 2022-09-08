using System;
using Enums;

namespace Data.ValueObject.AIDatas
{
    [Serializable]
    public class WorkerAITypeData
    {
        public WorkerType WorkerType;
        public int CapacityOrDamage;
        public float Speed;
    }

}