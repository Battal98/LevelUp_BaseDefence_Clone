using UnityEngine;

namespace Interfaces
{
    public interface IEnemy
    {
        public int Health();
        public int Damage();
        public float AttackRange();
        public float AttackSpeed();
        public float MoveSpeed();
        public float ChaseSpeed();
    } 
}
