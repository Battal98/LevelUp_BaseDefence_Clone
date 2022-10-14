using System;
using Enums;

namespace Data.ValueObject.LevelDatas
{
    [Serializable]
    public class BuyablesData
    {
        public int MoneyWorkerCost;
        public int MoneyWorkerPayedAmount;
        public int AmmoWorkerCost;
        public int AmmoWorkerPayedAmount;

        public int MoneyWorkerLevel;
        public int AmmoWorkerLevel;
        public AvabilityType AvabilityState;
    }
}
