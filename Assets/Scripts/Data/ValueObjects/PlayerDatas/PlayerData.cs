using Interfaces;
using System;

namespace Data.ValueObject
{
    [Serializable]
    public class PlayerData : ISavableEntity
    {
        public PlayerDatas PlayerDatas;
        public string Key = "PlayerData";

        public string GetKey()
        {
            return Key;
        }
    }

    [Serializable]
    public class PlayerDatas
    {
        public float PlayerJoystickSpeed = 3;
        public int PlayerHealth = 100;
        public float AttackRange = 10;
    }
}