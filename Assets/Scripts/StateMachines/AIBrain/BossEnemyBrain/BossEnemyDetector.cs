using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Managers;

namespace Controllers
{
    public class BossEnemyDetector : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent<PlayerPhysicsController>(out PlayerPhysicsController playerManager))
            {
                Debug.Log("Zort Player");
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
                Debug.Log("Zort Player");
                //out AttackState 
            }
        }
    } 
}
