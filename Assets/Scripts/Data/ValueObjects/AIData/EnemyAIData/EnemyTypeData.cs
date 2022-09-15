using System;
using UnityEngine;
using Enums;
using Interfaces;

namespace Data.ValueObject.AIDatas
{
    [Serializable]
    public class EnemyTypeData : AEnemy
    {
        public float ChaseSpeed;
        public float MoveSpeed;
        public float NavMeshRadius;
        public float NavMeshHeight;
        public EnemyTypeData(EnemyType enemyType, int health, int damage, float attackRange, float attackSpeed, Vector3 scaleSize, Color bodyColor) : base(enemyType, health, damage, attackRange, attackSpeed, scaleSize, bodyColor)
        {
        }
    }
}
