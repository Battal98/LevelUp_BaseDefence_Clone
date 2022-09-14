using System;
using UnityEngine;
using Enums;
using System.Collections.Generic;

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
        public float NavMeshRadius;
        public float NavMeshHeight;
        public List<Transform> SpawnPoint;
        //public List<List<Transform>> TurretTargetList;
    } 
}
