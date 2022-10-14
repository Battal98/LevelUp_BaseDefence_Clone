using System;
using Enums;
using Interfaces;

namespace Data.ValueObject.LevelDatas
{
    [Serializable]
    public class StageData: ISavableEntity
    {
        public AvabilityType AvabilityState;
        public int StageCost;
        public int StagePayedAmount;

        public string Key = "StageData";

        public StageData()
        {

        }
        public string GetKey()
        {
            return Key;
        }
    }
}