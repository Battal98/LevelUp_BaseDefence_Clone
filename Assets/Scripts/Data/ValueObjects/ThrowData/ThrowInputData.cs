using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Data.ValueObjects
{
    public struct ThrowInputData
    {
        public readonly Vector3 initialVelocity;
        public readonly float timeToTarget;

        public ThrowInputData(Vector3 initialVelocity, float timeToTarget)
        {
            this.initialVelocity = initialVelocity;
            this.timeToTarget = timeToTarget;
        }
    } 
}
