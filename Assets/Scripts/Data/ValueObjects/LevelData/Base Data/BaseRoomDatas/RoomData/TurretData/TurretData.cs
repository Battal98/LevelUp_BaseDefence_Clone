using System;
using UnityEngine;
using Enums;

namespace Data.ValueObject.LevelDatas
{
    [Serializable]
    public class TurretData/*: SavableEntity*/
    {
        public AvabilityState AvabilityState;
        public bool HasTurretSoldier;
        public int SoldierCost;
        public int SoldierPayedAmount;
        public int TurretAmmoCapacity;
        public int TurretDamage;
        public ParticleSystem TurretParticle; 
    }
}
