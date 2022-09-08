using System;
using Enums;

namespace Data.ValueObject.LevelDatas
{
    [Serializable]
    public class StageData
    {
        public AvabilityState AvabilityState;
        public int StageCost;
        public int StagePayedAmount;
    }
}