using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Managers;
using StateMachines.AIBrain.Enemy;
using Interfaces;

namespace Controllers
{

    public class EnemyDetectionController : MonoBehaviour
    {
        [SerializeField]
        private GameObject collisionColliderObj;
        private Transform _detectedMine;

        private EnemyAIBrain _enemyAIBrain;
        public bool IsBombInRange() => _detectedMine != null;
        private void Awake()
        {
            _enemyAIBrain = this.gameObject.GetComponentInParent<EnemyAIBrain>();
        }
        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out PlayerPhysicsController controller))
            {
                PickOneTarget(other);
                _enemyAIBrain.CachePlayer(controller.transform.parent.gameObject);
                _enemyAIBrain.CacheSoldier(null);
            }
            if (other.TryGetComponent(out SoldierHealthController soldierHealthController))
            {
                _enemyAIBrain.CachePlayer(null);
                PickOneTarget(other);
                _enemyAIBrain.CacheSoldier(soldierHealthController);
            }
        }

        private void OnTriggerExit(Collider other)
        {

            if (other.TryGetComponent(out PlayerPhysicsController controller))
            {
                this.gameObject.GetComponentInParent<EnemyAIBrain>().CurrentTarget = null;
                _enemyAIBrain.SetTarget(null);
                _enemyAIBrain.CachePlayer(null);
            }
            if (other.TryGetComponent(out IDamageable Idamageable))
            {
                _enemyAIBrain.SetTarget(null);
                _enemyAIBrain.CacheSoldier(null);
            }

        }
        private void PickOneTarget(Collider other)
        {
            if (_enemyAIBrain.CurrentTarget != other.TryGetComponent(out PlayerPhysicsController physicsController) || !_enemyAIBrain.CurrentTarget)
            {
                _enemyAIBrain.SetTarget(other.transform.parent.gameObject.transform);
            }
        }
    } 
}
