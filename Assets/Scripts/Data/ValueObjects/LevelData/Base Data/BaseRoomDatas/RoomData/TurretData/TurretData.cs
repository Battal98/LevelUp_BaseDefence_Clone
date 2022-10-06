using System;
using UnityEngine;
using Enums;
using Interfaces;

namespace Data.ValueObject.LevelDatas
{
    [Serializable]
    public class TurretData: ISavableEntity
    {
        public AvabilityState AvabilityState; // player etkilesime gecebilir mi?
        public bool HasTurretSoldier; // Has turret soldier savelenecek data
        public int TurretAmmoCapacity;
        public int TurretDamage;
        public ParticleSystem TurretParticle;

        public string Key = "TurretData";
        public TurretData()
        {

        }
        public string GetKey()
        {
            return Key; 
        }
    }
}
