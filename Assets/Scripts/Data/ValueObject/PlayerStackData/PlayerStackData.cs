using System;
using Enums;
using UnityEngine;

namespace Data.ValueObject.PlayerStackData
{
    [Serializable]
    public class PlayerStackData
    {
        public PlayerStackType PlayerStackType;
        public Vector2 Capacity;
        public Vector3 Offset;
    }
}