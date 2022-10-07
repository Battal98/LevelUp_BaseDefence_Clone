using System;
using System.Collections.Generic;
using UnityEngine;

namespace Data.ValueObject.LevelDatas
{
    [Serializable]
    public class MilitaryBaseData/*: SavableEntity*/
    {
        public Vector2Int SlotsGrid;
        public Vector2 SlotOffSet;
        public int BaseCapacity;
        public int TotalAmount;
        public int TentCapacity;
        public int CurrentSoldierAmount;
        public int SoldierUpgradeTime;
        public int SoldierSlotCost;
        public int SlotAmount;
        public Transform SlotTransform;
        public int AttackTime;
        public GameObject SlotPrefab;
        public Transform frontYardSoldierPosition;

    }
}
