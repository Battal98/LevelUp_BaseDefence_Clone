using System;
using UnityEngine;
using Enums;

namespace Data.ValueObject.AIDatas
{
    [Serializable]
    public class EnemyTypeData
    {
        public EnemyType EnemyType;
        public Vector3 ScaleSize;
        public Color BodyColor;
        public int Health;
        public int Damage;
        public float AttackRange;
        public float AttackSpeed;
        public float MoveSpeed;
        public float ChaseSpeed;
    } 
}
