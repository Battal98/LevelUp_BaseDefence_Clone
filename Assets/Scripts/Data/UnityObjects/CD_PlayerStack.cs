using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Data.ValueObject.Player;

namespace Data.UnityObject
{
    [CreateAssetMenu(fileName = "CD_PlayerStack", menuName = "BaseDefence/CD_PlayerStack", order = 0)]
    public class CD_PlayerStack : ScriptableObject
    {
        public List<PlayerStackData> PlayerStackDatas;
    }
}
