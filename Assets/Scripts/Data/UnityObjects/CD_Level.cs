using System.Collections.Generic;
using UnityEngine;
using Data.ValueObject.LevelDatas;

namespace Data.UnityObject
{
    [CreateAssetMenu(fileName = "CD_Level", menuName = "BaseDefence/CD_Level", order = 0)]
    public class CD_Level : ScriptableObject
    {
        public List<LevelData> LevelDatas;
    }
}