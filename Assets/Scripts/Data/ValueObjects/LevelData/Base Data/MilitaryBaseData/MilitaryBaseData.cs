using System;
using UnityEngine;

namespace Data.ValueObject.LevelDatas
{
    [Serializable]
    public class MilitaryBaseData/*: SavableEntity*/
    {
        public int MaxSoldierAmount;
        public int CandidateAmount;
        public int CurrentSoldierAmount;
        public int SoldierUpgradeTime;
        public int SoldierSlotCost;
        public int SoldierSlotAmount;
        public int AttackTime;
        public Transform SlotTransform;

    }
}
