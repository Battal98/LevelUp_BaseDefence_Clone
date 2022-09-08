using UnityEngine;
using System;
using Enums;
using Interfaces;

namespace Data.ValueObject.Player
{
    [Serializable]
    public class PlayerStackData: ISavableEntity
    {
        public PlayerStackType PlayerStackType;
        public Vector2 Capacity;
        public Vector3 Offset;

        public string Key = "PlayerStackData";

        public string GetKey()
        {
            return Key;
        }
    } 
}
