using System;
using System.Collections.Generic;

namespace Data.ValueObject.LevelDatas
{
    [Serializable]
    public class FrontYardData
    {
        public List<StageData> Stages;
        public List<FrontYardItemsData> FrontYardItems;
        public HostageData HostageData;
    }
}