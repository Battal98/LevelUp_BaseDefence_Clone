using System;
using Interfaces;
using Enums;

namespace Data.ValueObject.LevelDatas
{
    [Serializable]
    public class RoomData: ISavableEntity // Roomlar üzerindeki payamount ve // turretler üzerinde ki soldier payedamaount savelenecek
    {
        public int RoomCost;
        public int RoomPayedAmount;
        public TurretLocationType TurretLocationType;
        public TurretData TurretData;

        public string Key = "RoomData";

        public RoomData()
        {

        }
        public string GetKey()
        {
            return Key;
        }
    }
}
