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
        private Transform _detectedPlayer;
        private Transform _detectedMine;

        private EnemyAIBrain _enemyAIBrain;

        public bool IsPlayerInRange() => _detectedPlayer != null;
        public bool IsBombInRange() => _detectedMine != null;
        private void Awake()
        {
            _enemyAIBrain = this.gameObject.GetComponentInParent<EnemyAIBrain>();
        }
        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out PlayerPhysicsController controller))
            {
                _detectedPlayer = other.GetComponentInParent<PlayerManager>().transform;
                //sinyalle çakmayý dene
                _enemyAIBrain.PlayerTarget = _detectedPlayer;
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                _detectedPlayer = null;
                this.gameObject.GetComponentInParent<EnemyAIBrain>().PlayerTarget = null;
            }

        }
    } 
}
