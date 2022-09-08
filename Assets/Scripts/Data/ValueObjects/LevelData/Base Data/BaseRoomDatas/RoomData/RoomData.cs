using System;

namespace Data.ValueObject.LevelDatas
{
    [Serializable]
    public class RoomData/*: SavableEntity*/
    {
        public int RoomCost;
        public int RoomPayedAmount;
        public TurretData TurretData;
    }
}
