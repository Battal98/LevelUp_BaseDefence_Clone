using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Managers;
using StateMachines.AIBrain.Enemy;
namespace Controllers
{
    public class BossEnemyDetector : MonoBehaviour
    {
        [SerializeField]
        private BossEnemyBrain bossBrain;
        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent<PlayerPhysicsController>(out PlayerPhysicsController playerPhysicsController))
            {
                bossBrain.PlayerTarget = other.transform.parent.transform;
                //in AttackState 
            }
        }

        private void OnTriggerStay(Collider other)
        {
            
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent<PlayerPhysicsController>(out PlayerPhysicsController playerManager))
            {
                bossBrain.PlayerTarget = null;
                //out AttackState 
            }
        }
    } 
}
