using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Data.ValueObject.AIDatas;

namespace Data.UnityObject
{
    [CreateAssetMenu(fileName = "CD_AIStack", menuName = "BaseDefence/CD_AIStack", order = 0)]
    public class CD_AIStack: ScriptableObject
    {
        public List<AIStackData> AIStackDatas;
    } 
}
