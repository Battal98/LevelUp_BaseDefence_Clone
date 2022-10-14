using System;
using Interfaces;
using Enums;
using Sirenix.OdinInspector;

namespace Data.ValueObject.LevelDatas
{
    [Serializable]
    public class RoomData // Roomlar üzerindeki payamount ve // turretler üzerinde ki soldier payedamaount savelenecek
    {
        [BoxGroup("Types")]
        public BaseRoomTypes BaseRoomTypes;
        [BoxGroup("Types")]
        public TurretLocationType TurretLocationType;
        [BoxGroup("Types")]
        public AvabilityType AvailabilityType;
        [BoxGroup("Variables")]
        public int RoomCost;
        [BoxGroup("Variables")]
        public int RoomPayedAmount;
        [BoxGroup("Variables")]
        public TurretData TurretData;
    }
}
