using System;
using UnityEngine;
using Enums;
using Interfaces;
using Sirenix.OdinInspector;

namespace Data.ValueObject.AIDatas
{
    [Serializable]
    public class EnemyTypeData : AEnemy
    {
        [HideIf("EnemyType", EnemyType.Boss)]
        public float ChaseSpeed;
        [HideIf("EnemyType", EnemyType.Boss)]
        public float MoveSpeed;
        [HideIf("EnemyType", EnemyType.Boss)]
        public float NavMeshRadius;
        [HideIf("EnemyType", EnemyType.Boss)]
        public float NavMeshHeight;
        public EnemyTypeData(EnemyType enemyType, int health, int damage, float attackRange, float attackSpeed, Vector3 scaleSize, Color bodyColor) : base(enemyType, health, damage, attackRange, attackSpeed, scaleSize, bodyColor)
        {
        }
    }
}
