using System;
using UnityEngine;

namespace Data.ValueObject.LevelDatas
{
    [Serializable]
    public class TurretData/*: SavableEntity*/
    {
        public bool IsActive;
        public bool HasTurretSoldier;
        public int SoldierCost;
        public int SoldierPayedAmount;
        public int TurretAmmoCapacity;
        public int TurretDamage;
        public ParticleSystem TurretParticle; 
    }
}
