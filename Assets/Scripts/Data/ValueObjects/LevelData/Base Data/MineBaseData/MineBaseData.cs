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
        public int DiamondCapacity;
        public int CurrentDiamondAmount;
        public int MineCartCapacity;
        public float GemCollectionOffset=5f;
        public Transform InstantiationPosition;
        public Transform GemHolderPosition;
        public List<Transform> MinePlaces;
        public List<Transform> CartPlaces;
    }
}