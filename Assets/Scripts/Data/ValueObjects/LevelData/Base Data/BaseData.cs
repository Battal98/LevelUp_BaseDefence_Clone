using System;

namespace Data.ValueObject.LevelDatas
{
    [Serializable]
    public class BaseData
    {
        public BaseRoomDatas BaseRoomDatas;
        public MineBaseData MineBaseData;
        public MilitaryBaseData MilitaryBaseData;
        public BuyablesData BuyablesData;
    }
}
