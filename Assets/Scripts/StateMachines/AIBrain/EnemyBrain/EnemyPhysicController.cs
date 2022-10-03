using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Managers;
using StateMachines.AIBrain.Enemy;
using Interfaces;

namespace Controllers
{

    public class EnemyPhysicController : MonoBehaviour, IDamagable
    {
        [SerializeField]
        private GameObject collisionColliderObj;
        private Transform _detectedPlayer;
        private Transform _detectedMine;

        private EnemyAIBrain _enemyAIBrain;

        public bool IsTaken { get; set; }
        public bool IsDead { get ; set; }

        public bool IsPlayerInRange() => _detectedPlayer != null;
        public bool IsBombInRange() => _detectedMine != null;
        private void Awake()
        {
            _enemyAIBrain = this.gameObject.GetComponentInParent<EnemyAIBrain>();
        }
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                _detectedPlayer = other.GetComponentInParent<PlayerManager>().transform;
                //sinyalle çakmayý dene
                _enemyAIBrain.PlayerTarget = other.transform.parent.transform;
            }

            if (other.CompareTag("Bullet"))
            {
                //_enemyAIBrain._health = 0;
                //collisionColliderObj.SetActive(false);
            }

            /*if (other.GetComponent<Mine>())
            {
                _detectedMine = other.GetComponent<Mine>();
            }*/
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                _detectedPlayer = null;
                this.gameObject.GetComponentInParent<EnemyAIBrain>().PlayerTarget = null;
            }

            /*if (other.GetComponent<Mine>())
            {

            }*/
        }

        public int TakeDamage(int damage)
        {
            throw new System.NotImplementedException();
        }

        public Transform GetTransform()
        {
            return this.transform;
        }
    } 
}
