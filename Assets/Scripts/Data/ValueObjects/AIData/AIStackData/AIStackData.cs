using UnityEngine;
using System;
using Enums;

namespace Data.ValueObject.AIDatas
{
    [Serializable]
    public class AIStackData
    {
        public AIStackType AIStackType;
        public Vector2 Capacity;
        public Vector3 Offset;
    } 
}
