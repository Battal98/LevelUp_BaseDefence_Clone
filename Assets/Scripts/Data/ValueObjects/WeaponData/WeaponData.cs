using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Enums;

namespace Data.ValueObject.WeaponData
{
    [Serializable]
    public class WeaponData
    {
        public WeaponType WeaponType;
        public int Damage;
        public float AttackRate;
        public ParticleSystem WeaponParticle;
    } 
}
