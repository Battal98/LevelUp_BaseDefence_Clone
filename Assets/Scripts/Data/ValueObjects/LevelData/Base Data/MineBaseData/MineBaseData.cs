using System;

namespace Data.ValueObject.LevelDatas
{
    [Serializable]
    public class MineBaseData/*: SavableEntity*/
    {
        public int MaxWorkerAmount;
        public int CurrentWorkerAmount;
        public int GemCapacity;
        public int CurrentGemAmount;
        public int MineCartCapacity;
    }
}
