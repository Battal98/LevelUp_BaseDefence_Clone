using System.Collections.Generic;
using UnityEngine;
using Data.ValueObject;

namespace Data.UnityObject
{
    [CreateAssetMenu(fileName = "CD_Level", menuName = "BaseDefence/CD_Level", order = 0)]
    public class CD_Level : ScriptableObject
    {
        public List<int> Levels = new List<int>();
        public LevelStageData LevelStageData;
    }
}