using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Data.ValueObject.ScoreData;

namespace Data.UnityObject
{

    [CreateAssetMenu(fileName = "CD_Score", menuName = "BaseDefence/CD_Score", order = 0)]
    public class CD_Score : ScriptableObject
    {
        public ScoreData ScoreData;
    } 
}
