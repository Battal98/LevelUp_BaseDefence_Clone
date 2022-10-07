using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Data.ValueObject.LevelDatas
{
    [Serializable]
    public class MineBaseData
    {
        public int MaxWorkerAmount;
        public int CurrentWorkerAmount;
        public float GemCollectionOffset=5f;
    }
}