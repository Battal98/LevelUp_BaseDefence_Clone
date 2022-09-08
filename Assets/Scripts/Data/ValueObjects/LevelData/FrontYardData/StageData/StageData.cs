using System;

namespace Data.ValueObject.LevelDatas
{
    [Serializable]
    public class StageData
    {
        public bool IsUnlocked;
        public int StageCost;
        public int StagePayedAmount;
    }
}