using System.Collections;
using UnityEngine;
using Interfaces;

namespace StateMachines.AIBrain.Enemy
{
    public class EnemyPhsicsController : MonoBehaviour, IDamageable
    {
        [SerializeField]
        private EnemyAIBrain enemyAIBrain;

        public bool IsTaken { get ; set; }
        public bool IsDead { get; set ; }

        public Transform GetTransform()
        {
            return this.transform;
        }

        public int TakeDamage(int damage)
        {
            if (enemyAIBrain.Health > 0)
            {
                enemyAIBrain.Health -= damage;
                if (enemyAIBrain.Health <= 0)
                {
                    IsDead = true;
                    return enemyAIBrain.Health;
                }
                return enemyAIBrain.Health;
            }
            return 0;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out IDamager damager))
            {
                TakeDamage(damager.Damage());
            }
        }
    }
}