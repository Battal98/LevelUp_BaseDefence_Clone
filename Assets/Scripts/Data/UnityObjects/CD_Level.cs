using System.Collections.Generic;
using UnityEngine;
using Data.ValueObject.LevelDatas;
using Data.ValueObjects;
using Interfaces;

namespace Data.UnityObject
{
    [CreateAssetMenu(fileName = "CD_Level", menuName = "BaseDefence/CD_Level", order = 0)]
    public class CD_Level : ScriptableObject, ISavableEntity
    {
        public List<LevelData> LevelDatas = new List<LevelData>();

        public ScoreData ScoreData;

        public int LevelID;

        public CD_Level()
        {

        }
        public CD_Level(int levelId, List<LevelData> levelDatas, ScoreData scoreData)
        {
            LevelID = levelId;
            LevelDatas = levelDatas;
            ScoreData = scoreData;
        }
        public string Key = "LevelData";
        public string GetKey()
        {
            return Key;
        }
    }
}