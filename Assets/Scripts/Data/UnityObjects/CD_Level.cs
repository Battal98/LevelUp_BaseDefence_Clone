using System.Collections.Generic;
using UnityEngine;
using Data.ValueObject.LevelDatas;
using Interfaces;

namespace Data.UnityObject
{
    [CreateAssetMenu(fileName = "CD_Level", menuName = "BaseDefence/CD_Level", order = 0)]
    public class CD_Level : ScriptableObject, ISavableEntity
    {
        public List<LevelData> LevelDatas = new List<LevelData>();
        public int LevelID;
        private string Key = "LevelData";

        public CD_Level()
        {

        }
        public CD_Level(int levelindex, List<LevelData> levelDatas)
        {
            LevelID = levelindex;
            LevelDatas = levelDatas;

        }
        public string GetKey()
        {
            return Key;
        }
    }
}