using System.Collections.Generic;
using UnityEngine;
using Data.ValueObject.WeaponData;

namespace Data.UnityObject
{
    [CreateAssetMenu(fileName = "CD_Weapons", menuName = "BaseDefence/CD_Weapons", order = 0)]
    public class CD_Weapons : ScriptableObject
    {
        public List<WeaponData> WeaponDatas;
    } 
}
