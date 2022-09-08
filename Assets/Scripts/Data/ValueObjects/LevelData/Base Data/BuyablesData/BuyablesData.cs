using System;

namespace Data.ValueObject.LevelDatas
{
    [Serializable]
    public class BuyablesData
    {
        public int MoneyWorkerCost;
        public int MoneyWorkerPayedAmount;
        public int AmmoWorkerCost;
        public int AmmoWorkerPayedAmount;
        public int BoughtMoneyWorkerAmount;
        public int BoughtAmmoWorkerAmount;
        public int MoneyWorkerLevel;
        public int AmmoWorkerLevel;
        public bool IsUpgradeButtonLocked;
    }
}
