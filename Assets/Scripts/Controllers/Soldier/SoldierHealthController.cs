using System.Collections;
using UnityEngine;
using StateMachines.AIBrain.Soldier;
using Interfaces;

namespace Controllers
{
    public class SoldierHealthController : Interactable, IDamageable
    {
        [SerializeField]
        private SoldierAIBrain soldierAIBrain;
        public bool IsTaken { get; set; }
        public bool IsDead { get; set; }

        private void OnEnable()
        {
            IsDead = false;
        }
        public int TakeDamage(int damage)
        {
            soldierAIBrain.Health -= damage;
            if (soldierAIBrain.Health <= 0)
            {
                IsDead = true;
            }
            return soldierAIBrain.Health;
        }
        public Transform GetTransform()
        {
            return transform;
        }
    }
}