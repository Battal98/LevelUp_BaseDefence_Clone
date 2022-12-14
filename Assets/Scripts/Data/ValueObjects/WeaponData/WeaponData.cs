using System;
using Enums;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Data.ValueObject.WeaponData
{
    [Serializable] 
    public class WeaponData
    {
        public WeaponType WeaponType;
        public Mesh WeaponMesh;
        public bool HasSideMesh;
        [ShowIf("HasSideMesh")]
        public Mesh SideMesh;
        public int Damage;
        public float AttackRate;
        public int WeaponLevel=1;
        public GameObject Bullet;
    }
}